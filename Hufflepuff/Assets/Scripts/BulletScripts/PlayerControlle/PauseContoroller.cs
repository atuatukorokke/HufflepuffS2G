// PauseContoroller.cs
//
// Pause��ʂ̊Ǘ������܂�
//

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseContoroller : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;             // �|�[�Y�p�l��
    [SerializeField] private GameObject FadeInPanel;            // �t�F�[�h�C���p�l��
    [SerializeField] private GameObject VolumeControllerPanel;  // ���ʒ����p�l��
    [SerializeField] private GameObject BuffCheckPanel;         // �o�t�m�F�p�l��
    [SerializeField] private AudioSource audio;             
    private AudioSource audioSource;
    [SerializeField] private AudioClip pauseOpenSE;             // �|�[�Y�J��SE
    [SerializeField] private AudioClip pauseCloseSE;            // �|�[�Y����SE
    [SerializeField] private AudioClip buttonSE;                // �{�^��SE

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PausePanel.SetActive(false);
        FadeInPanel.SetActive(false);
        VolumeControllerPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }

    void Update()
    {
        // Escape�L�[�Ń|�[�Y�̊J��
        if (Input.GetKeyDown(KeyCode.Escape)) 
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
    /// ���^�C�A�{�^��
    /// </summary>
    public void Retire()
    {
        FadeInPanel.SetActive(true);
        StartCoroutine(RetireAnim());
    }
    /// <summary>
    /// ���ʒ����p�l�����J��
    /// </summary>
    public void VolumeControllerOpen()
    {
        audioSource.PlayOneShot(buttonSE);
        VolumeControllerPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }
    /// <summary>
    /// �o�t�m�F�p�l�����J��
    /// </summary>
    public void BuffCheckOpen()
    {
        audioSource.PlayOneShot(buttonSE);
        VolumeControllerPanel.SetActive(false);
        BuffCheckPanel.SetActive(true);
    }

    /// <summary>
    /// ���^�C�A�A�j���[�V����
    /// </summary>
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
