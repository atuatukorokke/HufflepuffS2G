using System.Collections;
using UnityEngine;

public class SpecialMove_Gomi : MonoBehaviour
{
    [Header("ボス全体を管理する変数")]
    [SerializeField] private Vector2 spellPos;// 必殺技・セミファイナルを打つときにこの座標に一旦戻る
    [SerializeField] private Boss1Bullet boss;

    [Header("一段階目の必殺技の変数")]
    [SerializeField] private FastSpecialBom fast;
    [Header("二段階目の必殺技の変数")]
    [SerializeField] private SecondSpecialBom second;
    [Header("三段階目の必殺技の変数")]
    [SerializeField] private ThirdSpecialBom third;
    [Header("四段階目の必殺技の変数")]
    [SerializeField] private FourSpecialBom four;
    [Header("最終段階目の必殺技の変数")]
    [SerializeField] private FinalSpecianBom final;
    [Header("セミファイナルの変数")]
    [SerializeField] private SpecialFinalAttack special;
   
    public ISpellPattern GetPattern(State state, Transform boss, Vector2 spellPos)
    {
        return state switch
        {
            State.fast => new FastSpecialPattern(fast, boss.transform, spellPos, this.boss),
            State.second => new SecondSpecialPattern(second, boss.transform, spellPos, this.boss),
            State.third => new ThirdSpecialPattern(third, boss.transform, spellPos, this.boss),
            State.four => new FourSpecialPattern(four, boss.transform, spellPos, this.boss),
            State.final => new FinalSpecialPattern(final, boss.transform, spellPos, this.boss),
            _ => null
        };
    }
    public ISpellPattern GetSemiFinalPattern()
    {
        return new SemiFinalPattern(special, transform, boss);
    }

    public IEnumerator MoveToSpellPosWithInvincible(Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        owner.SetInvincible(true);   // ★ 移動中だけ無敵

        float t = 0f;
        float duration = 0.5f;
        Vector2 start = boss.position;

        while (t < duration)
        {
            boss.position = Vector2.Lerp(start, spellPos, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        owner.SetInvincible(false);  // ★ 移動が終わったら被弾可能に戻す
    }

}
