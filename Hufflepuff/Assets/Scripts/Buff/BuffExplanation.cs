// ========================================
//
// BuffExplanation.cs
//
// ========================================
//
// バフの名前・説明・アイコンを UI に表示するためのクラス。
// ・SetBuffExplanation() を呼ぶだけで UI が更新される
// ・ショップ画面やバフ確認パネルで使用
//
// ========================================

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BuffExplanation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buffNameTxt;        // バフ名
    [SerializeField] private TextMeshProUGUI buffExplanationTxt; // バフ説明文
    [SerializeField] private Image buffIcon;                     // バフアイコン

    /// <summary>
    /// バフ情報を UI に反映する
    /// </summary>
    public void SetBuffExplanation(string Name, string Explanation, Sprite Icon)
    {
        buffNameTxt.text = Name;
        buffExplanationTxt.text = Explanation;
        buffIcon.sprite = Icon;
    }
}
