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

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// PuzzleGrid���Ăяo���p�Y���s�[�X���z�u�\�����m�F���܂�
    /// </summary>
    public void PiecePossible()
    {

    }
}