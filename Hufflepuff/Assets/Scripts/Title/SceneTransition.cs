// SceneTransition.cs
// 
// �V�[���J�ڂ��s���܂�
// 

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// ���C���V�[����ǂݍ��݂܂�
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("BulletScene");
    }

    /// <summary>
    /// �X�^�[�g�A�j���[�V�������Đ����܂�   
    /// </summary>
    public void GetStartAnim()
    {
        animator.SetBool("GetStart", true);
    }

    /// <summary>
    /// �p�l�����\���ɂ��܂�
    /// </summary>
    public void PanelActive()
    {
        gameObject.SetActive(false);
    }
}
