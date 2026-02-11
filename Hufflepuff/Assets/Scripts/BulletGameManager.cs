// ========================================
// 
// BulletGameManager.cs
// 
// ========================================
// 
// ゲーム全体の管理
// 
// ========================================

using UnityEngine;

public class BulletGameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 0.0f; // ゲームの経過時間を管理する変数
    private bool isStart = false;　 // ゲームが開始されているかどうかを管理するフラグ

    void Start()
    {
        gameTime = 0.0f;
    }

    void Update()
    {
        if(isStart)
        {
            gameTime += Time.deltaTime; // ゲームの経過時間を更新
        }
    }
    /// <summary>
    /// ゲームを開始するメソッドです。
    /// </summary>
    public void StartGame()
    {
        isStart = true; // ゲームを開始する
        gameTime = 0.0f; // ゲーム時間をリセット
    }
}
