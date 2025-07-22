using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    public List<Buff> datas = new List<Buff>(); // 各バフ内容を記録するリスト

    /// <summary>
    /// 各バフをリストに記録します
    /// </summary>
    /// <param name="forId">ステータス</param>
    /// <param name="value">値</param>
    public void AddBuff(BuffForID forId, float value)
    {
        datas.Add(new Buff { buffID = forId, value = value });
    }
}
