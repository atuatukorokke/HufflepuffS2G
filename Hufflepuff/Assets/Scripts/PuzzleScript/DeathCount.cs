// DeathCount.cs
// 
// �s�[�X���Ƃ��ז��u���b�N�𐔂��A���ʂ��̔�����s���܂�
// 

using UnityEngine;

public class DeathCount : MonoBehaviour
{

    [Header("���S����")]
    [SerializeField] private bool isDead = false; // false = �����Ă�, true = ����ł�

    [Header("�u���b�N��")]
    [SerializeField] private int pieceCount = 0;    // �s�[�X��
    [SerializeField] private int blockCount = 0;    // ���ז��u���b�N��

    void Start()
    {
        // �f�o�b�O�p�̃s�[�X��
        SetPieceCount(21); // 21�̔{��
        SetBlockCount(0);
    }

    void Update()
    {

    }

    public void SetPieceCount(int newPieceCount)
    {
        pieceCount = pieceCount + newPieceCount;
    }

    public void SetBlockCount(int newBlockCount)
    {
        blockCount = blockCount + newBlockCount;

        if (pieceCount * 0.2 < blockCount)
        {
            isDead = true;  // �u���b�N�����s�[�X����20%�𒴂����玀��
            Debug.Log("����");
        }
        else
        {
            isDead = false; // ����ȊO�͐����Ă�
        }
    }

    public int GetTotalBlock()
    {
        return pieceCount + blockCount;
    }

}