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
    [SerializeField] public float hP; // エネミーのＨＰ
    [SerializeField] GameObject prehab; // 箱のプレハブ
    [SerializeField] private int dropLate; // 箱を落とす確率
    [SerializeField] private DropManager dropManager; // ドロップ管理のスクリプト

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
                    GameObject present = Instantiate(prehab, transform.position, Quaternion.identity); // 箱の生成
                    present.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-2, 0); // 箱を下に落とす
                    dropLate = dropManager.LateReset();
                }
                else
                {
                    dropLate = dropManager.LateChange();
                }
                Destroy(gameObject); // エネミーの消滅
            }
        }
    }
    public void SetHealth(float health)
    {
        hP = health; // エネミーのＨＰを設定
    }
}
