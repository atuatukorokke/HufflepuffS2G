using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    private int currentTutorialIndex;
    private int maxTutorialIndex;

    [Header("0.�ړ����@\r\n1.�U�����@\r\n2.�K�E�Z\r\n3.�G��|������\r\n4.�p�Y������\r\n5.�o�t�֘A�i���j")]
    [SerializeField] private Sprite[] TutorialImage; // �`���[�g���A���摜�i�[
    [SerializeField] private string[] TutorialExplanation; // �`���[�g���A���������i�[
    [SerializeField] private Sprite MainTutorialImage; // ���C���`���[�g���A���摜
    [SerializeField] private TextMeshProUGUI MainTutorialExplanation; // ���C���`���[�g���A��������
    [SerializeField] private GameObject startButton; // �X�^�[�g�{�^��

    private void Start()
    {
        startButton.SetActive(false);
        maxTutorialIndex = TutorialImage.Length - 1;
        currentTutorialIndex = 0;
        SetTutorial(currentTutorialIndex);
    }

    /// <summary>
    /// �`���[�g���A���̃C���f�b�N�X���X�V���A���̃`���[�g���A����\�����܂��B
    /// </summary>
    public void IndexUpdate()
    {
        currentTutorialIndex++;
        currentTutorialIndex = (int)Mathf.Repeat(currentTutorialIndex, maxTutorialIndex);
        SetTutorial(currentTutorialIndex);
        if(currentTutorialIndex == maxTutorialIndex) startButton.SetActive(true);
    }

    /// <summary>
    /// �`���[�g���A���̃C���f�b�N�X���X�V���A�O�̃`���[�g���A����\�����܂��B
    /// </summary>
    public void IndexDown()
    {
        currentTutorialIndex--;
        currentTutorialIndex = (int)Mathf.Repeat(currentTutorialIndex, maxTutorialIndex);
        SetTutorial(currentTutorialIndex);
    }

    /// <summary>
    /// �w�肳�ꂽ�`���[�g���A���ԍ��Ɋ�Â��āA���C���̃`���[�g���A���摜�Ɛ�������ݒ肵�܂��B
    /// </summary>
    /// <param name="TutorialNumber">T�`���[�g���A���ԍ�</param>
    public void SetTutorial(int TutorialNumber)
    {
        MainTutorialImage = TutorialImage[TutorialNumber];
        MainTutorialExplanation.text = TutorialExplanation[TutorialNumber];
    }
}
