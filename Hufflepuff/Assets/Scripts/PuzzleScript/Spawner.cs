using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] tetrominoes;

    void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        int i = Random.Range(0, tetrominoes.Length);
        Instantiate(tetrominoes[i], transform.position, Quaternion.identity);
    }
}