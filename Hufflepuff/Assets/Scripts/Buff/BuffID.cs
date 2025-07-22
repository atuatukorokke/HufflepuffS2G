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
    AtackMethod, // UŒ‚è’i
    InvincibleTime, // –³“GŠÔ
    PuzzleTime, // ƒpƒYƒ‹ŠÔ
    CarryOverSpecialGauge // •KE‹ZƒQ[ƒW‚ªŒJ‚è‰z‚µ‰Â”\‚©‚Ç‚¤‚©
}