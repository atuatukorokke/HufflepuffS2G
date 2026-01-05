// Boss1Bulletrs.cs
//
// ボスの弾幕を生成する
// 移動の際は画面の左半分は入らない
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 一段階目の通常弾幕の変数
[System.Serializable]
public class FastBullet
{
    [SerializeField] public GameObject BulletPrehab;    // 弾幕のプレハブ
    [SerializeField] public int FlyingNum;              // 発射する数
    [SerializeField] public int frequency;              // 発射回数
    [SerializeField] public float speed;                // 弾幕のスピード
    [SerializeField] public float DeleteTime;           // 削除する時間
    [SerializeField] public float delayTime;            // 弾幕を出す間隔
    public float angleOffset = 0f;                      // ずらし用の角度
    [SerializeField] public float moveSpeed;            // 移動速度
}

// 二段階目の通常弾幕の変数
[System.Serializable] 
public class SecondBullet
{
    [SerializeField] public GameObject RevolutionBulletPrehab;  // 弾幕のプレハブ
    [SerializeField] public int FevolutionFlyingNum;            // 発射する数
    [SerializeField] public int FevolutionFrequency;            // 発射回数
    [SerializeField] public float FevolutionSpeed;              // 弾幕のスピード
    [SerializeField] public float FevolutionDeleteTime;         // 削除する時間
    [SerializeField] public float FevolutionDelayTime;          // 弾幕を出す間隔
    public float FevolutionAngleOffset = 0;                     // ずらし用の角度
}

// 三段階目の通常弾幕の変数
[System.Serializable] 
public class ThirdBullet
{
    [SerializeField] public GameObject RotationBulletPrehab;    // 弾幕のプレハブ
    [SerializeField] public int RotationFlyingNum;              // 発射する数
    [SerializeField] public int RotationFrequency;              // 発射回数
    [SerializeField] public float RotationSpeed;                // 弾幕のスピード
    [SerializeField] public float RotationDeleteTime;           // 削除する時間
    [SerializeField] public float RotationDelayTime;            // 弾幕を出す間隔
    public float RotationAngleOffset = 0;                       // ずらし用の角度
}

// 四段階目の通常弾幕の変数
[System.Serializable]
public class FourBullet
{
    [SerializeField] public GameObject BulletPrehab;            // 弾幕のプレハブ
    [SerializeField] public int FlyingNum;                      // 発射する数
    [SerializeField] public float Speed;                        // 弾幕のスピード
    [SerializeField] public float DeleteTime;                   // 削除するまでの時間
    [SerializeField] public float DelayTime;                    // 弾幕を出す間隔
    [SerializeField] public float AngleSpacing;                 // 弾同士の角度のズレ
    public float AngleOffset = 0;
}

