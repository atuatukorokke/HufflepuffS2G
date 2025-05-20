using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowingBullet : MonoBehaviour
{
    private GameObject targetObj; // �^�[�Q�b�g(�v���C���[)
    private Vector2 targetPos; // �^�[�Q�b�g(�v���C���[)�Ƃ̈ʒu�֌W������
    [SerializeField] private GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] private int ShotNum; // �e�����o����
    [SerializeField] private float speed; // �e���̃X�s�[�h
    [SerializeField] private float delayTime; // �e�����o���Ԋu
    [SerializeField] private float destroyTime; // �e������������
    GameObject proj;

     void Start()
    {
        InvokeRepeating("FollowingShoot", 0, delayTime);
    }

    /// <summary>
    /// �ǔ��e�̐���
    /// �v���C���[�̍��W����ړ��������v�Z����
    /// </summary>
    private void FollowingShoot()
    {
        // �v���C���[�̍��W���擾
        targetObj = GameObject.Find("Player");
        targetPos = targetObj.transform.position;
        // �v���C���[�̍��W�ƃG�l�~�[�̍��W�̈ʒu�֌W
        float dx = targetPos.x - transform.position.x;
        float dy = targetPos.y - transform.position.y;
        Vector3 moveDirection = new Vector3(dx, dy, 0);
        StartCoroutine(TimeDelayShot(moveDirection));
    }

    /// <summary>
    /// ShotNum�̐������e�𐶐����āAmoveDirection�̕����ɔ�΂�
    /// </summary>
    /// <param name="moveDirection">�ړ�����</param>
    /// <returns>�҂�����0.06�b��Ԃ�</returns>
    IEnumerator TimeDelayShot(Vector3 moveDirection)
    {
        for (int i = 0; i < ShotNum; i++)
        {
            proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
            proj.transform.parent = transform;
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = moveDirection.normalized * speed;
            Destroy(proj, destroyTime);
            yield return new WaitForSeconds(0.06f);
        }
    }
}
