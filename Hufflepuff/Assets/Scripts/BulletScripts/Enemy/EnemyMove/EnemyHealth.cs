using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Range (0, 100)]
    [SerializeField] private float hP;
    [SerializeField] GameObject prehab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("P_Bullet"))
        {
            hP -= 10;
            if(hP == 0)
            {
                // ピースのドロップ
                if(Random.Range(0, 10) == 1)
                {
                    Debug.Log("あああああ");
                }
                Instantiate(prehab, transform.position, Quaternion.identity); //ドロップ確認用の再生成
                Destroy(gameObject); // エネミーの消滅
            }
        }
    }
}
