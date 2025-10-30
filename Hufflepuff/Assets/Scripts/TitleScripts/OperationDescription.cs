// OperationDescription.cs
//
// �I�v�V�����֌W���Ǘ����܂�
//

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OperationDescription : MonoBehaviour
{
    [SerializeField] private GameObject panel;          // �����p�l��
    [SerializeField] private Animator gameAanimator;    // �Q�[���A�j���[�^�[
    [SerializeField] private Animator optionAnimator;   // �I�v�V�����A�j���[�^�[
    private bool optionCheck;

    private AudioSource aidio;
    [SerializeField] private AudioClip ButtonTouch;

    private void Start()
    {
        aidio = GetComponent<AudioSource>();
        gameAanimator = GetComponent<Animator>();
        panel.SetActive(false);
        optionCheck = true;
    }

    /// <summary>
    /// �����p�l����\�����܂�
    /// </summary>
    public void ShowDescription()
    {
        panel.SetActive(true);
    }
    /// <summary>
    /// �Q�[���A�j���[�V�������Đ����܂�
    /// </summary>
    public void GaemAnim()
    {
        aidio.PlayOneShot(ButtonTouch);
        gameAanimator.SetBool("OptionOpen", true);
    }

    /// <summary>
    /// �I�v�V�����p�l�����J���܂�
    /// </summary>
    public void OptionOpen()
    {
        aidio.PlayOneShot(ButtonTouch);
        optionAnimator.SetBool("Open", optionCheck);
        optionCheck = !optionCheck;
    }

}
