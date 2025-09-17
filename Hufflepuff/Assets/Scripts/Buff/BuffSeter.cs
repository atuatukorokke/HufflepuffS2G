using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player; // �v���C���[�ɑ΂��Ċe�ϐ��̒l�̕ύX���s��
    [SerializeField] BuffManager buffManager; // �o�t���e�̓��������X�g�^�̕ϐ����Q�Ƃ���
    [SerializeField] private GameObject buffExplanationObj; // �o�t�̐������s���I�u�W�F�N�g

    [SerializeField] private GameObject buffListObj; // �o�t�̃��X�g��\������I�u�W�F�N�g

    [Header ("�o�t�̃A�C�R��")]
    [SerializeField] private Sprite AttackIcon; // �U���̓A�b�v�̃A�C�R��
    [SerializeField] private Sprite InvincibleIcon; // ���G���ԃA�b�v�̃A�C�R��
    [SerializeField] private Sprite PuzzleTimeIcon; // �p�Y�����ԉ����̃A�C�R��
    [SerializeField] private Sprite CarryOverSpecialGaugeIcon; // �X�y�V�����Q�[�W�����z���̃A�C�R��

    private string buffName; // �o�t�̖��O
    private string buffExplanationText; // �o�t�̐�����
    private Sprite buffIcon; // �o�t�̃A�C�R��

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ApplyBuffs();
        }
    }

    /// <summary>
    /// �e�o�t�̓K���������܂�
    /// </summary>
    public void ApplyBuffs()
    {
        foreach (Transform child in buffListObj.transform)
        {
            Destroy(child.gameObject);
        }


        foreach(var buff in buffManager.datas)
        {
            switch(buff.buffID)
            {
                case BuffForID.AtackMethod:
                    player.Attack = 1 + (buff.value / 100); // �U���͂̑���
                    buffName = "�U���̓A�b�v";
                    buffExplanationText = $"�v���C���[�̍U���́{<color=#ffd700>{buff.value}��</color>";
                    buffIcon = AttackIcon;
                    break;
                case BuffForID.InvincibleTime:
                    player.InvincibleTime = buff.value; // ���G���Ԃ̑���
                    buffName = "���G���ԉ���";
                    buffExplanationText = $"�v���C���[�̖��G���ԁ{<color=#ffd700>{buff.value}�b</color>";
                    buffIcon = InvincibleIcon;
                    break;
                case BuffForID.DamageDownLate:
                    buffName = "���ז��������_�E��";
                    player.OutPieceLate = 100 - buff.value; // ���ז��s�[�X���o��m���̌���
                    buffExplanationText = $"��e���ɂ��ז��s�[�X���o��m����-<color=#ffd700>{buff.value}%</color>";
                    buffIcon = PuzzleTimeIcon;
                    // �p�Y�����ԉ����̏����������ɒǉ�
                    break;
                case BuffForID.CoinGetLate:
                    buffName = "�R�C���擾�ʃA�b�v";
                    player.DefultCoinIncreaseCount = 20 + (int)(20 * buff.value / 100); // �R�C���擾�ʂ̑���
                    buffExplanationText = $"�R�C���̎擾�����{<color=#ffd700>{buff.value}��</color>";
                    buffIcon = CarryOverSpecialGaugeIcon;
                    // �X�y�V�����Q�[�W�����z���̏����������ɒǉ�
                    break;
            }

            // �o�t�̐�����\������
            GameObject buffExplanation = Instantiate(buffExplanationObj, buffExplanationObj.transform);
            buffExplanation.transform.parent = buffListObj.transform;
            buffExplanation.GetComponent<BuffExplanation>().SetBuffExplanation(buffName, buffExplanationText, buffIcon);
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayrController>();
        buffManager = GetComponent<BuffManager>();
    }
}
