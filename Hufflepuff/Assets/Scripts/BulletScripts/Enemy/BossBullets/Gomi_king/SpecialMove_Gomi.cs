using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

// ��i�K��-----------------------------------------------------------------------
[System.Serializable]
public class FastSpecialBom
{
    [SerializeField] public GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] public int ShotNum; // �e����ł�
    [SerializeField] public float DelayTime; // �e����łԊu
    [SerializeField] public float speed; // �S�~�[�̑���
}
// ��i�K��-----------------------------------------------------------------------
[System.Serializable]
public class SecondSpecialBom
{
    public GameObject RightBulletPrehab; // �E�����̃n�G
    public GameObject LeftBulletPrehab; // �������̃n�G
    public float delayTime;
    public int BulletNum; // �ł�
    public float time; // ���b��Ɍ�납��n�G���o����
    public float speed; // �e���̑���
    [Range(0, 360)]
    public float angle;

}
// �O�i�K��-----------------------------------------------------------------------
[System.Serializable]
public class ThirdSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float minSpeed;
    [SerializeField] public float delayTime;
 }
// �l�i�K��-----------------------------------------------------------------------
[System.Serializable]
public class FourSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float stopTime; // �~�܂�܂ł̎���
    [SerializeField] public int bulletNum; // �����e���̂܂Ƃ܂������
    [SerializeField] public float circleDelayTime; // �~�`�̒e���ŉ��b�ҋ@���邩
    [SerializeField] public float speed; // �e���̑���
    [SerializeField] public float angleOffset; // �e���̊p�x�����炷���߂̕ϐ�
    [SerializeField] public float crossSpeed; // ������ɒe�𓮂����Ƃ��̑��� 
    [SerializeField] public float expandSpeed; // �g�U�X�s�[�h
    [SerializeField] public float rotationSpeed; // ���b��]�p�x�i�x�j
    [SerializeField] public float arcCount; // �e���̐��i�e���̂܂Ƃ܂�̐��j
    [SerializeField] public float arcAngle; // �e���̂܂Ƃ܂�̊p�x
    [SerializeField] public float arcSpeed; // �e���̂܂Ƃ܂�̑���
    [SerializeField] public float movementSpeed; // �e���̂܂Ƃ܂�̈ړ����x

}
// �ŏI�i�K��---------------------------------------------------------------------
[System.Serializable]
public class FinalSpecianBom
{
    [SerializeField] public GameObject BulletPrehab;
}
// �Z�~�t�@�C�i��-----------------------------------------------------------------
[System.Serializable]
public class SpecialFinalAttack
{
    public GameObject BulletPrehab; // �e���̃v���n�u
    public int bulletNum; // �e���̐�
    public float speed; // �e���̃X�s�[�h
    public float delayTime; // �e���̏o���Ԋu
    public float angleOffset;
 }



public class SpecialMove_Gomi : MonoBehaviour
{
    [Header("�{�X�S�̂��Ǘ�����ϐ�")]
    [SerializeField] private Vector2 spellPos;// �K�E�Z�E�Z�~�t�@�C�i����łƂ��ɂ��̍��W�Ɉ�U�߂�
    [SerializeField] private Boss1Bullet boss1Bullet;

