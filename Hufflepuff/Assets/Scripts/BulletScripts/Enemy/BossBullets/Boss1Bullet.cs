// Boss1Bulletrs.cs
//
// ボスの弾幕を生成する
// 移動の際は画面の左半分は入らない
//

using System.Collections;
using UnityEngine;
public enum BulletState
{
    normal,
    spell
}

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
    [SerializeField] private State state = State.fast; // 今の攻撃が何段階目かの判別
    [SerializeField] private BulletState bulletState = BulletState.normal; // HP判別でどの弾幕を撃つか判別する
    [SerializeField] private float maxHP = 100f; // 敵の最大HP
    [Range(0, 100)]
    [SerializeField] private float currentHP; // 今のHP
    private bool isSpecialBulletActive = false; // HPが0になったらtrueになる→ラストワード発生
    private float specialBulletDuration = 15f; // 何秒ラストワードを撃つか
    private float timer = 0f; // 今何秒たったか
    private float damageLate = 1f; // ダメージを与える割合
    [SerializeField] private float attak = 1f; // 攻撃力
    [SerializeField] private Vector2 spellPos; // 必殺技・セミファイナルを打つときにこの座標に一旦戻る

    [Header("ノーマル円形弾幕の変数")]
    [SerializeField] private GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] private int FlyingNum; // 発射する数
    [SerializeField] private int frequency; // 発射回数
    [SerializeField] private float speed; // 弾幕のスピード
    [SerializeField] private float DeleteTime; // 削除する時間
    [SerializeField] private float delayTime; // 弾幕を出す間隔
    private float angleOffset = 0f; // ずらし用の角度
    [SerializeField] private float moveSpeed;

    [Header("ノーマル回転円形弾幕の変数")]
    [SerializeField] private GameObject RevolutionBulletPrehab;
    [SerializeField] private int FevolutionFlyingNum;
    [SerializeField] private int FevolutionFrequency;
    [SerializeField] private float FevolutionSpeed;
    [SerializeField] private float FevolutionDeleteTime;
    [SerializeField] private float FevolutionDelayTime;
    private float FevolutionAngleOffset = 0;

    void Start()
    {
        currentHP = maxHP;
        StartCoroutine(BulletUpdate());
    }


    private void Update()
    {
        // 数秒間セミファイナルを打ち続けると消える
        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;
            if (timer >= specialBulletDuration)
            {
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
        if (currentHP > 0 && bulletState == BulletState.normal)
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
        // 楽しいのでワース
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
    /// <summary>
    /// 一段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireFastBullet()
    {
        // 画面右半分をランダムに移動してから
        // 円形の弾幕を打つ
        while(state == State.fast && bulletState == BulletState.normal)
        {
            // 弾の横間隔の計算
            float angleStep = 360f / FlyingNum;
            float angle = angleOffset;

            // frequencyの回数だけ弾幕を生成する
            // FlyingNumは一回の生成で何個弾幕を作り出すか
            for (int i = 0; i < frequency; i++)
            {
                for (int j = 0; j < FlyingNum; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                    GameObject proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection.normalized * speed;

                    angle += angleStep;

                    Destroy(proj, DeleteTime); // 何秒後に弾幕を消す

                }
                angleOffset += 10f; // ここを変えれば回転速度が変わる
                if (angleOffset >= 360) angleOffset -= 360f; // 範囲内を保つ
                yield return new WaitForSeconds(delayTime);
            }
            Vector2 randomPos = RandomPos(); // ランダムな移動先の排出
            float limitTime = 0.5f; // 移動にかける時間
            float elapsedTime = 0f; // 移動にかかった時間
            Vector2 startPosition = transform.position;
            // randomPosにlimitTimeかけて移動する
            while (elapsedTime < limitTime)
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
    /// <summary>
    /// 二段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireSecondBullet()
    {
        // 円形の弾幕を回転させながら打ちます

        while (state == State.second && bulletState == BulletState.normal)
        {
            // 弾の横間隔の計算
            float angleStep = 360f / FevolutionFlyingNum;
            float angle = FevolutionAngleOffset;
            for (int i = 0; i < FevolutionFlyingNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = Instantiate(RevolutionBulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * FevolutionSpeed;

                angle += angleStep;

                Destroy(proj, FevolutionDeleteTime); // 何秒後に弾幕を消す
            }
            FevolutionAngleOffset += 20f; // ここを変えれば回転速度が変わる
            if (FevolutionAngleOffset >= 360) FevolutionAngleOffset -= 360f; // 範囲内を保つ
            yield return new WaitForSeconds(FevolutionDelayTime);
        }
        yield return null;
    }
    /// <summary>
    /// 三段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireThirdBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
        yield return null;
    }
    /// <summary>
    /// 四段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireFourBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
        yield return null;
    }
    /// <summary>
    /// 最終段階の通常弾幕です
    /// </summary>
    private IEnumerator FireFinalBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
        yield return null;
    }
    // LastWard
    private IEnumerator FireSpecialBullet()
    {
        float limitTime = 1f; // 移動にかける時間
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
        Debug.Log("特殊弾幕発射: " + state);
        yield return null;
    }

    /// <summary>
    /// 最後の大技を出します
    /// </summary>
    private IEnumerator SpecialFinalBullet()
    {
        if (!isSpecialBulletActive)
        {
            isSpecialBulletActive = true;
            Debug.Log("Final状態: 特別な弾幕を発射");
            yield return null;
        }
    }

    /// <summary>
    /// エネミーの状態回復とStateの更新をします
    /// </summary>
    private IEnumerator TransitionToNextState()
    {
        if (state < State.final)
        {
            state++;
            bulletState = BulletState.normal; // 弾幕の変更
            damageLate = 1f;
            currentHP = maxHP; // HPを回復
            Debug.Log("Stateが変更されました: " + state);
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
        if (currentHP <= maxHP * 0.2f && bulletState == BulletState.normal)
        {
            damageLate = 0.2f; // HPの減少スピードの変更
            bulletState = BulletState.spell; // 弾幕の変更
            yield return StartCoroutine(FireSpecialBullet());
        }
        else if (currentHP <= 0)
        {
            if (state == State.final)
            {
                StartCoroutine(SpecialFinalBullet());
            }
            else
            {
                StartCoroutine(TransitionToNextState());
            }
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
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            StartCoroutine(TakeDamage(attak * damageLate)); // ダメージ計算
            Destroy(collision.gameObject); // 弾を消す
        }
    }
}
