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

    [SerializeField] private DeathCount deathCount;     // ���ʂ��̔�����s���X�N���v�g

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();

        //deathCount.SetPieceCount(50);   // �f�o�b�O�p�̃s�[�X��
    }

    void Update()
    {
        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Pmoves == null) Pmoves = FindAnyObjectByType<PieceMoves>();
            Pmoves.PiecePossible();
        }

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
