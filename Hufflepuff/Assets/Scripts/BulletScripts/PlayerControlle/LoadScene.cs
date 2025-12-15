// ========================================
// 
// LoadScene.cs
// 
// ========================================
// 
// ゲームシーンをロードします
// 
// ========================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("BulletScene", LoadSceneMode.Additive);
    }
}
