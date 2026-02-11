// ========================================
//
// TitleScene.cs
//
// ========================================
//
// タイトル画面のフェード演出とシーン遷移を管理するクラス。
// ・ボタン押下でフェードイン → アニメーション再生
// ・アニメーションイベントから Title シーンを読み込む
//
// ========================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    [SerializeField] private GameObject FadeInPanel; // フェードイン用パネル
    [SerializeField] private Animator animator;      // フェードアニメーション用アニメーター

    private void Start()
    {
        FadeInPanel.SetActive(false);
    }

    /// <summary>
    /// タイトルのフェードインアニメーションを開始する
    /// </summary>
    public void TitleAnim()
    {
        FadeInPanel.SetActive(true);
        animator.SetBool("FadeIn", true);
    }

    /// <summary>
    /// Title シーンを読み込む（アニメーションイベントから呼ばれる）
    /// </summary>
    public void LoadScene()
    {
        SceneManager.LoadScene("Title");
    }
}
