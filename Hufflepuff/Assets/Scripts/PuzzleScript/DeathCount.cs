// DeathCount.cs
// 
// �s�[�X���Ƃ��ז��u���b�N�𐔂��A���ʂ��̔�����s���܂�
// 

using UnityEngine;

public class DeathCount : MonoBehaviour
{
    [SerializeField] private GameObject GameOberPanel; // �Q�[���I�[�o�[�p�l��
    [SerializeField] private GameObject EnemySummoningManager;
    [SerializeField] private PlayrController playerController; // �v���C���[�̃R���g���[���[

    [Header("���S����")]
    [SerializeField] private bool isDead = false; // false = �����Ă�, true = ����ł�

    [Header("�u���b�N��")]
    [SerializeField] private int pieceCount = 0;    // �s�[�X��
    [SerializeField] private int blockCount = 0;    // ���ז��u���b�N��

    void Start()
    {
        GameOberPanel.SetActive(false);
        playerController = FindAnyObjectByType<PlayrController>();
        // �f�o�b�O�p�̃s�[�X��
        SetPieceCount(5); // 21�̔{��
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
            // �Q�[���I�[�o�[�̏���
            EnemySummoningManager.GetComponent<AudioSource>().Stop(); // �G�̉����~�߂�
            Time.timeScale = 0f; // �Q�[�����~
            GameOberPanel.SetActive(true);
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