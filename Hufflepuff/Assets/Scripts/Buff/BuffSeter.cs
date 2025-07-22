using Mono.Cecil.Cil;
using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player; // �v���C���[�ɑ΂��Ċe�ϐ��̒l�̕ύX���s��
    [SerializeField] BuffManager buffManager; // �o�t���e�̓��������X�g�^�̕ϐ����Q�Ƃ���

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
                    break;
                case BuffForID.InvincibleTime:
                    break;
                case BuffForID.PuzzleTime:
                    break;
                case BuffForID.CarryOverSpecialGauge:
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
