// ========================================
//
// GoldManager.cs
//
// ========================================
//
// 所持金（ゴールド）の管理を行うクラス。
// ・SetGoldCount() でゴールドを加算し UI に反映
// ・GetGold() で現在の所持金を取得
//
// ========================================

using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [Header("所持金")]
    [SerializeField] private int goldCount = 0;              // 所持金
    [SerializeField] private TextMeshProUGUI goldText;       // 所持金テキスト

    private void Start()
    {
        goldCount = 0;
    }

    /// <summary>
    /// 所持金を加算して UI に反映する
    /// </summary>
    /// <param name="newGoldCount">増加するコイン量</param>
    public void SetGoldCount(int newGoldCount)
    {
        goldCount = goldCount + newGoldCount;
        goldText.text = $"今回のコイン:<color=#ffd700>{goldCount}</color>";
    }

    /// <summary>
    /// 現在の所持金を返す
    /// </summary>
    public int GetGold()
    {
        return goldCount;
    }
}
