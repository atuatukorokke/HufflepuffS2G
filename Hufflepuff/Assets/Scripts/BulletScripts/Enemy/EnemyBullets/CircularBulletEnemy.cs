using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using static UnityEngine.GraphicsBuffer;

public class CircularBulletEnemy : MonoBehaviour
{
    [Header ("円形弾幕の変数")]
    [SerializeField] private GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] private int FlyingNum; // 発射する数
    [SerializeField] private int frequency; // 発射回数
    [SerializeField] private float speed; // 弾幕のスピード
    [SerializeField] private float DeleteTime; // 削除する時間
    [SerializeField] private float delayTime; // 弾幕を出す間隔
    private float angleOffset = 0f; // ずらし用の角度

    [Header("移動用変数")]
    [SerializeField] private float destination; // 到着座標
    [SerializeField] private float limitTime; // 移動にかける時間

    private void Start()
    {
        StartCoroutine(CircularBullet(destination, limitTime));
    }

    private IEnumerator CircularBullet(float targetX, float time)
    {
        // 移動するよん
        // destinationまで移動する
        Vector2 startPosition = transform.position;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetX, elapsedTime / time),
                startPosition.y
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        while (true)
        {
            yield return StartCoroutine(ShootIncircle());
            yield return new WaitForSeconds(delayTime + 2.0f);
        }
    }

    /// <summary>
    /// 弾幕を円形に出します
    /// </summary>
    private IEnumerator ShootIncircle()
    {
        // 弾の横間隔の計算
        float angleStep = 360f / FlyingNum;
        float angle = angleOffset;

        // frequencyの回数だけ弾幕を生成する
        // FlyingNumは一回の生成で何個弾幕を作り出すか
        for (int i = 0; i < frequency; i++)
        {
            for (int j = 0; j < FlyingNum; j++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * speed;

                angle += angleStep;

                Destroy(proj, DeleteTime); // 何秒後に弾幕を消す

            }
            angleOffset += 10f; // ここを変えれば回転速度が変わる
            if (angleOffset >= 360) angleOffset -= 360f; // 範囲内を保つ
            yield return new WaitForSeconds(delayTime);
        }
    }
}
