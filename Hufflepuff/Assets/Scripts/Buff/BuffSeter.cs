using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player; // プレイヤーに対して各変数の値の変更を行う
    [SerializeField] BuffManager buffManager; // バフ内容の入ったリスト型の変数を参照する
    [SerializeField] private GameObject buffExplanationObj; // バフの説明を行うオブジェクト

    [SerializeField] private GameObject buffListObj; // バフのリストを表示するオブジェクト

    [Header ("バフのアイコン")]
    [SerializeField] private Sprite AttackIcon; // 攻撃力アップのアイコン
    [SerializeField] private Sprite InvincibleIcon; // 無敵時間アップのアイコン
    [SerializeField] private Sprite PuzzleTimeIcon; // パズル時間延長のアイコン
    [SerializeField] private Sprite CarryOverSpecialGaugeIcon; // スペシャルゲージ持ち越しのアイコン

    private string buffName; // バフの名前
    private string buffExplanationText; // バフの説明文
    private Sprite buffIcon; // バフのアイコン

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ApplyBuffs();
        }
    }

    /// <summary>
    /// 各バフの適応をさせます
    /// </summary>
    public void ApplyBuffs()
    {
        foreach (Transform child in buffListObj.transform)
        {
            Destroy(child.gameObject);
        }


        foreach(var buff in buffManager.datas)
        {
            switch(buff.buffID)
            {
                case BuffForID.AtackMethod:
                    player.Attack = player.Attack * buff.value; // 攻撃力の増加
                    buffName = "攻撃力アップ";
                    buffExplanationText = $"プレイヤーの攻撃力＋<color=#ffd700>{buff.value}％</color>";
                    buffIcon = AttackIcon;
                    break;
                case BuffForID.InvincibleTime:
                    player.InvincibleTime = buff.value; // 無敵時間の増加
                    buffName = "無敵時間延長";
                    buffExplanationText = $"プレイヤーの無敵時間＋<color=#ffd700>{buff.value}秒</color>";
                    buffIcon = InvincibleIcon;
                    break;
                case BuffForID.PuzzleTime:
                    buffName = "パズル時間延長";
                    buffExplanationText = $"パズル時間が＋<color=#ffd700>{buff.value}秒</color>";
                    buffIcon = PuzzleTimeIcon;
                    // パズル時間延長の処理をここに追加
                    break;
                case BuffForID.CarryOverSpecialGauge:
                    buffName = "スペシャルゲージ持ち越し";
                    buffExplanationText = $"スペシャルゲージが<color=#ffd700>{buff.value * 100}％</color>持ち越し可能に";
                    buffIcon = CarryOverSpecialGaugeIcon;
                    // スペシャルゲージ持ち越しの処理をここに追加
                    break;
            }

            // バフの説明を表示する
            GameObject buffExplanation = Instantiate(buffExplanationObj, buffExplanationObj.transform);
            buffExplanation.transform.parent = buffListObj.transform;
            buffExplanation.GetComponent<BuffExplanation>().SetBuffExplanation(buffName, buffExplanationText, buffIcon);
        }
    }

    private void Start()
    {
        player = FindAnyObjectByType<PlayrController>();
        buffManager = GetComponent<BuffManager>();
    }
}
