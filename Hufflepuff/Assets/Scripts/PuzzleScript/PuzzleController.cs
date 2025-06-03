// PuzzleController.cs
// 
// プレイヤーからの入力を受け取り、
// 他のパズル用スクリプトを発火します。
// 

using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    private PieceMoves Pmoves;  // パズルピースを動かすスクリプトを呼び出す用
    private PuzzleGrid Pgrid;   // パズルピースを配置するスクリプトを呼び出す用

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
    }

    void Update()
    {
        // 矢印キーでパズルピースを移動
        if (Input.GetKey(KeyCode.RightArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.LeftArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.UpArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.DownArrow)) Pmoves.PieceMove();

        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z)) Pgrid.PiecePossible(Pmoves.transform.position, Pmoves.transform.rotation);

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X)) Pmoves.PieceRotation();
    }
}
