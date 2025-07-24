using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] public float hP; // �G�l�~�[�̂g�o

    public event Action OnDeath; // ���{�X���j�ʒm�p�C�x���g

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // �v���C���[�̒e������
            if (hP <= 0)
            {
                OnDeath?.Invoke(); // ���{�X���j�ʒm�C�x���g�𔭉�
                Destroy(this.gameObject); // �G�l�~�[�̏���
            }
        }
    }
}
