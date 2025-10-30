using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    /// <summary>
    /// ‰æ–ÊŠO‚Éo‚½‚ç’e‚ğÁ‚·
    /// </summary>
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}