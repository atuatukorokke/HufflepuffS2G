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

    private bool isColliding = false;

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    /// <summary>
    /// 旧　PuzzleGridを呼び出しパズルピースが配置可能かを確認します
    /// 新　コライダーの判定確認で配置出来ているかの確認を行います
    /// </summary>
    public void PiecePossible()
    {
        if (isColliding)
        {
            Debug.Log("おけてない");
        }
        else
        {
            Debug.Log("おけてる");
        }
    }
}