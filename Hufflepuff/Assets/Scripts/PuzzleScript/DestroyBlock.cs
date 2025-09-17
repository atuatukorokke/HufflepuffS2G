// DestroyBlock.cs
// 
// 呼び出されたときタグにBlockがついているオブジェクトをすべて消します
// 

using UnityEngine;

public class DestroyBlock : MonoBehaviour
{

    [Header("スクリプトをアタッチ")]
    [SerializeField] private DeathCount deathCount;     // 死ぬかの判定を行うスクリプト
    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト
    [SerializeField] private PieceCreate pieceCreate;

    public string targetTag = "block"; // 削除したいタグ名を指定

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 呼び出した時ブロック総数が21の倍数ならブロックを消す
    public void DestroyPieceBlock()
    {
        int TotalBlock = deathCount.GetTotalBlock();
        if (TotalBlock % 21 == 0)
        {
            GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag(targetTag);

            foreach (GameObject obj in objectsToDelete)
            {
                Destroy(obj);
            }

            pieceCreate.BlockBoardInitialize();
        }
    }

}
