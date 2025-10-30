// TutorialManager.cs
//
// �`���[�g���A�������̊Ǘ�
//

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int currentTutorialIndex;
    [SerializeField] private int maxTutorialIndex;

    [Header("0.�ړ����@ �U�����@\r\n1.�K�E�Z\r\n2.�G��|������\r\n3.�p�Y�������o�t�܂�\r\n")]
    [SerializeField] private Sprite[] TutorialImage;                    // �`���[�g���A���摜�i�[
    [SerializeField] private string[] TutorialExplanation;              // �`���[�g���A���������i�[
    [SerializeField] private Image MainTutorialImage;                   // ���C���`���[�g���A���摜
    [SerializeField] private TextMeshProUGUI MainTutorialExplanation;   // ���C���`���[�g���A��������
    [SerializeField] private GameObject startButton;                    // �X�^�[�g�{�^��
    [SerializeField] private GameObject leftButton;                     // ���{�^��
    [SerializeField] private GameObject rightButton;                    // �E�{�^��

    private AudioSource audio;
    [SerializeField] private AudioClip buttonSE;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        startButton.SetActive(false);
        maxTutorialIndex = TutorialImage.Length;
        currentTutorialIndex = 0;
        SetTutorial(currentTutorialIndex);
    }

    private void Update()
    {
        // ���E�{�^���̕\����\���؂�ւ�
        // �ŏ��̃`���[�g���A���̏ꍇ
        if (currentTutorialIndex == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
        }
        // �Ō�̃`���[�g���A���̏ꍇ
        if (currentTutorialIndex == maxTutorialIndex - 1)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(false);
        }

        // ���Ԃ̃`���[�g���A���̏ꍇ
        if (currentTutorialIndex > 0 & currentTutorialIndex < maxTutorialIndex - 1)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }

    /// <summary>
    /// �`���[�g���A���̃C���f�b�N�X���X�V���A���̃`���[�g���A����\�����܂��B
    /// </summary>
    public void IndexUpdate()
    {
        audio.PlayOneShot(buttonSE);
        currentTutorialIndex++;
        currentTutorialIndex = (int)Mathf.Repeat(currentTutorialIndex, maxTutorialIndex);
        SetTutorial(currentTutorialIndex);
        if(currentTutorialIndex == maxTutorialIndex - 1) startButton.SetActive(true);
    }

    /// <summary>
    /// �`���[�g���A���̃C���f�b�N�X���X�V���A�O�̃`���[�g���A����\�����܂��B
    /// </summary>
    public void IndexDown()
    {
        audio.PlayOneShot(buttonSE);
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
        MainTutorialImage.sprite = TutorialImage[TutorialNumber];
        MainTutorialExplanation.text = TutorialExplanation[TutorialNumber];
    }
}
