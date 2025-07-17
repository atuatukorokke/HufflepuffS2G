using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject panel;
    void Start()
    {
        panel.SetActive(false);
    }

    /// <summary>
    /// �A�j���[�V�����𔭐������܂�
    /// </summary>
    public void TutorialAnim()
    {
        animator.SetBool("Open",true);
    }

    /// <summary>
    /// �p�l����\�������܂�
    /// </summary>
    public void Tutorialobj()
    {
        panel.SetActive(true);
    }
}
