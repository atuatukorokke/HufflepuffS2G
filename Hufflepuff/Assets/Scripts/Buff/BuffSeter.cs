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
                    player.Attack = player.Attack * buff.value; // �U���͂̑���
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
                case BuffForID.PuzzleTime:
                    buffName = "�p�Y�����ԉ���";
                    buffExplanationText = $"�p�Y�����Ԃ��{<color=#ffd700>{buff.value}�b</color>";
                    buffIcon = PuzzleTimeIcon;
                    // �p�Y�����ԉ����̏����������ɒǉ�
                    break;
                case BuffForID.CarryOverSpecialGauge:
                    buffName = "�X�y�V�����Q�[�W�����z��";
                    buffExplanationText = $"�X�y�V�����Q�[�W��<color=#ffd700>{buff.value * 100}��</color>�����z���\��";
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
