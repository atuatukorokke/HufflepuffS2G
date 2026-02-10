using System.Collections;
using UnityEngine;

[System.Serializable]
public class ThirdSpecialBom
{
    public GameObject BulletPrehab;     // ’e–‹‚ÌƒvƒŒƒnƒu
    public float maxSpeed;              // ƒ‰ƒ“ƒ_ƒ€‚È’e–‹‚ÌÅ‘å‘¬‚³
    public float minSpeed;              // ƒ‰ƒ“ƒ_ƒ€‚È’e–‹‚ÌÅ¬‘¬‚³
    public float delayTime;             // ’e–‹‚ğ‘Å‚Â‚Ü‚Å‚Ì‘Ò‹@ŠÔ
    public AudioClip bulletSE;          // ’e–‹‚ğŒ‚‚Â‚Æ‚«‚ÌŒø‰Ê‰¹
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
        int creatCount = 0;

        while (owner.State == State.third && owner.BulletState == BulletState.spell)
        {
            owner.Audio.PlayOneShot(config.bulletSE);

            creatCount++;
            if (creatCount % 10 == 0) owner.Audio.PlayOneShot(config.bulletSE);

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

    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
