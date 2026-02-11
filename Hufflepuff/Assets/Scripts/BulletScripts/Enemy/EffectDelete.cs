// ========================================
//
// EffectDelete.cs
//
// ========================================
//
// アニメーション終了時に自身を削除するためのシンプルなクラス。
// ・アニメーションイベントから OnDelete() を呼び出すことで削除
// ・爆発エフェクトや一時的な演出オブジェクトに使用
//
// ========================================

using UnityEngine;

public class EffectDelete : MonoBehaviour
{
    /// <summary>
    /// アニメーションイベントから呼び出され、エフェクトを削除する。
    /// </summary>
    public void OnDelete()
    {
        Destroy(gameObject);
    }
}
