using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOvre : MonoBehaviour
{
    public void Retry()
    {
        if(Time.timeScale == 0) Time.timeScale = 1f; // �Q�[������~���Ă���ꍇ�A�ĊJ����
        SceneManager.LoadScene("BulletScene");
    }

    public void Title()
    {
        if (Time.timeScale == 0) Time.timeScale = 1f; // �Q�[������~���Ă���ꍇ�A�ĊJ����
        SceneManager.LoadScene("Title");
    }
}
