// PieceButtonManager.cs
// 
// ボタン入力を受付、入力されたピースを生成します。
// 

using UnityEngine;

public class PieceButtonManager : MonoBehaviour
{
    private PieceCreate Pcreate;                                // パズルピースを生成するスクリプト
    private ShopOpen shop;                                      // ショップを開くスクリプト
    private BuffSeter buffSeter;                                // バフを適応させるスクリプト
    private BuffManager buffManager;                            // バフ内容を記録するスクリプト
    private PlayrController playerController;                   // プレイヤーのコントローラー
    private GoldManager goldManager;                            // 金額管理を行うスクリプト
    private PuzzleController puzzleController;                  // パズル全体を管理するスクリプト
    private EnemySummoningManagement enemySummoningManager;     // エネミーの配置を管理するスクリプト
    [SerializeField] private AudioClip normalBGM;               // 弾幕時のBGM

    void Start()
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
    /// ショップ画面を閉じて、プレイヤーに対してバフを付与します。
    /// </summary>
    public void ShopClose()
    {
        // パズル中だったらパズル画面を閉じる
        if (enemySummoningManager.isPuzzle)
        {
            enemySummoningManager.isPuzzle = false;
            foreach (var buff in puzzleController.ProvisionalBuffs)
            {
                if (buff != null) buffManager.AddBuff(buff.buffID, buff.value);
            }
            buffSeter.ApplyBuffs(); // バフを適応させる
            playerController.CoinCount = goldManager.GetGold(); // 所持金を更新
            enemySummoningManager.GetComponent<AudioSource>().clip = normalBGM; // 通常用のBGMを設定
            enemySummoningManager.GetComponent<AudioSource>().Play(); // BGMを再生
            shop.ShopOpenAni(); // ショップのカメラを切り替える
        }
    }

    public void minoClick(int number)
    {
        Pcreate.NewPiece(number, number * 10);
    }

    public void debugPresentClick()
    {
        Pcreate.PresentBox();
    }

    public void debugBlockClick()
    {
        Pcreate.BlockCreate();
    }
}
