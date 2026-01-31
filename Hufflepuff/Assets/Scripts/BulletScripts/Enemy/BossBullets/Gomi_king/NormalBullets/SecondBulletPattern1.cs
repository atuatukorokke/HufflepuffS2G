using System.Collections;
using UnityEngine;

// 二段階目の通常弾幕の変数
[System.Serializable]
public class SecondBullet
{
    public GameObject BulletPrefab;                   // 弾幕のプレハブ
    public int FlyingNum;                             // 発射する数
    public int Frequency;                             // 発射回数
    public float Speed;                               // 弾幕のスピード
    public float DeleteTime;                          // 削除する時間
    public float DelayTime;                           // 弾幕を出す間隔
    public float AngleOffset = 0;                     // ずらし用の角度
}

public class SecondBulletPattern : INormalBulletPattern
{
    private SecondBullet config;
    private Transform boss;

    public SecondBulletPattern(SecondBullet config, Transform boss)
    {
        this.config = config;
        this.boss = boss;
    }

    /// <summary>
    /// 角度オフセットを初期化します
    /// </summary>
    public void Initialize()
    {
        config.AngleOffset = 0f;
    }

    public IEnumerator Fire()
    {
        float angleStep = 360f / config.FlyingNum;
        float angle = config.AngleOffset;

        for(int i = 0; i < config.FlyingNum; i++)
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

            angle += angleStep;
        }

        config.AngleOffset += 20f;
        if (config.AngleOffset >= 360f)
            config.AngleOffset = 0f;

        yield return new WaitForSeconds(config.DelayTime);
    }

    public void Clear()
    {
        foreach(var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
        {
            GameObject.Destroy(bullet);
        }
    }
}
