using System.Collections;
using UnityEngine;

public class CircularBulletEnemy : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] private int FlyingNum; // 発射する数
    [SerializeField] private float speed;

    private void Start()
    {
        InvokeRepeating("ShootIncircle",0f, 3.0f);
    }

    private void ShootIncircle()
    {
        float angleStep = 360f / FlyingNum;
        float angle = 0f;

        for (int i = 0; i < FlyingNum; i++)
        {
            float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 moveDirection = new Vector3(dirX, dirY, 0);

            GameObject proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.linearVelocity = moveDirection.normalized * speed;

            angle += angleStep;
        }
    }
}
