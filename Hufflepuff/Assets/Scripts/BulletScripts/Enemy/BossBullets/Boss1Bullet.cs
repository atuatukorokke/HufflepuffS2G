// Boss1Bulletrs.cs
//
// ボスの弾幕を生成する
// 移動の際は画面の左半分は入らない
//


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
    [SerializeField] private State state = State.fast; // 今の攻撃が何段階目かの判別
    [SerializeField] private BulletState bulletState = BulletState.normal; // HP判別でどの弾幕を撃つか判別する
    [SerializeField] private float maxHP = 100f; // 敵の最大HP
    [Range(0, 100)]
    [SerializeField] private float currentHP; // 今のHP
    private bool isSpecialBulletActive = false; // HPが0になったらtrueになる→ラストワード発生
    private float specialBulletDuration = 15f; // 何秒ラストワードを撃つか
    private float timer = 0f; // 今何秒たったか
    private float damageLate = 1f;
    [SerializeField] private float attak = 1f;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (currentHP > 0 && bulletState == BulletState.normal)
        {
            HandleBulletPattern();
        }
        else if(currentHP <= 0)
        {
            if (state == State.final)
            {
                SpecialFinalBullet();
            }
            else
            {
                TransitionToNextState();
            }
        }

        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;
            if (timer >= specialBulletDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandleBulletPattern()
    {
        // 今の状態によって通常の弾幕を変化させる
        // ボスによって変化させるのでかなり大変
        // 楽しいのでワース
        switch (state)
        {
            case State.fast:
                FireFastBullet();
                break;
            case State.second:
                FireSecondBullet();
                break;
            case State.third:
                FireThirdBullet();
                break;
            case State.four:
                FireFourBullet();
                break;
            case State.final:
                FireFinalBullet();
                break;
        }
    }
    /// <summary>
    /// 一段階目の通常弾幕です
    /// </summary>
    private void FireFastBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
    }
    /// <summary>
    /// 二段階目の通常弾幕です
    /// </summary>
    private void FireSecondBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
    }
    /// <summary>
    /// 三段階目の通常弾幕です
    /// </summary>
    private void FireThirdBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
    }
    /// <summary>
    /// 四段階目の通常弾幕です
    /// </summary>
    private void FireFourBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
    }
    /// <summary>
    /// 最終段階の通常弾幕です
    /// </summary>
    private void FireFinalBullet()
    {
        Debug.Log("通常弾幕発射: " + state);
    }
    // LastWard
    private void FireSpecialBullet()
    {
        Debug.Log("特殊弾幕発射: " + state);
    }

    /// <summary>
    /// 最後の大技を出します
    /// </summary>
    private void SpecialFinalBullet()
    {
        if (!isSpecialBulletActive)
        {
            isSpecialBulletActive = true;
            Debug.Log("Final状態: 特別な弾幕を発射");
        }
    }

    /// <summary>
    /// エネミーの状態回復とStateの更新をします
    /// </summary>
    private void TransitionToNextState()
    {
        if (state < State.final)
        {
            state++;
            bulletState = BulletState.normal; // 弾幕の変更
            damageLate = 1f;
            currentHP = maxHP; // HPを回復
            Debug.Log("Stateが変更されました: " + state);
        }
    }

    /// <summary>
    /// エネミーにダメージを与えます
    /// </summary>
    /// <param name="damage">与ダメージ</param>
    private void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= maxHP * 0.2f)
        {
            damageLate = 0.2f; // HPの減少スピードの変更
            bulletState = BulletState.spell; // 弾幕の変更
            FireSpecialBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            TakeDamage(attak * damageLate); // ダメージ計算
            Destroy(collision.gameObject); // 弾を消す
        }
    }
}
