using UnityEngine;
using UnityEngine.UI;

public class ShopItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab; // ショップアイテムのプレハブ
    [SerializeField] private Transform contentParent;   // プレハブを生成する親(背景オブジェクトなど)
    [SerializeField] private Button updateButton;       // 更新ボタン
    [SerializeField] private PieceCreate pieceCreate;   // ピースの情報を持つクラス
    [SerializeField] private GoldManager goldManager;   // ゴールド(コイン)管理クラス

    private void Start()
    {
        if (updateButton != null)
        {
            updateButton.onClick.AddListener(OnUpdateButtonClicked);
        }

        if (pieceCreate == null)
        {
            pieceCreate = FindAnyObjectByType<PieceCreate>();
        }

        if (goldManager == null)
        {
            goldManager = FindAnyObjectByType<GoldManager>();
        }

        RefreshShop(); // 初期生成（コイン消費なし）
    }

    /// <summary>
    /// 更新ボタンが押されたときの処理
    /// </summary>
    private void OnUpdateButtonClicked()
    {
        if (goldManager != null)
        {
            if (goldManager.GetGold() >= 10)
            {
                goldManager.SetGoldCount(-10);
                RefreshShop();
            }
            else
            {
                Debug.Log("コインが足りないためショップを更新できません。");
            }
        }
        else
        {
            RefreshShop(); // GoldManagerがない場合はとりあえず更新
        }
    }

    /// <summary>
    /// ショップのアイテムを更新(再生成)する
    /// </summary>
    public void RefreshShop()
    {
        if (contentParent == null || shopItemPrefab == null)
        {
            Debug.LogWarning("ShopItemSpawner: contentParent または shopItemPrefab が設定されていません。");
            return;
        }

        // 既存のアイテムをすべて削除
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        if (pieceCreate == null || pieceCreate.Pieces == null || pieceCreate.Pieces.Length == 0)
        {
            Debug.LogWarning("ShopItemSpawner: PieceCreate または Pieces が設定されていません。");
            return;
        }

        // 5つのアイテムをランダムに生成
        for (int i = 0; i < 5; i++)
        {
            GameObject item = Instantiate(shopItemPrefab, contentParent);
            ShopItemDisplay display = item.GetComponent<ShopItemDisplay>();

            if (display != null)
            {
                // 最後のピースはゴミ箱などの特殊用途とみられるため除外
                int maxIndex = pieceCreate.Pieces.Length;
                int randomIndex = Random.Range(0, maxIndex - 1); 
                GameObject piecePrefab = pieceCreate.Pieces[randomIndex];

                display.Setup(piecePrefab, randomIndex, pieceCreate);
            }
        }
    }
}
