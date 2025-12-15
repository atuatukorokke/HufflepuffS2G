// ShopOpen.cs
//
// ショップ画面の開閉を管理
//

using System;
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
        startPos = transform.localPosition; // 初期位置を保存
        OpenState = true;
        shopCamera.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    /// <summary>
    /// ショップ画面を開く
    /// </summary>
    public void ShopOpenAni()
    {
        transform.localPosition = startPos; // 初期位置に戻す
        // プレイヤーがパズルをしているかどうかを判別
        if(!isPuzzle)
        {
            // シューティングをしていたらパズル状態にする
            playerController.Playstate = PlayState.Puzzle; // プレイヤーをパズル状態にする
            isPuzzle = true;
        }
        else
        {
            // パズル状態ならシューティング状態にする
            playerController.Playstate = PlayState.Shooting;
            isPuzzle = false;
        }
        animator.SetTrigger("SetShop");
    }

    /// <summary>
    /// 最初のアニメーションが終った後にショップ画面の待機状態に入る
    /// </summary>
    public void SetEndAnim()
    {
        animator.SetBool("EndAnim", true);
    }

    /// <summary>
    /// ショップカメラの移動
    /// </summary>
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
