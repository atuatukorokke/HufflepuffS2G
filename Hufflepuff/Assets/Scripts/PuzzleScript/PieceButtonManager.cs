// PieceButtonManager.cs
// 
// ボタン入力を受付、入力されたピースを生成します。
// 

using UnityEngine;

public class PieceButtonManager : MonoBehaviour
{
    private PieceCreate Pcreate;    // パズルピースを生成するスクリプト
    private ShopOpen shop;   // ショップを開くスクリプト
    private BuffSeter buffSeter; // バフを適応させるスクリプト
    private BuffManager buffManager; // バフ内容を記録するスクリプト
    private PlayrController playerController; // プレイヤーのコントローラー
    private GoldManager goldManager; // 金額管理を行うスクリプト
    private PuzzleController puzzleController; // パズル全体を管理するスクリプト

    void Start()
    {
        puzzleController = FindAnyObjectByType<PuzzleController>();
        goldManager = FindAnyObjectByType<GoldManager>();
        playerController = FindAnyObjectByType<PlayrController>();
        buffSeter = FindAnyObjectByType<BuffSeter>();
        buffManager = FindAnyObjectByType<BuffManager>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
        shop = FindAnyObjectByType<ShopOpen>();
    }
    public void ShopClose()
    {
        foreach (var buff in puzzleController.ProvisionalBuffs)
        {
            buffManager.AddBuff(buff.buffID, buff.value);
        }
        buffSeter.ApplyBuffs(); // バフを適応させる
        playerController.CoinCount = goldManager.GetGold(); // 所持金を更新
        shop.ShopOpenAni(); // ショップのカメラを切り替える
    }

    public void minoClick(int number)
    {
        Pcreate.NewPiece(number);
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
