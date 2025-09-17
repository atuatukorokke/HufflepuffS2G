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
    [SerializeField] private PieceCreate pieceCreate;

    public string targetTag = "block"; // �폜�������^�O�����w��

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �Ăяo�������u���b�N������21�̔{���Ȃ�u���b�N������
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
