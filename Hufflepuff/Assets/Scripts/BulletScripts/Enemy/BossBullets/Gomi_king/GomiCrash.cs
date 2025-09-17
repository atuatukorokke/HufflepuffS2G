// GomiCrach.cs
// 
// ボスエネミーの必殺技の弾幕です
// 
using System.Collections;
using UnityEngine;

public class GomiCrash : MonoBehaviour
{
    [SerializeField] private GameObject Prehab;
    [SerializeField] private float speed;
    [SerializeField] private int ShotNum;
    [SerializeField] private float angleOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Crash());
    }

    private IEnumerator Crash()
    {
        yield return new WaitForSeconds(2f);
        float speedLate = 1f;
        for(int j = 0; j < 6; j++)
        {
            float angleStep = 360 / ShotNum;
            float angle = angleOffset;
            for (int i = 0; i < ShotNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject bullet = Instantiate(Prehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * speed * speedLate;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 180); 

                angle += angleStep;
            }
            speedLate *= 0.9f;
        }
        Destroy(gameObject);
        yield return null;
    }
}
