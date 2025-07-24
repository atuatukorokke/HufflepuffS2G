// PuzzleGrid.cs
// 
// パズルピースを置けるかの確認をします
// 

using NUnit.Framework.Interfaces;
using UnityEngine;

public class PuzzleGrid : MonoBehaviour
{
    private PieceList PList;   // パズルピースの形を確認するよう

    // グリッドサイズ      ※注　この変数でUnity上の枠線は制御していません、直接いじってください
    private const int width = 5;
    private const int height = 5;

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
        grid = new int[height, width];

        // すべてにfalseを入れる処理
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = 0;
            }
        }

        grid[0, 0] = 99;                    // 左上の位置を配置不可にする
        grid[0, width - 1] = 99;            // 右上の位置を配置不可にする
        grid[height - 1, 0] = 99;           // 左下の位置を配置不可にする
        grid[height - 1, width - 1] = 99;   // 右下の位置を配置不可にする
    }

    /// <summary>
    /// ピースが配置可能かを判定します
    /// </summary>
    /// <param name="inGameObject">オブジェクト情報</param>
    /// <param name="inx">パズル画面の左上を0, 0として現在のx座標を入れてください</param>
    /// <param name="iny">パズル画面の左上を0, 0として現在のy座標を入れてください</param>
    /// <param name="inz">ピースの回転角度を入れてください</param>
    /// <returns>配置可能 = true　配置不可 = false</returns>
    public bool PuzzleCheck(GameObject inGameObject ,float inx, float iny, int inz)
    {
        // 移動中を許さない
        if (inx % 1 != 0 || iny % 1 != 0) return false;
        
        // 扱いやすいように座標データを整数化,もしかしたらいらないかも？要検証
        int pTestx = (int)(inx);
        int pTesty = (int)(iny * -1);

        // ピースの形の情報をPieceShapeに入れます
        int[,] PieceShape = new int[3, 5];
        PieceShape = PList.PieceShapes(inGameObject, inz);


        // 時計回りに0度回転しています
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (PieceShape[i, j] == 1)
                {
                    // x方向にピースが範囲外に出ていないかの検証
                    if ((pTestx + j) < 0 || (pTestx + j) > width - 1)
                    {
                        Debug.Log("x範囲外です");
                        return false;
                    }

                    // y方向にピースが範囲外に出ていないかの検証
                    if ((pTesty + i) < 0 || (pTesty + i) > height - 1)
                    {
                        Debug.Log("y範囲外です");
                        return false;
                    }
                }
                
                if ((pTestx + j) > width - 1 || (pTestx + j) < 0) break;
                if ((pTesty + i) > height - 1 || (pTesty + i) < 0) break;
                // すでに置かれているオブジェクトの上に置かないようにする処理
                if (grid[i + pTesty, j + pTestx] > 0 && PieceShape[i, j] > 0)
                {
                    Debug.Log("ピースの上には配置できません");
                    return false;
                }
            }
        }


        // パズルを配置
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
        Debug.Log("配置できました");

        return true;
    }
}
