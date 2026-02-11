// ========================================
//
// BuffID.cs
//
// ========================================
//
// バフの種類と、その値・有効状態をまとめたデータクラス。
// ・Buff は 1つのバフ効果を表す（ID / 値 / 有効フラグ）
// ・BuffForID はバフの種類を列挙
// ・ショップやプレイヤー強化処理で使用される
//
// ========================================

using System;

[Serializable]
public class Buff
{
    public BuffForID buffID; // バフの種類
    public float value;      // バフの効果量
    public bool isActive;    // バフが有効かどうか
}

public enum BuffForID
{
    AtackMethod,     // 攻撃方法（攻撃力アップなど）
    InvincibleTime,  // 無敵時間延長
    CoinGetLate,     // コイン獲得量アップ
    DamageDownLate,  // 被ダメージ軽減
}
