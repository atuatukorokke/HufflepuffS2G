// EnemyHealth.cs
//
// 雑魚エネミーのＨＰ管理とドロップ確認を行います
//

using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Range (0, 100)]
    [SerializeField] private float hP; // エネミーのＨＰ
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
            hP -= 10;
            if(hP == 0)
            {
                // ピースのドロップ
                if (Random.Range(0, dropLate) == 0)
                {
                    Debug.Log("あああああ");
                    dropLate = dropManager.LateReset();
                }
                else
                {
                    dropLate = dropManager.LateChange();
                }
                    Instantiate(prehab, transform.position, Quaternion.identity); //ドロップ確認用の再生成
                Destroy(gameObject); // エネミーの消滅
            }
        }
    }
}
