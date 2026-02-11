// ========================================
//
// SecondSpecialPattern.cs
//
// ========================================
//
// 二段階目の必殺技（スペルカード）パターン。
// ・無敵移動 → 回転しながらの多方向弾 → 横からの直進弾 → ループ
// ・角度が時間で変化する「回転弾」と、画面左からの「直進弾」を組み合わせた攻撃
// ・ISpellPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

[System.Serializable]
public class SecondSpecialBom
{
    public GameObject RightBulletPrehab;    // 右側から流れてくる弾
    public GameObject LeftBulletPrehab;     // ボス本体から出る回転弾
    public float delayTime;                 // 回転弾フェーズの継続時間
    public int BulletNum;                   // 右側から流す弾の数
    public float time;                      // 回転弾の角度変化に使う時間（未使用）
    public float speed;                     // 弾の速度
    [Range(0, 360)]
    public float angle;                     // 回転弾の基準角度
    public AudioClip mainBulletSE;          // 回転弾の効果音
    public AudioClip leftBulletSE;          // 右側弾の効果音
}

public class SecondSpecialPattern : ISpellPattern
{
    private SecondSpecialBom config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public SecondSpecialPattern(SecondSpecialBom config, Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.spellPos = spellPos;
        this.owner = owner;
    }

    public void Initialize() { }

    /// <summary>
    /// 二段階目スペルのメイン処理。
    /// 無敵移動 → 回転弾 → 横からの直進弾 → ループ。
    /// </summary>
    public IEnumerator Execute()
    {
        // 無敵状態でスペル位置へ移動
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        float shotTime = 0f;

        // 二段階目かつ spell 状態の間はスペルを続ける
        while (owner.State == State.second && owner.BulletState == BulletState.spell)
        {
            // -----------------------------------------
            // 回転しながらの多方向弾フェーズ
            // -----------------------------------------
            while (shotTime < config.delayTime)
            {
                if (owner.BulletState != BulletState.spell) break;

                owner.Audio.PlayOneShot(config.mainBulletSE);

                // -3 ～ +2 の 6方向に回転弾を撃つ
                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + config.angle;

                    // shotTime に応じて角度が変化する（回転）
                    float incremental = shotTime * 10f;

                    float rad = (baseAngle + incremental) * Mathf.Deg2Rad;
                    Vector3 dir = new(Mathf.Cos(rad), Mathf.Sin(rad), 0);

                    GameObject bullet = GameObject.Instantiate(
                        config.LeftBulletPrehab,
                        boss.position,
                        Quaternion.identity
                    );

                    // 左弾は逆方向へ飛ばす（-speed）
                    bullet.GetComponent<Rigidbody2D>().linearVelocity =
                        dir.normalized * -config.speed;
                }

                shotTime += 0.07f;
                yield return new WaitForSeconds(0.07f);
            }

            if (owner.BulletState != BulletState.spell) break;

            // -----------------------------------------
            // 画面左から右へ流れる直進弾フェーズ
            // -----------------------------------------
            for (int i = 0; i < config.BulletNum; i++)
            {
                if (owner.BulletState != BulletState.spell) break;

                owner.Audio.PlayOneShot(config.leftBulletSE);

                // 左端のランダム位置から右へ直進
                Vector2 pos = new(-9f, Random.Range(-4.5f, 4.5f));

                GameObject bullet = GameObject.Instantiate(
                    config.RightBulletPrehab,
                    pos,
                    Quaternion.identity
                );

                bullet.GetComponent<Rigidbody2D>().linearVelocity =
                    new Vector2(1, 0) * config.speed;

                yield return new WaitForSeconds(0.05f);
            }

            // 回転弾フェーズの時間をリセット
            shotTime = 0f;
        }
    }

    /// <summary>
    /// このスペルで生成された弾をすべて削除する。
    /// </summary>
    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
