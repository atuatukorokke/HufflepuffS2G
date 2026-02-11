using System.Buffers;
using System.Collections;
using UnityEngine;

// 一段階目の通常弾幕の変数
[System.Serializable]
public class FastBullet
{
    public GameObject BulletPrehab;                     // 弾幕のプレハブ
    public int FlyingNum;                               // 発射する数
    public int frequency;                               // 発射回数
    public float speed;                                 // 弾幕のスピード
    public float DeleteTime;                            // 削除する時間
    public float delayTime;                             // 弾幕を出す間隔
    public float angleOffset = 0f;                      // ずらし用の角度
    public float moveSpeed;                             // 移動速度
    public AudioClip bulletSE;                          // 弾幕を生成するときの効果音
}
public class FastBulletPattern : INormalBulletPattern
{
    private FastBullet config;
    private Transform boss;
    private Coroutine moveRoutine;
    private Boss1Bullet owner;

    public FastBulletPattern(FastBullet config, Transform boss, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.owner = owner;
    }

    public void Initialize()
    {
        config.angleOffset = 0;
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            for (int f = 0; f < config.frequency; f++)
            {
                float angleStep = 360f / config.FlyingNum;
                float angle = config.angleOffset;

                owner.Audio.PlayOneShot(config.bulletSE);

                for (int i = 0; i < config.FlyingNum; i++)
                {
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 dir = new(x, y, 0);

                    GameObject bullet = GameObject.Instantiate(
                        config.BulletPrehab,
                        boss.position,
                        Quaternion.identity);

                    bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * config.speed;

                    angle += angleStep;
                }

                config.angleOffset += 10;
                if (config.angleOffset >= 360f)
                    config.angleOffset -= 360f;

                yield return new WaitForSeconds(config.delayTime);
            }

            yield return MoveToRandomPos();
        }
    }

    private IEnumerator MoveToRandomPos()
    {
        Vector2 target = new Vector2(
            Random.Range(1.5f, 8.5f),
            Random.Range(-4.5f, 4.5f)
        );

        Vector2 start = boss.position;
        float t = 0f;
        float duration = config.moveSpeed;

        while (t < duration)
        {
            boss.position = Vector2.Lerp(start, target, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }

    public void Clear()
    {
        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }
}
