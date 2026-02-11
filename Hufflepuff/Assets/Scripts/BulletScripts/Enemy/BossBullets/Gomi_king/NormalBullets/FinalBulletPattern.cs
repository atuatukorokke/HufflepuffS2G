using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//最終段階の通常弾幕
[System.Serializable]
public class FinalBulletValue
{
    public GameObject Prehab;                                   // 弾幕のプレハブ
    public int FlyingNum;                                       // 弾幕の数
    public float speed;                                         // 弾幕のスピード
    public float DeleteTime;                                    // 弾幕の消す時間
    public float DelayTime;                                     // 弾幕の消す時間
    public float radius;                                        // 半径
    public Transform player;                                    // プレイヤーのTransform
    public List<GameObject> bullets = new List<GameObject>();   // 生成した弾幕のリスト
    public AudioClip bulletSE;                                  // 弾幕を出すときの効果音
    public AudioClip bulletMoveSE;                              // 弾幕を動かすときの効果音
}

public class FinalBulletPattern : INormalBulletPattern
{
    private FinalBulletValue config;
    private Transform boss;
    private List<GameObject> bullets = new List<GameObject>();
    private Boss1Bullet owner;

    public FinalBulletPattern(FinalBulletValue config, Transform boss, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.owner = owner;
    }

    public void Initialize()
    {
        bullets.Clear();
    }

    public IEnumerator Fire()
    {
        while (true)
        {
            bullets.Clear();

            // 円形に弾を配置
            for (int i = 0; i < config.FlyingNum; i++)
            {
                owner.Audio.PlayOneShot(config.bulletSE);

                float angle = (360f / config.FlyingNum) * i;

                Vector3 spawnPos = boss.position + new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * config.radius,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * config.radius,
                    0f
                );

                GameObject bullet = GameObject.Instantiate(
                    config.Prehab,
                    spawnPos,
                    Quaternion.identity
                );

                bullets.Add(bullet);

                yield return new WaitForSeconds(config.DelayTime);
            }

            // 一斉発射
            yield return CoroutineRunner.Start(MoveBullets());

            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator MoveBullets()
    {
        if (config.player == null)
            config.player = GameObject.Find("Player").transform;

        Vector3 targetPos = config.player.position;
        owner.Audio.PlayOneShot(config.bulletMoveSE);

        foreach (GameObject bullet in bullets)
        {
            if (bullet == null) continue;

            Vector3 direction = (targetPos - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * config.speed;
        }

        bullets.Clear();
        yield return null;
    }

    public void Clear()
    {
        bullets.Clear();

        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }
}
