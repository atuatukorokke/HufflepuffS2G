using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// ˆê’iŠK–Ú-----------------------------------------------------------------------
[System.Serializable]
public class FastSpecialBom
{
    [SerializeField] public GameObject BulletPrehab; // ’e–‹‚ÌƒvƒŒƒnƒu
    [SerializeField] public int ShotNum; // ’e–‹‚ğ‘Å‚Â‰ñ”
    [SerializeField] public float DelayTime; // ’e–‹‚ğ‘Å‚ÂŠÔŠu
    [SerializeField] public float speed; // ƒSƒ~[‚Ì‘¬‚³
}
// “ñ’iŠK–Ú-----------------------------------------------------------------------
[System.Serializable]
public class SecondSpecialBom
{
    public GameObject RightBulletPrehab; // ‰EŒü‚«‚ÌƒnƒG
    public GameObject LeftBulletPrehab; // ¶Œü‚«‚ÌƒnƒG
    public float delayTime;
    public int BulletNum; // ‘Å‚Â”
    public float time; // ‰½•bŒã‚ÉŒã‚ë‚©‚çƒnƒG‚ğo‚·‚©
    public float speed; // ’e–‹‚Ì‘¬‚³
    [Range(0, 360)]
    public float angle;

}
// O’iŠK–Ú-----------------------------------------------------------------------
[System.Serializable]
public class ThirdSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float minSpeed;
    [SerializeField] public float delayTime;
 }
// l’iŠK–Ú-----------------------------------------------------------------------
[System.Serializable]
public class FourSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float stopTime; // ~‚Ü‚é‚Ü‚Å‚ÌŠÔ
    [SerializeField] public int bulletNum; // ‰½”­’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚ğŒ‚‚Â‚©
    [SerializeField] public float circleDelayTime; // ‰~Œ`‚Ì’e–‹‚Å‰½•b‘Ò‹@‚·‚é‚©
    [SerializeField] public float speed; // ’e–‹‚Ì‘¬‚³
    [SerializeField] public float angleOffset; // ’e–‹‚ÌŠp“x‚ğ‚¸‚ç‚·‚½‚ß‚Ì•Ï”
    [SerializeField] public float crossSpeed; // Œğ·ã‚É’e‚ğ“®‚©‚·‚Æ‚«‚Ì‘¬‚³ 
    [SerializeField] public float expandSpeed; // ŠgUƒXƒs[ƒh
    [SerializeField] public float rotationSpeed; // –ˆ•b‰ñ“]Šp“xi“xj
    [SerializeField] public float arcCount; // ’e–‹‚Ì”i’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚Ì”j
    [SerializeField] public float arcAngle; // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚ÌŠp“x
    [SerializeField] public float arcSpeed; // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚Ì‘¬‚³
    [SerializeField] public float movementSpeed; // ’e–‹‚Ì‚Ü‚Æ‚Ü‚è‚ÌˆÚ“®‘¬“x

}
// ÅI’iŠK–Ú---------------------------------------------------------------------
[System.Serializable]
public class FinalSpecianBom
{
    [SerializeField] public GameObject BulletPrehab;
}
// ƒZƒ~ƒtƒ@ƒCƒiƒ‹-----------------------------------------------------------------
[System.Serializable]
public class SpecialFinalAttack
{
    public GameObject BulletPrehab; // ’e–‹‚ÌƒvƒŒƒnƒu
    public int bulletNum; // ’e–‹‚Ì”
    public float speed; // ’e–‹‚ÌƒXƒs[ƒh
    public float delayTime; // ’e–‹‚Ìo‚·ŠÔŠu
    public float angleOffset;
 }



public class SpecialMove_Gomi : MonoBehaviour
{
    [Header("ƒ{ƒX‘S‘Ì‚ğŠÇ—‚·‚é•Ï”")]
    [SerializeField] private Vector2 spellPos;// •KE‹ZEƒZƒ~ƒtƒ@ƒCƒiƒ‹‚ğ‘Å‚Â‚Æ‚«‚É‚±‚ÌÀ•W‚Éˆê’U–ß‚é
    [SerializeField] private Boss1Bullet boss1Bullet;

    [Header("ˆê’iŠK–Ú‚Ì•KE‹Z‚Ì•Ï”")]
    [SerializeField] private FastSpecialBom fastSpecialBom;
    [Header("“ñ’iŠK–Ú‚Ì•KE‹Z‚Ì•Ï”")]
    [SerializeField] private SecondSpecialBom secondSpecialBom;
    [Header("O’iŠK–Ú‚Ì•KE‹Z‚Ì•Ï”")]
    [SerializeField] private ThirdSpecialBom thirdSpecialBom;
    [Header("l’iŠK–Ú‚Ì•KE‹Z‚Ì•Ï”")]
    [SerializeField] private FourSpecialBom fourSpecialBom;
    [Header("ÅI’iŠK–Ú‚Ì•KE‹Z‚Ì•Ï”")]
    [SerializeField] private FinalSpecianBom finalSpecianBom;
    [Header("ƒZƒ~ƒtƒ@ƒCƒiƒ‹‚Ì•Ï”")]
    [SerializeField] private SpecialFinalAttack specialFinalAttack;
   
