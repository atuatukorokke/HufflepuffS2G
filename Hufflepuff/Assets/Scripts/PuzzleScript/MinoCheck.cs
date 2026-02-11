// ========================================
//
// MinoCheck.cs
//
// ========================================
//
// ピースが盤面外に出たかどうかを検知し、PieceMoves に状態を通知するクラス。
// ・トリガーに入ったら「衝突中」扱い
// ・トリガーから出たら「衝突解除」扱い
// ・PieceMoves 側で衝突数をカウントして判定に利用
//
// ========================================

using UnityEngine;

public class MinoCheck : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves; // ピースの衝突状態を管理するスクリプト

    private void Start()
    {
        // このスクリプトがアタッチされたオブジェクト生成時に PieceMoves を取得
        pieceMoves = Object.FindFirstObjectByType<PieceMoves>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ピースがトリガー内に入った → 衝突数を +1
        pieceMoves.SetColliding(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ピースがトリガーから出た → 衝突数を -1
        pieceMoves.SetColliding(-1);
    }
}
