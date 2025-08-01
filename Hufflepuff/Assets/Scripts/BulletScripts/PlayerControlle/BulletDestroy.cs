using UnityEngine;

public class BulletDestroy : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        // Destroy the bullet when it goes off-screen
        Destroy(gameObject);
    }
}