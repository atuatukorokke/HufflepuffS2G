// ========================================
//
// FastBulletPattern.cs
//
// ========================================
//
// 一段階目の通常弾幕パターン。
// ・円形弾幕を連続発射
// ・発射角度をずらして弾幕に変化をつける
// ・一定回数発射後にボスがランダム移動
// ・INormalBulletPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

// 一段階目の通常弾幕の設定データ
[System.Serializable]
public class FastBullet
{
    public GameObject BulletPrehab;     // 弾のプレハブ
    public int FlyingNum;               // 一度に発射する弾の数
    public int frequency;               // 発射回数
    public float speed;                 // 弾の速度
    public float DeleteTime;            // 弾が消えるまでの時間
    public float delayTime;             // 発射間隔
    public float angleOffset = 0f;      // 発射角度のずらし
    public float moveSpeed;             // ボスの移動速度
    public AudioClip bulletSE;          // 発射音
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

    /// <summary>
    /// パターン開始時の初期化処理。
    /// 発射角度をリセットする。
    /// </summary>
    public void Initialize()
    {
        config.angleOffset = 0;
    }

    /// <summary>
    /// 弾幕を撃ち続けるメインループ。
    /// 発射 → 移動 → 発射 → 移動… を繰り返す。
    /// </summary>
    public IEnumerator Fire()
    {
        // 弾幕を無限に繰り返す
        while (true)
        {
            // 指定された回数だけ弾を発射する
            for (int f = 0; f < config.frequency; f++)
            {
                float angleStep = 360f / config.FlyingNum;
                float angle = config.angleOffset;

                owner.Audio.PlayOneShot(config.bulletSE);

                // 円形に FlyingNum 個の弾を発射する
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

                    // 次の弾の角度へ進める
                    angle += angleStep;
                }

                // 発射角度を少しずらして弾幕に変化をつける
                config.angleOffset += 10;

                // 角度が 360° を超えたらループさせる
                if (config.angleOffset >= 360f)
                    config.angleOffset -= 360f;

                // 次の発射まで待機
                yield return new WaitForSeconds(config.delayTime);
            }

            // 発射後、ボスをランダム位置へ移動させる
            yield return MoveToRandomPos();
        }
    }

    /// <summary>
    /// ボスを画面内のランダムな位置へ移動させる。
    /// </summary>
    private IEnumerator MoveToRandomPos()
    {
        Vector2 target = new Vector2(
            Random.Range(1.5f, 8.5f),
            Random.Range(-4.5f, 4.5f)
        );

        Vector2 start = boss.position;
        float t = 0f;
        float duration = config.moveSpeed;

        // 指定時間かけてランダム位置へ移動する
        while (t < duration)
        {
            boss.position = Vector2.Lerp(start, target, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// このパターンで生成された弾をすべて削除する。
    /// </summary>
    public void Clear()
    {
        // 画面上の敵弾をすべて削除
        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }
}
