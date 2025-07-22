using UnityEngine;
using System.Collections.Generic;

public class BuffManager : MonoBehaviour
{
    public List<Buff> datas = new List<Buff>(); // �e�o�t���e���L�^���郊�X�g

    /// <summary>
    /// �e�o�t�����X�g�ɋL�^���܂�
    /// </summary>
    /// <param name="forId">�X�e�[�^�X</param>
    /// <param name="value">�l</param>
    public void AddBuff(BuffForID forId, float value)
    {
        datas.Add(new Buff { buffID = forId, value = value });
    }
}
