// PuzzleController.cs
// 
// プレイヤーからの入力を受け取り、
// 他のパズル用スクリプトを発火します。
// 

using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PieceMoves Pmove;  // パズルピースを動かすスクリプトを呼び出す用

    private void Start()
    {
        Pmove = FindAnyObjectByType<PieceMoves>();
    }

    void Update()
    {
        // 矢印キーでパズルピースを移動
        if (Input.GetKey(KeyCode.RightArrow)) Pmove.Move();
        if (Input.GetKey(KeyCode.LeftArrow)) Pmove.Move();
        if (Input.GetKey(KeyCode.UpArrow)) Pmove.Move();
        if (Input.GetKey(KeyCode.DownArrow)) Pmove.Move();

        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z)) Debug.Log("z");

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X)) Debug.Log("x");
    }
}
