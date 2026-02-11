// ========================================
//
// PuzzleController.cs
//
// ========================================
//
// プレイヤーの入力を受け取り、パズル操作を管理するクラス。
// ・Zキーでピースの設置判定を実行
// ・設置可能ならお邪魔ブロック削除処理を呼び出す
// ・仮バフリスト（ProvisionalBuffs）はショップ確定前の一時保存用
//
// ========================================

using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;             // ピースの設置可否判定
    [SerializeField] private DestroyBlock destroyBlock;         // お邪魔ブロック削除処理
    [SerializeField] private PlayrController playerController;  // プレイヤー情報
    public List<Buff> ProvisionalBuffs = new List<Buff>();      // バフリスト（ショップ確定前）

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayrController>();
    }

    private void Update()
    {
        // -----------------------------------------
        // Zキーでピースを設置しようとする
        // -----------------------------------------
        if (Input.GetKeyDown(KeyCode.Z) && playerController.Playstate == PlayState.Puzzle)
        {
            pieceMoves.PiecePossible(); // 設置可能かどうかを判定

            // -----------------------------------------
            // 設置可能ならお邪魔ブロック削除処理を実行
            // -----------------------------------------
            if (pieceMoves.GetPiecePossible())
            {
                destroyBlock.DestroyPieceBlock();
            }
        }
    }
}
