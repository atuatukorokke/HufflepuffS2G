// ========================================
//
// BulletDestroy.cs
//
// ========================================
//
// 画面外に出た弾を自動で削除するためのクラス。
// ・OnBecameInvisible() はカメラに映らなくなった瞬間に呼ばれる
// ・弾のメモリ管理や処理負荷軽減に役立つ
//
// ========================================

using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    /// <summary>
    /// 画面外に出た瞬間に弾を削除する。
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
