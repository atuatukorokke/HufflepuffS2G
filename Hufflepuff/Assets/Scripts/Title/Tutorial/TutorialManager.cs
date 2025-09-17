using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    private int currentTutorialIndex;
    private int maxTutorialIndex;

    [Header("0.移動方法\r\n1.攻撃方法\r\n2.必殺技\r\n3.敵を倒したら\r\n4.パズル説明\r\n5.バフ関連（仮）")]
    [SerializeField] private Sprite[] TutorialImage; // チュートリアル画像格納
    [SerializeField] private string[] TutorialExplanation; // チュートリアル説明文格納
    [SerializeField] private Sprite MainTutorialImage; // メインチュートリアル画像
    [SerializeField] private TextMeshProUGUI MainTutorialExplanation; // メインチュートリアル説明文
    [SerializeField] private GameObject startButton; // スタートボタン

    private void Start()
    {
        startButton.SetActive(false);
        maxTutorialIndex = TutorialImage.Length - 1;
        currentTutorialIndex = 0;
        SetTutorial(currentTutorialIndex);
    }

    /// <summary>
    /// チュートリアルのインデックスを更新し、次のチュートリアルを表示します。
    /// </summary>
    public void IndexUpdate()
    {
        currentTutorialIndex++;
        currentTutorialIndex = (int)Mathf.Repeat(currentTutorialIndex, maxTutorialIndex);
        SetTutorial(currentTutorialIndex);
        if(currentTutorialIndex == maxTutorialIndex) startButton.SetActive(true);
    }

    /// <summary>
    /// チュートリアルのインデックスを更新し、前のチュートリアルを表示します。
    /// </summary>
    public void IndexDown()
    {
        currentTutorialIndex--;
        currentTutorialIndex = (int)Mathf.Repeat(currentTutorialIndex, maxTutorialIndex);
        SetTutorial(currentTutorialIndex);
    }

    /// <summary>
    /// 指定されたチュートリアル番号に基づいて、メインのチュートリアル画像と説明文を設定します。
    /// </summary>
    /// <param name="TutorialNumber">Tチュートリアル番号</param>
    public void SetTutorial(int TutorialNumber)
    {
        MainTutorialImage = TutorialImage[TutorialNumber];
        MainTutorialExplanation.text = TutorialExplanation[TutorialNumber];
    }
}
