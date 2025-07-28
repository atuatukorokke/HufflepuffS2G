using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera shopCamera;
    [SerializeField] private bool OpenState;
    public event Action OnShop;
    private PlayrController playerController;
    [SerializeField] private bool isPuzzle = false;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayrController>();
        startPos = transform.localPosition; // �����ʒu��ۑ�
        OpenState = true;
        shopCamera.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// �V���b�v��ʂ��J��
    /// </summary>
    public void ShopOpenAni()
    {
        transform.localPosition = startPos; // �����ʒu�ɖ߂�
        // �v���C���[���p�Y�������Ă��邩�ǂ����𔻕�
        if(!isPuzzle)
        {
            // �V���[�e�B���O�����Ă�����p�Y����Ԃɂ���
            playerController.PlayState = PlayState.Puzzle; // �v���C���[���p�Y����Ԃɂ���
            isPuzzle = true;
        }
        else
        {
            // �p�Y����ԂȂ�V���[�e�B���O��Ԃɂ���
            playerController.PlayState = PlayState.Shooting;
            isPuzzle = false;
        }
        animator.SetBool("PuzzleState", true);
    }

    /// <summary>
    /// �V���b�v��ʂ����
    /// </summary>
    public void SetIdelAnim()
    {
        playerController.PlayState = PlayState.Shooting; // �v���C���[�̏�Ԃ��V���[�e�B���O��Ԃɂ���
        animator.SetBool("PuzzleState", false);
    }

    /// <summary>
    /// �ŏ��̃A�j���[�V�������I������ɃV���b�v��ʂ̑ҋ@��Ԃɓ���
    /// </summary>
    public void SetEndAnim()
    {
        animator.SetBool("EndAnim", true);
    }

    public void ShopCameraMove()
    {
        shopCamera.gameObject.SetActive(OpenState);
        if(!OpenState)
        {
            OnShop?.Invoke(); // �V���b�v���J���ꂽ���Ƃ�ʒm
        }
        OpenState = !OpenState; // �J�����̏�Ԃ�؂�ւ���
    }
}
