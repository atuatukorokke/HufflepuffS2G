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

    void Start()
    {
        buffSeter = FindAnyObjectByType<BuffSeter>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
        shop = FindAnyObjectByType<ShopOpen>();
    }
    public void ShopClose()
    {
        buffSeter.ApplyBuffs(); // バフを適応させる
        shop.ShopOpenAni(); // ショップのカメラを切り替える
    }

    public void minoClick(int number)
    {
        Pcreate.NewPiece(number);
    }
    public void PieceAddBuff(int BuffID)
    {
        Pcreate.PieceAddBuff((BuffForID)BuffID);
    }
}
