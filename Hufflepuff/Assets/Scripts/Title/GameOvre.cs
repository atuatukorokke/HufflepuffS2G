using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOvre : MonoBehaviour
{
    public void Retry()
    {
        if(Time.timeScale == 0) Time.timeScale = 1f; // ゲームが停止している場合、再開する
        SceneManager.LoadScene("BulletScene");
    }

    public void Title()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f; // ゲームが停止している場合、再開する
        SceneManager.LoadScene("Title");
    }
}
