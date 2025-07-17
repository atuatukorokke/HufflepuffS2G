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
    /// アニメーションを発生させます
    /// </summary>
    public void TutorialAnim()
    {
        animator.SetBool("Open",true);
    }

    /// <summary>
    /// パネルを表示させます
    /// </summary>
    public void Tutorialobj()
    {
        panel.SetActive(true);
    }
}
