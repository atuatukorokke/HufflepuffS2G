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
    private PieceMoves Pmoves;      // �p�Y���s�[�X�𓮂����X�N���v�g
    [SerializeField] private BuffManager buffManager;
    [SerializeField] private PieceCreate Pcreate;    // �p�Y���s�[�X�𐶐�����X�N���v�g

    int CountRotate = 0;    // ��]�����J�E���g����ϐ�

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        buffManager = FindAnyObjectByType<BuffManager>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //if (Pmoves == null)
            //{
            //    Pmoves = FindAnyObjectByType<PieceMoves>();
            //    Pmoves.PiecePossible();
            //}
            if(Pcreate.IsCreate)
            {
                buffManager.AddBuff(Pcreate.BuffID, Pcreate.BuffValue);
                Pcreate.IsCreate = false;
            }
        }

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
