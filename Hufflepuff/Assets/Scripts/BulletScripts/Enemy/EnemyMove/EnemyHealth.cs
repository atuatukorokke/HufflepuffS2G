// EnemyHealth.cs
//
// 雑魚エネミーのＨＰ管理とドロップ確認を行います
//

using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    [Range (0, 100)]
    [SerializeField] private float hP; // エネミーのＨＰ
    [SerializeField] GameObject prehab; // 箱のプレハブ
    [SerializeField] private int dropLate; // 箱を落とす確率
    [SerializeField] private DropManager dropManager; // ドロップ管理のスクリプト

    public event Action OnDeath; // 中ボス撃破通知用イベント

    private void Start()
    {
        dropManager = FindAnyObjectByType<DropManager>();
        dropLate = dropManager.DropLate;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("P_Bullet"))
        {
            Destroy(collision.gameObject); // プレイヤーの弾を消す
            hP -= 10;
            if(hP == 0)
            {
                // ピースのドロップ
                if (UnityEngine.Random.Range(0, dropLate) == 0)
                {
                    Debug.Log("ドロップ成功！");
                    Instantiate(prehab, transform.position, Quaternion.identity); // 箱の生成
                    dropLate = dropManager.LateReset();
                }
                else
                {
                    dropLate = dropManager.LateChange();
                }
                OnDeath?.Invoke(); // 中ボス撃破通知イベントを発火
                Instantiate(prehab, transform.position, Quaternion.identity); //ドロップ確認用の再生成
                Destroy(gameObject); // エネミーの消滅
            }
        }
    }
    public void SetHealth(float health)
    {
        hP = health; // エネミーのＨＰを設定
    }
}