    /// <summary>
    /// ‚Ç‚Ì•KE‹Z‚ğ‘Å‚Â‚©‚Ì”»’è‚ğs‚¢‚Ü‚·
    /// </summary>
    /// <param name="state">¡‚Ìƒ{ƒX‚Ìó‘Ô‚Å‚·</param>
    public void BomJudgement(State state)
    {
        switch(state)
        {
            case State.fast:
                StartCoroutine(FastSpecialBullet());
                break;
            case State.second:
                StartCoroutine(SecondSpecialBullet());
                break;
            case State.third:
                StartCoroutine(ThirdSpecialBullet());
                break;
            case State.four:
                StartCoroutine(FourSpecialBullet());
                break;
            case State.final:
                StartCoroutine(FinalSpecialBullet());
                break;

        }
    }

    /// <summary>
    /// ˆê’iŠK–Ú‚Ì•KE‹Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator FastSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while(boss1Bullet.State == State.fast && boss1Bullet.BulletState == BulletState.spell)
        {
            for(int i = 0; i < fastSpecialBom.ShotNum; i++)
            {
                float dirX = Random.Range(1.5f, -8.5f) - transform.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - transform.position.y;
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);
                GameObject gomi = Instantiate(fastSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = gomi.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * fastSpecialBom.speed;
                yield return new WaitForSeconds(fastSpecialBom.DelayTime);
            }
            yield return new WaitForSeconds(2.5f);
        }
        yield return null;
    }

    /// <summary>
    /// “ñ’iŠK–Ú‚Ì•KE‹Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator SecondSpecialBullet()
    {
        // •úËó‚É’e–‹‚ğ¶¬‚·‚é
        // ”•bŒã‚ÉƒvƒŒƒCƒ„[‚Ì”½‘Î‘¤‚©‚çƒ‰ƒ“ƒ_ƒ€‚ÈxÀ•W‚É¶¬‚·‚é
        // ã‹L‚Ì’e–‹‚Í‰E•ûŒü‚É’¼ü“I‚É”ò‚Ô

        float shotTime = 0;
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.second && boss1Bullet.BulletState == BulletState.spell)
        {
            while (shotTime < secondSpecialBom.delayTime)
            {
                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + secondSpecialBom.angle;
                    float incrementalAngle = shotTime * 10f; // ŠÔŒo‰ß‚ÅŠp“x‚ğ•Ï‰»‚³‚¹‚é

                    float rad = (baseAngle + incrementalAngle) * Mathf.Deg2Rad;

                    float dirX = Mathf.Cos(rad);
                    float dirY = Mathf.Sin(rad);

                    Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;

                    GameObject proj = Instantiate(secondSpecialBom.LeftBulletPrehab, transform.position, Quaternion.identity);

                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection * -secondSpecialBom.speed;


                }
                shotTime += 0.07f;
                yield return new WaitForSeconds(0.07f);
            }

            // ”½‘Î‘¤‚©‚ç’e–‹‚ğ”ò‚Î‚·iƒnƒGj
            for(int i = 0; i < secondSpecialBom.BulletNum; i++)
            {
                Vector2 randomPos = new Vector2(-9f, Random.Range(-4.5f, 4.5f)); // ¶¬À•W‚Ìİ’è
                GameObject proj = Instantiate(secondSpecialBom.RightBulletPrehab, randomPos, Quaternion.identity); // ’e–‹‚Ì¶¬
                Vector3 moveDirection = new Vector3(-20f, 0, 0).normalized; // •ûŒü‚Ìİ’è
                proj.GetComponent<Rigidbody2D>().linearVelocity = moveDirection * -secondSpecialBom.speed; // ”ò‚Î‚·
                yield return new WaitForSeconds(0.05f);
            }
            shotTime = 0f;

        }
        yield return null;
    }

    /// <summary>
    /// O’iŠK–Ú‚Ì•KE‹Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThirdSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.third && boss1Bullet.BulletState == BulletState.spell)
        {
            float angle = Random.Range(0f, 360f); // ƒ‰ƒ“ƒ_ƒ€‚ÈŠp“x‚ğ¶¬
            float speed = Random.Range(thirdSpecialBom.minSpeed, thirdSpecialBom.maxSpeed); // ƒ‰ƒ“ƒ_ƒ€‚È‘¬“x‚ğ¶¬
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // ƒ‰ƒ“ƒ_ƒ€‚È•ûŒü‚ğŒvZ

            GameObject bullet = Instantiate(thirdSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * speed; // ’e‚Ì‘¬“x‚ğİ’è
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// l’iŠK–Ú‚Ì•KE‹Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator FourSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.four && boss1Bullet.BulletState == BulletState.spell)
        {
            // ’e–‹‚ğ‰~ó‚ÉŒ‚‚Á‚½Œã‚É
            // ’e‚ğ•úËó‚É‘Å‚Â‚æ‚¤‚É•ÏŠ·‚·‚é
            for (int i = 0; i < 3; i++)
            {
                List<GameObject> bullets = new List<GameObject>();
                for (int k = 0; k < 2; k++)
                {
                    float angleStep = 360f / fourSpecialBom.bulletNum;
                    float angle = fourSpecialBom.angleOffset; // Šp“x‚ğ‚¸‚ç‚·
                    for (int j = 0; j < fourSpecialBom.bulletNum; j++)
                    {
                        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                        Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                        GameObject proj = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                        rb.linearVelocity = moveDirection.normalized * fourSpecialBom.speed * (k + 1) * 0.7f;

                        bullets.Add(proj); // ’e‚ğƒŠƒXƒg‚É’Ç‰Á
                        angle += angleStep; // Šp“x‚ğ‚¸‚ç‚·
                    }
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // ‰~Œ`‚Ì’e–‹‚Å‘Ò‹@
                // ’e–‹‚ğ’â~‚·‚é
                foreach (GameObject bullet in bullets)
                {
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = Vector2.zero; // ’e‚Ì‘¬“x‚ğƒ[ƒ‚É‚·‚é   
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // ‰~Œ`‚Ì’e–‹‚Å‘Ò‹@

                StartCoroutine(ExpandMove(bullets)); // ’e‚ğŠgU‚³‚¹‚é
                yield return null;
            }

            // ƒ‰ƒ“ƒ_ƒ€‚ÈÀ•W‚ÉˆÚ“®‚µ‚È‚ª‚ç’e‚ğ”ò‚Î‚·
            Vector2 randomPos = new Vector2(Random.Range(2.0f, 8.5f), Random.Range(-4.5f, 4.5f));
            StartCoroutine(FireSpecialPositionMove(randomPos)); // ƒ‰ƒ“ƒ_ƒ€‚ÈÀ•W‚ÖˆÚ“®
            for (int i = 0; i < 3; i++)
            {
                // ’e–‹¶¬
                List<GameObject> bullets = new List<GameObject>();
                // îó‚É’e–‹‚ğ¶¬‚µ‚Ä
                float startAngle = 180f - fourSpecialBom.arcAngle / 2f; // î‚ÌŠJnŠp“x
                float angleStep = fourSpecialBom.arcAngle / (fourSpecialBom.arcCount - 1);

                for(int j = 0; j < fourSpecialBom.arcCount; j++)
                {
                    float angle = startAngle + j * angleStep; // ’e–‹‚ÌŠp“x‚ğŒvZ
                    float rad = angle * Mathf.Deg2Rad; // ƒ‰ƒWƒAƒ“‚É•ÏŠ·   
                    Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // •ûŒüƒxƒNƒgƒ‹‚ğŒvZ

                    GameObject bullet = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * fourSpecialBom.arcSpeed; // ’e‚Ì‘¬“x‚ğİ’è
                    bullets.Add(bullet); // ’e‚ğƒŠƒXƒg‚É’Ç‰Á
                }
                StartCoroutine(BulletMover(bullets));
                yield return new WaitForSeconds(0.7f);
            }
            yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        }
    }

    /// <summary>
    /// ’e‚ğŠgU‚³‚¹‚Ä“®‚©‚·ƒRƒ‹[ƒ`ƒ“
    /// </summary>
    /// <param name="bullets">’e–‹‚ÌƒŠƒXƒg</param>
    /// <returns></returns>
    private IEnumerator ExpandMove(List<GameObject> bullets)
    {
        while (true)
        {
            float angleDelta = fourSpecialBom.rotationSpeed * Time.deltaTime;

            for (int j = 0; j < bullets.Count; j++)
            {
                GameObject bullet = bullets[j];

                if (bullet == null) continue; // ’e‚ª‘¶İ‚µ‚È‚¢ê‡‚ÍƒXƒLƒbƒv
                if (j % 2 == 0)
                {
                    // ’e‚ÌˆÊ’uƒxƒNƒgƒ‹‚ğæ“¾
                    Vector3 dir = bullet.transform.position - transform.position;

                    // Œ»İ‚ÌŠp“x + ’Ç‰ÁŠp“x‚Å‰ñ“]
                    dir = Quaternion.Euler(0, 0, angleDelta) * dir;

                    // ŠgU
                    dir += dir.normalized * fourSpecialBom.expandSpeed * Time.deltaTime;

                    bullet.transform.position = transform.position + dir;
                }
                else
                {
                    // ’e‚ÌˆÊ’uƒxƒNƒgƒ‹‚ğæ“¾
                    Vector3 dir = bullet.transform.position - transform.position;

                    // Œ»İ‚ÌŠp“x + ’Ç‰ÁŠp“x‚Å‰ñ“]
                    dir = Quaternion.Euler(0, 0, -angleDelta) * dir;

                    // ŠgU
                    dir += dir.normalized * fourSpecialBom.expandSpeed * Time.deltaTime;

                    bullet.transform.position = transform.position + dir;
                }

            }
            yield return null;
        }
    }

    /// <summary>
    /// ’e–‹‚ğ©‹@‘_‚¢‚É”ò‚Î‚·ƒRƒ‹[ƒ`ƒ“ƒR
    /// </summary>
    /// <param name="bullets"></param>
    /// <returns></returns>
    private IEnumerator BulletMover(List<GameObject> bullets)
    {
        // ”•b’â~ó‘Ô‚É‚·‚é
        yield return new WaitForSeconds(1f);
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null)
            {
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.zero; // ’e‚Ì‘¬“x‚ğƒ[ƒ‚É‚·‚é
            }
        }
        yield return new WaitForSeconds(1f); // ”•bŠÔ‘Ò‹@
                                                                         // ”•bŒã‚É©‹@‘_‚¢‚Å’e‚ğ”ò‚Î‚·
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets != null)
            {
                Vector3 direction = (GameObject.Find("Player").transform.position - bullets[i].transform.position).normalized;
                bullets[i].GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // ’e‚Ì‘¬‚³‚ğİ’è
                yield return new WaitForSeconds(0.01f); // ’e‚ğ”ò‚Î‚·ŠÔŠu
            }
        }
        yield return null;
    }

    /// <summary>
    /// ÅI’iŠK–Ú‚Ì•KE‹Z
    /// </summary>
    /// <returns></returns>
    public IEnumerator FinalSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        while(boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.spell) 
        {
            yield return new WaitForSeconds(20f);
        }
        yield return null;  
    }

    /// <summary>
    /// ƒZƒ~ƒtƒ@ƒCƒiƒ‹
    /// </summary>
    /// <returns></returns>
    public IEnumerator FinalSpecialAttack()
    {
        float angleStep = 360 / specialFinalAttack.bulletNum;
        float angle = specialFinalAttack.angleOffset;
        while (boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.spell)
        {
            Vector3 randomPos = new Vector3(Random.Range(-8.4f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
            Debug.Log(randomPos);
            for (int i = 0; i < specialFinalAttack.bulletNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = Instantiate(specialFinalAttack.BulletPrehab, randomPos, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * specialFinalAttack.speed;

                angle += angleStep;
            }
            specialFinalAttack.angleOffset += 10f; // ‚±‚±‚ğ•Ï‚¦‚ê‚Î‰ñ“]‘¬“x‚ª•Ï‚í‚é
            if (specialFinalAttack.angleOffset >= 360) specialFinalAttack.angleOffset -= 360f; // ”ÍˆÍ“à‚ğ•Û‚Â
            yield return new WaitForSeconds(specialFinalAttack.delayTime);

        }
        yield return null;
    }

    /// <summary>
    /// •KE‹Z‚ğ‘Å‚Â‚Æ‚«‚ÌˆÚ“®‚ğ‚µ‚Ü‚·
    /// </summary>
    /// <returns>“®ì‚ğI—¹‚³‚¹‚Ü‚·</returns>
    private IEnumerator FireSpecialPositionMove(Vector2 targetPos)
    {
        boss1Bullet.DamageLate = 0f;
        float limitTime = 1.5f; // ˆÚ“®‚É‚©‚¯‚éŠÔ
        float elapsedTime = 0f; // ˆÚ“®‚É‚©‚©‚Á‚½ŠÔ
        Vector2 startPosition = transform.position;
        // randomPos‚ÉlimitTime‚©‚¯‚ÄˆÚ“®‚·‚é
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetPos.x, elapsedTime / limitTime),
                Mathf.Lerp(startPosition.y, targetPos.y, elapsedTime / limitTime)
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        boss1Bullet.DamageLate = 0.2f;
        yield return null;
    }
}
