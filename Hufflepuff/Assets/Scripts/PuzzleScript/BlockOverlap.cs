// BlockOverlap.cs
//
// ���ז��u���b�N�������ɂق��̃u���b�N�Əd�Ȃ��Ă��Ȃ������m�F����
// �����d�Ȃ��Ă�����폜�ƍĐ���������
// PieceCreate�̂��ז��J�E���g���C������
//�@

using UnityEngine;
using UnityEngine.Rendering;

public class BlockOverlap : MonoBehaviour
{
    [SerializeField] private PieceCreate pieceCreate;
    [SerializeField] private DeathCount deathCount; // ���ʂ��̔�����s���X�N���v�g
    public Vector2Int blockPosition; // �u���b�N�̈ʒu

    /// <summary>
    /// Start���\�b�h�ŏd�Ȃ���m�F���A�d�Ȃ��Ă�����폜�ƍĐ������s��
    /// </summary>
    void Start()
    {
        pieceCreate = FindAnyObjectByType<PieceCreate>();
        deathCount = FindAnyObjectByType<DeathCount>();
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().bounds.size, 0f);
        foreach (var col in overlaps)
        {
            if (col.gameObject != this.gameObject && col.CompareTag("block"))
            {
                deathCount.SetBlockCount(-1); // ���ז��u���b�N�������炷
                pieceCreate.BlockCreate(); // �u���b�N���Đ�������
                Destroy(gameObject);
            }
        }
    }
}
