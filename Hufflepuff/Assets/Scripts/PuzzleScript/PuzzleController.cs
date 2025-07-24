// PuzzleController.cs
// 
// プレイヤーからの入力を受け取り、
// 他のパズル用スクリプトを発火します。
// 

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    private PieceMoves Pmoves;      // パズルピースを動かすスクリプト
    private PieceCreate Pcreate;    // パズルピースを生成するスクリプト

    int CountRotate = 0;    // 回転数をカウントする変数

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Pmoves == null) Pmoves = FindAnyObjectByType<PieceMoves>();
            Pmoves.PiecePossible(CountRotate);
            CountRotate = 0; // 回転数をリセット
        }

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Pmoves == null) Pmoves = FindAnyObjectByType<PieceMoves>();
            CountRotate = Pmoves.PieceRotation(CountRotate);
            Pmoves = null;
            Destroy(Pmoves, 0.1f); // PieceMovesスクリプトを削除
        }


        // デバッグ用にキーを押したらピースを生成する
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pcreate.NewPiece(0);
            Start();
        }

    }
}
