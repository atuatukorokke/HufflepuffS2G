// ========================================
// 
// INormalBulletPattern.cs
// 
// ========================================
// 
// 通常弾幕用のインターフェースです
// 
// ========================================

using System.Collections;

public interface INormalBulletPattern
{
    void Initialize();      // 弾幕の初期化
    IEnumerator Fire();     // 弾幕の発射処理
    void Clear();           // 弾幕の削除処理
}
