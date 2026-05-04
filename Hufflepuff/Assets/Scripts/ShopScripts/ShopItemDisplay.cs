using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemDisplay : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text buffText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private Button buyButton;

    private PieceCreate pieceCreate;
    private int pieceNumber;
    private int cost;

    /// <summary>
    /// アイテムの表示をセットアップする
    /// </summary>
    public void Setup(GameObject piecePrefab, int index, PieceCreate pc)
    {
        pieceCreate = pc;
        pieceNumber = index + 1; // PieceCreate.NewPiece は 1-indexed (0はランダム)

        ObjectDragTransform odt = piecePrefab.GetComponent<ObjectDragTransform>();
        if (odt != null)
        {
            cost = odt.SellGold;
            costText.text = cost.ToString();
            
            Buff buff = odt.PieceBuff;
            buffText.text = GetBuffExplanationText(buff);
        }

        SpriteRenderer sr = piecePrefab.GetComponent<SpriteRenderer>();
        if (sr != null && iconImage != null)
        {
            iconImage.sprite = sr.sprite;
        }

        // 購入ボタンのイベント登録
        if (buyButton != null)
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(OnBuyButtonClicked);
        }
    }

    private void OnBuyButtonClicked()
    {
        if (pieceCreate != null)
        {
            // 購入処理を呼び出す
            // PieceCreate.NewPiece の内部で所持コインのチェックが行われ、生成・コイン消費が実行される
            pieceCreate.NewPiece(pieceNumber, cost);
        }
    }

    private string GetBuffExplanationText(Buff buff)
    {
        switch (buff.buffID)
        {
            case BuffForID.AtackMethod:
                return $"攻撃力を <color=#ffd700>{buff.value}%</color> 上昇";
            case BuffForID.InvincibleTime:
                return $"無敵時間を <color=#ffd700>{buff.value}秒</color> 延長";
            case BuffForID.DamageDownLate:
                return $"パズルダメージ <color=#ffd700>{buff.value}%</color> 増加";
            case BuffForID.CoinGetLate:
                return $"コイン取得量を <color=#ffd700>{buff.value}%</color> 増加";
            default:
                return "バフ効果なし";
        }
    }
}
