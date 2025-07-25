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
        startPos = transform.localPosition; // 初期位置を保存
        OpenState = true;
        shopCamera.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }
    public void ShopOpenAni()
    {
        transform.localPosition = startPos; // 初期位置に戻す
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
            OnShop?.Invoke(); // ショップが開かれたことを通知
        }
        OpenState = !OpenState; // カメラの状態を切り替える
    }
}
