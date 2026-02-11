// ========================================
//
// SpecialMove_Gomi.cs
//
// ========================================
//
// ボス「ゴミ」の必殺技（スペルカード）管理クラス。
// ・各段階ごとのスペルパターンを返す
// ・セミファイナル専用パターンも返す
// ・無敵移動（スペル開始演出）もここで管理
//
// ========================================

using System.Collections;
using UnityEngine;

public class SpecialMove_Gomi : MonoBehaviour
{
    [Header("スペル開始位置の設定")]
    [SerializeField] private Vector2 spellPos;   // 必殺技・セミファイナル開始時に移動する座標
    [SerializeField] private Boss1Bullet boss;   // ボス本体の参照

    [Header("一段階目の必殺技データ")]
    [SerializeField] private FastSpecialBom fast;

    [Header("二段階目の必殺技データ")]
    [SerializeField] private SecondSpecialBom second;

    [Header("三段階目の必殺技データ")]
    [SerializeField] private ThirdSpecialBom third;

    [Header("四段階目の必殺技データ")]
    [SerializeField] private FourSpecialBom four;

    [Header("最終段階の必殺技データ")]
    [SerializeField] private FinalSpecianBom final;

    [Header("セミファイナルのデータ")]
    [SerializeField] private SpecialFinalAttack special;

    /// <summary>
    /// 現在の段階に応じたスペルパターンを返す。
    /// </summary>
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

    /// <summary>
    /// セミファイナル専用パターンを返す。
    /// </summary>
    public ISpellPattern GetSemiFinalPattern(Vector2 spellPos)
    {
        return new SemiFinalPattern(special, transform, spellPos, boss);
    }

    /// <summary>
    /// ボスをスペル開始位置へ無敵状態で移動させる。
    /// スペル演出のための共通処理。
    /// </summary>
    public IEnumerator MoveToSpellPosWithInvincible(Transform boss, Vector2 spellPos, Boss1Bullet owner)
    {
        owner.SetInvincible(true);   // 移動中は無敵

        float t = 0f;
        float duration = 0.5f;
        Vector2 start = boss.position;

        // 指定位置へ向けて移動
        while (t < duration)
        {
            boss.position = Vector2.Lerp(start, spellPos, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        owner.SetInvincible(false);  // 移動完了後に無敵解除
    }
}
