using System;
using Unity.VisualScripting;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Animator animator;
    [SerializeField] private Camera shopCamera;
    [SerializeField] private bool OpenState;
    public event Action OnShop;

    private void Start()
    {
        startPos = transform.localPosition; // �����ʒu��ۑ�
        OpenState = true;
        shopCamera.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }
    public void ShopOpenAni()
    {
        transform.localPosition = startPos; // �����ʒu�ɖ߂�
        animator.SetBool("PuzzleState", true);
    }

    public void SetIdelAnim()
    {
        animator.SetBool("PuzzleState", false);
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
