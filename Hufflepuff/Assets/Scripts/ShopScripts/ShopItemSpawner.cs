using UnityEngine;
using UnityEngine.UI;

public class ShopItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab; // 繧ｷ繝ｧ繝・・繧｢繧､繝・Β縺ｮ繝励Ξ繝上ヶ
    [SerializeField] private Transform contentParent;   // 繝励Ξ繝上ヶ繧堤函謌舌☆繧玖ｦｪ(閭梧勹繧ｪ繝悶ず繧ｧ繧ｯ繝医↑縺ｩ)
    [SerializeField] private Button updateButton;       // 譖ｴ譁ｰ繝懊ち繝ｳ
    [SerializeField] private PieceCreate pieceCreate;   // 繝斐・繧ｹ縺ｮ諠・ｱ繧呈戟縺､繧ｯ繝ｩ繧ｹ
    [SerializeField] private GoldManager goldManager;   // 繧ｴ繝ｼ繝ｫ繝・繧ｳ繧､繝ｳ)邂｡逅・け繝ｩ繧ｹ

    [Header("ボムピース設定")]
    [SerializeField] private GameObject bombPiecePrefab;
    [Range(0, 100)]
    [SerializeField] private float bombPieceSpawnRate = 5f;

    private void Start()
    {
        // 更新ボタンのクリックイベントを登録
        if (updateButton != null)
        {
            updateButton.onClick.AddListener(OnUpdateButtonClicked);
        }

        // PieceCreate と GoldManager がシーン内に存在しない場合は、FindAnyObjectByType を使用して取得
        if (pieceCreate == null)
        {
            pieceCreate = FindAnyObjectByType<PieceCreate>();
        }

        // GoldManager がシーン内に存在しない場合は、FindAnyObjectByType を使用して取得
        if (goldManager == null)
        {
            goldManager = FindAnyObjectByType<GoldManager>();
        }

        RefreshShop();
    }

    /// <summary>
    /// 更新ボタンがクリックされたときの処理
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
        }
        else
        {
            RefreshShop();  // GoldManager が見つからない場合でもショップを更新する
        }
    }

    /// <summary>
    /// ショップのアイテムを更新する処理
    /// </summary>
    public void RefreshShop()
    {
        // 既存のアイテムを削除
        if (contentParent == null || shopItemPrefab == null)
        {
            return;
        }

        // contentParent の子オブジェクトをすべて削除
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // 新しいアイテムを生成
        if (pieceCreate == null || pieceCreate.Pieces == null || pieceCreate.Pieces.Length == 0)
        {
            return;
        }

        // 5つのアイテムを生成（必要に応じて数を変更可能）
        for (int i = 0; i < 5; i++)
        {
            GameObject item = Instantiate(shopItemPrefab, contentParent);
            ShopItemDisplay display = item.GetComponent<ShopItemDisplay>();

            if (display != null)
            {
                // ボムピースの出現判定
                bool spawnBomb = bombPiecePrefab != null && Random.Range(0f, 100f) < bombPieceSpawnRate;

                if (spawnBomb)
                {
                    // ボムピースを表示（indexは特殊用途として -1 などを渡すか、適切な処理を行う）
                    display.Setup(bombPiecePrefab, -1, pieceCreate);
                }
                else
                {
                    // 通常のピースをランダムに選択して表示
                    int maxIndex = pieceCreate.Pieces.Length;
                    int randomIndex = Random.Range(0, maxIndex - 1); 
                    GameObject piecePrefab = pieceCreate.Pieces[randomIndex];

                    display.Setup(piecePrefab, randomIndex, pieceCreate);
                }
            }
        }
    }
}
