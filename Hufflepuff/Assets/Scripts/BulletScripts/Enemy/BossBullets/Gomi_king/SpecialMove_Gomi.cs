using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��i�K��-----------------------------------------------------------------------
[System.Serializable]
public class FastSpecialBom
{
    [SerializeField] public GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] public int ShotNum; // �e����ł�
    [SerializeField] public float DelayTime; // �e����łԊu
    [SerializeField] public float speed; // �S�~�[�̑���
    [SerializeField] public float delayTime; // �e����ł܂ł̑ҋ@����
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
    [SerializeField] public int arcLine; // �e���̂܂Ƃ܂�̃��C�����i�e���̂܂Ƃ܂�̐��j

}
// �ŏI�i�K��---------------------------------------------------------------------
[System.Serializable]
public class FinalSpecianBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float maxSpeed; // �����_���Ȓe���̍ő呬��
    [SerializeField] public float minSpeed; // �����_���Ȓe���̍ŏ�����
    [SerializeField] public float randomSpeed; // �����_���Ȓe���̑���
    [SerializeField] public float randomBulletTime; // �����_���Ȓe�����o������
    [SerializeField] public int radiationBulletNum; // ���ˏ�ɏo���e���̐�
    [SerializeField] public float radiationBulletSpeed; // ���ˏ�ɏo���e���̑���
    [SerializeField] public float radiationBulletDelayTime; // ���ˏ�ɏo���e���̏o���Ԋu
    [SerializeField] public float radiationBulletCount; // ���ˏ�ɏo���e���̐��i������ˏ�ɏo�����j
    [SerializeField] public float radiationBulletAngle; // ���ˏ�ɏo���e���̊p�x
    [SerializeField] public float breakTime; // ��~�����e���𓮂�������̑ҋ@����
    [SerializeField] public Color bulletColor; // �e���̐F
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
    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// ��i�K�ڂ̕K�E�Z�i���������_���e���j
    /// </summary>
    /// <returns>IEnumerator�i�R���[�`���j</returns>
    private IEnumerator FastSpecialBullet()
    {
        // �w��ʒu�ispellPos�j�ֈړ��i���o��z�u�Ӑ}����j
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        // ��Ԃ� fast ���� spell ���̊ԁA�U�����[�v���p��
        while (boss1Bullet.State == State.fast && boss1Bullet.BulletState == BulletState.spell)
        {
            for (int i = 0; i < fastSpecialBom.ShotNum; i++)
            {
                // ���݈ʒu�����ʍ����i�ő� -8.5f �܂Łj�Ɍ����ă����_���ȕ����𐶐�
                float dirX = Random.Range(1.5f, -8.5f) - transform.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - transform.position.y;
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                // �e�v���n�u�����݈ʒu�ɐ������A���x��ݒ肵�Ĕ���
                GameObject gomi = Instantiate(fastSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = gomi.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * fastSpecialBom.speed;

                // �e�e���˂��Ƃɒx���i�Ԋu�𒲐��\�j
                yield return new WaitForSeconds(fastSpecialBom.DelayTime);
            }

            // ��A�̔��˂��I������珬�x�~�i�e���|�𒲐��j
            yield return new WaitForSeconds(fastSpecialBom.delayTime);
        }

        // ��ԑJ�ڂȂǂŃ��[�v�𔲂�����I��
        yield return null;
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// ��i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator SecondSpecialBullet()
    {
        // ���ˏ�ɒe���𐶐�����
        // ���b��Ƀv���C���[�̔��Α����烉���_����x���W�ɐ�������
        // ��L�̒e���͉E�����ɒ����I�ɔ��

        float shotTime = 0; // �o�ߎ��Ԃ��������i�p�x�ω��Ɏg���j

        // �X�y�������ʒu�Ɉړ��i���o�ƒe���J�n�̃g���K�[�Ƃ��āj
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        // ��Ԃ��usecond�v���uspell�v�̊ԁA�e�����p��
        while (boss1Bullet.State == State.second && boss1Bullet.BulletState == BulletState.spell)
        {
            // ��莞�Ԃ̊Ԃ����A��]�e�𔭎˂�������
            while (shotTime < secondSpecialBom.delayTime)
            {
                // ��ԕω����������瑦���I���i���S�΍�j
                if (boss1Bullet.State != State.second || boss1Bullet.BulletState != BulletState.spell) break;

                // ����������ɒe�𔭎�
                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + secondSpecialBom.angle; // ��{�p�x�i������ŊԊu���󂯂�j
                    float incrementalAngle = shotTime * 10f; // ���Ԍo�߂ɉ����Ċp�x�ɕω���������
                    float rad = (baseAngle + incrementalAngle) * Mathf.Deg2Rad; // ���W�A���ɕϊ�

                    // �w��p�x�̕����x�N�g�����v�Z
                    float dirX = Mathf.Cos(rad);
                    float dirY = Mathf.Sin(rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;

                    // �e�v���n�u���{�X�̈ʒu���琶��
                    GameObject proj = Instantiate(secondSpecialBom.LeftBulletPrehab, transform.position, Quaternion.identity);

                    // �x�N�g�������ɑ��x��^���Ĕ��ˁi�}�C�i�X�����ŊO�Ɍ������Ĕ��ˁj
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection * -secondSpecialBom.speed;
                    proj.transform.rotation = Quaternion.Euler(0, 0, baseAngle); // �e�̌����𒲐�
                }

                // ���̃��[�v�̂��߂Ɏ��Ԃ����Z���E�F�C�g
                shotTime += 0.07f;
                yield return new WaitForSeconds(0.07f);
            }

            // ��Ԃ��Ċm�F�i���[�v�̍ē˓��O�Ɉ��S�Ƀu���[�N�j
            if (boss1Bullet.State != State.second || boss1Bullet.BulletState != BulletState.spell) break;

            // �E�����璼�i�e�������_��Y���W�Ŕ��ˁi���E����̋����ɂȂ�j
            for (int i = 0; i < secondSpecialBom.BulletNum; i++)
            {
                if (boss1Bullet.State != State.second || boss1Bullet.BulletState != BulletState.spell) continue;

                Vector2 randomPos = new Vector2(-9f, Random.Range(-4.5f, 4.5f)); // ���[����Y�����_������
                GameObject proj = Instantiate(secondSpecialBom.RightBulletPrehab, randomPos, Quaternion.identity);
                Vector3 moveDirection = new Vector3(-20f, 0, 0).normalized; // ��ʍ������֒��i

                // �����Ȓ��i�e�ň��͂�������
                proj.GetComponent<Rigidbody2D>().linearVelocity = moveDirection * -secondSpecialBom.speed;
                yield return new WaitForSeconds(0.05f);
            }

            // shotTime �����Z�b�g���čă��[�v�ցi���v���Ɋp�x�����̍ĊJ�n�j
            shotTime = 0f;
        }

        // ��Ԃ��ω�������I��
        yield return null;

    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// �O�i�K�ڂ̕K�E�Z�i360�x�����փ����_�����x�Œe�𔭎ˁj
    /// </summary>
    /// <returns>IEnumerator�i�R���[�`���j</returns>
    private IEnumerator ThirdSpecialBullet()
    {
        // �X�y���J�[�h�����ʒu�ֈړ��i���o�I�Ȍ��ʂ�ʒu����j
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        // �{�X���uthird�v��Ԃ��uspell�v��Ԃ̊ԁA�e�����[�v���p��
        while (boss1Bullet.State == State.third && boss1Bullet.BulletState == BulletState.spell)
        {
            // �����_���Ȋp�x�i0�`360�x�j�𐶐�
            float angle = Random.Range(0f, 360f);

            // �e���������_���ɐݒ�i�w��͈͓��j
            float speed = Random.Range(thirdSpecialBom.minSpeed, thirdSpecialBom.maxSpeed);

            // �p�x�ɉ����������x�N�g�����Z�o�iVector2.right ����]�j
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            // �e�v���n�u�����݈ʒu�ɐ���
            GameObject bullet = Instantiate(thirdSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // �����x�N�g���ɑ��x���|���Ēe����ݒ�
                rb.linearVelocity = direction * speed;
            }

            // ���ˊԊu���ɏ��ɐݒ�i���ɍ����x�Ȓe�����\���j
            yield return new WaitForSeconds(0.01f);
        }

        // ��Ԃ��ω�������I��
        yield return null;
    }

    // ------------------------------------------------------------------------------------------------------------------------------
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
                    if(boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;
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

                // �{�X�̃X�e�[�g���ς������_���}�N�̋����I��
                if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;

                // �e�����~����
                foreach (GameObject bullet in bullets)
                {
                    if(bullet == null) continue;
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = Vector2.zero; // �e�̑��x���[���ɂ���   
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // �~�`�̒e���őҋ@

                StartCoroutine(ExpandMove(bullets)); // �e���g�U������
                yield return null;
            }

            // �{�X�̃X�e�[�g���ς������_���}�N�̋����I��
            if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;

            // �����_���ȍ��W�Ɉړ����Ȃ���e���΂�
            Vector2 randomPos = new Vector2(Random.Range(2.0f, 8.5f), Random.Range(-4.5f, 4.5f));
            StartCoroutine(PositionMove(randomPos)); // �����_���ȍ��W�ֈړ�
            for (int i = 0; i < 3; i++)
            {
                // �{�X�̃X�e�[�g���ς������_���}�N�̋����I��
                if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;

                // �e������
                List<GameObject> bullets = new List<GameObject>();
                // ���ɒe���𐶐�����
                float startAngle = 180f - fourSpecialBom.arcAngle / 2f; // ��̊J�n�p�x
                float angleStep = fourSpecialBom.arcAngle / (fourSpecialBom.arcCount - 1);

                for(int j = 0; j < fourSpecialBom.arcCount; j++)
                {
                    for(int k = 0; k < fourSpecialBom.arcLine; k++)
                    {
                        // �e�����c���ɐ��ɍ쐬
                        // �e�̊p�x���v�Z
                        float angle = startAngle + j * angleStep; // �e�̊p�x
                        float rad = angle * Mathf.Rad2Deg; // ���W�A���ɕϊ�
                        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // �e�̕������v�Z

                        // �e�𐶐�
                        GameObject bullet = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                        // �e���̑��x��k�̒l�ɂ���ĕω�
                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                        if (rb != null)
                        {
                            rb.linearVelocity = direction * (fourSpecialBom.arcSpeed - (k * 0.1f)); // �e�̑��x��ݒ�
                        }
                        bullets.Add(bullet); // �e�����X�g�ɒǉ�
                    }
                    yield return new WaitForSeconds(0.01f); // �e���΂��Ԋu
                }
                StartCoroutine(BulletMover(bullets));
                yield return new WaitForSeconds(0.7f);
            }
            yield return StartCoroutine(PositionMove(spellPos));
        }
        yield return null; // �R���[�`���̏I��
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
            if(bullet == null) continue;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero; // �e�̑��x���[���ɂ���
        }
        if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) StopCoroutine(BulletMover(bullets));
        yield return new WaitForSeconds(1f); // ���b�ԑҋ@
                                                                         // ���b��Ɏ��@�_���Œe���΂�
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets != null)
            {
                Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - bullets[i].transform.position).normalized;
                bullets[i].GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // �e�̑�����ݒ�
                yield return new WaitForSeconds(0.01f); // �e���΂��Ԋu
            }
        }
        yield return null;
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// �ŏI�i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    public IEnumerator FinalSpecialBullet()
    {
        // ���G�ɒe���𐔕b�Ԕ�΂��Z
        // ���̌�A��ʓ��̒e���̓������~�߂�Z
        // ���̌�A�ړ����Ȃ��琔�b�ԕ��ˏ�ɒe�����΂�
        // �Ō�ɉ�ʓ��̒e���������_���ȕ����Ɉ��̑��x�Ŕ�΂�
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        while(boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.spell) 
        {
            // ���G�ɒe�����΂�
            float randomTime = 0; // ���G�ɒe�����΂�����
            List<GameObject> bullets = new List<GameObject>(); // �e���̃��X�g
            while (randomTime < finalSpecianBom.randomBulletTime) // �e���𗐎G�ɔ�΂����Ԃ��o�߂���܂Ń��[�v
            {
                if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // ��Ԃ��ς�����烋�[�v�𔲂���
                float angle = Random.Range(0f, 360f); // �����_���Ȋp�x�𐶐�
                float speed = Random.Range(finalSpecianBom.minSpeed, finalSpecianBom.maxSpeed); // �����_���ȑ��x�𐶐�
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // �����_���ȕ������v�Z

                GameObject bullet = Instantiate(thirdSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = direction * speed; // �e�̑��x��ݒ�
                }
                yield return new WaitForSeconds(0.01f);
                bullets.Add(bullet); // �e�����X�g�ɒǉ�
                randomTime += 0.01f; // ���G�ɒe�����΂����Ԃ��X�V
            }
            if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // ��Ԃ��ς�����烋�[�v�𔲂���
            // ���G�ɔ�΂����e�����~����
            yield return new WaitForSeconds(1f); // ���G�ɒe�����΂�����̑ҋ@����
            foreach (GameObject bullet in bullets) // ���G�ɔ�΂����e�����~����
            {
                if (bullet != null)
                {
                    bullet.GetComponent<SpriteRenderer>().color = finalSpecianBom.bulletColor; // �e�̐F�����ɕύX
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = Vector2.zero; // �e�̑��x���[���ɂ���
                }
            }
            // ���ˏ�ɒe�����΂�
            if(boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // ��Ԃ��ς�����烋�[�v�𔲂���
            StartCoroutine(PositionMove(new Vector2(Random.Range(1.5f, 8.5f), Random.Range(-4.5f, 4.5f))));
            for (int i = 0; i < finalSpecianBom.radiationBulletCount; i++)
            {
                if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // ��Ԃ��ς�����烋�[�v�𔲂���
                float startAngle = 180f - finalSpecianBom.radiationBulletAngle / 2f; // ���ˏ�̊J�n�p�x
                float angleStep = finalSpecianBom.radiationBulletAngle / (finalSpecianBom.radiationBulletNum - 1); // ���ˏ�̊p�x�X�e�b�v
                for (int j = 0; j < finalSpecianBom.radiationBulletNum; j++)
                {
                    if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // ��Ԃ��ς�����烋�[�v�𔲂���
                    float angle = startAngle + j * angleStep; // �e�̊p�x���v�Z    
                    float rad = angle * Mathf.Deg2Rad; // ���W�A���ɕϊ�

                    Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // �e�̕������v�Z
                    GameObject bullet = Instantiate(finalSpecianBom.BulletPrehab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = direction * (finalSpecianBom.radiationBulletSpeed - (j * 0.1f)); // �e�̑��x��ݒ�
                    }
                }
                yield return new WaitForSeconds(finalSpecianBom.radiationBulletDelayTime); // ���ˏ�ɒe�����΂��Ԋu
            }
            if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // ��Ԃ��ς�����烋�[�v�𔲂��� 
            // ���ˏ�ɒe�����΂�����̑ҋ@����
            yield return new WaitForSeconds(1f); // ���ˏ�ɒe�����΂�����̑ҋ@����
            // ��ʓ��̒e���������_���ȕ����Ɉ��̑��x�Ŕ�΂�
            foreach (GameObject bullet in bullets)
            {
                if (bullet != null)
                {
                    // �����_���ȕ������v�Z
                    float angle = Random.Range(0f, 360f);
                    Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // �����_���ȕ������v�Z
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = direction * finalSpecianBom.randomSpeed; // �e�̑��x��ݒ�
                    }
                }
            }
            yield return new WaitForSeconds(finalSpecianBom.breakTime); // ��~�����e���𓮂�������̑ҋ@���� 
        }
        yield return null;  
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// �Z�~�t�@�C�i��
    /// </summary>
    /// <returns>IEnumerator for coroutine execution</returns>
    public IEnumerator FinalSpecialAttack()
    {
        // �e�̑����Ɋ�Â��ĉ~���̊p�x�X�e�b�v���v�Z�i360�x�𓙕��j
        float angleStep = 360 / specialFinalAttack.bulletNum;
        float angle = specialFinalAttack.angleOffset;

        // �{�X�̏�Ԃ��t�@�C�i�����X�y����Ԃ̊ԁA�U�����J��Ԃ�
        while (boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.special)
        {
            // �����_���Ȑ����ʒu����ʓ����猈��i���L�߂̎w��͈́j
            Vector3 randomPos = new Vector3(Random.Range(-8.4f, 8.5f), Random.Range(-4.5f, 4.5f), 0);

            // �e�����\������X�̒e�����[�v�Ŕ���
            List<GameObject> bullets = new List<GameObject>(); // �e�̃��X�g��������
            for (int i = 0; i < specialFinalAttack.bulletNum; i++)
            {
                // ���݂̊p�x�Ɋ�Â��ĕ����x�N�g�����v�Z�i���W�A���ɕϊ��j
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                // �e�v���n�u�������_���ʒu�ɐ������A���x��^���Ĕ���
                GameObject proj = Instantiate(specialFinalAttack.BulletPrehab, randomPos, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * specialFinalAttack.speed;

                // �e�����X�g�ɒǉ�
                bullets.Add(proj);

                // ���̒e�̊p�x��ݒ�
                angle += angleStep;
            }
            yield return new WaitForSeconds(0.2f); // �e���̔��ˊԊu��ҋ@

            // �e���̃X�s�[�h�������x������
            foreach(GameObject bullet in bullets)
            {
                if (bullet != null)
                {
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity *= 0.2f; // �e�̑��x�������x������
                    }
                }
            }

            // �e������]�����邽�߂Ɋp�x�I�t�Z�b�g�����Z
            specialFinalAttack.angleOffset += 10f; // ���̒l��ς���Ɖ�]���x���ς��

            // �I�t�Z�b�g�p�x��360�x�𒴂�����0�ɖ߂�
            if (specialFinalAttack.angleOffset >= 360)
                specialFinalAttack.angleOffset -= 360f;

            // ��莞�ԑҋ@���Ă��玟�̔g�����s
            yield return new WaitForSeconds(specialFinalAttack.delayTime);
        }

        // �����𔲂����ꍇ�ɃR���[�`�����I��
        yield return null;
    }

    /// <summary>
    /// �K�E�Z�������Ƀ{�X������̈ʒu�֊��炩�Ɉړ����鏈��
    /// </summary>
    /// <returns>�ړ���ɓ���I������R���[�`��</returns>
    private IEnumerator FireSpecialPositionMove(Vector2 targetPos)
    {
        // �ړ����̓{�X�̔�_���[�W���ꎞ�I�ɖ�����
        boss1Bullet.DamageLate = 0f;

        float limitTime = 1.5f; // �ړ��ɂ����鎞�ԁi�b�j
        float elapsedTime = 0f; // �o�ߎ��Ԃ̏�����
        Vector2 startPosition = transform.position; // �ړ��J�n�ʒu���L�^

        // �w�莞�ԓ���targetPos�܂Ő��`��ԁiLerp�j��p���Ċ��炩�Ɉړ�
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetPos.x, elapsedTime / limitTime), // X�����ɕ��
                Mathf.Lerp(startPosition.y, targetPos.y, elapsedTime / limitTime)  // Y�����ɕ��
            );

            // ���Ԃ��X�V���A���̃t���[����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �ړ�������A�{�X�̔�_���[�W���ĂїL�����i��F�x��0.2�b�j
        boss1Bullet.DamageLate = 0.2f;

        // �R���[�`�����I��
        yield return null;
    }
    /// <summary>
    /// �K�E�Z�������Ƀ{�X������̈ʒu�֊��炩�Ɉړ����鏈��
    /// </summary>
    /// <returns>�ړ���ɓ���I������R���[�`��</returns>
    private IEnumerator PositionMove(Vector2 targetPos)
    {

        float limitTime = 1.5f; // �ړ��ɂ����鎞�ԁi�b�j
        float elapsedTime = 0f; // �o�ߎ��Ԃ̏�����
        Vector2 startPosition = transform.position; // �ړ��J�n�ʒu���L�^

        // �w�莞�ԓ���targetPos�܂Ő��`��ԁiLerp�j��p���Ċ��炩�Ɉړ�
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetPos.x, elapsedTime / limitTime), // X�����ɕ��
                Mathf.Lerp(startPosition.y, targetPos.y, elapsedTime / limitTime)  // Y�����ɕ��
            );

            // ���Ԃ��X�V���A���̃t���[����
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �R���[�`�����I��
        yield return null;
    }
}
