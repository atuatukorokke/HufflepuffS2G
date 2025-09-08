using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PauseContoroller : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel; // �|�[�Y���
    [SerializeField] private GameObject AudioOperationPanel; // ���ʒ����p�l��
    [SerializeField] private GameObject BuffCheckPanel; // �o�t�m�F�p�l��
    [SerializeField] private GameObject FadeInPanel; // �t�F�[�h�C���p�l��
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
                Cursor.visible = false; // �J�[�\�����\��
                Cursor.lockState = CursorLockMode.Locked; // �J�[�\������ʒ����ɌŒ�
            }
            else
            {
                Time.timeScale = 0f;
                PausePanel.SetActive(true);
                Cursor.visible = true; // �J�[�\����\��
                Cursor.lockState = CursorLockMode.None; // �J�[�\���̌Œ������
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

    // ���ʒ����p�l�����J��
    public void OpenAudioComtroller()
    {
        AudioOperationPanel.SetActive(true);
        BuffCheckPanel.SetActive(false);
    }
    // �o�t�m�F�p�l�����J��
    public void OpenBuffCheckPanel()
    {
        BuffCheckPanel.SetActive(true);
        AudioOperationPanel.SetActive(false);
    }
}
