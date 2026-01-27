using System.Collections;
using UnityEngine;


// 三段階目の通常弾幕の変数
[System.Serializable]
public class ThirdBullet
{
    public GameObject bulletPrefab;                     // 弾幕のプレハブ
    public int flyingNum;                               // 発射する数
    public int frequency;                               // 発射回数
    public float speed;                                 // 弾幕のスピード
    public float deleteTime;                            // 削除する時間
    public float delayTime;                             // 弾幕を出す間隔
    public float angleOffset = 0;                       // ずらし用の角度
}

public class ThirdBulletPattern : INormalBulletPattern
{
    private ThirdBullet config;
    private Transform boss;

    public ThirdBulletPattern(ThirdBullet config, Transform boss)
    {
        this.config = config;
        this.boss = boss;
    }

    public void Initialize()
    {
        config.angleOffset = 0f;
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            float angleStep = 360f / config.flyingNum;
            float angle = config.angleOffset;

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

                angle += angleStep;
            }

            // 回転速度
            config.angleOffset += 10f;
            if (config.angleOffset >= 360f)
                config.angleOffset -= 360f;

            yield return new WaitForSeconds(config.delayTime);
        }
    }

    public void Clear()
    {
        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
        {
            GameObject.Destroy(bullet);
        }
    }
}
