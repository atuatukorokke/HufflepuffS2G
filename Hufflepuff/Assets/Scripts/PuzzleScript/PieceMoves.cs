// PieceMoves.cs
// 
// 旧パズルピースの動きを制御をします
// ピース数の重なりをカウントします
// 

using UnityEngine;

public class PieceMoves : MonoBehaviour
{

    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト
    [SerializeField] private PieceCreate pieceCreate; // ピースを生成するスクリプト
    [SerializeField] private DestroyBlock destroyBlock; // ブロックを消すスクリプト
    private AudioSource audio;
    [SerializeField] private AudioClip putSE;
    [SerializeField] private AudioClip removeSE;

    [Header("0なら配置可能")]
    [SerializeField] private int Colliding = 0;
    [Header("配置不可の時はfalse, 配置可能ならtrue")]
    [SerializeField] private bool isPiesePossible = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        pieceCreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// コライダーを確認して、重なっていた時にCollidingをtrueにする
    /// </summary>
    public void PiecePossible()
    {
        if (Colliding == 0)
        {
            audio.PlayOneShot(putSE);
            pieceCreate.isBlockCreate = true;
            isPiesePossible = true;
        }
        else
        {
            audio.PlayOneShot(removeSE);
            isPiesePossible = false;
        }

    }

    public bool GetPiecePossible()
    {
        if (Colliding == 0)
        {
            audio.PlayOneShot(putSE);
            isPiesePossible = true;
        }
        else
        {
            audio.PlayOneShot(removeSE);
            isPiesePossible = false;
        }
        return isPiesePossible;
    }

    public void SetColliding(int newColliding)
    {
        Colliding = Colliding + newColliding;
    }
}