using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    /// <summary>
    /// ��ʊO�ɏo����e������
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}