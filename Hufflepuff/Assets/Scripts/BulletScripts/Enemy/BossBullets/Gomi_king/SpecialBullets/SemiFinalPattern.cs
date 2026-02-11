// ========================================
//
// SemiFinalPattern.cs
//
// ========================================
//
// 最終段階の「セミファイナル」スペルパターン。
// ・無敵移動 → ランダム位置から円形弾生成 → 減速 → 角度変化 → ループ
// ・FinalSpecial よりシンプルだが、密度の高い円形弾を連続生成する攻撃
// ・ISpellPattern に準拠
//
// ========================================

using System.Collections.Generic;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpecialFinalAttack
{
    public GameObject BulletPrehab;     // 弾のプレハブ
    public int bulletNum;               // 一度に生成する弾の数
    public float speed;                 // 弾の初速
    public float delayTime;             // 次の生成までの待機時間
    public float angleOffset;           // 発射角度の初期オフセット
    public AudioClip bulletSE;          // 発射音
}

public class SemiFinalPattern : ISpellPattern
{
    private const float decreasingLate = 0.5f;       // 減速率
    private const float mainDelayTime = 0.2f;        // 弾生成後の待機
    private const float bulletCreatRadius = 1.5f;    // ランダム生成位置の半径

    private SpecialFinalAttack config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public SemiFinalPattern(SpecialFinalAttack config, Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.spellPos = spellPos;
        this.owner = owner;
    }

    public void Initialize() { }

    /// <summary>
    /// セミファイナルスペルのメイン処理。
    /// 無敵移動 → ランダム位置から円形弾生成 → 減速 → 角度変化 → ループ。
    /// </summary>
    public IEnumerator Execute()
    {
        float angleStep = 360f / config.bulletNum;
        float angle = config.angleOffset;

        // 無敵状態でスペル位置へ移動
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        // final段階かつ special 状態の間はスペルを続ける
        while (owner.State == State.final && owner.BulletState == BulletState.special)
        {
            // -------------------------------
            // ランダム位置から円形弾を生成
            // -------------------------------
            float radius = bulletCreatRadius;

            // ランダムな円周上の位置を生成
            Vector2 offset = Random.insideUnitSphere * radius;
            Vector3 randomPos = boss.position + (Vector3)offset;

            List<GameObject> bullets = new List<GameObject>();

            owner.Audio.PlayOneShot(config.bulletSE);

            // 円形に bulletNum 個の弾を生成
            for (int i = 0; i < config.bulletNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = GameObject.Instantiate(
                    config.BulletPrehab,
                    randomPos,
                    Quaternion.identity
                );

                proj.GetComponent<Rigidbody2D>().linearVelocity =
                    moveDirection.normalized * config.speed;

                bullets.Add(proj);
                angle += angleStep;
            }

            // 弾生成後の短い待機
            yield return new WaitForSeconds(mainDelayTime);

            // -------------------------------
            // 弾の速度を減速させる
            // -------------------------------
            foreach (GameObject obj in bullets)
            {
                if (obj != null)
                {
                    obj.GetComponent<Rigidbody2D>().linearVelocity *= decreasingLate;
                }
            }

            // -------------------------------
            // 発射角度をずらして次の弾幕に変化をつける
            // -------------------------------
            config.angleOffset += 10;
            if (config.angleOffset >= 360)
                config.angleOffset = 0;

            // 次の生成まで待機
            yield return new WaitForSeconds(config.delayTime);
        }

        yield return null;
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
