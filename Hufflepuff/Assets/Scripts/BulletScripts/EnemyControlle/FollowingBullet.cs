using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FollowingBullet : MonoBehaviour
{
    private GameObject targetObj; // ターゲット(プレイヤー)
    private Vector2 targetPos; // ターゲット(プレイヤー)との位置関係を入れる
    [SerializeField] private GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] private int ShotNum; // 弾幕を出す数
    [SerializeField] private float speed; // 弾幕のスピード
    [SerializeField] private float delayTime; // 弾幕を出す間隔
    [SerializeField] private float destroyTime; // 弾幕を消す時間
    GameObject proj;

     void Start()
    {
        InvokeRepeating("FollowingShoot", 0, delayTime);
    }

    private void FollowingShoot()
    {
        // プレイヤーの座標を取得
        targetObj = GameObject.Find("Player");
        targetPos = targetObj.transform.position;
        // プレイヤーの座標とエネミーの座標の位置関係
        float dx = targetPos.x - transform.position.x;
        float dy = targetPos.y - transform.position.y;
        Vector3 moveDirection = new Vector3(dx, dy, 0);
        StartCoroutine(TimeDelayShot(moveDirection));
        Destroy(proj, destroyTime);
    }

    IEnumerator TimeDelayShot(Vector3 moveDirection)
    {
        for (int i = 0; i < ShotNum; i++)
        {
            proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
            proj.transform.parent = transform;
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = moveDirection.normalized * speed;
            yield return new WaitForSeconds(0.06f);
        }
    }
}
