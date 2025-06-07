// Boss1Bulletrs.cs
//
// ボスの弾幕を生成する
// 移動の際は画面の左半分は入らない
//

using System.Collections;
using UnityEngine;

// 一段階目の通常弾幕の変数
[System.Serializable]
public class FastBullet
{
    [SerializeField] public GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] public int FlyingNum; // 発射する数
    [SerializeField] public int frequency; // 発射回数
    [SerializeField] public float speed; // 弾幕のスピード
    [SerializeField] public float DeleteTime; // 削除する時間
    [SerializeField] public float delayTime; // 弾幕を出す間隔
    public float angleOffset = 0f; // ずらし用の角度
    [SerializeField] public float moveSpeed;
}

// 二段階目の通常弾幕の変数
[System.Serializable] 
public class SecondBullet
{
    [SerializeField] public GameObject RevolutionBulletPrehab; // 弾幕のプレハブ
    [SerializeField] public int FevolutionFlyingNum; // 発射する数
    [SerializeField] public int FevolutionFrequency; // 発射回数
    [SerializeField] public float FevolutionSpeed; // 弾幕のスピード
    [SerializeField] public float FevolutionDeleteTime; // 削除する時間
    [SerializeField] public float FevolutionDelayTime; // 弾幕を出す間隔
    public float FevolutionAngleOffset = 0;
}

// 三段階目の通常弾幕の変数
[System.Serializable] 
public class ThirdBullet
{
    [SerializeField] public GameObject RotationBulletPrehab; // 弾幕のプレハブ
    [SerializeField] public int RotationFlyingNum; // 発射する数
    [SerializeField] public int RotationFrequency; // 発射回数
    [SerializeField] public float RotationSpeed; // 弾幕のスピード
    [SerializeField] public float RotationDeleteTime; // 削除する時間
    [SerializeField] public float RotationDelayTime; // 弾幕を出す間隔
    public float RotationAngleOffset = 0;
}

// 四段階目の通常弾幕の変数
[System.Serializable]
public class FourBullet
{
    [SerializeField] private GameObject Prehab;
}


//敵の弾幕の状態
public enum BulletState
{
    normal,
    spell
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

    [Header("一段階目の通常弾幕の変数")]
    [SerializeField] private FastBullet fastBulletValue;

    [Header("二段階目の通常弾幕の変数")]
    [SerializeField] private SecondBullet secondBulletValue;

    [Header("三段階目の通常弾幕の変数")]
    [SerializeField] private ThirdBullet thirdBulletValue;

    [Header("四段階目の通常弾幕の変数")]
    [SerializeField] private FourBullet FourBulletValue;

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

                    Destroy(proj, fastBulletValue.DeleteTime); // 何秒後に弾幕を消す

                }
                fastBulletValue.angleOffset += 10f; // ここを変えれば回転速度が変わる
                if (fastBulletValue.angleOffset >= 360) fastBulletValue.angleOffset -= 360f; // 範囲内を保つ
                yield return new WaitForSeconds(fastBulletValue.delayTime);
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

                // 一定時間後に弾を削除
                Destroy(proj, secondBulletValue.FevolutionDeleteTime);
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
    /// <summary>
    /// 三段階目の通常弾幕です
    /// </summary>
    private IEnumerator FireThirdBullet()
    {
        while(state == State.third && bulletState == BulletState.normal)
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

                // 一定時間後に弾を削除
                Destroy(proj, thirdBulletValue.RotationDeleteTime);
            }

            // 弾の回転角度を更新（回転速度を調整）
            thirdBulletValue.RotationAngleOffset += 10f;
            if (thirdBulletValue.RotationAngleOffset >= 360) thirdBulletValue.RotationAngleOffset -= 360f; // 360度を超えないよう調整

            // 一定時間待機
            yield return new WaitForSeconds(thirdBulletValue.RotationDelayTime);

        }
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
    /// <summary>
    /// プレイヤーの弾幕に当たった際に作動します
    /// </summary>
    /// <param name="collision">プレイヤーの弾幕のタグの名前です。</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            StartCoroutine(TakeDamage(attak * damageLate)); // ダメージ計算
            Destroy(collision.gameObject); // 弾を消す
        }
    }
}
