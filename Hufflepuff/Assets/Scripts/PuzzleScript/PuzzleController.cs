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
    [SerializeField] private BuffManager buffManager;
    [SerializeField] private PieceCreate Pcreate;    // パズルピースを生成するスクリプト

    int CountRotate = 0;    // 回転数をカウントする変数

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        buffManager = FindAnyObjectByType<BuffManager>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //if (Pmoves == null)
            //{
            //    Pmoves = FindAnyObjectByType<PieceMoves>();
            //    Pmoves.PiecePossible();
            //}
            if(Pcreate.IsCreate)
            {
                buffManager.AddBuff(Pcreate.BuffID, Pcreate.BuffValue);
                Pcreate.IsCreate = false;
            }
        }

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
