// BuffManager.cs
//
// バフ内容をまとめます。
//

using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    Dictionary<Stat, int> keyValuePairs = new Dictionary<Stat, int>();
}
/// <summary>
/// バフの名前
/// </summary>
public enum Stat
{
    attack, // プレイヤーの攻撃力
    intrusionDefence, // お邪魔ができる量
    intrusionRecover, // お邪魔を減らす量

}
