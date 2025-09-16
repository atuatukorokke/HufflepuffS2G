// PieceCreate.cs
// 
// スクリプト呼び出した時にパズルピースをランダム生成（パズルには必ず当てはめることが可能）する
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("各種ピース")]
    [SerializeField] public GameObject mino1;
    [SerializeField] public GameObject mino2;
    [SerializeField] public GameObject mino3;
    [SerializeField] public GameObject mino4;
    [SerializeField] public GameObject mino5;
    [SerializeField] public GameObject mino6;
    [SerializeField] public GameObject mino9;

    [SerializeField] public GameObject block;

    [SerializeField] private DeathCount deathCount;     // 死ぬかの判定を行うスクリプト

    private int[,] BlockBoard = new int[5, 5]
    {{1, 0, 0, 0, 1},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {1, 0, 0, 0, 1}};

    /// <summary>
    /// 新しいピースを生成します
    /// </summary>
    /// <param name="x">0 = ランダム生成, 1 ~ 7 = 対応したピースを生成</param>
    public void NewPiece(int x)
    {
        int rndMino = x;
        // 入力が０の時1~7の整数をランダムで生成
        if (x == 0)
        {
            //int rndMino = Random.Range(1, 8);
            rndMino = 3; // デバッグ用に2を固定
        }

        // 生成位置
        Vector3 pos = new Vector3(-40.0f, -10.0f, 0.0f);

        // プレハブを指定位置に生成
        switch (rndMino)
        {
            case 1:
                Instantiate(mino1, pos, Quaternion.identity);
                deathCount.SetPieceCount(1);
                break;
            case 2:
                Instantiate(mino2, pos, Quaternion.identity);
                deathCount.SetPieceCount(2);
                break;
            case 3:
                Instantiate(mino3, pos, Quaternion.identity);
                deathCount.SetPieceCount(3);
                break;
            case 4:
                Instantiate(mino4, pos, Quaternion.identity);
                deathCount.SetPieceCount(4);
                break;
            case 5:
                Instantiate(mino5, pos, Quaternion.identity);
                deathCount.SetPieceCount(5);
                break;
            case 6:
                Instantiate(mino6, pos, Quaternion.identity);
                deathCount.SetPieceCount(6);
                break;
            case 7:
                Instantiate(mino9, pos, Quaternion.identity);
                deathCount.SetPieceCount(9);
                break;
            default:
                Debug.Log("なんか変な値が出てるやでー");
                break;
        }
    }

    // お邪魔ブロックを生成(呼び出すように変更)
    public void BlockCreate()
    {
        bool isBlock = false;

        int BlockRndX = 0;
        int BlockRndY = 0;

        while(isBlock == false)
        {
            isBlock = true;

            BlockRndX = Random.Range(0, 5);
            BlockRndY = Random.Range(0, 5);

            if (BlockBoard[BlockRndY, BlockRndX] == 1)
            {
                isBlock = false;
            }

            if (BlockRndX == 0 && BlockRndY == 0)
            {
                isBlock = false;
            }

            if (BlockRndX == 0 && BlockRndY == 4)
            {
                isBlock = false;
            }

            if (BlockRndX == 4 && BlockRndY == 0)
            {
                isBlock = false;
            }

            if (BlockRndX == 4 && BlockRndY == 4)
            {
                isBlock = false;
            }
        }

        Debug.Log(BlockRndX);
        Debug.Log(BlockRndY);

        Debug.Log(BlockBoard[BlockRndY, BlockRndX]);

        BlockBoard[BlockRndY, BlockRndX] = 1;

        // 生成位置 盤面の左上を指定
        Vector3 pos = new Vector3(3.0f + BlockRndX, 2.0f - BlockRndY, 0.0f);
        Instantiate(block, pos, Quaternion.identity);

        deathCount.SetBlockCount(1);
    }
}
