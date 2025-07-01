// PuzzleGrid.cs
// 
// �p�Y���s�[�X��u���邩�̊m�F�����܂�
// 

using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    private PieceList PList;   // �p�Y���s�[�X�̌`���m�F����悤

    // �O���b�h�T�C�Y
    private const int width = 7;
    private const int height = 7;

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
        grid = new int[width, height];

        // ���ׂĂ�false�����鏈��
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0;
            }
        }
        // �f�o�b�O�p�@�����ĂȂ�����������Ă�������
        grid[3, 2] = 1;
    }

    /// <summary>
    /// �s�[�X���z�u�\���𔻒肵�܂�
    /// </summary>
    /// <param name="inGameObject">�I�u�W�F�N�g���</param>
    /// <param name="inx">�p�Y����ʂ̍����0, 0�Ƃ��Č��݂�x���W�����Ă�������</param>
    /// <param name="iny">�p�Y����ʂ̍����0, 0�Ƃ��Č��݂�y���W�����Ă�������</param>
    /// <param name="inz">�s�[�X�̉�]�p�x�����Ă�������</param>
    /// <returns>�z�u�\ = true�@�z�u�s�� = false</returns>
    public bool PuzzleCheck(GameObject inGameObject ,float inx, float iny, float inz)
    {
        Debug.Log("inx:" + inx);
        Debug.Log("iny:" + iny);
        // ���W�̏����_�����O
        if (inx % 1 != 0 || iny % 1 != 0) return false;
        
        int pTestx = (int)(inx);
        int pTesty = (int)(iny * -1);

        // �s�[�X�̌`�̏���PieceShape�ɓ���܂�
        int[,] PieceShape = new int[4, 4];
        PieceShape = PList.PieceShapes(inGameObject);


        // �p�Y���s�[�X���͈͊O�ɏo�Ă��Ȃ������m�F���܂�
        switch (inz)
        {

            case 0:
                // ���v   0����]
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        // x�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ������m�F
                        if (PieceShape[j, i] == 1 && ((pTestx + i) > width - 1 || (pTestx + i) < 0))
                        {
                            Debug.Log("x�͈͊O�ł�");
                            return false;
                        }
                        // y�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ������m�F
                        if (PieceShape[j, i] == 1 && ((pTesty + j) > height - 1 || (pTesty + j) < 0))
                        {
                            Debug.Log("y�͈͊O�ł�");
                            return false;
                        }
                        if (pTestx + j <= 0 || pTestx + j >= width || pTestx + j <= 0 || pTesty + i >= height) break;
                        if (grid[i + pTesty, j + pTestx] == 1 && PieceShape[i, j] == 1)
                        {
                            Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                            return false;
                        }
                    }
                }
                break;
            case 1:
                // ���v  90����]
                // ���ݏ���������
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (PieceShape[j, i] == 1 && ((pTestx + i) > width - 1 || (pTestx + i) < 0))
                        {
                            Debug.Log("�͈͊O�ł�");
                            return false;
                        }
                        if (PieceShape[i, j] == 1 && ((pTesty + i) > height - 1 || (pTesty + i) < 0))
                        {
                            Debug.Log("�͈͊O�ł�");
                            return false;
                        }
                    }
                }
                break;
            case 2:
                // ���v 180����]
                Debug.Log("���������");
                break;
            case 3:
                // ���v 270����]
                Debug.Log("���������");
                break;
            default:
                Debug.Log("�΂����Ă��Ł[");
                break;
        }

        /*
        // �p�Y���s�[�X�̔z�u�ӏ��Ɋ��Ƀs�[�X���u����Ă��Ȃ����m�F
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i + pTestx > width || j + pTesty > height) break;
                if (grid[i + pTestx, j + pTesty] == 1 && PieceShape[j, i] == 1)
                {
                    Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                    return false;
                }
            }
        }
        */

        return true;
    }
}
