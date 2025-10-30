// TutorialManager.cs
//
// チュートリアル説明の管理
//

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private int currentTutorialIndex;
    [SerializeField] private int maxTutorialIndex;

    [Header("0.移動方法 攻撃方法\r\n1.必殺技\r\n2.敵を倒したら\r\n3.パズル説明バフ含む\r\n")]
    [SerializeField] private Sprite[] TutorialImage;                    // チュートリアル画像格納
    [SerializeField] private string[] TutorialExplanation;              // チュートリアル説明文格納
    [SerializeField] private Image MainTutorialImage;                   // メインチュートリアル画像
    [SerializeField] private TextMeshProUGUI MainTutorialExplanation;   // メインチュートリアル説明文
    [SerializeField] private GameObject startButton;                    // スタートボタン
    [SerializeField] private GameObject leftButton;                     // 左ボタン
    [SerializeField] private GameObject rightButton;                    // 右ボタン

    private AudioSource audio;
    [SerializeField] private AudioClip buttonSE;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        startButton.SetActive(false);
        maxTutorialIndex = TutorialImage.Length;
        currentTutorialIndex = 0;
        SetTutorial(currentTutorialIndex);
    }

    private void Update()
    {
        // 左右ボタンの表示非表示切り替え
        // 最初のチュートリアルの場合
        if (currentTutorialIndex == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
        }
        // 最後のチュートリアルの場合
        if (currentTutorialIndex == maxTutorialIndex - 1)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(false);
        }

        // 中間のチュートリアルの場合
        if (currentTutorialIndex > 0 & currentTutorialIndex < maxTutorialIndex - 1)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }

    /// <summary>
    /// チュートリアルのインデックスを更新し、次のチュートリアルを表示します。
    /// </summary>
    public void IndexUpdate()
    {
        audio.PlayOneShot(buttonSE);
        currentTutorialIndex++;
        currentTutorialIndex = (int)Mathf.Repeat(currentTutorialIndex, maxTutorialIndex);
        SetTutorial(currentTutorialIndex);
        if(currentTutorialIndex == maxTutorialIndex - 1) startButton.SetActive(true);
    }

    /// <summary>
    /// チュートリアルのインデックスを更新し、前のチュートリアルを表示します。
    /// </summary>
    public void IndexDown()
    {
        audio.PlayOneShot(buttonSE);
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
        MainTutorialImage.sprite = TutorialImage[TutorialNumber];
        MainTutorialExplanation.text = TutorialExplanation[TutorialNumber];
    }
}
