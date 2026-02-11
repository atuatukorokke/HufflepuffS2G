// ========================================
//
// DeathCount.cs
//
// ========================================
//
// ピース数とお邪魔ブロック数を管理し、死亡判定を行うクラス。
// ・ピース数とブロック数を加算管理
// ・ブロック率（ブロック数 ÷ ピース数）を UI に反映
// ・20% を超えたら死亡判定 → ゲームオーバー演出
//
// ========================================

using UnityEngine;
using TMPro;

public class DeathCount : MonoBehaviour
{
    [SerializeField] private Animator CanvasAnim;               // メインキャンバスのアニメーター
    [SerializeField] private GameObject EnemySummoningManager;  // 敵管理オブジェクト
    [SerializeField] private PlayrController playerController;  // プレイヤー
    [SerializeField] private TextMeshProUGUI goldText;          // 所持金テキスト
    [SerializeField] private TextMeshProUGUI deathLateText;     // 死亡率テキスト

    [Header("死亡判定")]
    [SerializeField] private bool isDead = false;               // false = 生存, true = 死亡

    [Header("ブロック数")]
    [SerializeField] private int pieceCount = 0;                // ピース数
    [SerializeField] private int blockCount = 0;                // お邪魔ブロック数

    public int PieceCount { get => pieceCount; private set => pieceCount = value; }
    public int BlockCount { get => blockCount; private set => blockCount = value; }

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayrController>();

        // デバッグ用初期値
        SetPieceCount(21); // 21の倍数
        SetBlockCount(0);
    }

    /// <summary>
    /// ピース数を加算する
    /// </summary>
    public void SetPieceCount(int newPieceCount)
    {
        PieceCount = PieceCount + newPieceCount;
    }

    /// <summary>
    /// お邪魔ブロック数を加算し、死亡判定を行う
    /// </summary>
    public void SetBlockCount(int newBlockCount)
    {
        BlockCount = BlockCount + newBlockCount;

        // -----------------------------------------
        // 死亡率（ブロック率）を UI に反映
        // -----------------------------------------
        deathLateText.text =
            $"お邪魔:<color=#ff0000>{((int)((float)BlockCount / (float)PieceCount * 100)).ToString()}%</color>";

        // -----------------------------------------
        // 死亡判定（ブロック数がピース数の20%を超えたら死亡）
        // -----------------------------------------
        if (PieceCount * 0.2 < BlockCount)
        {
            isDead = true;

            // ゲームオーバー処理
            EnemySummoningManager.GetComponent<AudioSource>().Stop(); // 敵の音を停止
            Time.timeScale = 0.0f;                                    // ゲーム停止
            CanvasAnim.SetTrigger("isDead");                          // 死亡アニメーション
        }
        else
        {
            isDead = false;
        }
    }

    /// <summary>
    /// ピース数 + ブロック数の合計を返す
    /// </summary>
    public int GetTotalBlock()
    {
        return PieceCount + BlockCount;
    }
}
