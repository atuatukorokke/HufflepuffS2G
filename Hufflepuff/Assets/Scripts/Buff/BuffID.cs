using System;

[Serializable]
public class Buff
{
    public BuffForID buffID;
    public float value;
    public bool isActive;
}

public enum BuffForID
{
    AtackMethod, // 攻撃手段
    InvincibleTime, // 無敵時間
    CoinGetLate, // コイン獲得率
    DamageDownLate, // ダメージダウン率
}