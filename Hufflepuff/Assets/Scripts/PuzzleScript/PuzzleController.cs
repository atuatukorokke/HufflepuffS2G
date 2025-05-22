// PuzzleController.cs
// 
// プレイヤーからの入力を受け取り、
// 他のスクリプトを発火します。
// 

using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // 矢印キーでパズルピースを移動
        if (Input.GetKey(KeyCode.RightArrow)) ;
        if (Input.GetKey(KeyCode.LeftArrow)) ;
        if (Input.GetKey(KeyCode.UpArrow)) ;
        if (Input.GetKey(KeyCode.DownArrow)) ;

        // zキーでパズルピースを設置
        if (Input.GetKeyDown(KeyCode.Z)) ;

        // xキーでパズルピースを回転
        if (Input.GetKeyDown(KeyCode.X)) ;
    }
}
