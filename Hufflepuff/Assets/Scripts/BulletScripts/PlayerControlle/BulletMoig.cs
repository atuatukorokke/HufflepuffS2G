using UnityEngine;

public class BulletMoig : MonoBehaviour
{
    [SerializeField] private float speed;
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime; 
    }
}
