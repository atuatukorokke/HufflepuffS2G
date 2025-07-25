// PieceMoves.cs
// 
// パズルピースの動きを制御をします
// 

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PieceMoves : MonoBehaviour
{
    private PuzzleGrid Pgrid;   // パズルピースを配置するスクリプトを呼び出す
    private PieceCreate Pcreate;    // パズルピースを生成するスクリプト

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// PuzzleGridを呼び出しパズルピースが配置可能かを確認します
    /// </summary>
    public void PiecePossible()
    {

    }
}