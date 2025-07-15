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
    private PieceMoves Pmoves;  // パズルピースを動かすスクリプトを呼び出す用
    private PieceCreate Pcreate;    // デバッグ用にピースを生成するスクリプトを呼び出す

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        /*
        // 矢印キーでパズルピースを移動
        if (Input.GetKey(KeyCode.RightArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.LeftArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.UpArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.DownArrow)) Pmoves.PieceMove();
        */

        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z)) Pmoves.PiecePossible();

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X)) Pmoves.PieceRotation();


        // デバッグ用にキーを押したらピースを生成する
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pcreate.NewPiece();
            Pmoves = FindAnyObjectByType<PieceMoves>();
        }

    }
}
