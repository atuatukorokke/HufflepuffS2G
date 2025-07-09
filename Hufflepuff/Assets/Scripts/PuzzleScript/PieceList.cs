// PieceList.cs
// 
// 各パズルピースの形の情報を保存します
// 列挙体で制御するのでパズルピースの名前で呼び出すことができるようにしたいです
// 

using JetBrains.Annotations;
using UnityEngine;

public class PieceList : MonoBehaviour
{
    // 1. モノ（ mono ） · 2. ジ（ di ） · 3. トリ（ tri ） · 4. テトラ（ tetra ）

    int[,] Imino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 0, 0 },     // □□□□
            { 1, 1, 1, 1 },     // ■■■■
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Jmino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 1, 1 },     // □■■■
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Lmino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 1, 0 },     // □□■□
            { 1, 1, 1, 0 },     // ■■■□
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Omino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Smino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 1, 1, 0, 0 },     // ■■□□
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Tmino = new int[4, 4] {
            { 0, 1, 0, 0 },     // □■□□
            { 1, 1, 1, 0 },     // ■■■□
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Zmino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 1, 1 },     // □□■■
            { 0, 0, 0, 0 } };   // □□□□

    /// <summary>
    /// 引数として渡されたゲームオブジェクトのタグでパズルピースの形を返す
    /// </summary>
    /// <param name="inGameObject">パズルピースをGameObjectで入れてください</param>
    /// <returns>タグを参照してピースの形を二次元配列で返却</returns>
    public int[,] PieceShapes(GameObject inGameObject, int inz)
    {
        int[,] PieceShape = new int[4,4] {
            { 00, 01, 02, 03 },
            { 10, 11, 12, 13 },
            { 20, 21, 22, 23 },
            { 30, 31, 32, 33 } };

        /*
        int[,] RotationPieceShape = new int[4, 4] {
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 },
            { 0, 0, 0, 0 } };
        */

        // 現在操作しているピースのタグ情報からピースの形を返却するために入れる
        switch (inGameObject.tag)
        {
            case "Imino":
                PieceShape = Imino;
                break;
            case "Jmino":
                PieceShape = Jmino;
                break;
            case "Lmino":
                PieceShape = Lmino;
                break;
            case "Omino":
                PieceShape = Omino;
                break;
            case "Smino":
                PieceShape = Smino;
                break;
            case "Tmino":
                PieceShape = Tmino;
                break;
            case "Zmino":
                PieceShape = Zmino;
                break;
            default:
                Debug.Log("タグが読み取れてない");
                break;
        }

        /*
        // 配列を回転して渡すための処理
        // inzの回数だけ右方向に回転させる
        for (int z = 0; z < inz; z++)
        {
            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    RotationPieceShape[j, 3 - i] = PieceShape[i, j];
                }
            }
            //PieceShape = RotationPieceShape;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PieceShape[i, j] = RotationPieceShape[i, j];
                }
            }
        }
        */

        return PieceShape;
    }
}
