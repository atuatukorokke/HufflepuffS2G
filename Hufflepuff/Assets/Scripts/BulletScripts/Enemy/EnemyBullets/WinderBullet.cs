// ========================================
//
// WinderBullet.cs
//
// ========================================
//
// ワインダー（蛇行）状の弾幕を生成する敵の挙動。
// ・横方向へ移動 → ワインダー弾幕を一定時間発射 → 待機 → 繰り返し
// ・サイン波で横揺れを加えた独特の軌道を持つ弾幕
//
// ========================================

using System;
using System.Collections;
using UnityEngine;

public class WinderBullet : MonoBehaviour
{
    [Header("弾幕設定")]
    [SerializeField] private GameObject BulletPrehab;   // 弾のプレハブ
    [SerializeField] private float speed;               // 弾の速度
    [SerializeField] private float delayTime;           // 弾幕を撃つ時間
    [SerializeField] private float destroyTime;         // 弾の寿命
    [SerializeField, Range(0, 360)] private float angle; // 基準角度

    [Header("移動設定")]
    [SerializeField] private float destination;         // 移動先X座標
    [SerializeField] private float limitTime;           // 移動にかける時間

    GameObject proj;

    private void Start()
    {
        StartCoroutine(WindBulletUpdate(destination, limitTime));
    }

    /// <summary>
    /// 横移動 → ワインダー弾幕発射 → 待機 のループ
    /// </summary>
    private IEnumerator WindBulletUpdate(float targetX, float time)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        // -------------------------------
        // 横方向へ移動
        // -------------------------------
        while (elapsedTime < time)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetX, elapsedTime / time),
                startPosition.y
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // -------------------------------
        // ワインダー弾幕を撃ち続ける
        // -------------------------------
        while (true)
        {
            yield return StartCoroutine(WinderBulletCreat());
            yield return new WaitForSeconds(delayTime + 2.0f);
        }
    }

    /// <summary>
    /// サイン波で横揺れを加えたワインダー弾幕を生成
    /// </summary>
    private IEnumerator WinderBulletCreat()
    {
        float shotTime = 0f;

        while (shotTime < delayTime)
        {
            for (int i = -3; i < 3; i++)
            {
                float baseAngle = i * 20 + angle;
                float rad = baseAngle * Mathf.Deg2Rad;

                // サイン波による横揺れ
                float timeOffset = Time.time * 5f;     // 周波数
                float wave = Mathf.Sin(timeOffset + i) * 0.3f; // 振幅

                float dirX = Mathf.Cos(rad) + wave;
                float dirY = Mathf.Sin(rad);

                Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;

                GameObject proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

                rb.linearVelocity = moveDirection * -speed;

                Destroy(proj, destroyTime);
            }

            shotTime += 0.07f;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
