// BuffManager.cs
//
// // �o�t���e�̊Ǘ�
//

using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    public List<Buff> datas = new List<Buff>(); // �e�o�t���e���L�^���郊�X�g

    /// <summary>
    /// �e�o�t�����X�g�ɋL�^���܂�
    /// </summary>
    /// <param name="forId">�X�e�[�^�XID</param>
    /// <param name="value">�l</param>
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
