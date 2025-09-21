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

    void Update()
    {
        
    }

    public void SetGoldCount(int newGoldCount)
    {
        goldCount = goldCount + newGoldCount;
        goldText.text = $"残りのコイン:<color=#ffd700>{goldCount.ToString()}</color>";

    }

    public int GetGold()
    {
        return goldCount;
    }
}
