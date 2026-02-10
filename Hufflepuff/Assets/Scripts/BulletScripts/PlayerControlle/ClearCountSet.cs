// ========================================
// 
// ClearCountSet.cs
// 
// ========================================
// 
// クリア時にピースの種類と数とのスコアを表示させます。
// 
// ========================================

using TMPro;
using UnityEngine;

public class ClearCountSet : MonoBehaviour
{
    [SerializeField] private string[] pieceName;
    [SerializeField] private TextMeshProUGUI[] pieceCountTxt;
    [SerializeField] private int[] pieceCount;

    [Header("スコア表示用")]
    [SerializeField] private TextMeshProUGUI rankText;
    private int[] scoreTable = { 5, 10, 15, 20, 25, 20, 15, -10};

    public void PieceUseCount(int id)
    {
        pieceCount[id]++;
    }

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

    public void PieceSellCount(int id)
    {
        pieceCount[id]--;

        if (pieceCount[id] < 0)
            pieceCount[id] = 0;
    }

    private int CalculateScore()
    {
        int score = 0;

        for(int i = 0; i < pieceCount.Length; i++)
        {
            score += pieceCount[i] * scoreTable[i];
        }
        return score;
    }

    private string GetRank(int score)
    {
        if (score >= 100) return "S";
        if (score >= 80) return "A";
        if (score >= 50) return "B";
        if (score >= 20) return "C";
        return "D";
    }

    private Color GetRankColor(string rank)
    {
        switch(rank)
        {
            case "S": return new Color(1f, 0.84f, 0f); // 金色
            case "A": return new Color(0.6f, 0.2f, 1f); // 紫
            case "B": return new Color(0.2f, 0.4f, 1f); // 青
            case "C": return new Color(0.2f, 1f, 0.4f); // 緑
            case "D": return new Color(1f, 0.2f, 0.2f); // 赤
        }
        return Color.white;
    }
}
