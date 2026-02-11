// ========================================
//
// BossHealth.cs
//
// ========================================
//
// ボスのHP管理クラス。
// ・プレイヤー弾 or ボムに当たった時にHPを判定
// ・HPが0以下になったら爆発エフェクト → 全弾削除 → OnDeathイベント発火 → 本体削除
// ・ダメージ処理そのものは外部で行い、このクラスは「死亡判定と後処理」を担当
//
// ========================================

using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float hP;                 // ボスのHP
    [SerializeField] private GameObject EexplosionEffect; // 爆発エフェクト

    public float HP { get => hP; set => hP = value; }

    public event Action OnDeath;                       // ボス死亡時の通知イベント

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject);

            if (HP <= 0)
            {
                Instantiate(EexplosionEffect, transform.position, Quaternion.identity);

                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach (GameObject obj in bullets)
                    Destroy(obj);

                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("P_Bom"))
        {
            if (HP <= 0)
            {
                Instantiate(EexplosionEffect, transform.position, Quaternion.identity);

                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach (GameObject obj in bullets)
                    Destroy(obj);

                OnDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
