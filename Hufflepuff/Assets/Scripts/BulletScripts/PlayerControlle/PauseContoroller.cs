using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PauseContoroller : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel; // ポーズ画面
    [SerializeField] private GameObject AudioOperationPanel; // 音量調整パネル
    [SerializeField] private GameObject BuffCheckPanel; // バフ確認パネル
    [SerializeField] private GameObject FadeInPanel; // フェードインパネル
    [SerializeField] private AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PausePanel.SetActive(false);
        FadeInPanel.SetActive(false);
        AudioOperationPanel.SetActive(false);
        BuffCheckPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(Time.timeScale == 0)
            {
                PausePanel.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false; // カーソルを非表示
                Cursor.lockState = CursorLockMode.Locked; // カーソルを画面中央に固定
            }
            else
            {
                Time.timeScale = 0f;
                PausePanel.SetActive(true);
                Cursor.visible = true; // カーソルを表示
                Cursor.lockState = CursorLockMode.None; // カーソルの固定を解除
            }
        }
    }

    public void Retire()
    {
        FadeInPanel.SetActive(true);
        StartCoroutine(RetireAnim());
    }

    private IEnumerator RetireAnim()
    {
        audio.volume = 0;
        Time.timeScale = 1.0f;
        for(int i = 0; i < 255; i++)
        {
            FadeInPanel.GetComponent<Image>().color += new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Title");
        yield return null;
    }

    // 音量調整パネルを開く
    public void OpenAudioComtroller()
    {
        AudioOperationPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }
    // バフ確認パネルを開く
    public void OpenBuffCheckPanel()
    {
        BuffCheckPanel.SetActive(true);
        AudioOperationPanel.SetActive(false);
    }
}
