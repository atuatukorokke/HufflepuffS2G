// PieceMoves.cs
// 
// パズルピースの動きを制御をします
// 

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PieceMoves : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] float moveSpeed;

    private PuzzleGrid Pgrid;   // パズルピースを配置するスクリプトを呼び出す用

    private int CountRotate = 0;
    /*
    bool isMoving;
    Vector2 input;
    */

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
    }

    // 過去のピースの移動スクリプト
    /*
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
                if (Mathf.Pow(targetPos.x, 2) <= 64 && Mathf.Pow(targetPos.y, 2) <= 16)
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
    */

    /// <summary>
    /// PuzzleGridを呼び出しパズルピースが配置可能かを確認します
    /// </summary>
    public void PiecePossible()
    {
        bool banana;
        // PuzzleCheck を呼び出すことで配置してもよいか処理をする
        banana = Pgrid.PuzzleCheck(gameObject, transform.position.x - 3, transform.position.y - 2, CountRotate);
        // 配置できるなら移動用スクリプトを削除して配置する
        if (banana)
        {
            var thisPieceScript = GetComponent<PieceMoves>();
            Destroy(thisPieceScript);
            BoxCollider2D thisPieceCollider = GetComponent<BoxCollider2D>();
            Destroy(thisPieceCollider);
        }
    }

    /// <summary>
    /// 呼び出すことでアタッチされたオブジェクトを反時計周りに回転させます
    /// </summary>
    public void PieceRotation()
    {
        // 必要最低カウント数を設定
        switch (gameObject.tag)
        {
            // 回転無し
            case "1mino":
            case "4mino":
            case "9mino":
                CountRotate = 0;
                break;
            // 回転２つ
            case "2mino":
            case "5mino":
            case "6mino":
                CountRotate = (CountRotate + 1) % 2;
                break;
            // 回転４つ
            case "3mino":
                CountRotate = (CountRotate + 1) % 4;
                break;
            default:
                Debug.Log("変なタグを読み取ってます");
                break;

        }

        int test = CountRotate * -90;
        Debug.Log(CountRotate);
        transform.localRotation = Quaternion.Euler(0, 0, test);
    }
}
