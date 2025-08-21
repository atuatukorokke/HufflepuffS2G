using UnityEngine;

public class BulletGameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 0.0f; // ゲームの経過時間を管理する変数
    private bool isStart = false;　 // ゲームが開始されているかどうかを管理するフラグ

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameTime = 0.0f;
        Cursor.visible = false; // カーソルを非表示にする
        Cursor.lockState = CursorLockMode.Locked; // カーソルを画面中央に固定する
    }

    // Update is called once per frame
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
