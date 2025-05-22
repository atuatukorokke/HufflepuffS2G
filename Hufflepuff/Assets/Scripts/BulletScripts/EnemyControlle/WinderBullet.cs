// Winderbullet.cs
//
// ワインダー状に弾幕を生成
//


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WinderBullet : MonoBehaviour
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
        InvokeRepeating("WindBulletUpdate", 0, delayTime + 2.0f);
    }

    private void WindBulletUpdate()
    {
        StartCoroutine(WinderBulletCreat());
    }

    private IEnumerator WinderBulletCreat()
    {
        int count = 0;
        while(count < 20)
        {
            targetPos = GetAnim(transform.position);
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(targetPos);
            }
            count++;
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// プレイヤーの座標を取得する
    /// </summary>
    /// <param name="E_position">エネミーの座標</param>
    /// <returns>プレイヤーとエネミーの位置関係を返す</returns>
    private Vector2 GetAnim(Vector2 E_position)
    {
        targetObj = GameObject.Find("Player");
        float dx = targetObj.transform.position.x - E_position.x;
        float dy = targetObj.transform.position.y - E_position.y;
        return new Vector2(dx, dy);
    }
}
