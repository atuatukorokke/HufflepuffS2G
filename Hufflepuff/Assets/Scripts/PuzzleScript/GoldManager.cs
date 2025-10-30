// GoldManager.cs
// 
// ���������Ǘ����܂�
// 

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

    /// <summary>
    /// ��������ݒ肵�܂�
    /// </summary>
    /// <param name="newGoldCount">�R�C���̑�����</param>
    public void SetGoldCount(int newGoldCount)
    {
        goldCount = goldCount + newGoldCount;
        goldText.text = $"�c��̃R�C��:<color=#ffd700>{goldCount.ToString()}</color>";

    }

    /// <summary>
    /// ���݂̏��������擾���܂�
    /// </summary>
    /// <returns>���̃R�C���̏�����</returns>
    public int GetGold()
    {
        return goldCount;
    }
}
