// ========================================
// 
// OperationDescription.cs
// 
// ========================================
// 
// オプション関係を管理します
// 
// ========================================

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class OperationDescription : MonoBehaviour
{
    [SerializeField] private GameObject panel;          // 説明パネル
    [SerializeField] private Animator gameAanimator;    // ゲームアニメーター
    [SerializeField] private Animator optionAnimator;   // オプションアニメーター
    private bool optionCheck;

    private AudioSource aidio;
    [SerializeField] private AudioClip ButtonTouch;

    private void Start()
    {
        aidio = GetComponent<AudioSource>();
        gameAanimator = GetComponent<Animator>();
        panel.SetActive(false);
        optionCheck = true;
    }

    /// <summary>
    /// 説明パネルを表示します
    /// </summary>
    public void ShowDescription()
    {
        panel.SetActive(true);
    }
    /// <summary>
    /// ゲームアニメーションを再生します
    /// </summary>
    public void GaemAnim()
    {
        aidio.PlayOneShot(ButtonTouch);
        gameAanimator.SetBool("OptionOpen", true);
    }

    /// <summary>
    /// オプションパネルを開閉します
    /// </summary>
    public void OptionOpen()
    {
        aidio.PlayOneShot(ButtonTouch);
        optionAnimator.SetBool("Open", optionCheck);
        optionCheck = !optionCheck;
    }

}
