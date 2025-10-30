using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject FadeInPanel;    // フェードイン用のパネル
    [SerializeField] private Animator animator;         // フェードイン用のアニメーター

    void Start()
    {
        FadeInPanel.SetActive(false);
        
    }

    /// <summary>
    /// タイトルアニメーションを再生します
    /// </summary>
    public void TitleAnim()
    {
        FadeInPanel.SetActive(true);
        animator.SetBool("FadeIn", true);
    }

    /// <summary>
    /// タイトルシーンを読み込みます
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("Title");
    }
}
