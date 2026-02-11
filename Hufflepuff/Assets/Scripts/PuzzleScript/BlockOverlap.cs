// ========================================
//
// BlockOverlap.cs
//
// ========================================
//
// お邪魔ブロックが生成された際に、既に同じ位置にブロックが
// 重なっていないかを確認するクラス。
// ・重なっていた場合はそのブロックを削除し、代わりに新しいブロックを生成
// ・DeathCount による死亡ブロック数の管理も行う
//
// ========================================

using UnityEngine;
using UnityEngine.Rendering;

public class BlockOverlap : MonoBehaviour
{
    [SerializeField] private PieceCreate pieceCreate; // ブロック生成管理
    [SerializeField] private DeathCount deathCount;   // 死亡ブロック数管理
    public Vector2Int blockPosition;                  // ブロックの座標

    /// <summary>
    /// Start 時に重なりを確認し、重なっていたら削除＋再生成
    /// </summary>
    private void Start()
    {
        pieceCreate = FindAnyObjectByType<PieceCreate>();
        deathCount = FindAnyObjectByType<DeathCount>();

        // -----------------------------------------
        // 自身の位置に重なっているコライダーを全取得
        // -----------------------------------------
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(
            transform.position,
            GetComponent<BoxCollider2D>().bounds.size,
            0f
        );

        // -----------------------------------------
        // 取得したコライダーを順番にチェック
        // -----------------------------------------
        foreach (var col in overlaps) // ← 重なっている全オブジェクトを確認
        {
            // -----------------------------------------
            // 自分自身ではなく、タグが "block" のものと重なっていた場合
            // -----------------------------------------
            if (col.gameObject != this.gameObject && col.CompareTag("block"))
            {
                deathCount.SetBlockCount(-1); // 死亡ブロック数を減らす
                pieceCreate.BlockCreate();    // 新しいブロックを生成
                Destroy(gameObject);          // 自身を削除
            }
        }
    }
}
