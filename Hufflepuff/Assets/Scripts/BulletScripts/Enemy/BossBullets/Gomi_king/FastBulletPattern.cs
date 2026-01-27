using System.Collections;
using UnityEngine;

// 一段階目の通常弾幕の変数
[System.Serializable]
public class FastBullet
{
    public GameObject BulletPrehab;                             // 弾幕のプレハブ
    public int FlyingNum;                                       // 発射する数
    public int frequency;                                       // 発射回数
    public float speed;                                         // 弾幕のスピード
    public float DeleteTime;                                    // 削除する時間
    public float delayTime;                                     // 弾幕を出す間隔
    public float angleOffset = 0f;                              // ずらし用の角度
    public float moveSpeed;                                     // 移動速度
}

public class FastBulletPattern : INormalBulletPattern
{
    private FastBullet config;
    private Transform boss;

    public FastBulletPattern(FastBullet config, Transform boss)
    {
        this.config = config;
        this.boss = boss;
    }

    public void Initialize()
    {
        config.angleOffset = 0;
    }

    public IEnumerator Fire()
    {
        while(true)
        {
            float angleStep = 360f / config.angleOffset;
            float angle = config.angleOffset;

            for(int i = 0; i < config.FlyingNum; i++)
            {
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 dir = new (x, y, 0);

                GameObject bullet = GameObject.Instantiate(
                    config.BulletPrehab,
                    boss.position,
                    Quaternion.identity);

                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * config.speed;
                angle += angleStep;
            }
            config.angleOffset += 10;
            yield return new WaitForSeconds(config.delayTime);
        }
    }

    public void Clear()
    {
        foreach(var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }
}
