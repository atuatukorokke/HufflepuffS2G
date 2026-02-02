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
        if (score >= 3000) return "S";
        if (score >= 2000) return "A";
        if (score >= 1000) return "B";
        if (score >= 500) return "C";
        return "D";
    }
}
