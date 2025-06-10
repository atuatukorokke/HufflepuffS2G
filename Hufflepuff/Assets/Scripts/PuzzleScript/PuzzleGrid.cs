// PuzzleGrid.cs
// 
// パズルピースを置けるかの確認をします
// 

using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    private PieceList PList;   // パズルピースの形を確認するよう

    // グリッドサイズ
    private const int width = 7;
    private const int height = 7;

    // グリッドの状態を管理する配列
    private bool[,] grid;

    private void Start()
    {
        // グリッド生成とマス目の初期化
        InitializeGrid();
        PList = FindAnyObjectByType<PieceList>();
    }

    /// <summary>
    /// グリッドの初期化（すべて空いている状態にする）
    /// </summary>
    private void InitializeGrid()
    {
        grid = new bool[width, height];

        // すべてにfalseを入れる処理
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = false;
            }
        }
    }

    /// <summary>
    /// ピースが配置可能かを判定します
    /// </summary>
    /// <param name="inx">オブジェクトのx座標</param>
    /// <param name="iny">オブジェクトのy座標</param>
    /// <returns>ture = 配置できた false = 配置できない</returns>
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
