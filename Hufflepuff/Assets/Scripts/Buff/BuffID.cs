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
    AtackMethod, // �U����i
    InvincibleTime, // ���G����
    CoinGetLate, // �R�C���l����
    DamageDownLate, // �_���[�W�_�E����
}