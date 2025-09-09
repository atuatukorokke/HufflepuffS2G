using UnityEngine;
using TMPro;
using System.Security;
using UnityEngine.UI;

public class BuffExplanation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buffNameTxt;
    [SerializeField] private TextMeshProUGUI buffExplanationTxt;
    [SerializeField] private Image buffIcon;
    
    /// <summary>
    /// 各バフ情報をセットする
    /// </summary>
    public void SetBuffExplanation(string Name, string Explanation, Sprite Icon)
    {
        buffNameTxt.text = Name;
        buffExplanationTxt.text = Explanation;
        buffIcon.sprite = Icon;
    }
}
