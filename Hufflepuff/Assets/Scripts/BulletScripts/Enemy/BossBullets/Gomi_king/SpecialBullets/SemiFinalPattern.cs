using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.ComponentModel.Design.Serialization;

[System.Serializable]
public class SpecialFinalAttack
{
    public GameObject BulletPrehab;     // ’e–‹‚ÌƒvƒŒƒnƒu
    public int bulletNum;               // ’e–‹‚Ì”
    public float speed;                 // ’e–‹‚ÌƒXƒs[ƒh
    public float delayTime;             // ’e–‹‚Ìo‚·ŠÔŠu
    public float angleOffset;           // ’e–‹‚ÌŠp“x‚ğ‚¸‚ç‚·‚½‚ß‚Ì•Ï”
    public AudioClip bulletSE;          // ’e–‹‚ğŒ‚‚Â‚Æ‚«‚ÌŒø‰Ê‰¹
}

public class SemiFinalPattern : ISpellPattern
{
    private const float decreasingLate = 0.5f;
    private const float mainDelayTime = 0.2f;
    private const float bulletCreatRadius = 1.5f;

    private SpecialFinalAttack config;
    private Transform boss;
    private Vector2 spellPos;
    private Boss1Bullet owner;

    public SemiFinalPattern(SpecialFinalAttack config, Transform boss, Vector2 spellPos,  Boss1Bullet owner)
    {
        this.config = config;
        this.boss = boss;
        this.spellPos = spellPos;
        this.owner = owner;
    }

    public void Initialize() { }

    public IEnumerator Execute()
    {
        float angleStep = 360f / config.bulletNum;
        float angle = config.angleOffset;

        yield return owner.MoveToSpellPosWithInvincible(boss, spellPos, owner);

        while (owner.State == State.final && owner.BulletState == BulletState.special)
        {
            float radius = bulletCreatRadius;
            Vector2 offset = Random.insideUnitSphere * radius;
            Vector3 randomPos = boss.position + (Vector3)offset;

            List<GameObject> bullets = new List<GameObject>();
            owner.Audio.PlayOneShot(config.bulletSE);
            for(int i = 0; i < config.bulletNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = GameObject.Instantiate(config.BulletPrehab, randomPos, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().linearVelocity = moveDirection.normalized * config.speed;

                bullets.Add(proj);
                angle += angleStep;
            }
            yield return new WaitForSeconds(mainDelayTime);

            // ’e–‹‚ÌƒXƒs[ƒh‚ğ­‚µ’x‚­‚·‚é
            foreach(GameObject obj in bullets)
            {
                if(bullets != null)
                {
                    obj.GetComponent<Rigidbody2D>().linearVelocity *= decreasingLate;
                }
            }
            config.angleOffset += 10;
            if (config.angleOffset >= 360)
                config.angleOffset = 0;

            yield return new WaitForSeconds(config.delayTime);
        }
        yield return null;
    }

    public void Clear()
    {
        foreach (var b in GameObject.FindGameObjectsWithTag("E_Bullet"))
            GameObject.Destroy(b);
    }
}
