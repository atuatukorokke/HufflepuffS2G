// PieceCreate.cs
// 
// スクリプト呼び出した時にパズルピースをランダム生成（パズルには必ず当てはめることが可能）する
// 

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    [Header("スクリプトをアタッチ")]
    [SerializeField] private DeathCount deathCount;     // 死ぬかの判定を行うスクリプト
    [SerializeField] private GoldManager goldManager;     // 金額管理を行うスクリプト
    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト
    [SerializeField] private DestroyBlock destroyBlock; // ブロックを消すスクリプト
    [SerializeField] private PlayrController playerController; // プレイヤーのコントローラー

    private AudioSource audio;
    [SerializeField] private AudioClip blockCreateSE;
    [SerializeField] private AudioClip notBlockCreateSE;

    [SerializeField] private TextMeshProUGUI goldText;

    [Header("ブロックを置けるかの判定")]
    public bool isBlockCreate = true; 

    public int[,] BlockBoard = new int[5, 5]
    {{1, 0, 0, 0, 1},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {1, 0, 0, 0, 1}};

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        playerController = FindAnyObjectByType<PlayrController>();
        goldText.text = $"残りのコイン:<color=#ffd700>{goldManager.GetGold().ToString()}</color>";
    }

    /// <summary>
    /// 新しいピースを生成します
    /// </summary>
    /// <param name="pieceNumber">0 = ランダム生成, 1 ~ 7 = 対応したピースを生成</param>
    public void NewPiece(int pieceNumber, int buyLate)
    {
        int rndMino = pieceNumber;
        // 入力が０の時1~7の整数をランダムで生成
        if (pieceNumber == 0)
        {
            //int rndMino = Random.Range(1, 8);
            rndMino = 3; // デバッグ用に2を固定
        }

        // 生成位置
        Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

        if(isBlockCreate)
        {
            // プレハブを指定位置に生成
            switch (rndMino)
            {
                case 1:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino1, pos, Quaternion.identity);
                        deathCount.SetPieceCount(1);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 2:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino2, pos, Quaternion.identity);
                        deathCount.SetPieceCount(2);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 3:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino3, pos, Quaternion.identity);
                        deathCount.SetPieceCount(3);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 4:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino4, pos, Quaternion.identity);
                        deathCount.SetPieceCount(4);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 5:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino5, pos, Quaternion.identity);
                        deathCount.SetPieceCount(5);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 6:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino6, pos, Quaternion.identity);
                        deathCount.SetPieceCount(6);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 7:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino9, pos, Quaternion.identity);
                        deathCount.SetPieceCount(9);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                default:
                    Debug.Log("なんか変な値が出てるやでー");
                    break;
            }
            goldText.text = $"残りのコイン:<color=#ffd700>{goldManager.GetGold().ToString()}</color>";
        }
    }

    /// <summary>
    /// 乱数でピースを作成します
    /// </summary>
    public void PresentBox()
    {
        if(playerController.PieceCount > 0 && isBlockCreate)
        {
            audio.PlayOneShot(blockCreateSE);
            isBlockCreate = false;
            playerController.PieceCount -= 1;
            int rndMino = Random.Range(1, 8);

            // 生成位置
            Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

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
                    break;
            }
        }
        else
        {
            audio.PlayOneShot(notBlockCreateSE);
        }
        
    }

    // お邪魔ブロックを生成(呼び出すように変更)
    public void BlockCreate()
    {
        List<Vector2Int> blockPositions = new List<Vector2Int>(); // 空いている位置のリスト

        // 空いている位置を検索->リストに追加
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (BlockBoard[i, j] == 0)
                {
                    blockPositions.Add(new Vector2Int(i, j));
                }
            }
        }

        Vector2Int randomIndex = blockPositions[Random.Range(0, blockPositions.Count)]; // リストからランダムに選択

        /*while (isBlock == false)
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
        }*/

        BlockBoard[randomIndex.x, randomIndex.y] = 1; // 盤面情報を更新

        // 生成位置 盤面の左上を指定
        Vector3 pos = new Vector3(-28.0f - randomIndex.x, -7.0f - randomIndex.y, 0.0f);
        GameObject Trash = Instantiate(block, pos, Quaternion.identity);
        Trash.GetComponent<BlockOverlap>().blockPosition = new Vector2Int(randomIndex.x, randomIndex.y);

        deathCount.SetBlockCount(1);

        if (pieceMoves.GetPiecePossible())
        {
            destroyBlock.DestroyPieceBlock();
        }
    }

    // お邪魔ブロックの配置情報を初期化
    public void BlockBoardInitialize()
    {
        BlockBoard = new int[5, 5]
        {{1, 0, 0, 0, 1},
         {0, 0, 0, 0, 0},
         {0, 0, 0, 0, 0},
         {0, 0, 0, 0, 0},
         {1, 0, 0, 0, 1}};
    }
}