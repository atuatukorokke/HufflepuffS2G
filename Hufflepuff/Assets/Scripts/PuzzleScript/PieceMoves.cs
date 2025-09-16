// PieceMoves.cs
// 
// �p�Y���s�[�X�̓����𐧌�����܂�
// 

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PieceMoves : MonoBehaviour
{
    private PuzzleGrid Pgrid;   // �p�Y���s�[�X��z�u����X�N���v�g���Ăяo��
    private PieceCreate Pcreate;    // �p�Y���s�[�X�𐶐�����X�N���v�g

    private bool isColliding = false;

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    /// <summary>
    /// ���@PuzzleGrid���Ăяo���p�Y���s�[�X���z�u�\�����m�F���܂�
    /// �V�@�R���C�_�[�̔���m�F�Ŕz�u�o���Ă��邩�̊m�F���s���܂�
    /// </summary>
    public void PiecePossible()
    {
        if (isColliding)
        {
            Debug.Log("�����ĂȂ�");
        }
        else
        {
            Debug.Log("�����Ă�");
        }
    }
}