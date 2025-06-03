// PieceMoves.cs
// 
// パズルピースの動きを制御をします
// 

using System.Collections;
using UnityEngine;

public class PieceMoves : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] float moveSpeed;

    bool isMoving;
    Vector2 input;

    /// <summary>
    /// 各種矢印キーで移動を可能にします
    /// </summary>
    public void PieceMove()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // 入力があったら
            if (input != Vector2.zero)
            {
                // 入力分を追加
                Vector2 targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                // 移動場所が範囲外ならば移動を許可しないif文
                if (Mathf.Pow(targetPos.x, 2) <= 49 && Mathf.Pow(targetPos.y, 2) <= 9)
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    /// <summary>
    /// 移動させるスクリプト
    /// マス目で止まるように制御できます
    /// </summary>
    private IEnumerator Move(Vector3 inTargetPos)
    {
        isMoving = true;

        // targetPosと現在のpositionの差がある間は、MoveTowardsでtargetPosに近く
        while ((inTargetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, inTargetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = inTargetPos;

        isMoving = false;
    }

    /// <summary>
    /// 呼び出すことでアタッチされたオブジェクトを反時計周りに回転させます
    /// </summary>
    public void PieceRotation()
    {
        transform.Rotate(0, 0, -90);
    }
}
