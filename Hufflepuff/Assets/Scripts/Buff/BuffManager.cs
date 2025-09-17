using UnityEngine;
using System.Collections.Generic;
using System.Net;
using Mono.Cecil.Cil;

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
        if (datas.Count == 0)
        {
            datas.Add(new Buff { buffID = forId, value = value });
        }
        else
        {
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
