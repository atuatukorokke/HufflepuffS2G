// ========================================
//
// FourSpecialPattern.cs
//
// ========================================
//
// l’iŠK–Ú‚Ì•KE‹ZiƒXƒyƒ‹ƒJ[ƒhjƒpƒ^[ƒ“B
// E‰~Œ`’e‚Ì‘½’i¶¬ ¨ ’â~ ¨ ŠgU‰ñ“] ¨ •úË’e ¨ —U“±’e ¨ ‹x~
// E•¡”ƒtƒF[ƒY‚ğ˜A‘±‚Ås‚¤‚“ï“xƒXƒyƒ‹
// EISpellPattern ‚É€‹’
//
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FourSpecialBom
{
    public GameObject BulletPrehab;     // ’e‚ÌƒvƒŒƒnƒu
    public float stopTime;              // ’e‚ğ’â~‚³‚¹‚éŠÔ
    public int bulletNum;               // ‰~Œ`‚É¶¬‚·‚é’e‚Ì”
    public float circleDelayTime;       // ‰~Œ`¶¬‚ÌŠÔŠu
    public float speed;                 // ’e‚Ì‘¬“x
    public float angleOffset;           // ”­ËŠp“x‚Ì‚¸‚ç‚µ
    public float crossSpeed;            // ƒNƒƒX’e‚Ì‘¬“x
    public float expandSpeed;           // ŠgU‘¬“x
    public float rotationSpeed;         // ‰ñ“]‘¬“x
    public float arcCount;              // •úË’e‚Ì’i”
    public float arcAngle;              // •úË’e‚ÌŠp“x”ÍˆÍ
    public float arcSpeed;              // •úË’e‚Ì‘¬“x
    public float movementSpeed;         // —U“±’e‚ÌˆÚ“®‘¬“x
    public int arcLine;                 // •úË’e‚Ìƒ‰ƒCƒ“”
    public AudioClip mainBulletSE;      // ƒƒCƒ“’e‚ÌŒø‰Ê‰¹
    public AudioClip subBulletSE;       // •úË’e‚ÌŒø‰Ê‰¹
    public AudioClip movementSE;        // —U“±’e‚ÌŒø‰Ê‰¹
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

    /// <summary>
    /// l’iŠK–ÚƒXƒyƒ‹‚ÌƒƒCƒ“ˆ—B
    /// –³“GˆÚ“® ¨ ‰~Œ`’e ¨ ’â~ ¨ ŠgU‰ñ“] ¨ •úË’e ¨ —U“±’e ¨ ‹x~ ‚Ìƒ‹[ƒvB
    /// </summary>
    public IEnumerator Execute()
    {
        // –³“Gó‘Ô‚ÅƒXƒyƒ‹ˆÊ’u‚ÖˆÚ“®
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        // l’iŠK–Ú‚©‚Â spell ó‘Ô‚ÌŠÔ‚ÍƒXƒyƒ‹‚ğ‘±‚¯‚é
        while (owner.State == State.four && owner.BulletState == BulletState.spell)
        {
            // -------------------------------
            // ‰~Œ`’e ¨ ’â~ ¨ ŠgU‰ñ“]ƒtƒF[ƒY
            // -------------------------------
            for (int i = 0; i < 3; i++)
            {
                List<GameObject> bullets = new();
                owner.Audio.PlayOneShot(config.mainBulletSE);

                // ‰~Œ`’e‚ğ2’iŠK‚Å¶¬i‘¬“xˆá‚¢j
                for (int k = 0; k < 2; k++)
                {
                    if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

                    float angleStep = 360f / config.bulletNum;
                    float angle = config.angleOffset;

                    // ‰~Œ`‚É’e‚ğ¶¬
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

                // ’e‚ğ’â~‚³‚¹‚é
                yield return new WaitForSeconds(config.stopTime);

                if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

                foreach (var b in bullets)
                {
                    if (b == null) continue;
                    b.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
                }

                // ’â~ŒãAŠgU‰ñ“]‚ğŠJn
                yield return new WaitForSeconds(config.stopTime);
                CoroutineRunner.Start(ExpandMove(bullets));
            }

            if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

            // -------------------------------
            // ƒ{ƒXˆÚ“®ƒtƒF[ƒY
            // -------------------------------
            Vector2 randomPos = new(Random.Range(2f, 8.5f), Random.Range(-4.5f, 4.5f));
            CoroutineRunner.Start(PositionMove(randomPos));

            // -------------------------------
            // •úË’eƒtƒF[ƒY
            // -------------------------------
            for (int i = 0; i < 3; i++)
            {
                if (owner.State != State.four || owner.BulletState != BulletState.spell) break;

                List<GameObject> bullets = new();

                float startAngle = 180f - config.arcAngle / 2f;
                float angleStep = config.arcAngle / (config.arcCount - 1);

                // •úË’e‚ğ arcCount ~ arcLine ¶¬
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

                // —U“±’eƒtƒF[ƒY‚ÖˆÚs
                CoroutineRunner.Start(BulletMover(bullets));
                yield return new WaitForSeconds(0.7f);
            }

            // -------------------------------
            // ƒXƒyƒ‹ˆÊ’u‚Ö–ß‚é
            // -------------------------------
            yield return PositionMove(spellPos);
        }
    }

    /// <summary>
    /// ’â~‚µ‚½’e‚ğ‰ñ“]‚³‚¹‚È‚ª‚çŠgU‚³‚¹‚éB
    /// </summary>
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

                // ‹ô”’e‚ÆŠï”’e‚Å‰ñ“]•ûŒü‚ğ•Ï‚¦‚é
                dir = Quaternion.Euler(0, 0, (i % 2 == 0 ? delta : -delta)) * dir;

                // ŠgU
                dir += dir.normalized * config.expandSpeed * Time.deltaTime;

                b.transform.position = boss.position + dir;
            }

            yield return null;
        }
    }

    /// <summary>
    /// •úË’e‚ğ’â~ ¨ —U“±’e‚Æ‚µ‚ÄƒvƒŒƒCƒ„[‚ÖŒü‚¯‚Ä”­ËB
    /// </summary>
    private IEnumerator BulletMover(List<GameObject> bullets)
    {
        // ’â~
        yield return new WaitForSeconds(1f);

        foreach (var b in bullets)
        {
            if (b == null) continue;
            b.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        // —U“±ŠJn
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

    /// <summary>
    /// ƒ{ƒX‚ğw’èˆÊ’u‚ÖˆÚ“®‚³‚¹‚éB
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
    /// ‚±‚Ìƒpƒ^[ƒ“‚Å¶¬‚³‚ê‚½’e‚ğ‚·‚×‚Äíœ‚·‚éB
    /// </summary>
    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
