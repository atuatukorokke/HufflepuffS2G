// PieceButtonManager.cs
// 
// ボタン入力を受付、入力されたピースを生成します。
// 

using UnityEngine;

public class PieceButtonManager : MonoBehaviour
{
    private PieceCreate Pcreate;    // パズルピースを生成するスクリプト

    void Start()
    {
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    void Update()
    {
        
    }

    public void mino1Click()
    {
        Pcreate.NewPiece(1);
    }

    public void mino2Click()
    {
        Pcreate.NewPiece(2);
    }

    public void mino3Click()
    {
        Pcreate.NewPiece(3);
    }

    public void mino4Click()
    {
        Pcreate.NewPiece(4);
    }

    public void mino5Click()
    {
        Pcreate.NewPiece(5);
    }

    public void mino6Click()
    {
        Pcreate.NewPiece(6);
    }

    public void mino9Click()
    {
        Pcreate.NewPiece(7);
    }
}
