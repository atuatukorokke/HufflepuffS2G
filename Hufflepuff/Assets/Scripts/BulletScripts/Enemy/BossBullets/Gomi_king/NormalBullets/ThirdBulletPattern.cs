// ========================================
//
// ThirdBulletPattern.cs
//
// ========================================
//
// 三段階目の通常弾幕パターン。
// ・円形に弾を生成して連続発射
// ・発射角度を少しずつずらして弾幕に変化をつける
// ・INormalBulletPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

// 三段階目の通常弾幕の設定データ
[System.Serializable]
public class ThirdBullet
{
    public GameObject bulletPrefab;     // 弾のプレハブ
    public int flyingNum;               // 一度に発射する弾の数
    public int frequency;               // 発射回数（未使用だが保持）
    public float speed;                 // 弾の速度
    public float deleteTime;            // 弾が消えるまでの時間
    public float delayTime;             // 発射間隔
    public float angleOffset = 0;       // 発射角度のずらし
    public AudioClip bulletSE;          // 発射音
}

public class ThirdBulletPattern : INormalBulletPattern
{
    private ThirdBullet config;
    private Transform boss;
    private Boss1Bullet owner;

    public ThirdBulletPattern(ThirdBullet config, Transform boss, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.owner = owner;
    }

    /// <summary>
    /// パターン開始時の初期化処理。
    /// 発射角度をリセットする。
    /// </summary>
    public void Initialize()
    {
        config.angleOffset = 0f;
    }

    /// <summary>
    /// 円形に弾を生成して連続発射するメイン処理。
    /// 発射角度をずらしながら無限に繰り返す。
    /// </summary>
    public IEnumerator Fire()
    {
        // 弾幕を無限に繰り返す
        while (true)
        {
            // 円形配置のための角度ステップを計算
            float angleStep = 360f / config.flyingNum;
            float angle = config.angleOffset;

            owner.Audio.PlayOneShot(config.bulletSE);

            // flyingNum 回、弾を円形に発射する
            for (int i = 0; i < config.flyingNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = GameObject.Instantiate(
                    config.bulletPrefab,
                    boss.position,
                    Quaternion.identity
                );

                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * config.speed;

                // 次の弾の角度へ進める
                angle += angleStep;
            }

            // 発射角度を少しずつずらして弾幕に変化をつける
            config.angleOffset += 10f;

            // 360° を超えたらループさせる
            if (config.angleOffset >= 360f)
                config.angleOffset -= 360f;

            // 次の発射まで待機
            yield return new WaitForSeconds(config.delayTime);
        }
    }

    /// <summary>
    /// このパターンで生成された弾をすべて削除する。
    /// </summary>
    public void Clear()
    {
        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
        {
            GameObject.Destroy(bullet);
        }
    }
}
