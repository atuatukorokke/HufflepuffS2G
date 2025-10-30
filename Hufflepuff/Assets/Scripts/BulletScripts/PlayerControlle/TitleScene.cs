using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject FadeInPanel;    // �t�F�[�h�C���p�̃p�l��
    [SerializeField] private Animator animator;         // �t�F�[�h�C���p�̃A�j���[�^�[

    void Start()
    {
        FadeInPanel.SetActive(false);
        
    }

    /// <summary>
    /// �^�C�g���A�j���[�V�������Đ����܂�
    /// </summary>
    public void TitleAnim()
    {
        FadeInPanel.SetActive(true);
        animator.SetBool("FadeIn", true);
    }

    /// <summary>
    /// �^�C�g���V�[����ǂݍ��݂܂�
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("Title");
    }
}
