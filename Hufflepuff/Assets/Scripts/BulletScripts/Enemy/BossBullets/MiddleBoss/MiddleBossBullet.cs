// ========================================
//
// MiddleBossBullet.cs
//
// ========================================
//
// 中ボスの弾幕・HP管理を行うクラス。
// ・通常弾幕：縮小しながら回転する円形弾
// ・スペル：ランダム位置から円形 → 収束弾
// ・HPに応じて通常 → スペルへ移行
// ・撃破時にプレゼントボックスをドロップ
//
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    noemal, // 通常弾幕
    spell,  // スペルカード
}

public class MiddleBossBullet : MonoBehaviour
{
    [SerializeField] private EnemyType enemyType;       // 中ボスの現在状態（通常 or スペル）
    [SerializeField] private BossHealth bossHealth;     // HP管理クラス
    private float damageLate = 1;                       // 被ダメージ倍率
    [SerializeField] private GameObject presentBox;     // 撃破時に落とすプレゼント
    [SerializeField] private Image HealthBar;           // HPバー
    [SerializeField] private float maxHp;               // 最大HP
    private float dangerHp;                             // スペル移行ライン（HP30%）
    private bool isDamage;                              // ダメージを受け付けるか
    GameObject canvas;                                  // HPバーのキャンバス

    [Header("通常弾幕の設定")]
    [SerializeField] private GameObject bulletPrefab;   // 通常弾のプレハブ
    [SerializeField] private int shotNum;               // 一度に撃つ弾数
    [SerializeField] private float radiusShrinkSpeed = 0.05f; // 円の縮小速度
    [SerializeField] private float radius = 3.0f;       // 弾の配置半径
    [SerializeField] private float bulletSpeed;         // 弾の速度
    [SerializeField] private float bulletDelayTime;     // 発射間隔

    /// <summary>
    /// 初期設定
    /// </summary>
    private void Start()
    {
        isDamage = true;
        dangerHp = maxHp * 0.3f;

        bossHealth = FindAnyObjectByType<BossHealth>();
        bossHealth.HP = maxHp;

        StartCoroutine(StartBullet());
    }

    /// <summary>
    /// 通常弾幕：縮小しながら回転する円形弾を生成
    /// </summary>
    private IEnumerator StartBullet()
    {
        float angleStep = 360f / shotNum;
        float stopThreshold = 0.2f;
        float globalAngle = 0f;

        while (enemyType == EnemyType.noemal)
        {
            // 半径が一定以上ある間は円形弾を撃ち続ける
            if (radius > stopThreshold && enemyType == EnemyType.noemal)
            {
                for (int i = 0; i < shotNum; i++)
                {
                    float angle = globalAngle + (i * angleStep);

                    Vector2 bulletPos = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad),
                        Mathf.Sin(angle * Mathf.Deg2Rad)
                    ) * radius;

                    Vector2 spawnPos = (Vector2)transform.position + bulletPos;
                    GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);

                    Vector2 direction = bulletPos.normalized;
                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
                }

                radius -= radiusShrinkSpeed;
                globalAngle += 10f;
            }

            yield return new WaitForSeconds(bulletDelayTime);
        }
    }

    /// <summary>
    /// スペル弾幕：ランダム位置に円形生成 → 収束弾
    /// </summary>
    private IEnumerator MiddleSpell()
    {
        yield return new WaitForSeconds(2f);
        isDamage = true;

        while (enemyType == EnemyType.spell)
        {
            List<GameObject> bullets = new List<GameObject>();
            Vector3 randomPos = new Vector2(
                Random.Range(-8.5f, 8.5f),
                Random.Range(-4.5f, 4.5f)
            );

            // ランダム位置に円形弾を生成
            for (int i = 0; i < 10; i++)
            {
                float angle = (360 / 10) * i;

                Vector3 spawnPos = randomPos + new Vector3(
                    Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                    Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                    0f
                );

                GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                bullets.Add(bullet);
            }

            yield return new WaitForSeconds(1f);

            // 収束弾（円の中心へ向かう）
            foreach (GameObject bullet in bullets)
            {
                if (bullet == null) continue;

                Vector3 direction = (randomPos - bullet.transform.position).normalized;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 4;
            }
        }
    }

    /// <summary>
    /// プレイヤー弾に当たった時の処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet") && isDamage)
        {
            Destroy(collision.gameObject);
            bossHealth.HP -= damageLate;
            HealthBar.fillAmount = bossHealth.HP / maxHp;

            // HP30%以下 → スペルへ移行
            if (bossHealth.HP <= dangerHp && enemyType == EnemyType.noemal)
            {
                isDamage = false;
                enemyType = EnemyType.spell;
                damageLate = 0.2f;
                StartCoroutine(MiddleSpell());
            }
            // 撃破
            else if (bossHealth.HP <= 0)
            {
                Destroy(canvas);
                GameObject present = Instantiate(presentBox, transform.position, Quaternion.identity);
                present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0);
            }
        }
    }

    /// <summary>
    /// ボムに当たっている間の処理（継続ダメージ）
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bom") && isDamage)
        {
            bossHealth.HP -= damageLate;
            HealthBar.fillAmount = bossHealth.HP / maxHp;

            if (bossHealth.HP <= dangerHp && enemyType == EnemyType.noemal)
            {
                isDamage = false;
                enemyType = EnemyType.spell;
                damageLate = 0.2f;
                StartCoroutine(MiddleSpell());
            }
            else if (bossHealth.HP <= 0)
            {
                Destroy(canvas);
                GameObject present = Instantiate(presentBox, transform.position, Quaternion.identity);
                present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0);
            }
        }
    }
}
