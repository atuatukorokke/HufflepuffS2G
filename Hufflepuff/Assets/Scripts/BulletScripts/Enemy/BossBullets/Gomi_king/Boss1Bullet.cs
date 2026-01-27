// Boss1Bulletrs.cs
//
// ボスの弾幕を生成する
// 移動の際は画面の左半分は入らない
//

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//敵の弾幕の状態
public enum BulletState
{
    normal, // 通常弾幕
    spell, //必殺技
    special // セミファイナル
}

// 敵が今何段階目か
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
    [SerializeField] private AudioClip deadSE;                              // 死亡時のSE
    [SerializeField] private Image HealthBar;                               // ボスの体力表示用画像
    [SerializeField] private GameObject CutInnCanvas;                       // カットイン用のキャンバス
    GameObject canvas;                                                      // ＨＰバーのキャンバス
    public event  System.Action Ondeath;                                    // 死亡時の処理
    private AudioSource audio;                                              // オーディオソース

    [Header("一段階目の通常弾幕の変数")]
    [SerializeField] private FastBullet fastBulletValue;

    [Header("二段階目の通常弾幕の変数")]
    [SerializeField] private SecondBullet secondBulletValue;

    [Header("三段階目の通常弾幕の変数")]
    [SerializeField] private ThirdBullet thirdBulletValue;

    [Header("四段階目の通常弾幕の変数")]
    [SerializeField] private FourBullet FourBulletValue;

    [Header("最終段階目の通常弾幕の変数")]
    [SerializeField] private FinalBulletValue finalBulletValue;

    // 現在の弾幕パターン
    private INormalBulletPattern currentPattern;
    private Coroutine fireRoutine;

    public State State { get => state; set => state = value; }
    public BulletState BulletState { get => bulletState; set => bulletState = value; }
    public float DamageLate { get => damageLate; set => damageLate = value; }

    void Start()
    {
        // 初期位置から指定場所へ移動する
        audio = GetComponent<AudioSource>();
        currentHP = maxHP;
        isDead = false;
        StartCoroutine(StartPositionMove()); // 初期位置から指定位置へ移動
    }

    private IEnumerator StartPositionMove()
    {
        float limitTime = 0.5f; // 移動にかける時間
        float elapsedTime = 0f; // 移動にかかった時間
        Vector2 startPosition = transform.position;

        // randomPosにlimitTimeかけて移動する
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

        // ダメージを与える割合を初期化して、次の弾幕を開始
        damageLate = 1.0f;
        yield return StartCoroutine(BulletUpdate());
    }

    private void Update()
    {
        // 数秒間セミファイナルを打ち続けると消える
        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;
            if (timer >= specialBulletDuration && !isDead)
            {
                isDead = true;
                Destroy(canvas); // ＨＰバーのキャンバスを消す
                audio.PlayOneShot(deadSE);
                Ondeath?.Invoke();
                BulletDelete(); // 弾幕を消す
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// ボスがどの弾幕を打つかの判別を行います
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletUpdate()
    {
        if (currentHP > 0 && BulletState == BulletState.normal)
        {
            StartCoroutine(HandleBulletPattern());
        }

        
        yield return null;
    }

    /// <summary>
    /// 通常弾幕の判別を行います
    /// </summary>
    private IEnumerator HandleBulletPattern()
    {
        if (isInvivle) yield break;

        switch (state)
        {
            case State.fast:
                SetPattern(new FastBulletPattern(fastBulletValue, transform));
                break;

            case State.second:
                SetPattern(new SecondBulletPattern(secondBulletValue, transform));
                break;

            case State.third:
                SetPattern(new ThirdBulletPattern(thirdBulletValue, transform));
                break;

            case State.four:
                SetPattern(new FourBulletPattern(FourBulletValue, transform));
                break;

            case State.final:
                SetPattern(new FinalBulletPattern(finalBulletValue, transform));
                break;
        }

        yield return null;
    }

    private void SetPattern(INormalBulletPattern pattern)
    {
        if(fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            currentPattern?.Clear();
        }

        currentPattern = pattern;
        currentPattern.Initialize();
        fireRoutine = StartCoroutine(currentPattern.Fire());
    }

    #region セミファイナル

    /// <summary>
    /// セミファイナル
    /// </summary>
    private IEnumerator SpecialFinalBullet()//
    {
        isSpecialBulletActive = true;

        // 移動にかける時間
        float limitTime = 0.5f;
        // 移動にかかった時間
        float elapsedTime = 0f;
        Vector2 startPosition = transform.position;

        // randomPosにlimitTimeかけて移動する
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, spellPos.x, elapsedTime / limitTime),
                Mathf.Lerp(startPosition.y, spellPos.y, elapsedTime / limitTime)
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return StartCoroutine(GomiSpecialMove.FinalSpecialAttack());
        yield return null;
    }

    #endregion

    /// <summary>
    /// エネミーの状態回復とStateの更新をします
    /// </summary>
    private IEnumerator TransitionToNextState()//
    {
        if (state < State.final)
        {
            state++;
            BulletState = BulletState.normal; // 弾幕の変更
            DamageLate = 1f;
            currentHP = maxHP; // HPを回復
            if (!isInvivle)
            {
                HealthBar.fillAmount = currentHP / maxHP;
            }
            yield return StartCoroutine(BulletUpdate());
        }
    }

    /// <summary>
    /// エネミーにダメージを与えます
    /// </summary>
    /// <param name="damage">与ダメージ</param>
    private IEnumerator TakeDamage(float damage)//
    {
        currentHP -= damage;

        // HPバーの更新
        if (!isInvivle)
        {
            HealthBar.fillAmount = currentHP / maxHP;
        }

        // HPが20%以下になったら必殺技に移行
        if (currentHP <= maxHP * 0.2f && BulletState == BulletState.normal)
        {
            BulletDelete();
            BulletState = BulletState.spell; // 弾幕の変更
            GomiSpecialMove.BomJudgement(state);
            GameObject CutInObj = Instantiate(
                CutInnCanvas,
                new Vector3(0, 0, 0),
                Quaternion.identity);
        }
        // HPが0になったら次の状態に移行
        else if (currentHP <= 0 && !isInvivle)
        {
            BulletDelete();
            // 最終段階なら最終技に移行
            if (state == State.final)
            {
                isInvivle = true;
                BulletState = BulletState.special; // 弾幕の変更
                yield return StartCoroutine(SpecialFinalBullet());
            }
            // それ以外なら次の段階に移行
            else
            {
                yield return StartCoroutine(TransitionToNextState());
            }
        }
    } 

    /// <summary>
    /// エネミーの弾幕をすべて消します
    /// </summary>
    private void BulletDelete()//
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("E_Bullet");
        if(state == State.final)
        {
            finalBulletValue.bullets.Clear();
        }
        foreach(GameObject bullet in objects)
        {
            Destroy(bullet);
        }
    }
    
    /// <summary>
    /// プレイヤーの弾幕に当たった際に作動します
    /// </summary>
    /// <param name="collision">プレイヤーの弾幕のタグの名前です。</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            StartCoroutine(TakeDamage(attak * DamageLate)); // ダメージ計算
            Destroy(collision.gameObject); // 弾を消す
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("P_Bom"))
        {
            StartCoroutine(TakeDamage(damageLate)); // ボムに当たったらダメージを与える
        }
    }
}