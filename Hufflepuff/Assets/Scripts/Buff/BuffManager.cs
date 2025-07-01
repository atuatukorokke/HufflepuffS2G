// BuffManager.cs
//
// バフ内容をまとめます。
//

using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // バフ内容
    [SerializeField] private float attackPoer; // 攻撃力
    　// 弾幕の種類
    [SerializeField] private float bulletDelayTime; // 弾の発射間隔
    [SerializeField] private float puzzleTimeLimit; // パズルの制限時間
    [SerializeField] private bool isSpecialBullet; // プレイヤーが必殺技を使用可能かどうか
    [SerializeField] private int IntrusionDeleteNum; // 侵入削除数

    public float AttackPoer { get => attackPoer; set => attackPoer = value; }
    public float BulletDelayTime { get => bulletDelayTime; set => bulletDelayTime = value; }
    public float PuzzleTimeLimit { get => puzzleTimeLimit; set => puzzleTimeLimit = value; }
    public bool IsSpecialBullet { get => isSpecialBullet; set => isSpecialBullet = value; }
    public int IntrusionDeleteNum1 { get => IntrusionDeleteNum; set => IntrusionDeleteNum = value; }


}
