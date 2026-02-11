// ========================================
//
// FastSpecialPattern.cs
//
// ========================================
//
// 一段階目の必殺技（スペルカード）パターン。
// ・無敵移動 → ランダム方向への高速弾連射 → 待機 → 繰り返し
// ・INormalBulletPattern ではなく ISpellPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

[System.Serializable]
public class FastSpecialBom
{
    public GameObject BulletPrehab;     // 弾のプレハブ
    public int ShotNum;                 // 連射する弾の数
    public float bulletDelayTime;       // 弾を撃つ間隔
    public float speed;                 // 弾の速度
    public float delayTime;             // 連射後の待機時間
    public AudioClip bulletSE;          // 発射音
}

public class FastSpecialPattern : ISpellPattern
{
    private FastSpecialBom config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public FastSpecialPattern(FastSpecialBom config, Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.spellPos = spellPos;
        this.owner = owner;
    }

    /// <summary>
    /// 必殺技開始時の初期化処理。
    /// 特に初期化は不要。
    /// </summary>
    public void Initialize() { }

    /// <summary>
    /// 必殺技のメイン処理。
    /// 無敵移動 → ランダム方向への高速弾連射 → 待機 → 繰り返し。
    /// </summary>
    public IEnumerator Execute()
    {
        // 無敵状態でスペル位置へ移動
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        // 一段階目かつ必殺技状態の間は撃ち続ける
        while (owner.State == State.fast && owner.BulletState == BulletState.spell)
        {
            // ShotNum 回、ランダム方向へ弾を撃つ
            for (int i = 0; i < config.ShotNum; i++)
            {
                owner.Audio.PlayOneShot(config.bulletSE);

                // ランダム方向を決定
                float dirX = Random.Range(-8.5f, 1.5f) - boss.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - boss.position.y;
                Vector3 dir = new(dirX, dirY, 0);

                GameObject bullet = GameObject.Instantiate(
                    config.BulletPrehab,
                    boss.position,
                    Quaternion.identity
                );

                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir.normalized * config.speed;

                // 次の弾を撃つまで待機
                yield return new WaitForSeconds(config.bulletDelayTime);
            }

            // 連射後の待機
            yield return new WaitForSeconds(config.delayTime);
        }
    }

    /// <summary>
    /// ボスをスペル位置へ移動させる補助コルーチン（未使用）。
    /// </summary>
    private IEnumerator MoveToSpellPos()
    {
        float t = 0f;
        float duration = 0.5f;
        Vector2 start = boss.position;

        while (t < duration)
        {
            boss.position = Vector2.Lerp(start, spellPos, t / duration);
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
