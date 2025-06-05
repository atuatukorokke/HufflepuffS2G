// PieceList.cs
// 
// 各パズルピースの形の情報を保存します
// 列挙体で制御するのでパズルピースの名前で呼び出すことができるようにしたいです
// 

using UnityEngine;

public class PieceList : MonoBehaviour
{
    enum PuzzlePiece
    {
        // 1. モノ（ mono ） · 2. ジ（ di ） · 3. トリ（ tri ） · 4. テトラ（ tetra ）

        // 四つブロック
        Imino = 1,
        Jmino = 2,
        Lmino = 3,
        Omino = 4,
        Smino = 5,
        Tmino = 6,
        Zmino = 7,

        // 三つのブロック
        triJmino = 8,
        triLmino = 9,

        // 二つのブロック
        dimino = 10,

        // 一つのブロック
        monomino = 11,
    }

    //int[,,] Pieces = new int[12, 2, 2];
    private int[,,] test = { { { 1, 0 }, { -1, 0 }, { 4, 0 } } };


    public int PiecesList()
    {


        return 0;
    }
}
