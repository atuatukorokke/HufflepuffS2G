// ========================================
//
// PieceCreate.cs
//
// ========================================
//
// ピース生成・プレゼント生成・お邪魔ブロック生成を管理するクラス。
// ・ショップ購入時のピース生成
// ・プレゼントボックスからのランダム生成
// ・お邪魔ブロックのランダム配置
// ・盤面管理（BlockBoard）
// ・生成時のカウント更新、SE 再生、UI 更新
//
// ========================================

using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PieceCreate : MonoBehaviour
{
    [Header("生成ピース")]
    [SerializeField] private GameObject[] pieces;

    [Header("スクリプト参照")]
    [SerializeField] private DeathCount deathCount;             // ピース数・ブロック数管理
    [SerializeField] private GoldManager goldManager;           // ゴールド管理
    [SerializeField] private PieceMoves pieceMoves;             // ピースの重なり判定
    [SerializeField] private DestroyBlock destroyBlock;         // ブロック削除処理
    [SerializeField] private PlayrController playerController;  // プレイヤー
    private ClearCountSet clearCountSet;

    private int[] pieceCountNum = { 1, 2, 3, 4, 5, 6, 9, 1 };   // ピースごとのマス数

    private AudioSource audio;
    [SerializeField] private AudioClip blockCreateSE;
    [SerializeField] private AudioClip notBlockCreateSE;

    [SerializeField] private TextMeshProUGUI goldText;

    [Header("ブロック生成可否")]
    public bool isBlockCreate = true;

    // 盤面（5×5）
    public int[,] BlockBoard = new int[5, 5]
    {
        {1, 0, 0, 0, 1},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0},
        {1, 0, 0, 0, 1}
    };

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
        clearCountSet = FindAnyObjectByType<ClearCountSet>();
        playerController = FindAnyObjectByType<PlayrController>();
    }

    private void Start()
    {
        goldText.text = $"今回のコイン:<color=#ffd700>{goldManager.GetGold()}</color>";
    }

    /// <summary>
    /// 新しいピースを生成する
    /// </summary>
    public void NewPiece(int pieceNumber, int buyLate)
    {
        int rndMino = pieceNumber - 1;

        // ランダム生成（デバッグ用）
        if (pieceNumber == 0)
        {
            rndMino = 3;
        }

        Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

        if (isBlockCreate)
        {
            if (goldManager.GetGold() >= buyLate)
            {
                isBlockCreate = false;
                audio.PlayOneShot(blockCreateSE);

                goldManager.SetGoldCount(-buyLate);
                Instantiate(pieces[rndMino], pos, Quaternion.identity);

                clearCountSet.PieceUseCount(rndMino);
                deathCount.SetPieceCount(pieceCountNum[rndMino]);
            }
            else
            {
                audio.PlayOneShot(notBlockCreateSE);
            }

            goldText.text = $"今回のコイン:<color=#ffd700>{goldManager.GetGold()}</color>";
        }
    }

    /// <summary>
    /// プレゼントからピースを生成する
    /// </summary>
    public void PresentBox()
    {
        if (playerController.PieceCount > 0 && isBlockCreate)
        {
            audio.PlayOneShot(blockCreateSE);
            isBlockCreate = false;

            playerController.PieceCount -= 1;

            int rndMino = Random.Range(0, pieces.Length - 1);
            Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

            Instantiate(pieces[rndMino], pos, Quaternion.identity);

            clearCountSet.PieceUseCount(rndMino);
            deathCount.SetPieceCount(pieceCountNum[rndMino]);
        }
        else
        {
            audio.PlayOneShot(notBlockCreateSE);
        }
    }

    /// <summary>
    /// お邪魔ブロックを生成する
    /// </summary>
    public void BlockCreate()
    {
        List<Vector2Int> blockPositions = new List<Vector2Int>();

        // 空きマスをリスト化
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (BlockBoard[i, j] == 0)
                {
                    blockPositions.Add(new Vector2Int(i, j));
                }
            }
        }

        // ランダムな空きマスを選択
        Vector2Int randomIndex = blockPositions[Random.Range(0, blockPositions.Count)];

        BlockBoard[randomIndex.x, randomIndex.y] = 1;

        // 生成位置
        Vector3 pos = new Vector3(-28.0f - randomIndex.x, -7.0f - randomIndex.y, 0.0f);

        GameObject Trash = Instantiate(pieces[pieces.Length - 1], pos, Quaternion.identity);
        clearCountSet.PieceUseCount(pieces.Length - 1);

        Trash.GetComponent<BlockOverlap>().blockPosition = new Vector2Int(randomIndex.x, randomIndex.y);

        deathCount.SetBlockCount(pieceCountNum[pieceCountNum.Length - 1]);

        // ピースが置ける状態ならブロック削除処理
        if (pieceMoves.GetPiecePossible())
        {
            destroyBlock.DestroyPieceBlock();
        }
    }

    /// <summary>
    /// お邪魔ブロックの盤面を初期化する
    /// </summary>
    public void BlockBoardInitialize()
    {
        BlockBoard = new int[5, 5]
        {
            {1, 0, 0, 0, 1},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {1, 0, 0, 0, 1}
        };
    }
}
