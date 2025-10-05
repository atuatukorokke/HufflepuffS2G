// MinoCheck.cs
// 
// ピースが盤面外やほかのピースを重なっている時にPieceMovesに値を送ります
// 

using UnityEngine;

public class MinoCheck : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト

    void Start()
    {
        // このスクリプトがアタッチされているオブジェクトを生成したときにオブジェクトを取得する
        pieceMoves = Object.FindFirstObjectByType<PieceMoves>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pieceMoves.SetColliding(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pieceMoves.SetColliding(-1);
    }
}
