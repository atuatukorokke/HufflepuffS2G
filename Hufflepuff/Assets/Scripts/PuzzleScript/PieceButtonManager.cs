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

    private void Start()
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

    /// <summary>
    /// ショップを閉じ、仮バフを本適用する
    /// </summary>
    public void ShopClose()
    {
        // パズル状態なら通常状態へ戻す
        if (enemySummoningManager.isPuzzle)
        {
            enemySummoningManager.isPuzzle = false;

            // -----------------------------------------
            // 仮バフを本バフとして登録
            // -----------------------------------------
            foreach (var buff in puzzleController.ProvisionalBuffs) // ← 仮バフを順番に処理
            {
                if (buff != null)
                {
                    buffManager.AddBuff(buff.buffID, buff.value);
                }
            }

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
}
