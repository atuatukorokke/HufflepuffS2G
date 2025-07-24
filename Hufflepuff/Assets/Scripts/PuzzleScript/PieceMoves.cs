// PieceMoves.cs
// 
// パズルピースの動きを制御をします
// 

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PieceMoves : MonoBehaviour
{
    private PuzzleGrid Pgrid;   // パズルピースを配置するスクリプトを呼び出す
    private PieceCreate Pcreate;    // パズルピースを生成するスクリプト

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// PuzzleGridを呼び出しパズルピースが配置可能かを確認します
    /// </summary>
    public void PiecePossible(int r)
    {
        // gameObjectを取得
        GameObject thisPiece = this.gameObject;

        bool banana;
        // PuzzleCheck を呼び出すことで配置してもよいか処理をする
        banana = Pgrid.PuzzleCheck(thisPiece, transform.position.x - 3, transform.position.y - 2, r);
        // 配置できるなら移動用スクリプトを削除して配置する
        if (banana)
        {
            var thisPieceScript = GetComponent<PieceMoves>();
            Destroy(thisPieceScript, 0.1f);
            BoxCollider2D thisPieceCollider = GetComponent<BoxCollider2D>();
            Destroy(thisPieceCollider, 0.1f);
        }
    }

    /// <summary>
    /// 呼び出すことでアタッチされたオブジェクトを反時計周りに回転させます
    /// </summary>
    public int PieceRotation(int r)
    {
        // gameObjectを取得
        GameObject thisPiece = this.gameObject;     // Destroyすると昔のgameObjectを読み込む　なぜ？

        switch (thisPiece.tag)
        {
            case "mino1":
            case "mino4":
            case "mino9":
                Debug.Log("回転しないピースです");
                r = 0;
                break;
            case "mino2":
            case "mino5":
            case "mino6":
                r = (r == 0) ? 1 : 0;
                Pcreate.PieceRotationCreate(thisPiece, r);
                Destroy(thisPiece, 0.1f);
                thisPiece = null;
                break;
            case "mino3":
                r = r == 0 ? 1 : r == 1 ? 2 : r == 2 ? 3 : 0; // 0,1,2,3の4回転
                Pcreate.PieceRotationCreate(thisPiece, r);
                Destroy(thisPiece, 0.1f);
                thisPiece = null;
                break;
            default:
                Debug.Log("タグが読み取れてません PieceRotation()");
                break;
        }

        return r;
    }
}