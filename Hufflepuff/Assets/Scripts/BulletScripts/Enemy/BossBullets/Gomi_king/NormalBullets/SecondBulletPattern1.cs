// ========================================
//
// SecondBulletPattern.cs
//
// ========================================
//
// 二段階目の通常弾幕パターン。
// ・円形に弾を生成して一斉発射
// ・発射角度を少しずつずらして弾幕に変化をつける
// ・INormalBulletPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

// 二段階目の通常弾幕の設定データ
[System.Serializable]
public class SecondBullet
{
    public GameObject BulletPrefab;     // 弾のプレハブ
    public int FlyingNum;               // 一度に発射する弾の数
    public int Frequency;               // 発射回数（未使用だが保持）
    public float Speed;                 // 弾の速度
    public float DeleteTime;            // 弾が消えるまでの時間
    public float DelayTime;             // 発射間隔
    public float AngleOffset = 0;       // 発射角度のずらし
    public AudioClip bulletSE;          // 発射音
}

public class SecondBulletPattern : INormalBulletPattern
{
    private SecondBullet config;
    private Transform boss;
    private Boss1Bullet owner;

    public SecondBulletPattern(SecondBullet config, Transform boss, Boss1Bullet owner)
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
        config.AngleOffset = 0f;
    }

    /// <summary>
    /// 円形に弾を生成して一斉発射する。
    /// 発射角度をずらしながら繰り返す。
    /// </summary>
    public IEnumerator Fire()
    {
        while (true)
        {

            // 円形配置のための角度ステップを計算
            float angleStep = 360f / config.FlyingNum;
            float angle = config.AngleOffset;

            owner.Audio.PlayOneShot(config.bulletSE);

            // FlyingNum 回、弾を円形に発射する
            for (int i = 0; i < config.FlyingNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new(dirX, dirY, 0);

                GameObject proj = GameObject.Instantiate(
                    config.BulletPrefab,
                    boss.position,
                    Quaternion.identity);

                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * config.Speed;

                // 次の弾の角度へ進める
                angle += angleStep;
            }

            // 発射角度をずらして弾幕に変化をつける
            config.AngleOffset += 20f;

            // 360° を超えたらループさせる
            if (config.AngleOffset >= 360f)
                config.AngleOffset = 0f;

            // 次の発射まで待機
            yield return new WaitForSeconds(config.DelayTime);
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
