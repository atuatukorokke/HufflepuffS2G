using System.Collections;
using UnityEngine;

// 四段階目の通常弾幕の変数
[System.Serializable]
public class FourBullet
{
    public GameObject BulletPrehab;                             // 弾幕のプレハブ
    public int FlyingNum;                                       // 発射する数
    public float Speed;                                         // 弾幕のスピード
    public float DeleteTime;                                    // 削除するまでの時間
    public float DelayTime;                                     // 弾幕を出す間隔
    public float AngleSpacing;                                  // 弾同士の角度のズレ
    public float AngleOffset = 0;                               // 弾幕の角度のズレ比率
    public AudioClip bulletSE;                                  // 弾幕を出すときの効果音
}

public class FourBulletPattern : INormalBulletPattern
{
    private FourBullet config;
    private Transform boss;
    private Coroutine fireRoutine;
    private Coroutine moveRoutine;
    private Boss1Bullet owner;

    public FourBulletPattern(FourBullet config, Transform boss, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.owner = owner;
    }

    public void Initialize()
    {
        config.AngleOffset = 0f;
    }

    public IEnumerator Fire()
    {
        // 弾幕発射コルーチン
        fireRoutine = CoroutineRunner.Start(FireBullets());

        // ボス移動コルーチン
        moveRoutine = CoroutineRunner.Start(MoveBoss());

        while (true)
            yield return null;
    }

    public void Clear()
    {
        if (fireRoutine != null) CoroutineRunner.Stop(fireRoutine);
        if (moveRoutine != null) CoroutineRunner.Stop(moveRoutine);

        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }

    private IEnumerator FireBullets()
    {
        while (true)
        {
            float angleStep = 360f / config.FlyingNum;
            float baseAngle = config.AngleOffset;

            owner.Audio.PlayOneShot(config.bulletSE);

            for (int i = 0; i < config.FlyingNum; i++)
            {
                float speed = config.Speed;

                // 3way（角度ずらし）
                for (int j = 1; j >= -1; j--)
                {
                    float offsetAngle = baseAngle + (j * config.AngleSpacing);

                    float dirX = Mathf.Cos(offsetAngle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(offsetAngle * Mathf.Deg2Rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                    GameObject proj = GameObject.Instantiate(
                        config.BulletPrehab,
                        boss.position,
                        Quaternion.identity
                    );

                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection.normalized * speed;

                    speed *= 0.9f; // 減速
                }

                baseAngle += angleStep;
            }

            config.AngleOffset += 5f;
            if (config.AngleOffset >= 360f)
                config.AngleOffset -= 360f;

            yield return new WaitForSeconds(config.DelayTime);
        }
    }

    // ---------------------------------------------------------
    // ボスのランダム移動
    // ---------------------------------------------------------
    private IEnumerator MoveBoss()
    {
        while (true)
        {
            Vector2 targetPos = new Vector2(
                Random.Range(1.5f, 8.5f),
                Random.Range(-4.5f, 4.5f)
            );

            Vector2 startPos = boss.position;
            float limitTime = 2f;
            float elapsed = 0f;

            while (elapsed < limitTime)
            {
                boss.position = Vector2.Lerp(startPos, targetPos, elapsed / limitTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(5f);
        }
    }
}
