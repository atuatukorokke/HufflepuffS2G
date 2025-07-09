// MiddleBossBullet.cs 
// 
// ���{�X�̒e�����Ǘ�����X�N���v�g
//

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyType
{
    noemal, // �ʏ�e��
    spell, // �X�y���J�[�h
}

public class MiddleBossBullet : MonoBehaviour
{
    private EnemyHealth enemyHealth; // �G�l�~�[�̃w���X���Ǘ�����X�N���v�g
    [SerializeField] private EnemyType enemyType; // �G�l�~�[�̏�Ԃ��Ǘ�����񋓌^

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
        enemyHealth = GetComponent<EnemyHealth>();
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
            if (radius > stopThreshold)
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
}
