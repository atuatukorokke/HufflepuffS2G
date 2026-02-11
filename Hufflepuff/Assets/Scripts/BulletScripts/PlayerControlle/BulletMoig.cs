// ========================================
//
// BulletMoig.cs
//
// ========================================
//
// 前方（transform.up 方向）へ一定速度で進むだけのシンプルな弾。
// ・Update() 内で毎フレーム前進
// ・Rigidbody を使わず Transform で移動するタイプ
//
// ========================================

using UnityEngine;

public class BulletMoig : MonoBehaviour
{
    [SerializeField] private float speed; // 弾の移動速度

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
