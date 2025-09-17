using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [Header("���z")]
    [SerializeField] private int goldCount = 0;    // �s�[�X��

    void Start()
    {
        // �f�o�b�O�p�̋��z
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
