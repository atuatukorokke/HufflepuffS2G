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
    private int[,] grid;

    private void Start()
    {
        // グリッド生成とマス目の初期化
        InitializeGrid();
        PList = FindAnyObjectByType<PieceList>();
    }

    /// <summary>
    /// パズル盤面の初期化（すべて空いている状態にする）
    /// </summary>
    private void InitializeGrid()
    {
        grid = new int[width, height];

        // すべてにfalseを入れる処理
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0;
            }
        }
        // デバッグ用　消えてなかったら消してください
        grid[3, 2] = 1;
    }

    /// <summary>
    /// ピースが配置可能かを判定します
    /// </summary>
    /// <param name="inGameObject">オブジェクト情報</param>
    /// <param name="inx">パズル画面の左上を0, 0として現在のx座標を入れてください</param>
    /// <param name="iny">パズル画面の左上を0, 0として現在のy座標を入れてください</param>
    /// <param name="inz">ピースの回転角度を入れてください</param>
    /// <returns>配置可能 = true　配置不可 = false</returns>
    public bool PuzzleCheck(GameObject inGameObject ,float inx, float iny, float inz)
    {
        Debug.Log("inx:" + inx);
        Debug.Log("iny:" + iny);
        // 座標の小数点を除外
        if (inx % 1 != 0 || iny % 1 != 0) return false;
        
        int pTestx = (int)(inx);
        int pTesty = (int)(iny * -1);

        // ピースの形の情報をPieceShapeに入れます
        int[,] PieceShape = new int[4, 4];
        PieceShape = PList.PieceShapes(inGameObject);


        // パズルピースが範囲外に出ていないかを確認します
        switch (inz)
        {

            case 0:
                // 時計   0°回転
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        // x方向にピースが範囲外に出ていないかを確認
                        if (PieceShape[j, i] == 1 && ((pTestx + i) > width - 1 || (pTestx + i) < 0))
                        {
                            Debug.Log("x範囲外です");
                            return false;
                        }
                        // y方向にピースが範囲外に出ていないかを確認
                        if (PieceShape[j, i] == 1 && ((pTesty + j) > height - 1 || (pTesty + j) < 0))
                        {
                            Debug.Log("y範囲外です");
                            return false;
                        }
                        if (pTestx + j <= 0 || pTestx + j >= width || pTestx + j <= 0 || pTesty + i >= height) break;
                        if (grid[i + pTesty, j + pTestx] == 1 && PieceShape[i, j] == 1)
                        {
                            Debug.Log("ピースの上には配置できません");
                            return false;
                        }
                    }
                }
                break;
            case 1:
                // 時計  90°回転
                // 現在書き換え中
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (PieceShape[j, i] == 1 && ((pTestx + i) > width - 1 || (pTestx + i) < 0))
                        {
                            Debug.Log("範囲外です");
                            return false;
                        }
                        if (PieceShape[i, j] == 1 && ((pTesty + i) > height - 1 || (pTesty + i) < 0))
                        {
                            Debug.Log("範囲外です");
                            return false;
                        }
                    }
                }
                break;
            case 2:
                // 時計 180°回転
                Debug.Log("未実装やね");
                break;
            case 3:
                // 時計 270°回転
                Debug.Log("未実装やね");
                break;
            default:
                Debug.Log("ばぐってるやでー");
                break;
        }

        /*
        // パズルピースの配置箇所に既にピースが置かれていないか確認
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (i + pTestx > width || j + pTesty > height) break;
                if (grid[i + pTestx, j + pTesty] == 1 && PieceShape[j, i] == 1)
                {
                    Debug.Log("ピースの上には配置できません");
                    return false;
                }
            }
        }
        */

        return true;
    }
}
