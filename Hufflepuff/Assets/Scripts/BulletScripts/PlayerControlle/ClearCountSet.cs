// ========================================
//
// ClearCountSet.cs
//
// ========================================
//
// クリア画面でピースの使用数・売却数を集計し、
// 合計スコアからランク（S?D）を算出して表示するクラス。
// ・pieceCount[] に各ピースの使用数を記録
// ・scoreTable に基づいてスコア計算
// ・ランク文字と色を rankText に反映
//
// ========================================

using TMPro;
using UnityEngine;

public class ClearCountSet : MonoBehaviour
{
    [SerializeField] private string[] pieceName;              // ピース名
    [SerializeField] private TextMeshProUGUI[] pieceCountTxt; // ピース数表示
    [SerializeField] private int[] pieceCount;                // 使用数カウント

    [Header("スコア表示用")]
    [SerializeField] private TextMeshProUGUI rankText;        // ランク表示
    private int[] scoreTable = { 5, 10, 15, 20, 25, 20, 15, -10 }; // ピースごとのスコア

    /// <summary>
    /// ピース使用数を加算
    /// </summary>
    public void PieceUseCount(int id)
    {
        pieceCount[id]++;
    }

    /// <summary>
    /// ピース売却数を減算（0未満にはしない）
    /// </summary>
    public void PieceSellCount(int id)
    {
        pieceCount[id]--;
        if (pieceCount[id] < 0)
            pieceCount[id] = 0;
    }

    /// <summary>
    /// ピース数とランクを画面に反映
    /// </summary>
    public void SetPieceCount()
    {
        for (int i = 0; i < pieceCountTxt.Length; i++)
        {
            pieceCountTxt[i].text = $"{pieceName[i]}{pieceCount[i]}";
        }

        int score = CalculateScore();
        string rank = GetRank(score);

        rankText.text = $"RANK\n{rank}";
        rankText.color = GetRankColor(rank);
    }

    /// <summary>
    /// 合計スコアを計算
    /// </summary>
    private int CalculateScore()
    {
        int score = 0;

        for (int i = 0; i < pieceCount.Length; i++)
        {
            score += pieceCount[i] * scoreTable[i];
        }

        return score;
    }

    /// <summary>
    /// スコアからランクを判定
    /// </summary>
    private string GetRank(int score)
    {
        if (score >= 100) return "S";
        if (score >= 80) return "A";
        if (score >= 50) return "B";
        if (score >= 20) return "C";
        return "D";
    }

    /// <summary>
    /// ランクに応じた色を返す
    /// </summary>
    private Color GetRankColor(string rank)
    {
        switch (rank)
        {
            case "S": return new Color(1f, 0.84f, 0f);   // 金
            case "A": return new Color(0.6f, 0.2f, 1f);  // 紫
            case "B": return new Color(0.2f, 0.4f, 1f);  // 青
            case "C": return new Color(0.2f, 1f, 0.4f);  // 緑
            case "D": return new Color(1f, 0.2f, 0.2f);  // 赤
        }
        return Color.white;
    }
}
