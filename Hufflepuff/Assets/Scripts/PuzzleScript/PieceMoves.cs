// ========================================
//
// PieceMoves.cs
//
// ========================================
//
// ピースの設置可否を判定するクラス。
// ・衝突数（Colliding）をカウントして、置ける状態かどうかを判断
// ・置ける場合は SE 再生＋フラグ更新
// ・置けない場合も SE 再生＋フラグ更新
//
// ========================================

using UnityEngine;

public class PieceMoves : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;     // ピースの重なり判定スクリプト
    [SerializeField] private PieceCreate pieceCreate;   // ピース生成スクリプト
    [SerializeField] private DestroyBlock destroyBlock; // ブロック削除スクリプト

    private AudioSource audio;
    [SerializeField] private AudioClip putSE;           // 設置可能時のSE
    [SerializeField] private AudioClip removeSE;        // 設置不可時のSE

    [Header("0なら設置可能")]
    [SerializeField] private int Colliding = 0;         // 衝突数（0なら設置可能）

    [Header("設置可能ならtrue")]
    [SerializeField] private bool isPiesePossible = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        pieceCreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// 設置可能かどうかを判定し、SE とフラグを更新する
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

    /// <summary>
    /// 設置可能かどうかを返す（呼び出し時に SE も再生）
    /// </summary>
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

    /// <summary>
    /// 衝突数を加算・減算する
    /// </summary>
    public void SetColliding(int newColliding)
    {
        Colliding = Colliding + newColliding;
    }
}
