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

    int[,] Imino0 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 0, 0 },     // □□□□
            { 1, 1, 1, 1 },     // ■■■■
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Imino1 = new int[4, 4] {
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 0, 0 } };   // □■□□
    int[,] Imino2 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 1, 1, 1, 1 },     // ■■■■
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Imino3 = new int[4, 4] {
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 1, 0 } };   // □□■□

    int[,] Jmino0 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 1, 1 },     // □■■■
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Jmino1 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 0, 0 } };   // □■□□
    int[,] Jmino2 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 1, 1, 1, 0 },     // ■■■□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Jmino3 = new int[4, 4] {
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Lmino0 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 1, 0 },     // □□■□
            { 1, 1, 1, 0 },     // ■■■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Lmino1 = new int[4, 4] {
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Lmino2 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 1 },     // □■■■
            { 0, 1, 0, 0 },     // □■□□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Lmino3 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 1, 0 } };   // □□■□

    int[,] Omino = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 0, 0 } };   // □□□□

    int[,] Smino0 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 1, 1, 0, 0 },     // ■■□□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Smino1 = new int[4, 4] {
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Smino2 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 1, 1 },     // □□■■
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Smino3 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 1, 0 } };   // □□■□

    int[,] Tmino0 = new int[4, 4] {
            { 0, 1, 0, 0 },     // □■□□
            { 1, 1, 1, 0 },     // ■■■□
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Tmino1 = new int[4, 4] {
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 1, 1 },     // □□■■
            { 0, 0, 1, 0 },     // □□■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Tmino2 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 1 },     // □■■■
            { 0, 0, 1, 0 } };   // □□■□
    int[,] Tmino3 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 0, 0 },     // □■□□
            { 1, 1, 0, 0 },     // ■■□□
            { 0, 1, 0, 0 } };   // □■□□

    int[,] Zmino0 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 1, 1 },     // □□■■
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Zmino1 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 0, 0, 1, 0 },     // □□■□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 1, 0, 0 } };   // □■□□
    int[,] Zmino2 = new int[4, 4] {
            { 0, 0, 0, 0 },     // □□□□
            { 1, 1, 0, 0 },     // ■■□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 0, 0 } };   // □□□□
    int[,] Zmino3 = new int[4, 4] {
            { 0, 1, 0, 0 },     // □■□□
            { 0, 1, 1, 0 },     // □■■□
            { 0, 0, 1, 0 },     // □□■□
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

        // 現在操作しているピースのタグ情報からピースの形を返却するために入れる
        switch (inGameObject.tag)
        {
            case "Imino":
                switch (inz)
                {
                    case 0:
                        PieceShape = Imino0;
                        break;
                    case 1:
                        PieceShape = Imino1;
                        break;
                    case 2:
                        PieceShape = Imino2;
                        break;
                    case 3:
                        PieceShape = Imino3;
                        break;
                }
                break;
            case "Jmino":
                switch (inz)
                {
                    case 0:
                        PieceShape = Jmino0;
                        break;
                    case 1:
                        PieceShape = Jmino1;
                        break;
                    case 2:
                        PieceShape = Jmino2;
                        break;
                    case 3:
                        PieceShape = Jmino3;
                        break;
                }
                break;
            case "Lmino":
                switch (inz)
                {
                    case 0:
                        PieceShape = Lmino0;
                        break;
                    case 1:
                        PieceShape = Lmino1;
                        break;
                    case 2:
                        PieceShape = Lmino2;
                        break;
                    case 3:
                        PieceShape = Lmino3;
                        break;
                }
                break;
            case "Omino":
                PieceShape = Omino;
                break;
            case "Smino":
                switch (inz)
                {
                    case 0:
                        PieceShape = Smino0;
                        break;
                    case 1:
                        PieceShape = Smino1;
                        break;
                    case 2:
                        PieceShape = Smino2;
                        break;
                    case 3:
                        PieceShape = Smino3;
                        break;
                }
                break;
            case "Tmino":
                switch (inz)
                {
                    case 0:
                        PieceShape = Tmino0;
                        break;
                    case 1:
                        PieceShape = Tmino1;
                        break;
                    case 2:
                        PieceShape = Tmino2;
                        break;
                    case 3:
                        PieceShape = Tmino3;
                        break;
                }
                break;
            case "Zmino":
                switch (inz)
                {
                    case 0:
                        PieceShape = Zmino0;
                        break;
                    case 1:
                        PieceShape = Zmino1;
                        break;
                    case 2:
                        PieceShape = Zmino2;
                        break;
                    case 3:
                        PieceShape = Zmino3;
                        break;
                }
                break;
            default:
                Debug.Log("タグが読み取れてない");
                break;
        }

        return PieceShape;
    }
}
