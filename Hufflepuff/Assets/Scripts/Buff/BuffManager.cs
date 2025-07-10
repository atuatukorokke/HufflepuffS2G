// BuffManager.cs
//
// ƒoƒt“à—e‚ð‚Ü‚Æ‚ß‚Ü‚·B
//

using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // ƒoƒt“à—e
    [SerializeField] private float attackPoer; // UŒ‚—Í
    @// ’e–‹‚ÌŽí—Þ
    [SerializeField] private float bulletDelayTime; // ’e‚Ì”­ŽËŠÔŠu
    [SerializeField] private float puzzleTimeLimit; // ƒpƒYƒ‹‚Ì§ŒÀŽžŠÔ
    [SerializeField] private bool isSpecialBullet; // ƒvƒŒƒCƒ„[‚ª•KŽE‹Z‚ðŽg—p‰Â”\‚©‚Ç‚¤‚©
    [SerializeField] private int IntrusionDeleteNum; // N“üíœ”
    [SerializeField] private int InvincibleTime; // –³“GŽžŠÔ

    public float AttackPoer { get => attackPoer; set => attackPoer = value; }
    public float BulletDelayTime { get => bulletDelayTime; set => bulletDelayTime = value; }
    public float PuzzleTimeLimit { get => puzzleTimeLimit; set => puzzleTimeLimit = value; }
    public bool IsSpecialBullet { get => isSpecialBullet; set => isSpecialBullet = value; }
    public int IntrusionDeleteNum1 { get => IntrusionDeleteNum; set => IntrusionDeleteNum = value; }
    public int InvincibleTime1 { get => InvincibleTime; set => InvincibleTime = value; }
}
