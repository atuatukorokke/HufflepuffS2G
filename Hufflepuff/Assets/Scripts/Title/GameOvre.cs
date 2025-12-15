using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOvre : MonoBehaviour
{
    /// <summary>
    /// リトライします
    /// </summary>
    public void Retry()
    {
        if(Time.timeScale == 0) Time.timeScale = 1f;
        SceneManager.LoadScene("MainGameScene");
    }

    /// <summary>
    /// タイトルに戻ります
    /// </summary>
    public void Title()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}
