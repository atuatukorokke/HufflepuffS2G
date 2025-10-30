// SceneTransition.cs
// 
// シーン遷移を行います
// 

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// メインシーンを読み込みます
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("BulletScene");
    }

    /// <summary>
    /// スタートアニメーションを再生します   
    /// </summary>
    public void GetStartAnim()
    {
        animator.SetBool("GetStart", true);
    }

    /// <summary>
    /// パネルを非表示にします
    /// </summary>
    public void PanelActive()
    {
        gameObject.SetActive(false);
    }
}
