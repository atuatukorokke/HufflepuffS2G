// BlockOverlap.cs
//
// お邪魔ブロック生成時にほかのブロックと重なっていないかを確認する
// もし重なっていたら削除と再生成をする
// PieceCreateのお邪魔カウントを修正する
//　

using UnityEngine;
using UnityEngine.Rendering;

public class BlockOverlap : MonoBehaviour
{
    [SerializeField] private PieceCreate pieceCreate;
    [SerializeField] private DeathCount deathCount; // 死ぬかの判定を行うスクリプト
    public Vector2Int blockPosition; // ブロックの位置

    /// <summary>
    /// Startメソッドで重なりを確認し、重なっていたら削除と再生成を行う
    /// </summary>
    void Start()
    {
        pieceCreate = FindAnyObjectByType<PieceCreate>();
        deathCount = FindAnyObjectByType<DeathCount>();
        Collider2D[] overlaps = Physics2D.OverlapBoxAll(transform.position, GetComponent<BoxCollider2D>().bounds.size, 0f);
        foreach (var col in overlaps)
        {
            if (col.gameObject != this.gameObject && col.CompareTag("block"))
            {
                deathCount.SetBlockCount(-1); // お邪魔ブロック数を減らす
                pieceCreate.BlockCreate(); // ブロックを再生成する
                Destroy(gameObject);
            }
        }
    }
}
