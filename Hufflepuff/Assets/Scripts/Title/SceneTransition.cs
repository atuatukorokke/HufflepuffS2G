using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadScene("BulletScene");
    }
}
