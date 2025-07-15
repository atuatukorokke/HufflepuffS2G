// PieceList.cs
// 
// 各パズルピースの形の情報を保存します
// 列挙体で制御するのでパズルピースの名前で呼び出すことができるようにしたいです
// 

using JetBrains.Annotations;
using UnityEngine;

public class PieceList : MonoBehaviour
{
    int[,] mino1 = new int[3, 5] {
            { 1, 0, 0, 0, 0 },  // ■□□□□
            { 0, 0, 0, 0, 0 },  // □□□□□
            { 0, 0, 0, 0, 0 } };// □□□□□

    int[,] mino2 = new int[3, 5] {
            { 1, 1, 0, 0, 0 },  // ■■□□□
            { 0, 0, 0, 0, 0 },  // □□□□□
            { 0, 0, 0, 0, 0 } };// □□□□□

    int[,] mino3 = new int[3, 5] {
            { 1, 1, 0, 0, 0 },  // ■■□□□
            { 1, 0, 0, 0, 0 },  // ■□□□□
            { 0, 0, 0, 0, 0 } };// □□□□□

    int[,] mino4 = new int[3, 5] {
            { 1, 1, 0, 0, 0 },  // ■■□□□
            { 1, 1, 0, 0, 0 },  // ■■□□□
            { 0, 0, 0, 0, 0 } };// □□□□□

    int[,] mino5 = new int[3, 5] {
            { 1, 1, 1, 1, 1 },  // ■■■■■
            { 0, 0, 0, 0, 0 },  // □□□□□
            { 0, 0, 0, 0, 0 } };// □□□□□

    int[,] mino6 = new int[3, 5] {
            { 1, 1, 1, 0, 0 },  // ■■■□□
            { 1, 1, 1, 0, 0 },  // ■■■□□
            { 0, 0, 0, 0, 0 } };// □□□□□

    int[,] mino9 = new int[3, 5] {
            { 1, 1, 1, 0, 0 },  // ■■■□□
            { 1, 1, 1, 0, 0 },  // ■■■□□
            { 1, 1, 1, 0, 0 } };// ■■■□□
    
    /// <summary>
    /// 引数として渡されたゲームオブジェクトのタグでパズルピースの形を返す
    /// </summary>
    /// <param name="inGameObject">パズルピースをGameObjectで入れてください</param>
    /// <returns>タグを参照してピースの形を二次元配列で返却</returns>
    public int[,] PieceShapes(GameObject inGameObject, int inz)
    {
        int[,] PieceShape = new int[3, 5] {
            { 0, 0, 0, 0, 0 },  // □□□□□
            { 0, 0, 0, 0, 0 },  // □□□□□
            { 0, 0, 0, 0, 0 } };// □□□□□

        // 現在操作しているピースのタグ情報からピースの形を返却するために入れる
        switch (inGameObject.tag)
        {
            case "1mino":
                PieceShape = mino1;
                break;
            case "2mino":
                PieceShape = mino2;
                break;
            case "3mino":
                PieceShape = mino3;
                break;
            case "4mino":
                PieceShape = mino4;
                break;
            case "5mino":
                PieceShape = mino5;
                break;
            case "6mino":
                PieceShape = mino6;
                break;
            case "9mino":
                PieceShape = mino9;
                break;
            default:
                Debug.Log("タグが読み取れてない");
                break;
        }

        return PieceShape;
    }
}
