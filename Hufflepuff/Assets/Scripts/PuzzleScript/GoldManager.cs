// GoldManager.cs
// 
// 所持金を管理します
// 

using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [Header("金額")]
    [SerializeField] private int goldCount = 0;    // ピース数
    [SerializeField] private TextMeshProUGUI goldText; // 所持金テキスト

    void Start()
    {
        goldCount = 0;
    }

    /// <summary>
    /// 所持金を設定します
    /// </summary>
    /// <param name="newGoldCount">コインの増加量</param>
    public void SetGoldCount(int newGoldCount)
    {
        goldCount = goldCount + newGoldCount;
        goldText.text = $"残りのコイン:<color=#ffd700>{goldCount.ToString()}</color>";

    }

    /// <summary>
    /// 現在の所持金を取得します
    /// </summary>
    /// <returns>今のコインの所持量</returns>
    public int GetGold()
    {
        return goldCount;
    }
}
