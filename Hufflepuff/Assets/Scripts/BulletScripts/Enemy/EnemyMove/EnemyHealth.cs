// ========================================
//
// EnemyHealth.cs
//
// ========================================
//
// 雑魚エネミーのHP管理とドロップ処理を行うクラス。
// ・プレイヤー弾 / ボムでHPを減少
// ・HPが0以下になったら爆発 → 箱をドロップ → ドロップレートをリセット → 自身を削除
// ・DropManager と連携してドロップ確率を制御
//
// ========================================

using UnityEngine;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] public float hP;                       // エネミーのHP
    [SerializeField] private GameObject prehab;             // ドロップする箱のプレハブ
    [SerializeField] private int dropLate;                  // ドロップ確率（DropManagerから取得）
    [SerializeField] private DropManager dropManager;       // ドロップ管理
    [SerializeField] private GameObject EexplosionEffect;   // 爆発エフェクト

    private void Start()
    {
        dropManager = FindAnyObjectByType<DropManager>();
        dropLate = dropManager.DropLate;
    }

    /// <summary>
    /// プレイヤー弾に当たった時の処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject);
            hP -= 10;

            if (hP <= 0)
            {
                Instantiate(EexplosionEffect, transform.position, Quaternion.identity);

                GameObject present = Instantiate(prehab, transform.position, Quaternion.identity);
                present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0);

                dropLate = dropManager.LateReset();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// ボムに当たり続けている間の処理（継続ダメージ）
    /// </summary>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bom"))
        {
            hP--;

            if (hP <= 0)
            {
                Instantiate(EexplosionEffect, transform.position, Quaternion.identity);

                GameObject present = Instantiate(prehab, transform.position, Quaternion.identity);
                present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0);

                dropLate = dropManager.LateReset();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// 外部からHPを設定する
    /// </summary>
    public void SetHealth(float health)
    {
        hP = health;
    }
}