    [Header("��i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private FastSpecialBom fastSpecialBom;
    [Header("��i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private SecondSpecialBom secondSpecialBom;
    [Header("�O�i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private ThirdSpecialBom thirdSpecialBom;
    [Header("�l�i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private FourSpecialBom fourSpecialBom;
    [Header("�ŏI�i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private FinalSpecianBom finalSpecianBom;
    [Header("�Z�~�t�@�C�i���̕ϐ�")]
    [SerializeField] private SpecialFinalAttack specialFinalAttack;
   
    /// <summary>
    /// �ǂ̕K�E�Z��ł��̔�����s���܂�
    /// </summary>
    /// <param name="state">���̃{�X�̏�Ԃł�</param>
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
    /// ��i�K�ڂ̕K�E�Z
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
    /// ��i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator SecondSpecialBullet()
    {
        // ���ˏ�ɒe���𐶐�����
        // ���b��Ƀv���C���[�̔��Α����烉���_����x���W�ɐ�������
        // ��L�̒e���͉E�����ɒ����I�ɔ��

        float shotTime = 0;
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.second && boss1Bullet.BulletState == BulletState.spell)
        {
            while (shotTime < secondSpecialBom.delayTime)
            {
                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + secondSpecialBom.angle;
                    float incrementalAngle = shotTime * 10f; // ���Ԍo�߂Ŋp�x��ω�������

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

            // ���Α�����e�����΂��i�n�G�j
            for(int i = 0; i < secondSpecialBom.BulletNum; i++)
            {
                Vector2 randomPos = new Vector2(-9f, Random.Range(-4.5f, 4.5f)); // �������W�̐ݒ�
                GameObject proj = Instantiate(secondSpecialBom.RightBulletPrehab, randomPos, Quaternion.identity); // �e���̐���
                Vector3 moveDirection = new Vector3(-20f, 0, 0).normalized; // �����̐ݒ�
                proj.GetComponent<Rigidbody2D>().linearVelocity = moveDirection * -secondSpecialBom.speed; // ��΂�
                yield return new WaitForSeconds(0.05f);
            }
            shotTime = 0f;

        }
        yield return null;
    }

    /// <summary>
    /// �O�i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThirdSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.third && boss1Bullet.BulletState == BulletState.spell)
        {
            float angle = Random.Range(0f, 360f); // �����_���Ȋp�x�𐶐�
            float speed = Random.Range(thirdSpecialBom.minSpeed, thirdSpecialBom.maxSpeed); // �����_���ȑ��x�𐶐�
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // �����_���ȕ������v�Z

            GameObject bullet = Instantiate(thirdSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = direction * speed; // �e�̑��x��ݒ�
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    /// <summary>
    /// �l�i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator FourSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.four && boss1Bullet.BulletState == BulletState.spell)
        {
            // �e�����~��Ɍ��������
            // �e����ˏ�ɑł悤�ɕϊ�����
            for (int i = 0; i < 3; i++)
            {
                List<GameObject> bullets = new List<GameObject>();
                for (int k = 0; k < 2; k++)
                {
                    float angleStep = 360f / fourSpecialBom.bulletNum;
                    float angle = fourSpecialBom.angleOffset; // �p�x�����炷
                    for (int j = 0; j < fourSpecialBom.bulletNum; j++)
                    {
                        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                        Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                        GameObject proj = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                        rb.linearVelocity = moveDirection.normalized * fourSpecialBom.speed * (k + 1) * 0.7f;

                        bullets.Add(proj); // �e�����X�g�ɒǉ�
                        angle += angleStep; // �p�x�����炷
                    }
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // �~�`�̒e���őҋ@
                // �e�����~����
                foreach (GameObject bullet in bullets)
                {
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = Vector2.zero; // �e�̑��x���[���ɂ���   
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // �~�`�̒e���őҋ@

                StartCoroutine(ExpandMove(bullets)); // �e���g�U������
                yield return null;
            }

            // �����_���ȍ��W�Ɉړ����Ȃ���e���΂�
            Vector2 randomPos = new Vector2(Random.Range(2.0f, 8.5f), Random.Range(-4.5f, 4.5f));
            StartCoroutine(FireSpecialPositionMove(randomPos)); // �����_���ȍ��W�ֈړ�
            for (int i = 0; i < 3; i++)
            {
                // �e������
                List<GameObject> bullets = new List<GameObject>();
                // ���ɒe���𐶐�����
                float startAngle = 180f - fourSpecialBom.arcAngle / 2f; // ��̊J�n�p�x
                float angleStep = fourSpecialBom.arcAngle / (fourSpecialBom.arcCount - 1);

                for(int j = 0; j < fourSpecialBom.arcCount; j++)
                {
                    float angle = startAngle + j * angleStep; // �e���̊p�x���v�Z
                    float rad = angle * Mathf.Deg2Rad; // ���W�A���ɕϊ�   
                    Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // �����x�N�g�����v�Z

                    GameObject bullet = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * fourSpecialBom.arcSpeed; // �e�̑��x��ݒ�
                    bullets.Add(bullet); // �e�����X�g�ɒǉ�
                }
                StartCoroutine(BulletMover(bullets));
                yield return new WaitForSeconds(0.7f);
            }
            yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        }
    }

    /// <summary>
    /// �e���g�U�����ē������R���[�`��
    /// </summary>
    /// <param name="bullets">�e���̃��X�g</param>
    /// <returns></returns>
    private IEnumerator ExpandMove(List<GameObject> bullets)
    {
        while (true)
        {
            float angleDelta = fourSpecialBom.rotationSpeed * Time.deltaTime;

            for (int j = 0; j < bullets.Count; j++)
            {
                GameObject bullet = bullets[j];

                if (bullet == null) continue; // �e�����݂��Ȃ��ꍇ�̓X�L�b�v
                if (j % 2 == 0)
                {
                    // �e�̈ʒu�x�N�g�����擾
                    Vector3 dir = bullet.transform.position - transform.position;

                    // ���݂̊p�x + �ǉ��p�x�ŉ�]
                    dir = Quaternion.Euler(0, 0, angleDelta) * dir;

                    // �g�U
                    dir += dir.normalized * fourSpecialBom.expandSpeed * Time.deltaTime;

                    bullet.transform.position = transform.position + dir;
                }
                else
                {
                    // �e�̈ʒu�x�N�g�����擾
                    Vector3 dir = bullet.transform.position - transform.position;

                    // ���݂̊p�x + �ǉ��p�x�ŉ�]
                    dir = Quaternion.Euler(0, 0, -angleDelta) * dir;

                    // �g�U
                    dir += dir.normalized * fourSpecialBom.expandSpeed * Time.deltaTime;

                    bullet.transform.position = transform.position + dir;
                }

            }
            yield return null;
        }
    }

    /// <summary>
    /// �e�������@�_���ɔ�΂��R���[�`���R
    /// </summary>
    /// <param name="bullets"></param>
    /// <returns></returns>
    private IEnumerator BulletMover(List<GameObject> bullets)
    {
        // ���b��~��Ԃɂ���
        yield return new WaitForSeconds(1f);
        foreach (GameObject bullet in bullets)
        {
            if (bullet != null)
            {
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.zero; // �e�̑��x���[���ɂ���
            }
        }
        yield return new WaitForSeconds(1f); // ���b�ԑҋ@
                                                                         // ���b��Ɏ��@�_���Œe���΂�
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets != null)
            {
                Vector3 direction = (GameObject.Find("Player").transform.position - bullets[i].transform.position).normalized;
                bullets[i].GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // �e�̑�����ݒ�
                yield return new WaitForSeconds(0.01f); // �e���΂��Ԋu
            }
        }
        yield return null;
    }

    /// <summary>
    /// �ŏI�i�K�ڂ̕K�E�Z
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
    /// �Z�~�t�@�C�i��
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
            specialFinalAttack.angleOffset += 10f; // ������ς���Ή�]���x���ς��
            if (specialFinalAttack.angleOffset >= 360) specialFinalAttack.angleOffset -= 360f; // �͈͓���ۂ�
            yield return new WaitForSeconds(specialFinalAttack.delayTime);

        }
        yield return null;
    }

    /// <summary>
    /// �K�E�Z��łƂ��̈ړ������܂�
    /// </summary>
    /// <returns>������I�������܂�</returns>
    private IEnumerator FireSpecialPositionMove(Vector2 targetPos)
    {
        boss1Bullet.DamageLate = 0f;
        float limitTime = 1.5f; // �ړ��ɂ����鎞��
        float elapsedTime = 0f; // �ړ��ɂ�����������
        Vector2 startPosition = transform.position;
        // randomPos��limitTime�����Ĉړ�����
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
