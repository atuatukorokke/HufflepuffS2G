// ========================================
//
// BuffSeter.cs
//
// ========================================
//
// バフをプレイヤーに適用し、UI に反映するクラス。
// ・BuffManager に登録されたバフを読み取り、プレイヤーの各種ステータスへ反映
// ・バフ一覧 UI を生成して表示
//
// ========================================

using UnityEngine;

public class BuffSeter : MonoBehaviour
{
    [SerializeField] PlayrController player;                 // プレイヤーのステータスを変更する
    [SerializeField] BuffManager buffManager;                // バフデータ管理
    [SerializeField] private GameObject buffExplanationObj;  // バフ説明UIプレハブ
    [SerializeField] private GameObject buffListObj;         // バフ一覧を表示する親オブジェクト

    [Header("バフのアイコン")]
    [SerializeField] private Sprite AttackIcon;               // 攻撃アップ
    [SerializeField] private Sprite InvincibleIcon;           // 無敵時間アップ
    [SerializeField] private Sprite PuzzleTimeIcon;           // ダメージ軽減
    [SerializeField] private Sprite CarryOverSpecialGaugeIcon; // コイン獲得アップ

    private string buffName;               // バフ名
    private string buffExplanationText;    // バフ説明文
    private Sprite buffIcon;               // バフアイコン

    /// <summary>
    /// 全バフをプレイヤーに適用し、UI を更新する
    /// </summary>
    public void ApplyBuffs()
    {
        // -----------------------------------------
        // 既存のバフUIを全削除
        // -----------------------------------------
        foreach (Transform child in buffListObj.transform) // ← UIリストの子要素を順番に処理
        {
            Destroy(child.gameObject);
        }

        // -----------------------------------------
        // 登録されている全バフを順番に処理
        // -----------------------------------------
        foreach (var buff in buffManager.datas) // ← すべてのバフデータを順番に処理
        {
            switch (buff.buffID) // ← バフの種類ごとに処理を分岐
            {
                // -----------------------------------------
                // 攻撃力アップ
                // -----------------------------------------
                case BuffForID.AtackMethod:
                    player.Attack = 1 + (buff.value / 100);
                    buffName = "攻撃力アップ";
                    buffExplanationText = $"プレイヤーの攻撃力が <color=#ffd700>{buff.value}%</color> 上昇";
                    buffIcon = AttackIcon;
                    break;

                // -----------------------------------------
                // 無敵時間延長
                // -----------------------------------------
                case BuffForID.InvincibleTime:
                    player.InvincibleTime = buff.value;
                    buffName = "無敵時間延長";
                    buffExplanationText = $"無敵時間が <color=#ffd700>{buff.value}秒</color> 延長";
                    buffIcon = InvincibleIcon;
                    break;

                // -----------------------------------------
                // ダメージ軽減（お邪魔ピース発生率低下）
                // -----------------------------------------
                case BuffForID.DamageDownLate:
                    player.OutPieceLate = 100 - buff.value;
                    buffName = "ダメージ軽減";
                    buffExplanationText = $"被弾時のお邪魔ピース発生率が <color=#ffd700>{buff.value}%</color> 減少";
                    buffIcon = PuzzleTimeIcon;
                    break;

                // -----------------------------------------
                // コイン獲得量アップ
                // -----------------------------------------
                case BuffForID.CoinGetLate:
                    player.DefultCoinIncreaseCount = 20 + (int)(20 * buff.value / 100);
                    buffName = "コイン獲得量アップ";
                    buffExplanationText = $"コイン獲得量が <color=#ffd700>{buff.value}%</color> 増加";
                    buffIcon = CarryOverSpecialGaugeIcon;
                    break;
            }

            // -----------------------------------------
            // バフ説明UIを生成してリストに追加
            // -----------------------------------------
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
