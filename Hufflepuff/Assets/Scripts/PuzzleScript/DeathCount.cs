// DeathCount.cs
// 
// ピース数とお邪魔ブロックを数え、死ぬかの判定を行います
// 

using UnityEngine;
using TMPro;

public class DeathCount : MonoBehaviour
{
    [SerializeField] private GameObject GameOberPanel; // ゲームオーバーパネル
    [SerializeField] private GameObject EnemySummoningManager;
    [SerializeField] private PlayrController playerController; // プレイヤーのコントローラー
    [SerializeField] private TextMeshProUGUI goldText; // 所持金テキスト
    [SerializeField] private TextMeshProUGUI deathLateText; // 死亡率テキスト

    [Header("死亡判定")]
    [SerializeField] private bool isDead = false; // false = 生きてる, true = 死んでる

    [Header("ブロック数")]
    [SerializeField] private int pieceCount = 0;    // ピース数
    [SerializeField] private int blockCount = 0;    // お邪魔ブロック数

    void Start()
    {
        GameOberPanel.SetActive(false);
        playerController = FindAnyObjectByType<PlayrController>();
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
        goldText.text = $"残りのコイン:<color=#ffd700>{(playerController.CoinCount).ToString()}</color>";
    }

    public void SetBlockCount(int newBlockCount)
    {
        blockCount = blockCount + newBlockCount;
        deathLateText.text = $"お邪魔:<color=#ff0000>{((int)((float)blockCount / (float)pieceCount * 100)).ToString()}%</color>";

        if (pieceCount * 0.2 < blockCount)
        {
            isDead = true;  // ブロック数がピース数の20%を超えたら死ぬ
            // ゲームオーバーの処理
            EnemySummoningManager.GetComponent<AudioSource>().Stop(); // 敵の音を止める
            Time.timeScale = 0f; // ゲームを停止
            GameOberPanel.SetActive(true);
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