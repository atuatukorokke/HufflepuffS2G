using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject FadeInPanel; // フェードイン用のパネル
    [SerializeField] private Animator animator; // フェードイン用のアニメーター

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
