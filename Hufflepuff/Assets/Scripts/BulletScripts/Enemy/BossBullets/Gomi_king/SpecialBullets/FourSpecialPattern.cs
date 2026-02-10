using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FourSpecialBom
{
    public GameObject BulletPrehab;     // ’e–‹‚ÌƒvƒŒƒnƒu
    public float stopTime;              // ~‚Ü‚é‚Ü‚Å‚ÌŠÔ
    public int bulletNum;               // ‰½”­’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚ğŒ‚‚Â‚©
    public float circleDelayTime;       // ‰~Œ`‚Ì’e–‹‚Å‰½•b‘Ò‹@‚·‚é‚©
    public float speed;                 // ’e–‹‚Ì‘¬‚³
    public float angleOffset;           // ’e–‹‚ÌŠp“x‚ğ‚¸‚ç‚·‚½‚ß‚Ì•Ï”
    public float crossSpeed;            // Œğ·ã‚É’e‚ğ“®‚©‚·‚Æ‚«‚Ì‘¬‚³ 
    public float expandSpeed;           // ŠgUƒXƒs[ƒh
    public float rotationSpeed;         // –ˆ•b‰ñ“]Šp“xi“xj
    public float arcCount;              // ’e–‹‚Ì”i’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚Ì”j
    public float arcAngle;              // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚ÌŠp“x
    public float arcSpeed;              // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚Ì‘¬‚³
    public float movementSpeed;         // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚ÌˆÚ“®‘¬“x
    public int arcLine;                 // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚Ìƒ‰ƒCƒ“”i’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚Ì”j
    public AudioClip mainBulletSE;      // Å‰‚Ì’e–‹‚ğo‚·‚Æ‚«‚ÌŒø‰Ê‰¹
    public AudioClip subBulletSE;       // Ÿ‚Éo‚·’e–‹‚ÌŒø‰Ê‰¹
    public AudioClip movementSE;        // ’e–‹‚ğ“®‚©‚·‚Æ‚«‚ÌŒø‰Ê‰¹
}

public class FourSpecialPattern : ISpellPattern
{
    private FourSpecialBom config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public FourSpecialPattern(FourSpecialBom config, Transform boss, Vector2 spellPos, Boss1Bullet owner)
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

        while (owner.State == State.four && owner.BulletState == BulletState.spell)
        {
            for (int i = 0; i < 3; i++)
            {
                List<GameObject> bullets = new();
                owner.Audio.PlayOneShot(config.mainBulletSE);

                for (int k = 0; k < 2; k++)
                {
                    if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

                    float angleStep = 360f / config.bulletNum;
                    float angle = config.angleOffset;

                    for (int j = 0; j < config.bulletNum; j++)
                    {
                        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                        float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                        Vector3 dir = new(x, y, 0);

                        GameObject proj = GameObject.Instantiate(
                            config.BulletPrehab,
                            boss.position,
                            Quaternion.identity
                        );

                        proj.GetComponent<Rigidbody2D>().linearVelocity =
                            dir.normalized * config.speed * (k + 1) * 0.7f;

                        bullets.Add(proj);
                        angle += angleStep;
                    }
                }

                yield return new WaitForSeconds(config.stopTime);

                if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

                foreach (var b in bullets)
                {
                    if (b == null) continue;
                    b.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                }

                yield return new WaitForSeconds(config.stopTime);

                CoroutineRunner.Start(ExpandMove(bullets));
            }

            if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

            Vector2 randomPos = new(Random.Range(2f, 8.5f), Random.Range(-4.5f, 4.5f));
            CoroutineRunner.Start(PositionMove(randomPos));

            for (int i = 0; i < 3; i++)
            {
                if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

                List<GameObject> bullets = new();

                float startAngle = 180f - config.arcAngle / 2f;
                float angleStep = config.arcAngle / (config.arcCount - 1);

                for (int j = 0; j < config.arcCount; j++)
                {
                    owner.Audio.PlayOneShot(config.subBulletSE);

                    for (int k = 0; k < config.arcLine; k++)
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
                            dir * (config.arcSpeed - k * 0.1f);

                        bullets.Add(bullet);
                    }

                    yield return new WaitForSeconds(0.01f);
                }

                CoroutineRunner.Start(BulletMover(bullets));
                yield return new WaitForSeconds(0.7f);
            }

            yield return PositionMove(spellPos);
        }
    }

    private IEnumerator ExpandMove(List<GameObject> bullets)
    {
        while (owner.State == State.four && owner.BulletState == BulletState.spell)
        {
            float delta = config.rotationSpeed * Time.deltaTime;

            for (int i = 0; i < bullets.Count; i++)
            {
                var b = bullets[i];
                if (b == null) continue;

                Vector3 dir = b.transform.position - boss.position;

                dir = Quaternion.Euler(0, 0, (i % 2 == 0 ? delta : -delta)) * dir;
                dir += dir.normalized * config.expandSpeed * Time.deltaTime;

                b.transform.position = boss.position + dir;
            }

            yield return null;
        }
    }

    private IEnumerator BulletMover(List<GameObject> bullets)
    {
        yield return new WaitForSeconds(1f);

        foreach (var b in bullets)
        {
            if (b == null) continue;
            b.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(1f);

        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        owner.Audio.PlayOneShot(config.movementSE);

        foreach (var b in bullets)
        {
            if (b == null) continue;

            Vector3 dir = (player.position - b.transform.position).normalized;
            b.GetComponent<Rigidbody2D>().linearVelocity = dir * 10f;

            yield return new WaitForSeconds(0.01f);
        }
    }

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

    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
