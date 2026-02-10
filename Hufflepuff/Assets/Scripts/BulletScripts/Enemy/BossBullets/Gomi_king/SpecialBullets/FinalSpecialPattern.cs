using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FinalSpecianBom
{
    public GameObject BulletPrehab;         // ’e–‹‚ÌƒvƒŒƒnƒu
    public float maxSpeed;                  // ƒ‰ƒ“ƒ_ƒ€‚È’e–‹‚ÌÅ‘å‘¬‚³
    public float minSpeed;                  // ƒ‰ƒ“ƒ_ƒ€‚È’e–‹‚ÌÅ¬‘¬‚³
    public float randomSpeed;               // ƒ‰ƒ“ƒ_ƒ€‚È’e–‹‚Ì‘¬‚³
    public float randomBulletTime;          // ƒ‰ƒ“ƒ_ƒ€‚È’e–‹‚ğo‚·ŠÔ
    public int radiationBulletNum;          // •úËó‚Éo‚·’e–‹‚Ì”
    public float radiationBulletSpeed;      // •úËó‚Éo‚·’e–‹‚Ì‘¬‚³
    public float radiationBulletDelayTime;  // •úËó‚Éo‚·’e–‹‚Ìo‚·ŠÔŠu
    public float radiationBulletCount;      // •úËó‚Éo‚·’e–‹‚Ì”i‰½‰ñ•úËó‚Éo‚·‚©j
    public float radiationBulletAngle;      // •úËó‚Éo‚·’e–‹‚ÌŠp“x
    public float breakTime;                 // ’â~‚µ‚½’e–‹‚ğ“®‚©‚µ‚½Œã‚Ì‘Ò‹@ŠÔ
    public Color bulletColor;               // ’e–‹‚ÌF
    public AudioClip mainBulletSE;          // Å‰‚ÉŒ‚‚Â’e–‹‚ÌŒø‰Ê‰¹
    public AudioClip bulletStopSE;          // ’e–‹‚Ì“®‚«‚ğ~‚ß‚é‚Æ‚«‚ÌŒø‰Ê‰¹
    public AudioClip subBulletSE;           // Ÿ‚Ì’e–‹‚ğŒ‚‚Â‚ÌŒø‰Ê‰¹
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

    public IEnumerator Execute()
    {
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        while (owner.State == State.final && owner.BulletState == BulletState.spell)
        {
            float time = 0f;
            int creatCount = 0;

            List<GameObject> bullets = new();

            while (time < config.randomBulletTime)
            {
                if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

                creatCount++;
                if (creatCount % 10 == 0) owner.Audio.PlayOneShot(config.mainBulletSE);

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

            yield return new WaitForSeconds(1f);

            foreach (var b in bullets)
            {
                if (b == null) continue;
                b.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                b.GetComponent<SpriteRenderer>().color = config.bulletColor;
            }
            owner.Audio.PlayOneShot(config.bulletStopSE);

            if (owner.State != State.final || owner.BulletState != BulletState.spell) break;

            Vector2 randomPos = new(Random.Range(1.5f, 8.5f), Random.Range(-4.5f, 4.5f));
            CoroutineRunner.Start(PositionMove(randomPos));

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

            yield return new WaitForSeconds(1f);

            foreach (var b in bullets)
            {
                if (b == null) continue;

                float angle = Random.Range(0f, 360f);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

                b.GetComponent<Rigidbody2D>().linearVelocity = dir * config.randomSpeed;
            }

            yield return new WaitForSeconds(config.breakTime);
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