//最終段階の通常弾幕
[System.Serializable]
public class FinalBulletValue
{
    [SerializeField] public GameObject Prehab;                  // 弾幕のプレハブ
    [SerializeField] public int FlyingNum;                      // 弾幕の数
    [SerializeField] public float speed;                        // 弾幕のスピード
    [SerializeField] public float DeleteTime;                   // 弾幕の消す時間
    [SerializeField] public float DelayTime;                    // 弾幕の消す時間
    [SerializeField] public float radius;                       // 半径
    public Transform player;                                    // プレイヤーのTransform
    public List<GameObject> bullets = new List<GameObject>();   // 生成した弾幕のリスト
}

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
    second = 1,
    third = 2,
    four = 3,
    final = 4
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
    [SerializeField] private Image HealthBar;                               // ボスの体力表示用画像
    [SerializeField] private GameObject CutInnCanvas;                       // カットイン用のキャンバス
    GameObject canvas; // ＨＰバーのキャンバス
    public event  System.Action Ondeath;

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

    public State State { get => state; set => state = value; }
    public BulletState BulletState { get => bulletState; set => bulletState = value; }
    public float DamageLate { get => damageLate; set => damageLate = value; }

    void Start()
    {
        // 初期位置から指定場所へ移動する
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
        damageLate = 1.0f; // ダメージを与える割合を初期化
        yield return StartCoroutine(BulletUpdate()); // 弾幕の更新を開始
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
        // 今の状態によって通常の弾幕を変化させる
        // ボスによって変化させるのでかなり大変
        if(!isInvivle)
        {
            switch (state)
            {
                case State.fast:
                    yield return StartCoroutine(FireFastBullet());
                    break;
                case State.second:
                    yield return StartCoroutine(FireSecondBullet());
                    break;
                case State.third:
                    yield return StartCoroutine(FireThirdBullet());
                    break;
                case State.four:
                    yield return StartCoroutine(FireFourBullet());
                    break;
                case State.final:
                    yield return StartCoroutine(FireFinalBullet());
                    break;
            }
        }
    }

    #region 一段階目の通常弾幕

    /// <summary>
    /// 一段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireFastBullet()
    {
        // 画面右半分をランダムに移動してから
        // 円形の弾幕を打つ
        while(state == State.fast && BulletState == BulletState.normal)
        {
            // 弾の横間隔の計算
            float angleStep = 360f / fastBulletValue.FlyingNum;
            float angle = fastBulletValue.angleOffset;

            // frequencyの回数だけ弾幕を生成する
            // FlyingNumは一回の生成で何個弾幕を作り出すか
            for (int i = 0; i < fastBulletValue.frequency; i++)
            {
                for (int j = 0; j < fastBulletValue.FlyingNum; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                    GameObject proj = Instantiate(fastBulletValue.BulletPrehab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection.normalized * fastBulletValue.speed;

                    angle += angleStep;

                }
                fastBulletValue.angleOffset += 10f; // ここを変えれば回転速度が変わる
                if (fastBulletValue.angleOffset >= 360) fastBulletValue.angleOffset -= 360f; // 範囲内を保つ
                yield return new WaitForSeconds(fastBulletValue.delayTime);
            }
            Vector2 randomPos = RandomPos(); // ランダムな移動先の排出
            float limitTime = 1f; // 移動にかける時間
            float elapsedTime = 0f; // 移動にかかった時間
            Vector2 startPosition = transform.position;
            // randomPosにlimitTimeかけて移動する
            while (elapsedTime < limitTime && BulletState == BulletState.normal)
            {
                transform.position = new Vector2(
                    Mathf.Lerp(startPosition.x, randomPos.x, elapsedTime / limitTime),
                    Mathf.Lerp(startPosition.y, randomPos.y, elapsedTime / limitTime)
                    );
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }

    #endregion

    #region 二段階目の通常弾幕

    /// <summary>
    /// 二段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireSecondBullet()
    {
        // 円形の弾幕を回転させながら打ちます

        while (state == State.second && BulletState == BulletState.normal)
        {
            // 弾の横間隔の計算（1周360度を指定の弾数で均等に分割）
            float angleStep = 360f / secondBulletValue.FevolutionFlyingNum;
            float angle = secondBulletValue.FevolutionAngleOffset;

            // 指定された弾数分ループ
            for (int i = 0; i < secondBulletValue.FevolutionFlyingNum; i++)
            {
                // 発射方向（x, y座標）を計算
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad); // X方向の速度を決定
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad); // Y方向の速度を決定
                Vector3 moveDirection = new Vector3(dirX, dirY, 0); // 弾の移動方向を作成

                // 弾を生成（プレハブを元にインスタンス化）
                GameObject proj = Instantiate(secondBulletValue.RevolutionBulletPrehab, transform.position, Quaternion.identity);

                // 弾の Rigidbody2D コンポーネントを取得し、速度を設定
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * secondBulletValue.FevolutionSpeed; // 速度を正規化して適用

                // 次の弾の発射方向を設定
                angle += angleStep;
            }

            // 弾の回転角度を更新（回転速度を調整）
            secondBulletValue.FevolutionAngleOffset += 20f;
            if (secondBulletValue.FevolutionAngleOffset >= 360) secondBulletValue.FevolutionAngleOffset -= 360f; // 360度を超えないよう調整

            // 一定時間待機
            yield return new WaitForSeconds(secondBulletValue.FevolutionDelayTime);
        }

        // 最終的に処理を終了
        yield return null;

    }

    #endregion

    #region 三段階目の通常弾幕

    /// <summary>
    /// 三段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireThirdBullet()
    {
        while(state == State.third && BulletState == BulletState.normal)
        {
            // 弾の横間隔の計算（360度を指定の弾数で均等に分割）
            float angleStep = 360f / thirdBulletValue.RotationFlyingNum;
            float angle = thirdBulletValue.RotationAngleOffset;

            for (int i = 0; i < thirdBulletValue.RotationFlyingNum; i++)
            {
                // 弾の発射方向（x, y座標）を計算
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad); // X方向の速度を決定
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad); // Y方向の速度を決定
                Vector3 moveDirection = new Vector3(dirX, dirY, 0); // 弾の移動方向を作成

                // 弾を生成（プレハブを元にインスタンス化）
                GameObject proj = Instantiate(thirdBulletValue.RotationBulletPrehab, transform.position, Quaternion.identity);

                // 弾の Rigidbody2D コンポーネントを取得し、速度を設定
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * thirdBulletValue.RotationSpeed; // 速度を正規化して適用

                // 次の弾の発射方向を設定
                angle += angleStep;
            }

            // 弾の回転角度を更新（回転速度を調整）
            thirdBulletValue.RotationAngleOffset += 10f;
            if (thirdBulletValue.RotationAngleOffset >= 360) thirdBulletValue.RotationAngleOffset -= 360f; // 360度を超えないよう調整

            // 一定時間待機
            yield return new WaitForSeconds(thirdBulletValue.RotationDelayTime);

        }
    }

    #endregion

    #region 四段階目の通常弾幕

    /// <summary>
    /// 四段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireFourBullet()
    {
        StartCoroutine(FireBullet());
        while (state == State.four && BulletState == BulletState.normal)
        {
            Vector2 targetPos = RandomPos(); // 移動先
            Vector2 startPosition = transform.position; // 移動の開始地点
            float limitTime = 2f; // 移動にかける時間
            float elapsedtime = 0; // 移動にかかってる時間
            while(elapsedtime < limitTime && BulletState == BulletState.normal)
            {
                transform.position = new Vector2(
                    Mathf.Lerp(startPosition.x, targetPos.x, elapsedtime / limitTime),
                    Mathf.Lerp(startPosition.y, targetPos.y, elapsedtime / limitTime)
                    );
                elapsedtime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(5f);
        }
        yield return null;
    }
    
    private IEnumerator FireBullet()
    {
        while(BulletState == BulletState.normal && state == State.four)
        {
            // 弾の横間隔の計算（360度を指定の弾数で均等に分割）
            float angleStep = 360f / FourBulletValue.FlyingNum;
            float baseAngle = FourBulletValue.AngleOffset;

            for (int i = 0; i < FourBulletValue.FlyingNum; i++)
            {
                float speed = FourBulletValue.Speed;
                for (int j = 1; j >= -1; j--)
                {
                    // それぞれの弾の発射方向をずらす
                    float offsetAngle = baseAngle + (j * FourBulletValue.AngleSpacing);

                    // 弾の発射方向（x, y座標）を計算
                    float dirX = Mathf.Cos(offsetAngle * Mathf.Deg2Rad); // X方向の速度を決定
                    float dirY = Mathf.Sin(offsetAngle * Mathf.Deg2Rad); // Y方向の速度を決定
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0); // 弾の移動方向を作成

                    // 弾を生成（プレハブを元にインスタンス化）
                    GameObject proj = Instantiate(FourBulletValue.BulletPrehab, transform.position, Quaternion.identity);

                    // 弾の Rigidbody2D コンポーネントを取得し、速度を設定
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection.normalized * speed; // 速度を正規化して適用

                    speed = speed * 0.9f;
                }
                
                // 次の弾の発射方向を設定
                baseAngle += angleStep;
            }

            // 弾の回転角度を更新（回転速度を調整）
            FourBulletValue.AngleOffset += 5f;
            if (FourBulletValue.AngleOffset >= 360) FourBulletValue.AngleOffset -= 360f; // 360度を超えないよう調整

            // 一定時間待機
            yield return new WaitForSeconds(FourBulletValue.DelayTime);
        }
        yield return null;  
    }

    #endregion

    #region 最終段階の通常弾幕

    /// <summary>
    /// 最終段階の通常弾幕です
    /// </summary>
    private IEnumerator FireFinalBullet()
    {
        while (state == State.final && BulletState == BulletState.normal) 
        {
            for (int i = 0; i < finalBulletValue.FlyingNum; i++)
            {
                if(BulletState == BulletState.normal)
                {
                    finalBulletValue.player = GameObject.Find("Player").transform;
                    float angle = (360 / finalBulletValue.FlyingNum) * i;
                    Vector3 spawnPos = transform.position + new Vector3(
                        Mathf.Cos(angle * Mathf.Deg2Rad) * finalBulletValue.radius,
                        Mathf.Sin(angle * Mathf.Deg2Rad) * finalBulletValue.radius,
                        0f);
                    GameObject bullet = Instantiate(finalBulletValue.Prehab, spawnPos, Quaternion.identity);

                    // 発射時にプレイヤーの位置を記録し、方向を決定
                    Vector3 direction = (finalBulletValue.player.position - spawnPos).normalized;
                    finalBulletValue.bullets.Add(bullet);
                    yield return new WaitForSeconds(finalBulletValue.DelayTime);
                }
            }
            StartCoroutine(BulletMover());
            yield return new WaitForSeconds(0.5f);
        } 
        yield return null;
    }

    /// <summary>
    /// 弾幕を自機方向に飛ばします
    /// </summary>
    /// <returns></returns>
    private IEnumerator BulletMover()
    {
        Vector3 targetPos = finalBulletValue.player.position;
        foreach(GameObject bullet in finalBulletValue.bullets)
        {
            if(finalBulletValue.bullets != null)
            {
                Vector3 direction = (targetPos - bullet.transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * finalBulletValue.speed;
            }
            else finalBulletValue.bullets.Clear();
        }
        finalBulletValue.bullets.Clear();
        yield return null;
    }

    #endregion

    #region セミファイナル

    /// <summary>
    /// セミファイナル
    /// </summary>
    private IEnumerator SpecialFinalBullet()
    {
        isSpecialBulletActive = true;
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
        yield return StartCoroutine(GomiSpecialMove.FinalSpecialAttack());
        yield return null;
    }

    #endregion

    /// <summary>
    /// エネミーの状態回復とStateの更新をします
    /// </summary>
    private IEnumerator TransitionToNextState()
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
    private IEnumerator TakeDamage(float damage)
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
    private void BulletDelete()
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
    /// ランダム移動の移動先を計算します
    /// </summary>
    /// <returns>移動先を返します</returns>
    private Vector2 RandomPos()
    {
        return new Vector2(Random.Range(1.5f, 8.5f), Random.Range(-4.5f, 4.5f));
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