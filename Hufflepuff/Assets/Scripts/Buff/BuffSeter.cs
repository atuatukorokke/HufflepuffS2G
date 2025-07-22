using Mono.Cecil.Cil;
using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player; // プレイヤーに対して各変数の値の変更を行う
    [SerializeField] BuffManager buffManager; // バフ内容の入ったリスト型の変数を参照する

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
                    break;
                case BuffForID.InvincibleTime:
                    break;
                case BuffForID.PuzzleTime:
                    break;
                case BuffForID.CarryOverSpecialGauge:
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
