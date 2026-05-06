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
        // -----------------------------------------
        // パズル領域にあるブロックの数をカウントする
        // -----------------------------------------
        GameObject[] allBlocks = GameObject.FindGameObjectsWithTag(targetTag);
        int placedBlockCount = 0;
        
        PuzzleController pc = Object.FindAnyObjectByType<PuzzleController>();
        float borderX = pc != null ? pc.PuzzleBorderX : 0f;

        foreach (var block in allBlocks)
        {
            if (block.transform.position.x > borderX)
            {
                int count = 0;
                
                // ObjectDragTransformが付いていればそのPieceCountを取得
                if (block.TryGetComponent<ObjectDragTransform>(out var dragTransform))
                {
                    count = dragTransform.PieceCount;
                }

                // お邪魔ブロックなどPieceCountが0、あるいはコンポーネントが無い場合は1マスとして扱う
                if (count == 0)
                {
                    count = 1;
                }

                placedBlockCount += count;
            }
        }

        // -----------------------------------------
        // パズル領域のブロックが21個（以上）なら盤面を更新
        // -----------------------------------------
        if (placedBlockCount >= 21)
        {
            if (pc != null)
            {
                pc.SaveCurrentBoardBuffs();
                Debug.Log("消去前にバフを確定リストに保存しました。");
            }

            // パズル領域にあるブロックのみを消去
            foreach (GameObject obj in allBlocks)
            {
                if (obj.transform.position.x > borderX)
                {
                    Destroy(obj);
                }
            }

            // -----------------------------------------
            // 盤面初期化
            // -----------------------------------------
            pieceCreate.BlockBoardInitialize();
        }
    }
}
