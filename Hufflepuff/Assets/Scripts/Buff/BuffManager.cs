// ========================================
//
// BuffManager.cs
//
// ========================================
//
// バフデータ（Buff）の管理クラス。
// ・AddBuff() でバフを追加 or 既存バフを強化
// ・同じ種類のバフが既にある場合は value を加算
// ・無ければ新規追加
//
// ========================================

using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    public List<Buff> datas = new List<Buff>(); // 全バフデータのリスト

    /// <summary>
    /// バフを追加または強化する
    /// </summary>
    /// <param name="forId">ステータスID</param>
    /// <param name="value">値</param>
    public void AddBuff(BuffForID forId, float value)
    {
        // リストが空の場合は新規追加、そうでない場合は既存のバフを更新または新規追加
        if (datas == null) datas.Add(new Buff { buffID = forId, value = value });

        // 既存バフの更新、なければ追加
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i].buffID == forId)
            {
                datas[i].value += value;
                return;
            }
        }

        datas.Add(new Buff { buffID = forId, value = value });
    }
}
