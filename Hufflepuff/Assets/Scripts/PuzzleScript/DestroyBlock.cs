// ========================================
//
// DestroyBlock.cs
//
// ========================================
//
// プレイヤーが特定条件を満たしたとき、指定タグのブロックを
// 一括削除するクラス。
// ・総ブロック数（ピース＋お邪魔）が 21 の倍数なら全削除
// ・削除後は盤面を初期化
//
// ========================================

using UnityEngine;

public class DestroyBlock : MonoBehaviour
{
    [Header("スクリプト参照")]
    [SerializeField] private DeathCount deathCount;   // ピース数・お邪魔ブロック数の管理
    [SerializeField] private PieceMoves pieceMoves;   // ブロックの重なり判定などを行うスクリプト
    [SerializeField] private PieceCreate pieceCreate; // ピース生成スクリプト

    public string targetTag = "block"; // 削除対象のタグ

    /// <summary>
    /// 総ブロック数が 21 の倍数なら、対象タグのブロックを全削除する
    /// </summary>
    public void DestroyPieceBlock()
    {
        int TotalBlock = deathCount.GetTotalBlock();

        // -----------------------------------------
        // 21 の倍数かどうかを判定
        // -----------------------------------------
        if (TotalBlock % 21 == 0)
        {
            // -----------------------------------------
            // 指定タグのオブジェクトをすべて取得
            // -----------------------------------------
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(targetTag);

            // -----------------------------------------
            // 取得したブロックを順番に削除
            // -----------------------------------------
            foreach (GameObject obj in objectsToDelete) // ← 全ブロックを1つずつ処理
            {
                Destroy(obj);
            }

            // -----------------------------------------
            // ブロック盤面を初期化
            // -----------------------------------------
            pieceCreate.BlockBoardInitialize();
        }
    }
}
