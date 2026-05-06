using UnityEngine;

public class PlayerHomingBullet : MonoBehaviour
{
    private Transform target;
    private float speed;
    private Rigidbody2D rb;

    /// <summary>
    /// ホーミング弾の初期化
    /// </summary>
    public void Initialize(Transform targetEnemy, float bulletSpeed)
    {
        target = targetEnemy;
        speed = bulletSpeed;
        rb = GetComponent<Rigidbody2D>();
        
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }

        // 初期速度を与えておく
        rb.linearVelocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        // ターゲットが既に消滅している場合はそのまま直進
        if (target == null)
        {
            if (rb != null)
            {
                rb.linearVelocity = transform.right * speed;
            }
            return;
        }

        // ターゲットの方向を計算
        Vector2 direction = (target.position - transform.position).normalized;

        // 向いている方向を更新
        transform.right = direction;

        // Rigidbody の速度を更新
        if (rb != null)
        {
            rb.linearVelocity = direction * speed;
        }
    }
}
