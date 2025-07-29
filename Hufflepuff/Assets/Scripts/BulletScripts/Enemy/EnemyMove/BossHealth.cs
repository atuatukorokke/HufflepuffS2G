using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] public float hP; // エネミーのＨＰ

    public event Action OnDeath; // 中ボス撃破通知用イベント

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // プレイヤーの弾を消す
            if (hP <= 0)
            {
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach(GameObject objects in bullets)
                {
                    Destroy(objects);
                }
                OnDeath?.Invoke(); // 中ボス撃破通知イベントを発火
                Destroy(this.gameObject); // エネミーの消滅
            }
        }
    }
}
