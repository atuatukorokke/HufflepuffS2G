using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpecialFinalAttack
{
    public GameObject BulletPrehab;     // ’e–‹‚ÌƒvƒŒƒnƒu
    public int bulletNum;               // ’e–‹‚Ì”
    public float speed;                 // ’e–‹‚ÌƒXƒs[ƒh
    public float delayTime;             // ’e–‹‚Ìo‚·ŠÔŠu
    public float angleOffset;           // ’e–‹‚ÌŠp“x‚ğ‚¸‚ç‚·‚½‚ß‚Ì•Ï”
}

public class SemiFinalPattern : ISpellPattern
{
    private SpecialFinalAttack config;
    private Transform boss;
    private Boss1Bullet owner;

    public SemiFinalPattern(SpecialFinalAttack config, Transform boss, Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.owner = owner;
    }

    public void Initialize() { }

    public IEnumerator Execute()
    {
        float angleStep = 360f / config.bulletNum;
        float angle = config.angleOffset;

        while (owner.State == State.final && owner.BulletState == BulletState.special)
        {
            for (int i = 0; i < config.bulletNum; i++)
            {
                float rad = angle * Mathf.Deg2Rad;

                Vector2 dir = new(Mathf.Cos(rad), Mathf.Sin(rad));

                GameObject bullet = GameObject.Instantiate(
                    config.BulletPrehab,
                    boss.position,
                    Quaternion.identity
                );

                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * config.speed;

                angle += angleStep;
            }

            yield return new WaitForSeconds(config.delayTime);
        }
    }

    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
