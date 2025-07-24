// MiddleBossBullet.cs 
// 
// 中ボスの弾幕を管理するスクリプト
//

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    noemal, // 通常弾幕
    spell, // スペルカード
}

public class MiddleBossBullet : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType; // エネミーの状態を管理する列挙型
    [SerializeField] private BossHealth bossHealth; // ボスのＨＰを管理するスクリプト
    private float damageLate = 1; // 被ダメージの割合
    [SerializeField] private GameObject presentBox; // ドロップ用のプレハブ
    [SerializeField] private GameObject HealthCanvas; // 現在のＨＰバーのキャンバス
    [SerializeField] private GameObject currentHpbar; // 現在のＨＰバーのオブジェクト
    private float maxHp; // ボスの最大ＨＰ
    GameObject canvas; // ＨＰバーのキャンバス

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
        canvas = Instantiate(HealthCanvas, Vector3.zero, Quaternion.identity); // ＨＰバーのキャンバスを生成
        currentHpbar = canvas.transform.GetChild(0).Find("currentHpBar").gameObject; // 現在のＨＰバーのオブジェクトを取得; // 現在のＨＰバーを取得

        bossHealth = FindAnyObjectByType<BossHealth>(); // ボスのＨＰ管理スクリプトを取得
        maxHp = bossHealth.hP; // ボスの最大ＨＰを取得
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
            if (radius > stopThreshold && enemyType == EnemyType.noemal)
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

    private IEnumerator MiddleApell()
    {
        while(enemyType == EnemyType.spell)
        {
            Debug.Log("スペルカード発動中");
            yield return new WaitForSeconds(5f); // スペルカードの間隔
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // プレイヤーの弾を消す
            bossHealth.hP -= damageLate; // エネミーのHPを減らす
            currentHpbar.transform.localScale = new Vector3(bossHealth.hP / 100, currentHpbar.transform.localScale.y, currentHpbar.transform.localScale.z); // 現在のHPバーを更新

            if (bossHealth.hP <= 20 && enemyType == EnemyType.noemal)
            {
                enemyType++; // エネミーの状態をスペルカードに変更
                damageLate = 0.2f; // 被ダメージの割合を変更
                StartCoroutine(MiddleApell()); // スペルカードの発動
            }
            else if(bossHealth.hP <= 0)
            {
                Destroy(canvas); // ＨＰバーのキャンバスを削除
                GameObject present = Instantiate(presentBox, transform.position, Quaternion.identity); // ドロップ用のプレハブを生成
                present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0); // ドロップの速度を設定
            }
        }
    }
}
