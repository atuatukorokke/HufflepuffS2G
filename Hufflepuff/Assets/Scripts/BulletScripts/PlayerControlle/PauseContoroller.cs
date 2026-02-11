// ========================================
//
// PauseContoroller.cs
//
// ========================================
//
// ポーズ画面の管理を行うクラス。
// ・EscapeキーでポーズON/OFFを切り替え
// ・音量設定パネル、バフ確認パネルの切り替え
// ・リタイア時のフェード演出とタイトル遷移
//
// ========================================

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseContoroller : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;            // ポーズパネル
    [SerializeField] private GameObject FadeInPanel;           // フェードインパネル
    [SerializeField] private GameObject VolumeControllerPanel; // 音量設定パネル
    [SerializeField] private GameObject BuffCheckPanel;        // バフ確認パネル

    [SerializeField] private AudioSource audio;                // BGM用
    private AudioSource audioSource;                           // SE用

    [SerializeField] private AudioClip pauseOpenSE;            // ポーズを開くSE
    [SerializeField] private AudioClip pauseCloseSE;           // ポーズを閉じるSE
    [SerializeField] private AudioClip buttonSE;               // ボタンSE

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PausePanel.SetActive(false);
        FadeInPanel.SetActive(false);
        VolumeControllerPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }

    private void Update()
    {
        // Escapeキーでポーズ切り替え
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
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
    /// 音量設定パネルを開く
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

    /// <summary>
    /// リタイア時のフェード演出
    /// </summary>
    private IEnumerator RetireAnim()
    {
        audio.volume = 0;
        Time.timeScale = 1.0f;

        for (int i = 0; i < 255; i++)
        {
            FadeInPanel.GetComponent<Image>().color += new Color32(0, 0, 0, 1);
            yield return new WaitForSeconds(0.01f);
        }

        SceneManager.LoadScene("Title");
    }
}
