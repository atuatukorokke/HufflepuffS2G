using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Range (0, 100)]
    [SerializeField] private float hP;
    [SerializeField] GameObject prehab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("P_Bullet"))
        {
            hP -= 10;
            if(hP == 0)
            {
                // �s�[�X�̃h���b�v
                if(Random.Range(0, 10) == 1)
                {
                    Debug.Log("����������");
                }
                Instantiate(prehab, transform.position, Quaternion.identity); //�h���b�v�m�F�p�̍Đ���
                Destroy(gameObject); // �G�l�~�[�̏���
            }
        }
    }
}
