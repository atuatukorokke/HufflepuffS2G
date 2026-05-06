// ========================================
//
// PieceButtonManager.cs
//
// ========================================
//
// ボタン入力に応じてピース生成やデバッグ操作を行うクラス。
// ・ショップを閉じた際に仮バフを本適用
// ・プレイヤーのコイン更新
// ・BGM の切り替え
// ・ピース生成ボタン / デバッグボタンの処理
//
// ========================================

using UnityEngine;
using TMPro;

public class PieceButtonManager : MonoBehaviour
{
    private PieceCreate Pcreate;                            // ピース生成スクリプト
    private ShopOpen shop;                                  // ショップ開閉スクリプト
    private BuffSeter buffSeter;                            // バフ適用スクリプト
    private BuffManager buffManager;                        // バフデータ管理
    private PlayrController playerController;               // プレイヤー操作スクリプト
    private GoldManager goldManager;                        // ゴールド管理
    private PuzzleController puzzleController;              // パズル管理
    private EnemySummoningManagement enemySummoningManager; // 敵召喚管理
    [SerializeField] private AudioClip normalBGM;           // 通常時のBGM
    [SerializeField] private GameObject shopPanell;         // ショップパネル

    [Header("プレゼントボックスUI")]
    [SerializeField] private TMP_Text presentBoxCountText;  // プレゼントボックス残数表示

    private void Awake()
    {
        puzzleController = FindAnyObjectByType<PuzzleController>();
        goldManager = FindAnyObjectByType<GoldManager>();
        playerController = FindAnyObjectByType<PlayrController>();
        buffSeter = FindAnyObjectByType<BuffSeter>();
        buffManager = FindAnyObjectByType<BuffManager>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
        shop = FindAnyObjectByType<ShopOpen>();
        enemySummoningManager = FindAnyObjectByType<EnemySummoningManagement>();
    }

    private void Start()
    {
        shopPanell.SetActive(false);
    }

    /// <summary>
    /// ショップを閉じ、仮バフを本適用する
    /// </summary>
    public void ShopClose()
    {
        // パズル状態なら通常状態へ戻す
        if (enemySummoningManager.isPuzzle)
        {
            // --- 退出チェック ---
            ObjectDragTransform[] allPieces = FindObjectsByType<ObjectDragTransform>(FindObjectsSortMode.None);
            foreach (var piece in allPieces)
            {
                // 右半分（パズル領域）にあるピースをチェック
                if (piece.transform.position.x > puzzleController.PuzzleBorderX)
                {
                    if (piece.LocalColliding != 0)
                    {
                        Debug.Log("配置できないピースがあるため、パズル画面から出られません。");
                        return; // 退出キャンセル
                    }
                }
            }

            enemySummoningManager.isPuzzle = false;

            // --- バフの反映 ---
            // 以前のバフを一旦クリアし、現在の盤面の状態から再集計する
            buffManager.datas.Clear();
            int appliedBuffCount = 0;

            // 1. 確定済み（過去に盤面から消滅したピース）のバフを追加
            foreach (var buff in puzzleController.ConfirmedBuffs)
            {
                if (buff != null)
                {
                    buffManager.AddBuff(buff.buffID, buff.value);
                    appliedBuffCount++;
                }
            }

            // 2. 現在盤面上にあるピースのバフを追加
            foreach (var piece in allPieces)
            {
                // 右半分（パズル領域）にあるピースのバフのみを適用
                if (piece.transform.position.x > puzzleController.PuzzleBorderX)
                {
                    Buff buff = piece.PieceBuff;
                    if (buff != null)
                    {
                        buffManager.AddBuff(buff.buffID, buff.value);
                        appliedBuffCount++;
                    }
                }
            }

            Debug.Log($"バフ再集計完了: 盤面判定用X座標閾値 = {puzzleController.PuzzleBorderX}, 反映されたバフ付きピース数 = {appliedBuffCount}");

            // バフをプレイヤーへ適用
            buffSeter.ApplyBuffs();

            // 所持コインを更新
            playerController.CoinCount = goldManager.GetGold();

            // -----------------------------------------
            // BGM を通常状態に戻す
            // -----------------------------------------
            enemySummoningManager.GetComponent<AudioSource>().clip = normalBGM;
            enemySummoningManager.GetComponent<AudioSource>().Play();

            // ショップを閉じるアニメーション
            shop.ShopOpenAni();
        }
    }

    /// <summary>
    /// ピース生成ボタン
    /// </summary>
    public void minoClick(int number)
    {
        Pcreate.NewPiece(number, number * 10);
    }

    /// <summary>
    /// デバッグ：プレゼント生成
    /// </summary>
    public void debugPresentClick()
    {
        Pcreate.PresentBox();
    }

    /// <summary>
    /// デバッグ：ブロック生成
    /// </summary>
    public void debugBlockClick()
    {
        Pcreate.BlockCreate();
    }

    /// <summary>
    /// プレゼントボックスを開けるボタン用のメソッド
    /// </summary>
    public void OpenPresentBox()
    {
        if (Pcreate != null)
        {
            Pcreate.PresentBox();
            UpdatePresentBoxText(); // 残数テキストの更新
        }
    }

    /// <summary>
    /// プレゼントボックスの残数をUIに反映する
    /// </summary>
    public void UpdatePresentBoxText()
    {
        if (presentBoxCountText != null && playerController != null)
        {
            presentBoxCountText.text = $"残り: {playerController.PieceCount}";
        }
    }

    /// <summary>
    /// ショップ画面を開きます
    /// </summary>
    public void ShopPanelOpen()
    {
        if (shopPanell != null)
        {
            // 現在の状態を反転させる
            bool isActive = !shopPanell.activeSelf;
            shopPanell.SetActive(isActive);

            // 開いた時だけ購入回数をリセットする
            if (isActive && Pcreate != null)
            {
                Pcreate.ResetShopBuyCount();
                UpdatePresentBoxText(); // ショップを開いた時に残数を更新
            }
        }
    }
}
