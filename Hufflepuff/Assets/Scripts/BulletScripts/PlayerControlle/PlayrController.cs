// PlayerControlle.cs
//
// 矢印キーで移動・Zキーで弾幕
//

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]

public class PlayrController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float Speed; // 移動速度
    [Range(0f, 5f)]
    [SerializeField] private float attack;
    [SerializeField] private GameObject straightBulletPrehab; // 直線弾幕のプレハブ
    [SerializeField] private GameObject diffusionBulletPrehab; // 拡散用の弾幕プレハブ
    [SerializeField] private float bulletSpeed = 20f; // 弾幕の速度
    [SerializeField] private Transform gunPort; // 弾幕の発射口
    [SerializeField] private float delayTime; // 発射してからのディレイ時間
    [SerializeField] private bool invincible = false; // 無敵判定
    [SerializeField] private float invincibleTime = 15f; // 無敵時間
    [SerializeField] private int presentCount = 0; // プレゼントの数


    private bool isShooting = false;



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
                // 直線状に弾幕を飛ばす
                StartCoroutine(BulletCreat());
                // 拡散するように弾幕を飛ばす
                //if (attack >= 2) StartCoroutine(DiffusionBullet());
            }
        }
        if(Input.GetKeyUp(KeyCode.Z))
        {
            isShooting = false;
        }
    }
    /// <summary>
    /// 直線状に弾幕を出します
    /// </summary>
    /// <returns>nullを返す</returns>
    private IEnumerator BulletCreat()
    {
        while(isShooting)
        {
            GameObject bullet = Instantiate(
            straightBulletPrehab, //弾幕
            gunPort.position, // 位置
            straightBulletPrehab.transform.rotation //回転                  
            );

            bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(1, 0, 0) * bulletSpeed;
            yield return new WaitForSeconds(delayTime); //1発打ったら待ち
        }
        
        yield return null;
    }

    /// <summary>
    /// 放射状に弾幕を出します
    /// </summary>
    private IEnumerator DiffusionBullet()
    {
        float spreadAngle = 60;

        while (isShooting)
        {
            for (int i = 0; i < 5; i++)
            {
                float angle = -spreadAngle / 2 + (spreadAngle / 2) * i;
                Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.right;

                GameObject bullet = Instantiate(diffusionBulletPrehab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * Speed;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "E_Bullet":
                if(!invincible)
                {
                    Destroy(collision.gameObject);
                    invincible = true;
                    StartCoroutine(ResetInvincibility()); // 一定時間後に無敵解除
                }
                break;
            case "Present":
                presentCount++;
                Destroy(collision.gameObject);
                if (presentCount == 10) 
                {
                    Debug.Log("プレゼントを10個集めた！");
                }
                break;  
        }
    }
    private IEnumerator ResetInvincibility()
    {
        for(int i = 0; i < invincibleTime; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // 赤く点滅
            yield return new WaitForSeconds(0.05f); // 0.1秒ごとに無敵状態を維持
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // 元の色に戻す
            yield return new WaitForSeconds(0.05f); // 0.1秒ごとに無敵状態を維持
        }
        invincible = false;
    }
}
