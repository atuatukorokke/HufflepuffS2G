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
    PuzzleTime, // �p�Y������
    CarryOverSpecialGauge // �K�E�Z�Q�[�W���J��z���\���ǂ���
}