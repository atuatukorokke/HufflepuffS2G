// PuzzleGrid.cs
// 
// �p�Y���s�[�X��u���邩�̊m�F�����܂�
// 

using NUnit.Framework.Interfaces;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    private PieceList PList;   // �p�Y���s�[�X�̌`���m�F����悤

    // �O���b�h�T�C�Y      �����@���̕ϐ���Unity��̘g���͐��䂵�Ă��܂���A���ڂ������Ă�������
    private const int width = 5;
    private const int height = 5;

    // �O���b�h�̏�Ԃ��Ǘ�����z��
    private int[,] grid;

    private void Start()
    {
        // �O���b�h�����ƃ}�X�ڂ̏�����
        InitializeGrid();
        PList = FindAnyObjectByType<PieceList>();
    }

    /// <summary>
    /// �p�Y���Ֆʂ̏������i���ׂċ󂢂Ă����Ԃɂ���j
    /// </summary>
    private void InitializeGrid()
    {
        grid = new int[height, width];

        // ���ׂĂ�false�����鏈��
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0;
            }
        }

        grid[0, 0] = 99;                    // ����̈ʒu��z�u�s�ɂ���
        grid[0, width - 1] = 99;            // �E��̈ʒu��z�u�s�ɂ���
        grid[height - 1, 0] = 99;           // �����̈ʒu��z�u�s�ɂ���
        grid[height - 1, width - 1] = 99;   // �E���̈ʒu��z�u�s�ɂ���
    }

    /// <summary>
    /// �s�[�X���z�u�\���𔻒肵�܂�
    /// </summary>
    /// <param name="inGameObject">�I�u�W�F�N�g���</param>
    /// <param name="inx">�p�Y����ʂ̍����0, 0�Ƃ��Č��݂�x���W�����Ă�������</param>
    /// <param name="iny">�p�Y����ʂ̍����0, 0�Ƃ��Č��݂�y���W�����Ă�������</param>
    /// <param name="inz">�s�[�X�̉�]�p�x�����Ă�������</param>
    /// <returns>�z�u�\ = true�@�z�u�s�� = false</returns>
    public bool PuzzleCheck(GameObject inGameObject ,float inx, float iny, int inz)
    {
        // �ړ����������Ȃ�
        if (inx % 1 != 0 || iny % 1 != 0) return false;
        
        // �����₷���悤�ɍ��W�f�[�^�𐮐���,�����������炢��Ȃ������H�v����
        int pTestx = (int)(inx);
        int pTesty = (int)(iny * -1);

        // �s�[�X�̌`�̏���PieceShape�ɓ���܂�
        int[,] PieceShape = new int[3, 5];
        PieceShape = PList.PieceShapes(inGameObject, inz);


        // ���v����0�x��]���Ă��܂�
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (PieceShape[i, j] == 1)
                {
                    // x�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                    if ((pTestx + j) < 0 || (pTestx + j) > width - 1)
                    {
                        Debug.Log("x�͈͊O�ł�");
                        return false;
                    }

                    // y�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                    if ((pTesty + i) < 0 || (pTesty + i) > height - 1)
                    {
                        Debug.Log("y�͈͊O�ł�");
                        return false;
                    }
                }
                
                if ((pTestx + j) > width - 1 || (pTestx + j) < 0) break;
                if ((pTesty + i) > height - 1 || (pTesty + i) < 0) break;
                // ���łɒu����Ă���I�u�W�F�N�g�̏�ɒu���Ȃ��悤�ɂ��鏈��
                if (grid[i + pTesty, j + pTestx] > 0 && PieceShape[i, j] > 0)
                {
                    Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                    return false;
                }
            }
        }


        // �p�Y����z�u
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (PieceShape[i, j] == 1)
                {
                    grid[pTesty + i, pTestx + j] = PieceShape[i, j];
                }
            }
        }
        Debug.Log("�z�u�ł��܂���");

        return true;
    }
}
