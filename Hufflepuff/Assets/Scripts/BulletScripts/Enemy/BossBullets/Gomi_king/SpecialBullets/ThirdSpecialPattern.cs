using System.Collections;
using UnityEngine;

[System.Serializable]
public class ThirdSpecialBom
{
    public GameObject BulletPrehab;    // 弾幕のプレハブ
    public float maxSpeed;             // ランダムな弾幕の最大速さ
    public float minSpeed;             // ランダムな弾幕の最小速さ
    public float delayTime;            // 弾幕を打つまでの待機時間
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

    public IEnumerator Execute()
    {
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        while (owner.State == State.third && owner.BulletState == BulletState.spell)
        {
            float angle = Random.Range(0f, 360f);
            float speed = Random.Range(config.minSpeed, config.maxSpeed);

            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            GameObject bullet = GameObject.Instantiate(
                config.BulletPrehab,
                boss.position,
                Quaternion.identity
            );

            bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * speed;

            yield return new WaitForSeconds(0.01f);
        }
    }

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

    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
