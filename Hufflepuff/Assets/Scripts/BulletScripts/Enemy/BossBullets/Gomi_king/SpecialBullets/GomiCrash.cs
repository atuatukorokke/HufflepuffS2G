// ========================================
//
// GomiCrash.cs
//
// ========================================
//
// ボスエネミーの「破裂（クラッシュ）弾」を生成するクラス。
// ・一定時間待機 → 多段階の円形弾生成 → 徐々に減速 → 自身を破棄
// ・単発の特殊攻撃として使用される
//
// ========================================

using System.Collections;
using UnityEngine;

public class GomiCrash : MonoBehaviour
{
    [SerializeField] private GameObject Prehab;     // 発射する弾のプレハブ
    [SerializeField] private float speed;           // 弾の初速
    [SerializeField] private int ShotNum;           // 一度に発射する弾の数
    [SerializeField] private float angleOffset;     // 発射角度の初期オフセット

    /// <summary>
    /// 生成後に自動でクラッシュ弾処理を開始する。
    /// </summary>
    void Start()
    {
        StartCoroutine(Crash());
    }

    /// <summary>
    /// ボスの破裂（クラッシュ）弾処理。
    /// 2秒待機 → 6回の円形弾生成（徐々に減速） → 自身を破棄。
    /// </summary>
    private IEnumerator Crash()
    {
        // 発射前の待機
        yield return new WaitForSeconds(2f);

        float speedLate = 1f;

        // 6回、円形に弾をばら撒く
        for (int j = 0; j < 6; j++)
        {
            float angleStep = 360f / ShotNum;
            float angle = angleOffset;

            // ShotNum 個の弾を円形に発射
            for (int i = 0; i < ShotNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject bullet = Instantiate(Prehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // 発射方向へ速度を与える（徐々に減速）
                rb.linearVelocity = moveDirection.normalized * speed * speedLate;

                // 弾の向きを発射方向に合わせる
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 180);

                angle += angleStep;
            }

            // 次の円形弾は少し減速させる
            speedLate *= 0.9f;
        }

        // 自身を破棄
        Destroy(gameObject);
        yield return null;
    }
}
