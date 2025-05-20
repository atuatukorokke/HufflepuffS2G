using UnityEngine;

public class Tetromino : MonoBehaviour
{
    // �����Ɋւ��鎞�Ԃ̕ϐ�
    float fall = 0;
    public float fallSpeed = 1; // �������x

    void Update()
    {
        CheckUserInput();
    }
    // ���[�U�[���͂��`�F�b�N���ău���b�N���ړ��܂��͉�]������
    void CheckUserInput()
    {
        // �����L�[�������ꂽ�ꍇ
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // �u���b�N�����Ɉړ�
            transform.position += new Vector3(-1, 0, 0);
            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
                transform.position += new Vector3(1, 0, 0); // �ʒu�������Ȃ猳�ɖ߂�
            else
                Grid.Instance.UpdateGrid(transform); // �ʒu���L���Ȃ�O���b�h���X�V
        }
        // �E���L�[�������ꂽ�ꍇ
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // �u���b�N���E�Ɉړ�
            transform.position += new Vector3(1, 0, 0);
            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
                transform.position += new Vector3(-1, 0, 0); // �ʒu�������Ȃ猳�ɖ߂�
            else
                Grid.Instance.UpdateGrid(transform); // �ʒu���L���Ȃ�O���b�h���X�V
        }
        // ����L�[�������ꂽ�ꍇ
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �u���b�N����]
            transform.Rotate(0, 0, -90);
            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
                transform.Rotate(0, 0, 90); // �ʒu�������Ȃ猳�ɖ߂�
            else
                Grid.Instance.UpdateGrid(transform); // �ʒu���L���Ȃ�O���b�h���X�V
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            // �u���b�N����i���Ɉړ�
            transform.position += new Vector3(0, -1, 0);
            // �ʒu���L�����`�F�b�N
            if (!IsValidGridPos())
            {
                // �ʒu�������Ȃ猳�ɖ߂�
                transform.position += new Vector3(0, 1, 0);
                // �O���b�h���X�V
                Grid.Instance.UpdateGrid(transform);
                // ���S�ɖ��܂����s���폜
                Grid.Instance.DeleteFullRows();
                // �V�����e�g���~�m�𐶐�
                FindObjectOfType<Spawner>().SpawnNext();
                enabled = false;
            }
            else
            {
                Grid.Instance.UpdateGrid(transform);
            }
            // �������Ԃ����Z�b�g
            fall = Time.time;
        }
    }
    // �O���b�h���ł̈ʒu���L�����ǂ����𔻒肷��
    bool IsValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.Instance.RoundVector2(child.position);

            if (!Grid.Instance.InsideBorder(v))
                return false;

            if (Grid.grid[(int)v.x, (int)v.y] != null &&
                Grid.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }

    void UpdateGrid()
    {
        for (int y = 0; y < Grid.height; ++y)
            for (int x = 0; x < Grid.width; ++x)
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;

        foreach (Transform child in transform)
        {
            Vector2 v = Grid.Instance.RoundVector2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }
}