// ========================================
//
// FourthBulletPattern.cs
//
// ========================================
//
// 四段階目の通常弾幕パターン。
// ・3way 弾を円形に連続発射
// ・発射角度を少しずつずらして弾幕に変化をつける
// ・ボスがランダム移動し続ける
// ・INormalBulletPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

// 四段階目の通常弾幕の設定データ
[System.Serializable]
public class FourBullet
{
    public GameObject BulletPrehab;     // 弾のプレハブ
    public int FlyingNum;               // 一度に発射する弾の数
    public float Speed;                 // 弾の速度
    public float DeleteTime;            // 弾が消えるまでの時間
    public float DelayTime;             // 発射間隔
    public float AngleSpacing;          // 3way の角度差
    public float AngleOffset = 0;       // 発射角度のずらし
    public AudioClip bulletSE;          // 発射音
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

    /// <summary>
    /// パターン開始時の初期化処理。
    /// 発射角度をリセットする。
    /// </summary>
    public void Initialize()
    {
        config.AngleOffset = 0f;
    }

    /// <summary>
    /// 弾幕発射とボス移動を同時に実行する。
    /// </summary>
    public IEnumerator Fire()
    {
        // 弾幕発射コルーチンを開始
        fireRoutine = CoroutineRunner.Start(FireBullets());

        // ボス移動コルーチンを開始
        moveRoutine = CoroutineRunner.Start(MoveBoss());

        // Fire 自体は何もしないが、コルーチンを維持する
        while (true)
            yield return null;
    }

    /// <summary>
    /// このパターンで生成された弾とコルーチンをすべて停止・削除する。
    /// </summary>
    public void Clear()
    {
        if (fireRoutine != null) CoroutineRunner.Stop(fireRoutine);
        if (moveRoutine != null) CoroutineRunner.Stop(moveRoutine);

        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }

    /// <summary>
    /// 3way 弾を円形に連続発射するメイン処理。
    /// </summary>
    private IEnumerator FireBullets()
    {
        while (true)
        {
            // 円形に弾を配置するための角度ステップ
            float angleStep = 360f / config.FlyingNum;
            float baseAngle = config.AngleOffset;

            owner.Audio.PlayOneShot(config.bulletSE);

            // FlyingNum 回、3way 弾を発射する
            for (int i = 0; i < config.FlyingNum; i++)
            {
                float speed = config.Speed;

                // 3way 発射（中央・左右）
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

                    // 3way の左右弾は少し速度を落とす
                    speed *= 0.9f;
                }

                // 次の弾の角度へ進める
                baseAngle += angleStep;
            }

            // 発射角度を少しずつずらして弾幕に変化をつける
            config.AngleOffset += 5f;

            // 360° を超えたらループさせる
            if (config.AngleOffset >= 360f)
                config.AngleOffset -= 360f;

            // 次の発射まで待機
            yield return new WaitForSeconds(config.DelayTime);
        }
    }

    /// <summary>
    /// ボスをランダムな位置へ移動させ続ける。
    /// </summary>
    private IEnumerator MoveBoss()
    {
        while (true)
        {
            // ランダムな移動先を決定
            Vector2 targetPos = new Vector2(
                Random.Range(1.5f, 8.5f),
                Random.Range(-4.5f, 4.5f)
            );

            Vector2 startPos = boss.position;
            float limitTime = 2f;
            float elapsed = 0f;

            // 指定時間かけてランダム位置へ移動する
            while (elapsed < limitTime)
            {
                boss.position = Vector2.Lerp(startPos, targetPos, elapsed / limitTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // 次の移動まで待機
            yield return new WaitForSeconds(5f);
        }
    }
}
