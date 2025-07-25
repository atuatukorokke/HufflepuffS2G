using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void LoadScene()
    {
        SceneManager.LoadScene("BulletScene");
    }

    public void GetStartAnim()
    {
        animator.SetBool("GetStart", true);
    }
}
