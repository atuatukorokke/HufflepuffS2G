// BuffManager.cs
//
// バフ内容の管理
//

using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    public List<Buff> datas = new List<Buff>(); // 各バフ内容を記録するリスト

    /// <summary>
    /// 各バフをリストに記録します
    /// </summary>
    /// <param name="forId">ステータスID</param>
    /// <param name="value">値</param>
    public void AddBuff(BuffForID forId, float value)
    {
        // リストが空の場合は新規追加、そうでない場合は既存のバフを更新または新規追加
        if (datas == null) datas.Add(new Buff { buffID = forId, value = value });

        else
        {
            // 既存のバフを更新、存在しない場合は新規追加
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
}
