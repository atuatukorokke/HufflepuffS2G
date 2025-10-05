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

    // 所持金を増減させる
    public void SetGoldCount(int newGoldCount)
    {
        goldCount = goldCount + newGoldCount;
        goldText.text = $"残りのコイン:<color=#ffd700>{goldCount.ToString()}</color>";

    }

    // 現在の所持金を取得します
    public int GetGold()
    {
        return goldCount;
    }
}
