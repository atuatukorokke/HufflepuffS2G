// PieceCreate.cs
// 
// スクリプト呼び出した時にパズルピースをランダム生成（パズルには必ず当てはめることが可能）する
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("各種ピース")]
    [SerializeField] public GameObject Imino;
    [SerializeField] public GameObject Jmino;
    [SerializeField] public GameObject Lmino;
    [SerializeField] public GameObject Omino;
    [SerializeField] public GameObject Smino;
    [SerializeField] public GameObject Tmino;
    [SerializeField] public GameObject Zmino;
    public void NewPiece()
    {
        // 1~7の整数をランダムで生成
        int rndMino = Random.Range(1, 8);

        // 生成位置
        Vector3 pos = new Vector3(6.5f, 0.5f, 0.0f);

        // プレハブを指定位置に生成
        switch (rndMino)
        {
            case 1:
                Instantiate(Imino, pos, Quaternion.identity);
                break;
            case 2:
                Instantiate(Jmino, pos, Quaternion.identity);
                break;
            case 3:
                Instantiate(Lmino, pos, Quaternion.identity);
                break;
            case 4:
                Instantiate(Omino, pos, Quaternion.identity);
                break;
            case 5:
                Instantiate(Smino, pos, Quaternion.identity);
                break;
            case 6:
                Instantiate(Imino, pos, Quaternion.identity);
                break;
            case 7:
                Instantiate(Zmino, pos, Quaternion.identity);
                break;
            default:
                Debug.Log("なんか変な値が出てるやでー");
                break;
        }
    }
}
