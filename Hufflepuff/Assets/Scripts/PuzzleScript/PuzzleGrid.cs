// PuzzleGrid.cs
// 
// �p�Y���s�[�X��u���邩�̊m�F�����܂�
// 

using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    private PieceList PList;   // �p�Y���s�[�X�̌`���m�F����悤

    // �O���b�h�T�C�Y      �����@���̕ϐ���Unity��̘g���͐��䂵�Ă��܂���A���ڂ������Ă�������
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
        grid = new int[height, width];

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
    public bool PuzzleCheck(GameObject inGameObject ,float inx, float iny, int inz)
    {
        inx = inx - 0.5f;
        iny = iny + 0.5f;

        Debug.Log("inx:" + inx);
        Debug.Log("iny:" + iny);

        // �ړ����������Ȃ�
        if (inx % 1 != 0 || iny % 1 != 0) return false;
        
        // �����₷���悤�ɍ��W�f�[�^�𐮐���,�����������炢��Ȃ������H�v����
        int pTestx = (int)(inx);
        int pTesty = (int)(iny * -1);

        // �s�[�X�̌`�̏���PieceShape�ɓ���܂�
        int[,] PieceShape = new int[4, 4];
        PieceShape = PList.PieceShapes(inGameObject, inz);


        

        switch (inz)
        {
            case 0:
                // �p�Y���s�[�X���͈͊O�ɏo�Ă��Ȃ������m�F���܂�
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        // x�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[i, j] == 1 && ((pTestx + j) < 0 || (pTestx + j) > width - 1))
                        {
                            Debug.Log("x�͈͊O�ł�");
                            return false;
                        }

                        // y�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[i, j] == 1 && ((pTesty + i) < 0 || (pTesty + i) > height - 1))
                        {
                            Debug.Log("y�͈͊O�ł�");
                            return false;
                        }
                        if ((pTestx + j) > width - 1 || (pTestx + j) < 0) break;
                        if ((pTesty + i) > height - 1 || (pTesty + i) < 0) break;
                        // ���łɒu����Ă���I�u�W�F�N�g�̏�ɒu���Ȃ��悤�ɂ��鏈��
                        if (grid[i + pTesty, j + pTestx] == 1 && PieceShape[i, j] == 1)
                        {
                            Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                            return false;
                        }
                    }
                }
                break;
            case 1:
                // �p�Y���s�[�X���͈͊O�ɏo�Ă��Ȃ������m�F���܂�
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        // x�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[3 - j, i] == 1 && ((pTestx + j) < 0 || (pTestx + j) > width - 1))
                        {
                            Debug.Log("x�͈͊O�ł�");
                            return false;
                        }

                        // y�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[3 - j, i] == 1 && ((pTesty + i) < 0 || (pTesty + i) > height - 1))
                        {
                            Debug.Log("y�͈͊O�ł�");
                            return false;
                        }
                        if ((pTestx + j) > width - 1 || (pTestx + j) < 0) break;
                        if ((pTesty + i) > height - 1 || (pTesty + i) < 0) break;
                        // ���łɒu����Ă���I�u�W�F�N�g�̏�ɒu���Ȃ��悤�ɂ��鏈��
                        if (grid[i + pTesty, j + pTestx] == 1 && PieceShape[3 - j, i] == 1)
                        {
                            Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                            return false;
                        }
                    }
                }
                break;
            case 2:
                // �p�Y���s�[�X���͈͊O�ɏo�Ă��Ȃ������m�F���܂�
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        // x�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[3 - j, 3 - i] == 1 && ((pTestx + j) < 0 || (pTestx + j) > width - 1))
                        {
                            Debug.Log("x�͈͊O�ł�");
                            return false;
                        }

                        // y�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[3 - j, 3 - i] == 1 && ((pTesty + i) < 0 || (pTesty + i) > height - 1))
                        {
                            Debug.Log("y�͈͊O�ł�");
                            return false;
                        }
                        if ((pTestx + j) > width - 1 || (pTestx + j) < 0) break;
                        if ((pTesty + i) > height - 1 || (pTesty + i) < 0) break;
                        // ���łɒu����Ă���I�u�W�F�N�g�̏�ɒu���Ȃ��悤�ɂ��鏈��
                        if (grid[i + pTesty, j + pTestx] == 1 && PieceShape[3 - j, 3 - i] == 1)
                        {
                            Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                            return false;
                        }
                    }
                }
                break;
            case 3:
                // �p�Y���s�[�X���͈͊O�ɏo�Ă��Ȃ������m�F���܂�
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        // x�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[j, 3 - i] == 1 && ((pTestx + j) < 0 || (pTestx + j) > width - 1))
                        {
                            Debug.Log("x�͈͊O�ł�");
                            return false;
                        }

                        // y�����Ƀs�[�X���͈͊O�ɏo�Ă��Ȃ����̌���
                        if (PieceShape[j, 3 - i] == 1 && ((pTesty + i) < 0 || (pTesty + i) > height - 1))
                        {
                            Debug.Log("y�͈͊O�ł�");
                            return false;
                        }
                        if ((pTestx + j) > width - 1 || (pTestx + j) < 0) break;
                        if ((pTesty + i) > height - 1 || (pTesty + i) < 0) break;
                        // ���łɒu����Ă���I�u�W�F�N�g�̏�ɒu���Ȃ��悤�ɂ��鏈��
                        if (grid[i + pTesty, j + pTestx] == 1 && PieceShape[j, 3 - i] == 1)
                        {
                            Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                            return false;
                        }
                    }
                }
                break;
        }

        return true;
    }
}
