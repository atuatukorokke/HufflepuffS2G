// ========================================
// 
// NonTouchPanelManager.cs
// 
// ========================================
// 
// 画面に触れないようにパネルを出します
//
// ========================================


using UnityEngine;

public class NonTouchPanelManager : MonoBehaviour
{
    private Animator animator;      // アニメーター

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// アニメーションからパネルを出します
    /// </summary>
    public void SetBoolAnim()
    {
        animator.SetBool("SetInstructions", false);
    }
}
