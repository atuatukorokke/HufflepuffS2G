// MiddleBossBullet.cs 
// 
// 中ボスの弾幕を管理するスクリプト
//

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyType
{
    noemal, // 通常弾幕
    spell, // スペルカード
}

public class MiddleBossBullet : MonoBehaviour
{
    private EnemyHealth enemyHealth; // エネミーのヘルスを管理するスクリプト
    [SerializeField] private EnemyType enemyType; // エネミーの状態を管理する列挙型

    [Header("通常弾幕用の変数")]
    [SerializeField] private GameObject bulletPrefab; // 弾幕のプレハブ
    [SerializeField] private int shotNum; // 弾幕を出す数
    [SerializeField] private float radiusShrinkSpeed = 0.05f; // 半径の縮小速度
    [SerializeField] private float radius = 3.0f; // 弾幕の生成位置の半径
    [SerializeField] private float bulletSpeed; // 弾幕のスピード
    [SerializeField] private float bulletDelayTime; // 弾幕を出す間隔

    /// <summary>
    /// 初期設定をします
    /// </summary>
    private void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        StartCoroutine(StartBullet());
    }

    /// <summary>
    /// 弾幕の生成位置を竜巻状に設定し、弾幕を生成します
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartBullet()
    {
        float angleStep = 360f / shotNum; // 弾幕の生成位置の角度差
        float stopThreshold = 0.2f; // 半径がこの値以下になったら停止
        float globalAngle = 0f; // 渦巻用の回転角

        while (enemyType == EnemyType.noemal)
        {
            if (radius > stopThreshold)
            {
                for (int i = 0; i < shotNum; i++)
                {
                    float angle = globalAngle + (i * angleStep); // 弾ごとの角度を計算

                    // 弾幕の生成位置を計算
                    Vector2 bulletPos = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad),
                        Mathf.Sin(angle * Mathf.Deg2Rad)
                        ) * radius;

                    // 弾幕を生成
                    Vector2 spwanPos = (Vector2)transform.position + bulletPos;
                    GameObject bullet = Instantiate(bulletPrefab, spwanPos, Quaternion.identity);

                    // 弾幕の回転を設定
                    Vector2 direction = bulletPos.normalized; // 弾幕の移動方向を計算
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = direction * bulletSpeed;
                }
                radius -= radiusShrinkSpeed; // 半径を縮小
                globalAngle += 10f; // グローバル角度を更新
            }
            yield return new WaitForSeconds(bulletDelayTime);
        }
        yield return null; // 1フレーム待機
    }
}
