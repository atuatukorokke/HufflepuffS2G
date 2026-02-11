// ========================================
//
// Boss1Bullet.cs
//
// ========================================
//
// ボスの弾幕・HP管理・状態遷移を制御するクラス。
// ・通常弾幕 / 必殺技 / セミファイナルの切り替え
// ・HP管理と段階遷移
// ・弾幕パターンの開始 / 停止
// ・無敵状態の制御
// ・死亡処理
//
// ========================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BulletState
{
    normal,   // 通常弾幕
    spell,    // 必殺技
    special   // セミファイナル（ラストワード）
}

public enum State
{
    fast = 0,
    second,
    third,
    four,
    final
}

public class Boss1Bullet : MonoBehaviour
{
    [Header("ボスの全体を管理する変数")]
    [SerializeField] private State state = State.fast;                      // 今の攻撃が何段階目かの判別
    [SerializeField] private BulletState bulletState = BulletState.normal;  // HP判別でどの弾幕を撃つか判別する
    [SerializeField] private float maxHP = 100f;                            // 敵の最大HP
    [Range(0, 100)]
    [SerializeField] private float currentHP;                               // 今のHP
    private bool isSpecialBulletActive = false;                             // HPが0になったらtrueになる→ラストワード発生
    private float specialBulletDuration = 15f;                              // 何秒ラストワードを撃つか
    private float timer = 0f;                                               // 今何秒たったか
    private float damageLate = 0f;                                          // ダメージを与える割合
    [SerializeField] private float attak = 1f;                              // 攻撃力
    [SerializeField] private Vector2 spellPos;                              // 必殺技・セミファイナルを打つときにこの座標に一旦戻る
    [SerializeField] private SpecialMove_Gomi GomiSpecialMove;              // 必殺技のクラス
    [SerializeField] private bool isInvivle = false;                        // 必殺技中の一時無敵判定
    [SerializeField] private bool isDead = false;                           // ボスの死亡判定
    [SerializeField] private GameObject EexplosionEffect;                   // 爆発のエフェクト
    [SerializeField] private Image HealthBar;                               // ボスの体力表示用画像
    [SerializeField] private GameObject CutInnCanvas;                       // カットイン用のキャンバス
    GameObject canvas;                                                      // ＨＰバーのキャンバス
    public event System.Action Ondeath;                                     // 死亡時の処理
    private AudioSource audio;                                              // オーディオソース

    [Header("通常弾幕のパターン")]
    [SerializeField] private FastBullet fastBulletValue;
    [SerializeField] private SecondBullet secondBulletValue;
    [SerializeField] private ThirdBullet thirdBulletValue;
    [SerializeField] private FourBullet FourBulletValue;
    [SerializeField] private FinalBulletValue finalBulletValue;

    private INormalBulletPattern currentPattern;
    private Coroutine fireRoutine;

    public State State { get => state; set => state = value; }
    public BulletState BulletState { get => bulletState; set => bulletState = value; }
    public float DamageLate { get => damageLate; set => damageLate = value; }
    public AudioSource Audio { get => audio; set => audio = value; }

    /// <summary>
    /// コンポーネント生成直後の初期化処理。
    /// オーディオソースを取得する。
    /// </summary>
    private void Awake()
    {
        Audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Awake の後に呼ばれる初期化処理。
    /// HP 初期化と初期位置への移動を開始する。
    /// </summary>
    void Start()
    {
        currentHP = maxHP;
        isDead = false;
        StartCoroutine(StartPositionMove());
    }

    /// <summary>
    /// 毎フレーム呼ばれる更新処理。
    /// セミファイナルの時間経過を監視し、終了時に死亡処理を行う。
    /// </summary>
    private void Update()
    {
        // セミファイナル発動中の時間計測
        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;

            // セミファイナルの持続時間を超えたら死亡処理
            if (timer >= specialBulletDuration && !isDead)
            {
                isDead = true;
                Destroy(canvas);
                Instantiate(EexplosionEffect, transform.position, Quaternion.identity);
                Ondeath?.Invoke();
                BulletDelete();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 初期位置から spellPos へ移動し、弾幕開始準備を行う。
    /// </summary>
    private IEnumerator StartPositionMove()
    {
        float limitTime = 0.5f;
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;

        // 指定位置へ向けて移動する処理
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, spellPos.x, elapsedTime / limitTime),
                Mathf.Lerp(startPosition.y, spellPos.y, elapsedTime / limitTime)
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);

        damageLate = 1.0f;

        yield return StartCoroutine(BulletUpdate());
    }

    /// <summary>
    /// 現在の状態に応じて弾幕を開始する。
    /// </summary>
    IEnumerator BulletUpdate()
    {
        // HP が残っていて、通常弾幕状態なら弾幕開始
        if (currentHP > 0 && BulletState == BulletState.normal)
        {
            StartCoroutine(HandleBulletPattern());
        }

        yield return null;
    }

    /// <summary>
    /// 現在の段階（State）に応じて通常弾幕パターンを切り替える。
    /// </summary>
    private IEnumerator HandleBulletPattern()
    {
        // 無敵中は弾幕を開始しない
        if (isInvivle) yield break;

        // 現在の段階に応じてパターンを切り替える
        switch (state)
        {
            case State.fast:
                SetPattern(new FastBulletPattern(fastBulletValue, transform, this));
                break;

            case State.second:
                SetPattern(new SecondBulletPattern(secondBulletValue, transform, this));
                break;

            case State.third:
                SetPattern(new ThirdBulletPattern(thirdBulletValue, transform, this));
                break;

            case State.four:
                SetPattern(new FourBulletPattern(FourBulletValue, transform, this));
                break;

            case State.final:
                SetPattern(new FinalBulletPattern(finalBulletValue, transform, this));
                break;
        }

        yield return null;
    }

    /// <summary>
    /// 弾幕パターンをセットし、Fire コルーチンを開始する。
    /// </summary>
    private void SetPattern(INormalBulletPattern pattern)
    {
        // 通常弾幕以外の状態ではパターンを開始しない
        if (bulletState != BulletState.normal)
            return;

        // 既存の弾幕パターンを停止
        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            currentPattern?.Clear();
        }

        currentPattern = pattern;
        currentPattern.Initialize();
        fireRoutine = StartCoroutine(currentPattern.Fire());
    }

