// ========================================
//
// FinalBulletPattern.cs
//
// ========================================
//
// 最終段階の通常弾幕パターン。
// ・ボスの周囲に円形に弾を生成
// ・一定数生成後、全弾をプレイヤーへ向けて一斉発射
// ・INormalBulletPattern に準拠
//
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 最終段階の弾幕設定データ
[System.Serializable]
public class FinalBulletValue
{
    public GameObject Prehab;                                   // 弾のプレハブ
    public int FlyingNum;                                       // 生成する弾の数
    public float speed;                                         // 弾の速度
    public float DeleteTime;                                    // 弾が消えるまでの時間
    public float DelayTime;                                     // 弾生成の間隔
    public float radius;                                        // ボスからの距離（円形配置の半径）
    public Transform player;                                    // プレイヤーの Transform
    public List<GameObject> bullets = new List<GameObject>();   // 生成した弾のリスト
    public AudioClip bulletSE;                                  // 弾生成時の効果音
    public AudioClip bulletMoveSE;                              // 弾移動開始時の効果音
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

    /// <summary>
    /// パターン開始時の初期化処理。
    /// </summary>
    public void Initialize()
    {
        bullets.Clear();
    }

    /// <summary>
    /// 弾幕を撃ち続けるメインループ。
    /// 円形生成 → 一斉発射 → 待機 → 繰り返し。
    /// </summary>
    public IEnumerator Fire()
    {
        // 弾幕を無限に繰り返す
        while (true)
        {
            bullets.Clear();

            // ボスの周囲に円形に弾を生成する
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

                // 次の弾を生成するまで待機
                yield return new WaitForSeconds(config.DelayTime);
            }

            // 生成した弾をプレイヤーへ向けて一斉発射する
            yield return CoroutineRunner.Start(MoveBullets());

            // 次の弾幕まで少し待機
            yield return new WaitForSeconds(0.5f);
        }
    }

    /// <summary>
    /// 生成した弾をプレイヤーへ向けて一斉に移動させる。
    /// </summary>
    private IEnumerator MoveBullets()
    {
        // プレイヤーが未設定なら検索して取得
        if (config.player == null)
            config.player = GameObject.Find("Player").transform;

        Vector3 targetPos = config.player.position;

        owner.Audio.PlayOneShot(config.bulletMoveSE);

        // 全弾をプレイヤー方向へ移動させる
        foreach (GameObject bullet in bullets)
        {
            if (bullet == null) continue;

            Vector3 direction = (targetPos - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * config.speed;
        }

        bullets.Clear();
        yield return null;
    }

    /// <summary>
    /// このパターンで生成された弾をすべて削除する。
    /// </summary>
    public void Clear()
    {
        bullets.Clear();

        foreach (var bullet in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(bullet);
    }
}
