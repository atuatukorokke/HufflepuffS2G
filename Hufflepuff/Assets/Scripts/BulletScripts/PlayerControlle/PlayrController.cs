// PlayerControlle.cs
//
// 矢印キーで移動・Zキーで弾幕
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayrController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField] private float Speed; //移動速度
    [SerializeField] private GameObject bulletPrehab; //弾幕のプレハブ
    [SerializeField] private Transform gunPort; //弾幕の発射口
    [SerializeField] private float delayTime; // 発射してからのディレイ時間

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
        float x = Input.GetAxisRaw("Horizontal") * Speed;
        float y = Input.GetAxisRaw("Vertical") * Speed;
        Vector2 movement = new Vector2(x, y) * Speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
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
            Instantiate(
            bulletPrehab, //弾幕
            gunPort.position, // 位置
            bulletPrehab.transform.rotation //回転                  
            );
            yield return new WaitForSeconds(delayTime); //1発打ったら待ち
        }
        
        yield return null;
    }
}
