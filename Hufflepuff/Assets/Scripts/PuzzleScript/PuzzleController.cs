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
    private PieceCreate Pcreate;    // �p�Y���s�[�X�𐶐�����X�N���v�g

    int CountRotate = 0;    // ��]�����J�E���g����ϐ�

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Pmoves == null) Pmoves = FindAnyObjectByType<PieceMoves>();
            Pmoves.PiecePossible(CountRotate);
            CountRotate = 0; // ��]�������Z�b�g
        }

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Pmoves == null) Pmoves = FindAnyObjectByType<PieceMoves>();
            CountRotate = Pmoves.PieceRotation(CountRotate);
            Pmoves = null;
            Destroy(Pmoves, 0.1f); // PieceMoves�X�N���v�g���폜
        }


        // �f�o�b�O�p�ɃL�[����������s�[�X�𐶐�����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pcreate.NewPiece(0);
            Start();
        }

    }
}
