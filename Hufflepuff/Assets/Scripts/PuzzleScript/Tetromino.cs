using UnityEngine;

public class Tetromino : MonoBehaviour
{
    // 落下に関する時間の変数
    float fall = 0;
    public float fallSpeed = 1; // 落下速度

    void Update()
    {
        CheckUserInput();
    }
    // ユーザー入力をチェックしてブロックを移動または回転させる
    void CheckUserInput()
    {
        // 左矢印キーが押された場合
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // ブロックを左に移動
            transform.position += new Vector3(-1, 0, 0);
            // 位置が有効かチェック
            if (!IsValidGridPos())
                transform.position += new Vector3(1, 0, 0); // 位置が無効なら元に戻す
            else
                Grid.Instance.UpdateGrid(transform); // 位置が有効ならグリッドを更新
        }
        // 右矢印キーが押された場合
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // ブロックを右に移動
            transform.position += new Vector3(1, 0, 0);
            // 位置が有効かチェック
            if (!IsValidGridPos())
                transform.position += new Vector3(-1, 0, 0); // 位置が無効なら元に戻す
            else
                Grid.Instance.UpdateGrid(transform); // 位置が有効ならグリッドを更新
        }
        // 上矢印キーが押された場合
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ブロックを回転
            transform.Rotate(0, 0, -90);
            // 位置が有効かチェック
            if (!IsValidGridPos())
                transform.Rotate(0, 0, 90); // 位置が無効なら元に戻す
            else
                Grid.Instance.UpdateGrid(transform); // 位置が有効ならグリッドを更新
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - fall >= fallSpeed)
        {
            // ブロックを一段下に移動
            transform.position += new Vector3(0, -1, 0);
            // 位置が有効かチェック
            if (!IsValidGridPos())
            {
                // 位置が無効なら元に戻す
                transform.position += new Vector3(0, 1, 0);
                // グリッドを更新
                Grid.Instance.UpdateGrid(transform);
                // 完全に埋まった行を削除
                Grid.Instance.DeleteFullRows();
                // 新しいテトリミノを生成
                FindObjectOfType<Spawner>().SpawnNext();
                enabled = false;
            }
            else
            {
                Grid.Instance.UpdateGrid(transform);
            }
            // 落下時間をリセット
            fall = Time.time;
        }
    }
    // グリッド内での位置が有効かどうかを判定する
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