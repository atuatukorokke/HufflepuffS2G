// ========================================
//
// FollowingBullet.cs
//
// ========================================
//
// プレイヤーを追尾しながら弾を撃つ敵の挙動。
// ・上下どちらかへ移動しながら一定間隔で追尾弾を発射
// ・プレイヤーの現在位置を取得してその方向へ ShotNum 連射
// ・画面外に出たら自壊
//
// ========================================

using System.Collections;
using UnityEngine;

public class FollowingBullet : MonoBehaviour
{
    [Header("弾幕設定")]
    private GameObject targetObj;                       // 追尾対象（プレイヤー）
    private Vector2 targetPos;                          // プレイヤー位置
    [SerializeField] private GameObject BulletPrehab;   // 弾のプレハブ
    [SerializeField] private int ShotNum;               // 一度に撃つ弾数
    [SerializeField] private float speed;               // 弾の速度
    [SerializeField] private float delayTime;           // 発射間隔
    [SerializeField] private float destroyTime;         // 弾の寿命
    GameObject proj;

    [Header("移動設定")]
    [SerializeField] private float moveSpeed;           // 敵本体の移動速度
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 初期位置が上なら下へ、下なら上へ移動
        if (transform.position.y > 0)
        {
            rb.linearVelocity = new Vector2(0, -moveSpeed) * Time.deltaTime * moveSpeed;
        }
        else
        {
            rb.linearVelocity = new Vector2(0, moveSpeed) * Time.deltaTime * moveSpeed;
        }

        // 一定間隔で追尾弾を撃つ
        InvokeRepeating(nameof(FollowingShoot), 0, delayTime);
    }

    private void Update()
    {
        // 画面外に出たら削除
        if (transform.position.y < -8 || transform.position.y > 8)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーの方向を計算して追尾弾を撃つ
    /// </summary>
    private void FollowingShoot()
    {
        targetObj = GameObject.Find("Player");
        if (targetObj == null) return;

        targetPos = targetObj.transform.position;

        float dx = targetPos.x - transform.position.x;
        float dy = targetPos.y - transform.position.y;

        Vector3 moveDirection = new Vector3(dx, dy, 0);

        StartCoroutine(TimeDelayShot(moveDirection));
    }

    /// <summary>
    /// ShotNum 回、一定間隔で追尾弾を発射
    /// </summary>
    private IEnumerator TimeDelayShot(Vector3 moveDirection)
    {
        // 画面内に見えている時だけ発射
        if (GetComponent<SpriteRenderer>().isVisible)
        {
            for (int i = 0; i < ShotNum; i++)
            {
                proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();

                rb.linearVelocity = moveDirection.normalized * speed;

                Destroy(proj, destroyTime);

                yield return new WaitForSeconds(0.06f);
            }
        }
    }
}
