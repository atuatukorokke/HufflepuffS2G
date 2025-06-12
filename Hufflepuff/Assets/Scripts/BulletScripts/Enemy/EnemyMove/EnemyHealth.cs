// EnemyHealth.cs
//
// �G���G�l�~�[�̂g�o�Ǘ��ƃh���b�v�m�F���s���܂�
//

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Range (0, 100)]
    [SerializeField] private float hP; // �G�l�~�[�̂g�o
    [SerializeField] GameObject prehab; // ���̃v���n�u
    [SerializeField] private int dropLate; // ���𗎂Ƃ��m��
    [SerializeField] private DropManager dropManager; // �h���b�v�Ǘ��̃X�N���v�g

    private void Start()
    {
        dropManager = FindAnyObjectByType<DropManager>();
        dropLate = dropManager.DropLate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("P_Bullet"))
        {
            hP -= 10;
            if(hP == 0)
            {
                // �s�[�X�̃h���b�v
                if (Random.Range(0, dropLate) == 0)
                {
                    Debug.Log("����������");
                    dropLate = dropManager.LateReset();
                }
                else
                {
                    dropLate = dropManager.LateChange();
                }
                    Instantiate(prehab, transform.position, Quaternion.identity); //�h���b�v�m�F�p�̍Đ���
                Destroy(gameObject); // �G�l�~�[�̏���
            }
        }
    }
}
