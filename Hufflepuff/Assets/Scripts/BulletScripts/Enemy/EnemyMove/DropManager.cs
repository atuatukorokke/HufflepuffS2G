// ========================================
//
// DropManager.cs
//
// ========================================
//
// 敵がアイテムをドロップする確率（レート）を管理するクラス。
// ・DropLate をカウントダウンしてドロップ発生を制御
// ・LateChange() でレートを1減らす
// ・LateReset() でレートを初期値に戻す
//
// ========================================

using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private int dropLate = 10; // ドロップ発生レート（小さいほど出やすい）

    public int DropLate
    {
        get => dropLate;
        set => dropLate = value;
    }

    /// <summary>
    /// ドロップレートを1減らす。
    /// </summary>
    public int LateChange()
    {
        DropLate--;
        return DropLate;
    }

    /// <summary>
    /// ドロップレートを初期値（10）にリセットする。
    /// </summary>
    public int LateReset()
    {
        DropLate = 10;
        return DropLate;
    }
}
