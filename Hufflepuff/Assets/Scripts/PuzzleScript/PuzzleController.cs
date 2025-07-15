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
    private PieceMoves Pmoves;  // �p�Y���s�[�X�𓮂����X�N���v�g���Ăяo���p
    private PieceCreate Pcreate;    // �f�o�b�O�p�Ƀs�[�X�𐶐�����X�N���v�g���Ăяo��

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        /*
        // ���L�[�Ńp�Y���s�[�X���ړ�
        if (Input.GetKey(KeyCode.RightArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.LeftArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.UpArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.DownArrow)) Pmoves.PieceMove();
        */

        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z)) Pmoves.PiecePossible();

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X)) Pmoves.PieceRotation();


        // �f�o�b�O�p�ɃL�[����������s�[�X�𐶐�����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pcreate.NewPiece();
            Pmoves = FindAnyObjectByType<PieceMoves>();
        }

    }
}
