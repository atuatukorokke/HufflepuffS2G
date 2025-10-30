// DestroyBlock.cs
// 
// �Ăяo���ꂽ�Ƃ��^�O��Block�����Ă���I�u�W�F�N�g�����ׂď����܂�
// 

using UnityEngine;

public class DestroyBlock : MonoBehaviour
{

    [Header("�X�N���v�g���A�^�b�`")]
    [SerializeField] private DeathCount deathCount;     // ���ʂ��̔�����s���X�N���v�g
    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g
    [SerializeField] private PieceCreate pieceCreate;   // �s�[�X�𐶐�����X�N���v�g

    public string targetTag = "block"; // �폜�������^�O�����w��


    /// <summary>
    /// �Ăяo�������u���b�N������21�̔{���Ȃ�u���b�N������
    /// </summary>
    public void DestroyPieceBlock()
    {
        int TotalBlock = deathCount.GetTotalBlock();
        if (TotalBlock % 21 == 0)
        {
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(targetTag);

            foreach (GameObject obj in objectsToDelete)
            {
                Destroy(obj);
            }

            pieceCreate.BlockBoardInitialize();
        }
    }

}
