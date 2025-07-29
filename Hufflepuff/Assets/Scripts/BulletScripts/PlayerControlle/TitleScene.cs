using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject FadeInPanel; // �t�F�[�h�C���p�̃p�l��
    [SerializeField] private Animator animator; // �t�F�[�h�C���p�̃A�j���[�^�[

    void Start()
    {
        FadeInPanel.SetActive(false);
        
    }

    public void TitleAnim()
    {
        FadeInPanel.SetActive(true);
        animator.SetBool("FadeIn", true);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Title");
    }
}
