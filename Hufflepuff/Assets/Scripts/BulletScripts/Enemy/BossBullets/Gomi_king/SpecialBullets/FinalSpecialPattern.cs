// ========================================
//
// FinalSpecialPattern.cs
//
// ========================================
//
// 最終段階の必殺技（スペルカード）パターン。
// ・無敵移動 → ランダム高速弾ばら撒き → 停止 → 色変化 → 放射弾 → 再加速 → 休止
// ・複数フェーズを連続で行う複雑なスペル
// ・ISpellPattern に準拠
//
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FinalSpecianBom
{
    public GameObject BulletPrehab;         // 弾のプレハブ
    public float maxSpeed;                  // ランダム弾の最大速度
    public float minSpeed;                  // ランダム弾の最小速度
    public float randomSpeed;               // 再加速時の速度
    public float randomBulletTime;          // ランダム弾を撒く時間
    public int radiationBulletNum;          // 放射弾の数
    public float radiationBulletSpeed;      // 放射弾の速度
    public float radiationBulletDelayTime;  // 放射弾の発射間隔
    public float radiationBulletCount;      // 放射弾の発射回数
    public float radiationBulletAngle;      // 放射弾の角度範囲
    public float breakTime;                 // 最後の休止時間
    public Color bulletColor;               // 停止後の弾の色
    public AudioClip mainBulletSE;          // ランダム弾の効果音
    public AudioClip bulletStopSE;          // 弾停止時の効果音
    public AudioClip subBulletSE;           // 放射弾の効果音
}

public class FinalSpecialPattern : ISpellPattern
{
    private FinalSpecianBom config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public FinalSpecialPattern(FinalSpecianBom config, Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.spellPos = spellPos;
        this.owner = owner;
    }

    public void Initialize() { }

    /// <summary>
    /// 最終スペルのメイン処理。
    /// 無敵移動 → ランダム弾 → 停止 → 放射弾 → 再加速 → 休止 のループ。
    /// </summary>
    public IEnumerator Execute()
    {
        // 無敵状態でスペル位置へ移動
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        // final段階かつ spell 状態の間はスペルを続ける
        while (owner.State == State.final && owner.BulletState == BulletState.spell)
        {
            float time = 0f;
            int creatCount = 0;
            List<GameObject> bullets = new();

            // -------------------------------
            // ランダム方向へ高速弾をばら撒くフェーズ
            // -------------------------------
            while (time < config.randomBulletTime)
            {
                // 状態が変わったら中断
                if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

                creatCount++;

                // 10発ごとに効果音
                if (creatCount % 10 == 0)
                    owner.Audio.PlayOneShot(config.mainBulletSE);

                float angle = Random.Range(0f, 360f);
                float speed = Random.Range(config.minSpeed, config.maxSpeed);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

                GameObject bullet = GameObject.Instantiate(
                    config.BulletPrehab,
                    boss.position,
                    Quaternion.identity
                );

                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * speed;
                bullets.Add(bullet);

                yield return new WaitForSeconds(0.01f);
                time += 0.01f;
            }

            if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

            // -------------------------------
            // 弾を停止させるフェーズ
            // -------------------------------
            yield return new WaitForSeconds(1f);

            foreach (var b in bullets)
            {
                if (b == null) continue;

                b.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                b.GetComponent<SpriteRenderer>().color = config.bulletColor;
            }

            owner.Audio.PlayOneShot(config.bulletStopSE);

            if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

            // -------------------------------
            // ボスをランダム位置へ移動
            // -------------------------------
            Vector2 randomPos = new(Random.Range(1.5f, 8.5f), Random.Range(-4.5f, 4.5f));
            CoroutineRunner.Start(PositionMove(randomPos));

            // -------------------------------
            // 放射弾フェーズ
            // -------------------------------
            for (int i = 0; i < config.radiationBulletCount; i++)
            {
                if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

                owner.Audio.PlayOneShot(config.subBulletSE);

                float startAngle = 180f - config.radiationBulletAngle / 2f;
                float angleStep = config.radiationBulletAngle / (config.radiationBulletNum - 1);

                for (int j = 0; j < config.radiationBulletNum; j++)
                {
                    float angle = startAngle + j * angleStep;
                    float rad = angle * Mathf.Deg2Rad;

                    Vector2 dir = new(Mathf.Cos(rad), Mathf.Sin(rad));

                    GameObject bullet = GameObject.Instantiate(
                        config.BulletPrehab,
                        boss.position,
                        Quaternion.identity
                    );

                    bullet.GetComponent<Rigidbody2D>().linearVelocity =
                        dir * (config.radiationBulletSpeed - j * 0.1f);
                }

                yield return new WaitForSeconds(config.radiationBulletDelayTime);
            }

            if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

            // -------------------------------
            // 停止していた弾を再加速させるフェーズ
            // -------------------------------
            yield return new WaitForSeconds(1f);

            foreach (var b in bullets)
            {
                if (b == null) continue;

                float angle = Random.Range(0f, 360f);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

                b.GetComponent<Rigidbody2D>().linearVelocity = dir * config.randomSpeed;
            }

            // -------------------------------
            // 最後の休止フェーズ
            // -------------------------------
            yield return new WaitForSeconds(config.breakTime);
        }
    }

    /// <summary>
    /// ボスを指定位置へ移動させる補助コルーチン。
    /// </summary>
    private IEnumerator PositionMove(Vector2 target)
    {
        float t = 0f;
        float duration = 1f;
        Vector2 start = boss.position;

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
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
