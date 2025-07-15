// MiddleBossBullet.cs 
// 
// ���{�X�̒e�����Ǘ�����X�N���v�g
//

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    noemal, // �ʏ�e��
    spell, // �X�y���J�[�h
}

public class MiddleBossBullet : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType; // �G�l�~�[�̏�Ԃ��Ǘ�����񋓌^
    [SerializeField] private BossHealth bossHealth; // �{�X�̂g�o���Ǘ�����X�N���v�g
    private float damageLate = 1; // ��_���[�W�̊���
    [SerializeField] private GameObject presentBox; // �h���b�v�p�̃v���n�u
    [SerializeField] private GameObject HealthCanvas; // ���݂̂g�o�o�[�̃L�����o�X
    [SerializeField] private GameObject currentHpbar; // ���݂̂g�o�o�[�̃I�u�W�F�N�g
    private float maxHp; // �{�X�̍ő�g�o
    GameObject canvas; // �g�o�o�[�̃L�����o�X

    [Header("�ʏ�e���p�̕ϐ�")]
    [SerializeField] private GameObject bulletPrefab; // �e���̃v���n�u
    [SerializeField] private int shotNum; // �e�����o����
    [SerializeField] private float radiusShrinkSpeed = 0.05f; // ���a�̏k�����x
    [SerializeField] private float radius = 3.0f; // �e���̐����ʒu�̔��a
    [SerializeField] private float bulletSpeed; // �e���̃X�s�[�h
    [SerializeField] private float bulletDelayTime; // �e�����o���Ԋu

    /// <summary>
    /// �����ݒ�����܂�
    /// </summary>
    private void Start()
    {
        canvas = Instantiate(HealthCanvas, Vector3.zero, Quaternion.identity); // �g�o�o�[�̃L�����o�X�𐶐�
        currentHpbar = canvas.transform.GetChild(0).Find("currentHpBar").gameObject; // ���݂̂g�o�o�[�̃I�u�W�F�N�g���擾; // ���݂̂g�o�o�[���擾

        bossHealth = FindAnyObjectByType<BossHealth>(); // �{�X�̂g�o�Ǘ��X�N���v�g���擾
        maxHp = bossHealth.hP; // �{�X�̍ő�g�o���擾
        StartCoroutine(StartBullet());
    }

    /// <summary>
    /// �e���̐����ʒu�𗳊���ɐݒ肵�A�e���𐶐����܂�
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartBullet()
    {
        float angleStep = 360f / shotNum; // �e���̐����ʒu�̊p�x��
        float stopThreshold = 0.2f; // ���a�����̒l�ȉ��ɂȂ������~
        float globalAngle = 0f; // �Q���p�̉�]�p

        while (enemyType == EnemyType.noemal)
        {
            if (radius > stopThreshold && enemyType == EnemyType.noemal)
            {
                for (int i = 0; i < shotNum; i++)
                {
                    float angle = globalAngle + (i * angleStep); // �e���Ƃ̊p�x���v�Z

                    // �e���̐����ʒu���v�Z
                    Vector2 bulletPos = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad),
                        Mathf.Sin(angle * Mathf.Deg2Rad)
                        ) * radius;

                    // �e���𐶐�
                    Vector2 spwanPos = (Vector2)transform.position + bulletPos;
                    GameObject bullet = Instantiate(bulletPrefab, spwanPos, Quaternion.identity);

                    // �e���̉�]��ݒ�
                    Vector2 direction = bulletPos.normalized; // �e���̈ړ��������v�Z
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = direction * bulletSpeed;
                }
                radius -= radiusShrinkSpeed; // ���a���k��
                globalAngle += 10f; // �O���[�o���p�x���X�V
            }
            yield return new WaitForSeconds(bulletDelayTime);
        }
        yield return null; // 1�t���[���ҋ@
    }

    private IEnumerator MiddleApell()
    {
        while(enemyType == EnemyType.spell)
        {
            Debug.Log("�X�y���J�[�h������");
            yield return new WaitForSeconds(5f); // �X�y���J�[�h�̊Ԋu
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // �v���C���[�̒e������
            bossHealth.hP -= damageLate; // �G�l�~�[��HP�����炷
            currentHpbar.transform.localScale = new Vector3(bossHealth.hP / 100, currentHpbar.transform.localScale.y, currentHpbar.transform.localScale.z); // ���݂�HP�o�[���X�V

            if (bossHealth.hP <= 20 && enemyType == EnemyType.noemal)
            {
                enemyType++; // �G�l�~�[�̏�Ԃ��X�y���J�[�h�ɕύX
                damageLate = 0.2f; // ��_���[�W�̊�����ύX
                StartCoroutine(MiddleApell()); // �X�y���J�[�h�̔���
            }
            else if(bossHealth.hP <= 0)
            {
                Destroy(canvas); // �g�o�o�[�̃L�����o�X���폜
                GameObject present = Instantiate(presentBox, transform.position, Quaternion.identity); // �h���b�v�p�̃v���n�u�𐶐�
                present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0); // �h���b�v�̑��x��ݒ�
            }
        }
    }
}
