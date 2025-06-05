// PlayerControlle.cs
//
// 矢印キーで移動・Zキーで弾幕
//

using System.Collections;
using UnityEngine;

public class PlayrController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float Speed; //移動速度
    [SerializeField] private GameObject bulletPrehab; //弾幕のプレハブ
    [SerializeField] private Transform gunPort; //弾幕の発射口
    [SerializeField] private float delayTime; // 発射してからのディレイ時間
    [SerializeField] private float Attack; // 攻撃力　パズル画面にも引き渡し
    [SerializeField] private bool invincible = false; // 無敵判定


    private bool isShooting = false;

    public float Attack1 { get => Attack; set => Attack = value; }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        PlayerMove();
    }

    /// <summary>
    /// プレイヤーの基本操作
    /// ・矢印キーによる移動
    /// ・Zキーを押してる間、弾幕を出す
    /// </summary>
    private void PlayerMove()
    {
        float x = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime;
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + x, -8.5f, 8.5f),
            Mathf.Clamp(transform.position.y + y, -4.5f, 4.5f));
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(!isShooting)
            {
                isShooting = true;
                StartCoroutine(BulletCreat());
            }
        }
        if(Input.GetKeyUp(KeyCode.Z))
        {
            isShooting = false;
        }
    }
    /// <summary>
    /// Zキーを押すと、球が出る
    /// </summary>
    /// <returns>nullを返す</returns>
    IEnumerator BulletCreat()
    {
        while(isShooting)
        {
            GameObject bullet = Instantiate(
            bulletPrehab, //弾幕
            gunPort.position, // 位置
            bulletPrehab.transform.rotation //回転                  
            );
            Destroy(bullet, 1);
            yield return new WaitForSeconds(delayTime); //1発打ったら待ち
        }
        
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("E_Bullet") && !invincible)
        {
            Debug.Log("死");
            Destroy(collision.gameObject);
            invincible = true;
            StartCoroutine(ResetInvincibility()); // 一定時間後に無敵解除
        }
    }
    private IEnumerator ResetInvincibility()
    {
        yield return new WaitForSeconds(3f); // 3秒間の無敵時間
        invincible = false;
    }
}
