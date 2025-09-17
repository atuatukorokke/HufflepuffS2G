using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [Header("金額")]
    [SerializeField] private int goldCount = 0;    // ピース数

    void Start()
    {
        // デバッグ用の金額
        SetGoldCount(100);
    }

    void Update()
    {
        
    }

    public void SetGoldCount(int newGoldCount)
    {
        goldCount = goldCount + newGoldCount;
    }

    public int GetGold()
    {
        return goldCount;
    }
}
