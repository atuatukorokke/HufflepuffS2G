// BuffManager.cs
//
// �o�t���e���܂Ƃ߂܂��B
//

using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    Dictionary<Stat, int> keyValuePairs = new Dictionary<Stat, int>();
}
/// <summary>
/// �o�t�̖��O
/// </summary>
public enum Stat
{
    attack, // �v���C���[�̍U����
    intrusionDefence, // ���ז����ł����
    intrusionRecover, // ���ז������炷��

}
