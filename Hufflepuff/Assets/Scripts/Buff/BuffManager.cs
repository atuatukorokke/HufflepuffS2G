// BuffManager.cs
//
// �o�t���e���܂Ƃ߂܂��B
//

using UnityEngine;

public class BuffManager : MonoBehaviour
{
    // �o�t���e
    [SerializeField] private float attackPoer; // �U����
    �@// �e���̎��
    [SerializeField] private float bulletDelayTime; // �e�̔��ˊԊu
    [SerializeField] private float puzzleTimeLimit; // �p�Y���̐�������
    [SerializeField] private bool isSpecialBullet; // �v���C���[���K�E�Z���g�p�\���ǂ���
    [SerializeField] private int IntrusionDeleteNum; // �N���폜��
    [SerializeField] private int InvincibleTime; // ���G����

    public float AttackPoer { get => attackPoer; set => attackPoer = value; }
    public float BulletDelayTime { get => bulletDelayTime; set => bulletDelayTime = value; }
    public float PuzzleTimeLimit { get => puzzleTimeLimit; set => puzzleTimeLimit = value; }
    public bool IsSpecialBullet { get => isSpecialBullet; set => isSpecialBullet = value; }
    public int IntrusionDeleteNum1 { get => IntrusionDeleteNum; set => IntrusionDeleteNum = value; }
    public int InvincibleTime1 { get => InvincibleTime; set => InvincibleTime = value; }
}
