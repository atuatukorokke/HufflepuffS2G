using System.Collections;
using UnityEngine;

[System.Serializable]
public class FastSpecialBom
{
    public GameObject BulletPrehab;     // ’e–‹‚ÌƒvƒŒƒnƒu
    public int ShotNum;                 // ’e–‹‚ğ‘Å‚Â‰ñ”
    public float bulletDelayTime;       // ’e–‹‚ğ‘Å‚ÂŠÔŠu
    public float speed;                 // ƒSƒ~[‚Ì‘¬‚³
    public float delayTime;             // ’e–‹‚ğ‘Å‚Â‚Ü‚Å‚Ì‘Ò‹@ŠÔ
    public AudioClip bulletSE;          // ’e–‹‚ğŒ‚‚Â‚Æ‚«‚ÌŒø‰Ê‰¹
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

    public void Initialize() { }

    public IEnumerator Execute()
    {
        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        while (owner.State == State.fast && owner.BulletState == BulletState.spell)
        {
            for (int i = 0; i < config.ShotNum; i++)
            {
                owner.Audio.PlayOneShot(config.bulletSE);

                float dirX = Random.Range(-8.5f, 1.5f) - boss.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - boss.position.y;

                Vector3 dir = new(dirX, dirY, 0);

                GameObject bullet = GameObject.Instantiate(
                    config.BulletPrehab,
                    boss.position,
                    Quaternion.identity
                );

                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir.normalized * config.speed;

                yield return new WaitForSeconds(config.bulletDelayTime);
            }

            yield return new WaitForSeconds(config.delayTime);
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
