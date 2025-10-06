// PauseContoroller.cs
//
// Pause画面の管理をします
//

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseContoroller : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject FadeInPanel;
    [SerializeField] private GameObject VolumeControllerPanel;
    [SerializeField] private GameObject BuffCheckPanel;
    [SerializeField] private AudioSource audio;
    private AudioSource audioSource;
    [SerializeField] private AudioClip pauseOpenSE;
    [SerializeField] private AudioClip pauseCloseSE;
    [SerializeField] private AudioClip buttonSE;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PausePanel.SetActive(false);
        FadeInPanel.SetActive(false);
        VolumeControllerPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(Time.timeScale == 0)
            {
                audioSource.PlayOneShot(pauseCloseSE);
                PausePanel.SetActive(false);
                Time.timeScale = 1.0f;
            }
            else
            {
                audioSource.PlayOneShot(pauseOpenSE);
                Time.timeScale = 0f;
                PausePanel.SetActive(true);
            }
        }
    }

    /// <summary>
    /// リタイアボタン
    /// </summary>
    public void Retire()
    {
        FadeInPanel.SetActive(true);
        StartCoroutine(RetireAnim());
    }
    /// <summary>
    /// 音量調整パネルを開く
    /// </summary>
    public void VolumeControllerOpen()
    {
        audioSource.PlayOneShot(buttonSE);
        VolumeControllerPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }
    /// <summary>
    /// バフ確認パネルを開く
    /// </summary>
    public void BuffCheckOpen()
    {
        audioSource.PlayOneShot(buttonSE);
        VolumeControllerPanel.SetActive(false);
        BuffCheckPanel.SetActive(true);
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
}
