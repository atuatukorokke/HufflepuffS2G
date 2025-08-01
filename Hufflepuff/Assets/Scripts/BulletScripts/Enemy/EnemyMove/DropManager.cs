// DropManager.cs
//
// 箱を落とす確率を管理します
//

using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private int dropLate = 10; // 箱を落とす確率

    public int DropLate { get => dropLate; set => dropLate = value; }

    /// <summary>
    /// ドロップ確率の更新をします
    /// </summary>
    /// <returns>新しい確率です</returns>
    public int LateChange()
    {
        DropLate--;
        return DropLate;
    }

    /// <summary>
    /// ドロップ確率のリセットをします
    /// </summary>
    /// <returns>新しい確率です</returns>
    public int LateReset()
    {
        DropLate = 10;
        return DropLate;
    }
}
