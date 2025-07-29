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
    [SerializeField] private float speedLate = 1.0f; // 移動速度の遅延
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
    [SerializeField] private PlayState playState; // プレイヤーの状態


    private bool isShooting = false;

    public float Attack { get => attack; set => attack = value; }
    public float InvincibleTime { get => invincibleTime; set => invincibleTime = value; }
    public PlayState PlayState { get => playState; set => playState = value; }

    void Start()
    {
        PlayState = PlayState.Shooting; // 初期状態をシューティングに設定
        myRigidbody = GetComponent<Rigidbody2D>();
        
    }

    void Update()
    {
        if(PlayState == PlayState.Shooting) PlayerMove();
    }

    /// <summary>
    /// プレイヤーの基本操作
    /// ・矢印キーによる移動
    /// ・Zキーを押してる間、弾幕を出す
    /// </summary>
    private void PlayerMove()
    {
        // プレイヤーの移動速度を制御する
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedLate = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedLate = 1.0f; // スペースキーを離したら通常の速度に戻す
        }

        float x = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime * speedLate;
        float y = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime * speedLate;
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
                if (Attack >= 2) StartCoroutine(DiffusionBullet(3));
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
    private IEnumerator DiffusionBullet(int bulletCount)
    {
        while(isShooting)
        {
            Vector2 player = transform.up.normalized; // プレイヤーの向き

            float spreadAngle = 30f; // 放射状の角度

            float baseAbgle = Mathf.Atan2(player.x, player.y) * Mathf.Rad2Deg; // プレイヤーの向きの角度
            float angleStep = spreadAngle / (bulletCount - 1); // 弾幕の間隔
            float startAngle = baseAbgle - (spreadAngle / 2); // 開始角度

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + angleStep * i; // 弾幕の角度
                float rad = angle * Mathf.Deg2Rad; // ラジアンに変換
                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // 方向ベクトル

                GameObject bullet = Instantiate(diffusionBulletPrehab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed; // 弾幕の速度を設定
            }
            yield return new WaitForSeconds(delayTime);
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
        for(int i = 0; i < InvincibleTime; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // 赤く点滅
            yield return new WaitForSeconds(0.05f); // 0.1秒ごとに無敵状態を維持
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // 元の色に戻す
            yield return new WaitForSeconds(0.05f); // 0.1秒ごとに無敵状態を維持
        }
        invincible = false;
    }
}

public enum  PlayState
{
    Shooting, // シューティング中
    Puzzle, // パズル中
    Clear, // ゲームクリア時のアニメーション中
}
