// Winderbullet.cs
//
// ワインダー状に弾幕を生成する
// このエネミーは生成時に縦移動はせず、横方向だけ移動します
//


using System;
using System.Collections;
using UnityEngine;

public class WinderBullet : MonoBehaviour
{
    [Header("弾幕用変数")]
    [SerializeField] private GameObject BulletPrehab;   // 弾幕のプレハブ
    [SerializeField] private float speed;               // 弾幕のスピード
    [SerializeField] private float delayTime;           // 弾幕を撃つ間隔
    [SerializeField] private float destroyTime;         // 弾幕を消すまでの時間
    [SerializeField]
    [Range(0, 360)]
    float angle;

    [Header("移動用変数")]
    [SerializeField] private float destination;         // 到着座標
    [SerializeField] private float limitTime;           // 移動にかける時間

    GameObject proj;
    void Start()
    {
        StartCoroutine(WindBulletUpdate(destination, limitTime));
    }

    /// <summary>
    /// ワインダー弾幕の更新
    /// </summary>
    /// <param name="targetX">移動先のX座標</param>
    /// <param name="time">移動に掛ける時間</param>
    private IEnumerator WindBulletUpdate(float targetX, float time)
    {
        // destinationまで移動する
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        // 移動処理
        while (elapsedTime < time)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetX, elapsedTime / time),
                startPosition.y
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        while (true)
        {
            yield return StartCoroutine(WinderBulletCreat());
            yield return new WaitForSeconds(delayTime + 2.0f);
        }
    }

    /// <summary>
    /// ワインダー弾幕の生成
    /// </summary>
    private IEnumerator WinderBulletCreat()
    {
        float shotTime = 0;

        // 弾幕をshotTimeの時間の間だけ撃つ
        while (shotTime < delayTime)
        {
            for(int i = -3; i < 3; i++)
            {
                float baseAngle = i * 20 + angle;
                float rad = baseAngle * Mathf.Deg2Rad;

                // サイン波の揺れ（時間をもとに横方向を補正）
                float timeOffset = Time.time * 5f; // 周波数を変更したい場合はここの「5f」を変える
                float wave = Mathf.Sin(timeOffset + i) * 0.3f; // 振幅を変えたいなら「0.3f」を変更

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
