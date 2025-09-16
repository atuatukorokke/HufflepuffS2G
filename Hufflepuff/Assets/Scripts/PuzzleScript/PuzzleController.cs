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

    [SerializeField] private DeathCount deathCount;     // 死ぬかの判定を行うスクリプト

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();

        //deathCount.SetPieceCount(50);   // デバッグ用のピース数
    }

    void Update()
    {
        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Pmoves == null) Pmoves = FindAnyObjectByType<PieceMoves>();
            Pmoves.PiecePossible();
        }

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X))
        {

        }
    }
}
