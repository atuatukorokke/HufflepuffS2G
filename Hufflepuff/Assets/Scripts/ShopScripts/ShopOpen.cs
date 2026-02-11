// ======================================== 
// 
// ShopOpen.cs
// 
// ========================================
// 
// ショップ画面の開閉を管理します
// 
// ========================================

using System;
using UnityEngine;

public class ShopOpen : MonoBehaviour
{
    [SerializeField] private Vector2 startPos;      // 最初の位置
    [SerializeField] private Animator animator;     // アニメーター
    [SerializeField] private Camera shopCamera;     // パズル画面を移す用のカメラ
    [SerializeField] private bool OpenState;        // パズル画面を開いているかのフラグ
    public event Action OnShop;                     // パズル画面を閉じるときのアクション
    private PlayrController playerController;       // プレイヤーコントローラー
    [SerializeField] private bool isPuzzle = false; // パズル画面を開いているかのフラグ

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
        // 初期位置に戻す
        transform.localPosition = startPos; 

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
