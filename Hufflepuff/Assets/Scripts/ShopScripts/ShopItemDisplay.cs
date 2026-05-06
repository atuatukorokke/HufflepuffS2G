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
    private GameObject specialPrefab;

    /// <summary>
    /// アイテムの表示をセットアップする
    /// </summary>
    public void Setup(GameObject piecePrefab, int index, PieceCreate pc)
    {
        pieceCreate = pc;
        pieceNumber = index + 1; // PieceCreate.NewPiece は 1-indexed (0はランダム、特殊はindex=-1でpieceNumber=0)
        
        if (index == -1)
        {
            specialPrefab = piecePrefab;
        }

        ObjectDragTransform odt = piecePrefab.GetComponent<ObjectDragTransform>();
        if (odt != null)
        {
            cost = odt.SellGold;
            costText.text = cost.ToString();
            
            Buff buff = odt.PieceBuff;
            buffText.text = GetBuffExplanationText(buff);

            if (iconImage != null && odt.IconSprite != null)
            {
                iconImage.sprite = odt.IconSprite;
            }
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
            bool isBought = false;
            
            if (pieceNumber == 0 && specialPrefab != null)
            {
                // 特殊ピース（ボム等）の購入
                isBought = pieceCreate.NewSpecialPiece(specialPrefab, cost);
            }
            else
            {
                // 通常ピースの購入
                isBought = pieceCreate.NewPiece(pieceNumber, cost);
            }

            if (isBought)
            {
                // 購入成功したらこのアイテムのUIを消す
                Destroy(gameObject);
            }
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
