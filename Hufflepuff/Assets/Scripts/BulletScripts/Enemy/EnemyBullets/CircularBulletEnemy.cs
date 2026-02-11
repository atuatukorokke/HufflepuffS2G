// ========================================
//
// CircularBulletEnemy.cs
//
// ========================================
//
// 円形弾幕を発射しながら移動する雑魚敵の挙動。
// ・指定座標まで移動 → 円形弾幕を複数回発射 → 画面外へ退場
// ・FlyingNum × frequency の弾を角度ずらしで生成
// ・ISpellPattern ではなく単体挙動の敵用スクリプト
//
// ========================================

using System.Collections;
using UnityEngine;

public class CircularBulletEnemy : MonoBehaviour
{
    [Header("円形弾幕の設定")]
    [SerializeField] private GameObject BulletPrehab;   // 弾のプレハブ
    [SerializeField] private int FlyingNum;             // 一度に発射する弾の数
    [SerializeField] private int frequency;             // 発射回数
    [SerializeField] private float speed;               // 弾の速度
    [SerializeField] private float DeleteTime;          // 弾を消すまでの時間
    [SerializeField] private float delayTime;           // 発射間隔
    private float angleOffset = 0f;                     // 発射角度のずらし

    [Header("移動設定")]
    [SerializeField] private float destination;         // 移動先のX座標
    [SerializeField] private float limitTime;           // 移動にかける時間

    private void Start()
    {
        StartCoroutine(CircularBullet(destination, limitTime));
    }

    /// <summary>
    /// 敵が移動しながら円形弾幕を発射する処理。
    /// </summary>
    private IEnumerator CircularBullet(float targetX, float time)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;

        // -------------------------------
        // 指定座標まで移動
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
        // 円形弾幕を発射
        // -------------------------------
        yield return StartCoroutine(ShootIncircle());

        // 少し待機
        yield return new WaitForSeconds(delayTime + 2.0f);

        // -------------------------------
        // 画面外へ退場
        // -------------------------------
        elapsedTime = 0f;
        startPosition = transform.position;

        while (elapsedTime < 5f)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, -10f, elapsedTime / 5f),
                Mathf.Lerp(startPosition.y, -10f, elapsedTime / 5f)
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// 円形弾幕を FlyingNum × frequency 回生成する。
    /// </summary>
    private IEnumerator ShootIncircle()
    {
        float angleStep = 360f / FlyingNum;
        float angle = angleOffset;

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

                Destroy(proj, DeleteTime);
            }

            // 発射角度をずらして回転させる
            angleOffset += 10f;
            if (angleOffset >= 360f)
                angleOffset -= 360f;

            yield return new WaitForSeconds(delayTime);
        }
    }
}
