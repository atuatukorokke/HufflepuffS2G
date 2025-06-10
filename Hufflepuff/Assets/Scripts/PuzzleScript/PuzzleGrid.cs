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
    private bool[,] grid;

    private void Start()
    {
        // �O���b�h�����ƃ}�X�ڂ̏�����
        InitializeGrid();
        PList = FindAnyObjectByType<PieceList>();
    }

    /// <summary>
    /// �O���b�h�̏������i���ׂċ󂢂Ă����Ԃɂ���j
    /// </summary>
    private void InitializeGrid()
    {
        grid = new bool[width, height];

        // ���ׂĂ�false�����鏈��
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = false;
            }
        }
    }

    /// <summary>
    /// �s�[�X���z�u�\���𔻒肵�܂�
    /// </summary>
    /// <param name="inx">�I�u�W�F�N�g��x���W</param>
    /// <param name="iny">�I�u�W�F�N�g��y���W</param>
    /// <returns>ture = �z�u�ł��� false = �z�u�ł��Ȃ�</returns>
    public bool PuzzleCheck(float inx, float iny, GameObject inGameObject)
    {
        if (inx % 1 == 0 && iny % 1 == 0)
        {
            int[,] PieceShape = new int[4, 4];
            PieceShape = PList.PieceShapes(inGameObject);

            Debug.Log(PieceShape);

            return true;
        }

        return false;
    }
}
