using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    [Header("���z")]
    [SerializeField] private int goldCount = 0;    // �s�[�X��
    [SerializeField] private TextMeshProUGUI goldText; // �������e�L�X�g

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
        goldText.text = $"�c��̃R�C��:<color=#ffd700>{goldCount.ToString()}</color>";

    }

    public int GetGold()
    {
        return goldCount;
    }
}
