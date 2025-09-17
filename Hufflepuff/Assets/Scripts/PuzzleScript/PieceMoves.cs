// PieceMoves.cs
// 
// 旧パズルピースの動きを制御をします
// ピース数の重なりをカウントします
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PieceMoves : MonoBehaviour
{

    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト
    [SerializeField] private DestroyBlock destroyBlock; // ブロックを消すスクリプト

    [Header("0なら配置可能")]
    [SerializeField] private int Colliding = 0;
    [Header("配置不可の時はfalse, 配置可能ならtrue")]
    [SerializeField] private bool isPiesePossible = false;

    private void Start()
    {

    }

    /// <summary>
    /// コライダーを確認して、重なっていた時にCollidingをtrueにする
    /// </summary>
    public void PiecePossible()
    {
        if (Colliding == 0)
        {
            Debug.Log("配置可能");
            isPiesePossible = true;
        }
        else
        {
            Debug.Log("配置不可");
            isPiesePossible = false;
        }

    }

    public bool GetPiecePossible()
    {
        if (Colliding == 0)
        {
            Debug.Log("配置可能");
            isPiesePossible = true;
        }
        else
        {
            Debug.Log("配置不可");
            isPiesePossible = false;
        }
        return isPiesePossible;
    }

    public void SetColliding(int newColliding)
    {
        Colliding = Colliding + newColliding;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("当たった");
        Colliding = Colliding + 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = Colliding - 1;
    }
    */
}