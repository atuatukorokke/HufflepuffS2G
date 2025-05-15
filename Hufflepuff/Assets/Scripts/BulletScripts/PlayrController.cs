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
    [SerializeField] private GameObject bulletPrehab;
    [SerializeField] private Transform gunPort;
    [SerializeField] private float delayTime;
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
        float x = Input.GetAxis("Horizontal") * Speed;
        float y = Input.GetAxis("Vertical") * Speed;
        Vector2 movement = new Vector2(x, y) * Speed * Time.deltaTime;
        transform.Translate(movement, Space.World);
        StartCoroutine(BulletCreat());
    }




    /// <summary>
    /// Zキーを押すと、球が出る
    /// </summary>
    /// <returns>nullを返す</returns>
    IEnumerator BulletCreat()
    {
        if(Input.GetKey(KeyCode.Z))
        {
            Instantiate(
                bulletPrehab, //弾幕
                gunPort.position, // 位置
                Quaternion.identity // 回転
                );
            yield return new WaitForSeconds(delayTime); //1発打ったら待ち
        }
        yield return null;
    }
}
