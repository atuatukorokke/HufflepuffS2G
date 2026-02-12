// ========================================
//
// ThirdSpecialPattern.cs
//
// ========================================
//
// 三段階目の必殺技（スペルカード）パターン。
// ・無敵移動 → ランダム方向へ高速弾を連射 → ループ
// ・最終スペルほど複雑ではないが、密度の高いランダム弾を継続的にばら撒く攻撃
// ・ISpellPattern に準拠
//
// ========================================

using System.Collections;
using UnityEngine;

[System.Serializable]
public class ThirdSpecialBom
{
    public GameObject BulletPrehab;     // 弾のプレハブ
    public float maxSpeed;              // ランダム弾の最大速度
    public float minSpeed;              // ランダム弾の最小速度
    public float delayTime;             // 次の弾を撃つまでの待機時間
    public AudioClip bulletSE;          // 発射音
}

public class ThirdSpecialPattern : ISpellPattern
{
    private ThirdSpecialBom config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public ThirdSpecialPattern(ThirdSpecialBom config, Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.spellPos = spellPos;
        this.owner = owner;
    }

    public void Initialize() { }

    /// <summary>
    /// 三段階目スペルのメイン処理。
    /// 無敵移動 → ランダム方向へ高速弾連射 → ループ。
    /// </summary>
    public IEnumerator Execute()
    {
        // 無敵状態でスペル開始位置へ移動
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        int creatCount = 0;

        // 三段階目かつ spell 状態の間はスペルを続ける
        while (owner.State == State.third && owner.BulletState == BulletState.spell)
        {
            creatCount++;

            // 10発ごとに追加で効果音を鳴らす
            if (creatCount % 10 == 0)
                owner.Audio.PlayOneShot(config.bulletSE);

            // ランダム方向を決定
            float angle = Random.Range(0f, 360f);
            float speed = Random.Range(config.minSpeed, config.maxSpeed);

            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            // 弾を生成してランダム方向へ発射
            GameObject bullet = GameObject.Instantiate(
                config.BulletPrehab,
                boss.position,
                Quaternion.identity
            );

            bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * speed;

            // 次の弾を撃つまで待機
            yield return new WaitForSeconds(0.01f);
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
