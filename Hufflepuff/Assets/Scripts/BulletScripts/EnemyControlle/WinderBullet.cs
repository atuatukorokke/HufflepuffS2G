// Winderbullet.cs
//
// ワインダー状に弾幕を生成する
//


using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class WinderBullet : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] private int ShotNum; // 弾幕を撃つ数
    [SerializeField] private float speed; // 弾幕のスピード
    [SerializeField] private float delayTime; // 弾幕を撃つ間隔
    [SerializeField] private float destroyTime; // 弾幕を消すまでの時間
    private float bulletSpacing;

    [SerializeField]
    [Range(0, 360)]
    float angle;
    float angleStep;

    GameObject proj;
    void Start()
    {
        StartCoroutine(WindBulletUpdate());
    }

    private IEnumerator WindBulletUpdate()
    {
        while(true)
        {
            yield return StartCoroutine(WinderBulletCreat());
            yield return new WaitForSeconds(delayTime + 2.0f);
        }
    }

    private IEnumerator WinderBulletCreat()
    {
        float shotTime = 0;

        // 弾幕をshotTimeの時間の間だけ撃つ
        while (shotTime < delayTime)
        {
            for(int i = -3; i < 3; i++)
            {
                float baseAngle = i * 20 + angle;
                float rad = baseAngle * Mathf.Deg2Rad;

                // サイン波の揺れ（時間をもとに横方向を補正）
                float timeOffset = Time.time * 5f; // 周波数を変更したい場合はここの「5f」を変える
                float wave = Mathf.Sin(timeOffset + i) * 0.3f; // 振幅を変えたいなら「0.3f」を変更

                float dirX = Mathf.Cos(rad) + wave;
                float dirY = Mathf.Sin(rad);

                Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;

                GameObject proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
                proj.transform.parent = transform;

                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * -speed;

                Destroy(proj, destroyTime);

            }
            shotTime += 0.07f;
            yield return new WaitForSeconds(0.07f);
        }
    }
}
