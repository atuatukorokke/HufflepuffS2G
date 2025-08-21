using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player; // プレイヤーに対して各変数の値の変更を行う
    [SerializeField] BuffManager buffManager; // バフ内容の入ったリスト型の変数を参照する

    [SerializeField] private float attack;
    [SerializeField] private float invincibleTime;
    [SerializeField] private float puzzleTime;
    [SerializeField] private float carryOverSpecialGauge;

    public float Attack { get => attack; private set => attack = value; }
    public float InvincibleTime { get => invincibleTime; private set => invincibleTime = value; }
    public float PuzzleTime { get => puzzleTime; private set => puzzleTime = value; }
    public float CarryOverSpecialGauge { get => carryOverSpecialGauge; private set => carryOverSpecialGauge = value; }


    /// <summary>
    /// 各バフの適応をさせます
    /// </summary>
    public void ApplyBuffs()
    {
        foreach(var buff in buffManager.datas)
        {
            switch(buff.buffID)
            {
                case BuffForID.AtackMethod:
                    player.Attack += player.Attack * buff.value; // 攻撃力の増加
                    Attack = player.Attack;
                    break;
                case BuffForID.InvincibleTime:
                    player.InvincibleTime += buff.value; // 無敵時間の増加
                    InvincibleTime = player.InvincibleTime;
                    break;
                case BuffForID.PuzzleTime:
                    PuzzleTime++;
                    break;
                case BuffForID.CarryOverSpecialGauge:
                    CarryOverSpecialGauge++;
                    break;
            }
        }

        // リストのクリア
        buffManager.datas.Clear();
    }

    private void Start()
    {
        player = GetComponent<PlayrController>();
        buffManager = FindAnyObjectByType<BuffManager>();
    }
}
