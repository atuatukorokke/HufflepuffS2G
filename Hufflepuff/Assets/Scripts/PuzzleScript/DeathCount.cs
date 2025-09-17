// DeathCount.cs
// 
// ピース数とお邪魔ブロックを数え、死ぬかの判定を行います
// 

using UnityEngine;

public class DeathCount : MonoBehaviour
{

    [Header("死亡判定")]
    [SerializeField] private bool isDead = false; // false = 生きてる, true = 死んでる

    [Header("ブロック数")]
    [SerializeField] private int pieceCount = 0;    // ピース数
    [SerializeField] private int blockCount = 0;    // お邪魔ブロック数

    void Start()
    {
        // デバッグ用のピース数
        SetPieceCount(21); // 21の倍数
        SetBlockCount(0);
    }

    void Update()
    {

    }

    public void SetPieceCount(int newPieceCount)
    {
        pieceCount = pieceCount + newPieceCount;
    }

    public void SetBlockCount(int newBlockCount)
    {
        blockCount = blockCount + newBlockCount;

        if (pieceCount * 0.2 < blockCount)
        {
            isDead = true;  // ブロック数がピース数の20%を超えたら死ぬ
            Debug.Log("死んだ");
        }
        else
        {
            isDead = false; // それ以外は生きてる
        }
    }

    public int GetTotalBlock()
    {
        return pieceCount + blockCount;
    }

}