    /// <summary>
    /// 必殺技・セミファイナル用の移動処理。
    /// 移動中は無敵になる。
    /// </summary>
    public IEnumerator MoveToSpellPosWithInvincible(Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        owner.SetInvincible(true);
        damageLate = 0f;

        float t = 0f;
        float duration = 1.5f;
        Vector2 start = boss.position;

        // spellPos へ向けて移動
        while (t < duration)
        {
            boss.position = Vector2.Lerp(start, spellPos, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        damageLate = 0.2f;
        owner.SetInvincible(false);
    }

    /// <summary>
    /// 次の段階へ移行し、HP 回復と弾幕再開を行う。
    /// </summary>
    private IEnumerator TransitionToNextState()
    {
        // final 以外なら段階を進める
        if (state < State.final)
        {
            state++;
            BulletState = BulletState.normal;
            DamageLate = 1f;
            currentHP = maxHP;

            // 無敵でない場合のみ HP バー更新
            if (!isInvivle)
            {
                HealthBar.fillAmount = currentHP / maxHP;
            }

            yield return StartCoroutine(BulletUpdate());
        }
    }

    /// <summary>
    /// プレイヤーの攻撃が当たった際のダメージ処理。
    /// HP に応じて必殺技・セミファイナルへ移行する。
    /// </summary>
    private IEnumerator TakeDamage(float damage)
    {
        currentHP -= damage;

        // 無敵でない場合のみ HP バー更新
        if (!isInvivle)
        {
            HealthBar.fillAmount = currentHP / maxHP;
        }

        // HP が 20% 以下 → 必殺技へ移行
        if (currentHP <= maxHP * 0.2f && BulletState == BulletState.normal)
        {
            StopNormalPattern();
            BulletState = BulletState.spell;

            var spellPattern = GomiSpecialMove.GetPattern(state, transform, spellPos);
            spellPattern.Initialize();
            StartCoroutine(spellPattern.Execute());

            Instantiate(CutInnCanvas, Vector3.zero, Quaternion.identity);
        }
        // HP が 0 → 次の段階 or セミファイナル
        else if (currentHP <= 0 && !isInvivle)
        {
            BulletDelete();

            // final 段階 → セミファイナルへ
            if (state == State.final && BulletState == BulletState.spell)
            {
                isInvivle = true;
                isSpecialBulletActive = true;
                BulletState = BulletState.special;

                var semiFinal = GomiSpecialMove.GetSemiFinalPattern(spellPos);
                semiFinal.Initialize();
                yield return StartCoroutine(semiFinal.Execute());
            }
            // それ以外 → 次の段階へ
            else
            {
                yield return StartCoroutine(TransitionToNextState());
            }
        }
    }

    /// <summary>
    /// 通常弾幕を停止し、パターンをクリアする。
    /// </summary>
    private void StopNormalPattern()
    {
        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
        }

        currentPattern?.Clear();
        currentPattern = null;
    }

    /// <summary>
    /// 画面上の敵弾をすべて削除する。
    /// </summary>
    private void BulletDelete()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("E_Bullet");

        // final の場合は専用リストもクリア
        if (state == State.final)
        {
            finalBulletValue.bullets.Clear();
        }

        // 全弾削除
        foreach (GameObject bullet in objects)
        {
            Destroy(bullet);
        }
    }

    public void SetInvincible(bool value)
    {
        isInvivle = value;
    }

    /// <summary>
    /// プレイヤー弾に当たった際の処理。
    /// </summary>
    void OnTriggerEnter2D(Collider2D collision)
    {
        // プレイヤー弾に当たったらダメージ
        if (collision.CompareTag("P_Bullet"))
        {
            StartCoroutine(TakeDamage(attak * DamageLate));
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// プレイヤーのボムに触れている間、継続ダメージを与える。
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // ボムに触れている間は継続ダメージ
        if (collision.CompareTag("P_Bom"))
        {
            StartCoroutine(TakeDamage(damageLate));
        }
    }
}
