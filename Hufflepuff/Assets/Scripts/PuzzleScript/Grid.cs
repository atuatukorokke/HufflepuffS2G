// Grid.cs
//
// �p�Y���̃}�X�ڂ��쐬���܂�
// 6*6�ł̐���\��ł�
//

using UnityEngine;

class Grid : MonoBehaviour
{
    // �V���O���g���C���X�^���X
    public static Grid Instance { get; private set; }
    
    // �����̒l��ύX����ƃO���b�h�͈̔͂��ς��
    // �O���b�h�̕��ƍ���
    public static int width = 10;
    public static int height = 20;

    // �O���b�h���i�[����2�����z��
    public static Transform[,] grid;
    private const float lineOffset = -0.5f;     // ������ς���ƃp�Y���̕\�������ʒu���ς��

    private void Awake()
    {
        // �V���O���g���̃C���X�^���X�����ɑ��݂���ꍇ�͂��̃I�u�W�F�N�g��j��
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // �C���X�^���X��ݒ肵�ADontDestroyOnLoad�ŃV�[���ԂŔj������Ȃ��悤�ɂ���
            Instance = this;
            grid = new Transform[width, height];
            DontDestroyOnLoad(gameObject);
        }
    }

    // �x�N�g���𐮐��Ɋۂ߂�
    public Vector2 RoundVector2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // �w�肳�ꂽ�ʒu���O���b�h�͈͓̔��ɂ��邩���`�F�b�N
    public bool InsideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < width &&
                (int)pos.y >= 0);
    }

    // �w�肵���s���폜
    public void DeleteRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void DecreaseRow(int y)
    {
        for (int x = 0; x < width; ++x)
        {
            if (grid[x, y] != null)
            {
                // �u���b�N����i������
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    // -----

    // �w�肵���s����̍s�����ׂĈ�i������
    public void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < height; ++i)
            DecreaseRow(i);
    }

    // �s�����S�ɖ��܂��Ă��邩���`�F�b�N
    public bool IsRowFull(int y)
    {
        for (int x = 0; x < width; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }

    // ���S�ɖ��܂����s���폜���A��̍s����i������
    public void DeleteFullRows()
    {
        for (int y = 0; y < height; ++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y;
            }
        }
    }

    // -----

    // �O���b�h�̏����X�V
    public void UpdateGrid(Transform t)
    {
        for (int y = 0; y < height; ++y)
            for (int x = 0; x < width; ++x)
                if (grid[x, y] != null)
                    if (grid[x, y].parent == t)
                        grid[x, y] = null;

        foreach (Transform child in t)
        {
            Vector2 v = RoundVector2(child.position);
            grid[(int)v.x, (int)v.y] = child;
        }
    }

    // �����͐�΂���
    // ���E����`�悷�郁�\�b�h��ǉ�
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // �����E��     �����A����
        Gizmos.DrawLine(new Vector3(lineOffset, lineOffset, 0), new Vector3(lineOffset, height + lineOffset, 0));
        // �E���E��     �E���A�E��
        Gizmos.DrawLine(new Vector3(width + lineOffset, lineOffset, 0), new Vector3(width + lineOffset, height + lineOffset, 0));
        // �����E��     �����A�E��
        Gizmos.DrawLine(new Vector3(lineOffset, lineOffset, 0), new Vector3(width + lineOffset, lineOffset, 0));
        // �㋫�E��     ����A�E��
        Gizmos.DrawLine(new Vector3(lineOffset, height + lineOffset, 0), new Vector3(width + lineOffset, height + lineOffset, 0));
    }
}