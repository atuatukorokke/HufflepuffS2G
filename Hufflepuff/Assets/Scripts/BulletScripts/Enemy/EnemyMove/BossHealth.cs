using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float hP; // �G�l�~�[�̂g�o

    public float HP { get => hP; set => hP = value; }

    public event Action OnDeath; // ���{�X���j�ʒm�p�C�x���g

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // �v���C���[�̒e������
            if (HP <= 0)
            {
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach(GameObject objects in bullets)
                {
                    Destroy(objects);
                }
                OnDeath?.Invoke(); // ���{�X���j�ʒm�C�x���g�𔭉�
                Destroy(this.gameObject); // �G�l�~�[�̏���
            }
        }
        else if(collision.CompareTag("P_Bom"))
        {
            if (HP <= 0)
            {
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach (GameObject objects in bullets)
                {
                    Destroy(objects);
                }
                OnDeath?.Invoke(); // ���{�X���j�ʒm�C�x���g�𔭉�
                Destroy(this.gameObject); // �G�l�~�[�̏���
            }
        }
    }
}
