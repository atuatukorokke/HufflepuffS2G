// PuzzleController.cs
// 
// �v���C���[����̓��͂��󂯎��A
// ���̃p�Y���p�X�N���v�g�𔭉΂��܂��B
// 

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g
    [SerializeField] private DestroyBlock destroyBlock; // �u���b�N�������X�N���v�g
    public List<Buff> ProvisionalBuffs = new List<Buff>();

    private void Start()
    {

    }

    void Update()
    {
        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pieceMoves.PiecePossible();

            if (pieceMoves.GetPiecePossible())
            {
                destroyBlock.DestroyPieceBlock();
            }
        }
    }
}
