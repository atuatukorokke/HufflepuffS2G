// ========================================
//
// LoadScene.cs
//
// ========================================
//
// ゲーム開始時に別シーン（BulletScene）を追加読み込みするクラス。
// ・Additive モードで読み込むため、現在のシーンを残したまま弾幕シーンを重ねる
// ・弾幕管理や共通処理を別シーンに分離している構成向け
//
// ========================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("BulletScene", LoadSceneMode.Additive);
    }
}
