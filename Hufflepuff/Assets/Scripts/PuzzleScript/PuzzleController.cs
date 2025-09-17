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
    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト
    [SerializeField] private DestroyBlock destroyBlock; // ブロックを消すスクリプト
    public List<Buff> ProvisionalBuffs = new List<Buff>();

    private void Start()
    {

    }

    void Update()
    {
        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z))
        {
            pieceMoves.PiecePossible();

            if (pieceMoves.GetPiecePossible())
            {
                destroyBlock.DestroyPieceBlock();
            }
        }
    }
}
