using System.Collections;
using UnityEngine;

[System.Serializable]
public class SecondSpecialBom
{
    public GameObject RightBulletPrehab; // ‰EŒü‚«‚ÌƒnƒG
    public GameObject LeftBulletPrehab;  // ¶Œü‚«‚ÌƒnƒG
    public float delayTime;@            // ‰½•bŠÔ‰ñ“]’e‚ğ‘Å‚Â‚©
    public int BulletNum;                // ‘Å‚Â”
    public float time;                   // ‰½•bŒã‚ÉŒã‚ë‚©‚çƒnƒG‚ğo‚·‚©
    public float speed;                  // ’e–‹‚Ì‘¬‚³
    [Range(0, 360)]
    public float angle;                  // ‰ñ“]’e‚ÌŠp“x’²®—p•Ï”

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

    public IEnumerator Execute()
    {
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        float shotTime = 0f;

        while (owner.State == State.second && owner.BulletState == BulletState.spell)
        {
            while (shotTime < config.delayTime)
            {
                if (owner.BulletState != BulletState.spell) break;

                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + config.angle;
                    float incremental = shotTime * 10f;
                    float rad = (baseAngle + incremental) * Mathf.Deg2Rad;

                    Vector3 dir = new(Mathf.Cos(rad), Mathf.Sin(rad), 0);

                    GameObject bullet = GameObject.Instantiate(
                        config.LeftBulletPrehab,
                        boss.position,
                        Quaternion.identity
                    );

                    bullet.GetComponent<Rigidbody2D>().linearVelocity =
                        dir.normalized * -config.speed;
                }

                shotTime += 0.07f;
                yield return new WaitForSeconds(0.07f);
            }

            if (owner.BulletState != BulletState.spell) break;

            for (int i = 0; i < config.BulletNum; i++)
            {
                Vector2 pos = new(-9f, Random.Range(-4.5f, 4.5f));
                GameObject bullet = GameObject.Instantiate(config.RightBulletPrehab, pos, Quaternion.identity);

                bullet.GetComponent<Rigidbody2D>().linearVelocity =
                    new Vector2(1, 0) * config.speed;

                yield return new WaitForSeconds(0.05f);
            }

            shotTime = 0f;
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
