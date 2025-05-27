using System.Security.Cryptography;
using UnityEngine;

public enum FiringType
{
    Circle, // 円形の弾幕を打つ敵
    Following, // 追尾弾を打つ敵
    Winder // サイン波の弾幕を打つ敵
}


public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // 移動スピード
    [SerializeField] private float TimingFiring; // 発射タイミング
    [SerializeField] private FiringType firingType; // 弾幕のタイプによって移動方法を変える

    public Vector3 startPoint;
    public Vector3 targetPoint;
    public float height = 2.0f;
    public float duration = 2.0f;

    private float elapsedTime = 0f;


    private void Start()
    {
        startPoint = transform.position;
    }


    private void Update()
    {
        switch (firingType)
        {
            case FiringType.Circle:
                CurveMovement();
                break;
        }
    }
    private void CurveMovement()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float linearT = elapsedTime / duration;

            float easedT = 1 - Mathf.Pow(1 - linearT, 2);

            Vector3 horizontalPos = Vector3.Lerp(startPoint, targetPoint, easedT);

            float heightOffset = 4 * height * easedT * (1 - easedT);

            transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(startPoint.y, targetPoint.y, easedT) + heightOffset, horizontalPos.z);
        }
    }
}
