using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player; // �v���C���[�ɑ΂��Ċe�ϐ��̒l�̕ύX���s��
    [SerializeField] BuffManager buffManager; // �o�t���e�̓��������X�g�^�̕ϐ����Q�Ƃ���

    [SerializeField] private float attack;
    [SerializeField] private float invincibleTime;
    [SerializeField] private float puzzleTime;
    [SerializeField] private float carryOverSpecialGauge;

    public float Attack { get => attack; private set => attack = value; }
    public float InvincibleTime { get => invincibleTime; private set => invincibleTime = value; }
    public float PuzzleTime { get => puzzleTime; private set => puzzleTime = value; }
    public float CarryOverSpecialGauge { get => carryOverSpecialGauge; private set => carryOverSpecialGauge = value; }


    /// <summary>
    /// �e�o�t�̓K���������܂�
    /// </summary>
    public void ApplyBuffs()
    {
        foreach(var buff in buffManager.datas)
        {
            switch(buff.buffID)
            {
                case BuffForID.AtackMethod:
                    player.Attack += player.Attack * buff.value; // �U���͂̑���
                    Attack = player.Attack;
                    break;
                case BuffForID.InvincibleTime:
                    player.InvincibleTime += buff.value; // ���G���Ԃ̑���
                    InvincibleTime = player.InvincibleTime;
                    break;
                case BuffForID.PuzzleTime:
                    PuzzleTime++;
                    break;
                case BuffForID.CarryOverSpecialGauge:
                    CarryOverSpecialGauge++;
                    break;
            }
        }

        // ���X�g�̃N���A
        buffManager.datas.Clear();
    }

    private void Start()
    {
        player = GetComponent<PlayrController>();
        buffManager = FindAnyObjectByType<BuffManager>();
    }
}
