using System;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] private float hP; // エネミーのＨＰ
    [SerializeField] private AudioClip deadSE;
    private AudioSource audio;

    public float HP { get => hP; set => hP = value; }

    public event Action OnDeath; // 中ボス撃破通知用イベント

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // プレイヤーの弾を消す
            if (HP <= 0)
            {
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach(GameObject objects in bullets)
                {
                    Destroy(objects);
                }
                OnDeath?.Invoke(); // 中ボス撃破通知イベントを発火
                audio.PlayOneShot(deadSE);
                Destroy(this.gameObject); // エネミーの消滅
            }
        }
        else if(collision.CompareTag("P_Bom"))
        {
            if (HP <= 0)
            {
                GameObject[] bullets = GameObject.FindGameObjectsWithTag("E_Bullet");
                foreach (GameObject objects in bullets)
                {
                    Destroy(objects);
                }
                OnDeath?.Invoke(); // 中ボス撃破通知イベントを発火
                audio.PlayOneShot(deadSE);
                Destroy(this.gameObject); // エネミーの消滅
            }
        }
    }
}
