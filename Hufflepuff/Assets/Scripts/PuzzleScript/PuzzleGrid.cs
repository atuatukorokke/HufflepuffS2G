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
        grid[3, 3] = 1;
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

        // �󂯎�������W�͂��������͈͊O�̏ꍇ�����O
        // x 0 ~ 6
        // y -6 ~ 0
        if (inx < 0 || inx > 6 || iny > 0 || iny < -6) return false;

        // �s�[�X�̌`�̏���PieceShape�ɓ���܂�
        int[,] PieceShape = new int[4, 4];
        PieceShape = PList.PieceShapes(inGameObject);

        // �p�x�������₷�����ɕύX����
        if (inz == 0)
        {
            Debug.Log("inz:1");
        }
        else if (inz > 0 && inz != 1)
        {
            Debug.Log("inz:2");
        }
        else if (inz == 1)
        {
            Debug.Log("inz:3");
        }
        else if (inz < 0)
        {
            Debug.Log("inz:4");
        }

        // �p�Y���s�[�X���͈͊O�ɏo�Ă��Ȃ������m�F���܂�
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (PieceShape[j, i] == 1 && (inx + i) > 6)
                {
                    Debug.Log("�͈͊O�ł�");
                    return false;
                }
                if (PieceShape[i, j] == 1 && (iny - i) < -6)
                {
                    Debug.Log("�͈͊O�ł�");
                    return false;
                }
            }

        }

        // �p�Y���s�[�X�̔z�u�ӏ��Ɋ��Ƀs�[�X���u����Ă��Ȃ����m�F
        int pTestx = (int)(inx);
        int pTesty = (int)(iny * -1);
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i + pTestx > 6 || j + pTesty > 6) break;
                if (grid[i + pTestx, j + pTesty] == 1 && PieceShape[j, i] == 1)
                {
                    Debug.Log("�s�[�X�̏�ɂ͔z�u�ł��܂���");
                    return false;
                }
            }
        }

        return true;
    }
}
