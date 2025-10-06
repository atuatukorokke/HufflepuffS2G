// SceneTransition.cs
// 
// ƒV[ƒ“‘JˆÚ‚ğs‚¢‚Ü‚·
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

    public void LoadScene()
    {
        SceneManager.LoadScene("BulletScene");
    }

    public void GetStartAnim()
    {
        animator.SetBool("GetStart", true);
    }
    public void PanelActive()
    {
        gameObject.SetActive(false);
    }
}
