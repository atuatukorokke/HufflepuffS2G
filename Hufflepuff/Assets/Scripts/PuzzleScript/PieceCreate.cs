// PieceCreate.cs
// 
// スクリプト呼び出した時にパズルピースをランダム生成（パズルには必ず当てはめることが可能）する
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("各種ピース")]
    [SerializeField] public GameObject mino1;
    [SerializeField] public GameObject mino2;
    [SerializeField] public GameObject mino3;
    [SerializeField] public GameObject mino4;
    [SerializeField] public GameObject mino5;
    [SerializeField] public GameObject mino6;
    [SerializeField] public GameObject mino9;
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
                Instantiate(mino1, pos, Quaternion.identity);
                break;
            case 2:
                Instantiate(mino2, pos, Quaternion.identity);
                break;
            case 3:
                Instantiate(mino3, pos, Quaternion.identity);
                break;
            case 4:
                Instantiate(mino4, pos, Quaternion.identity);
                break;
            case 5:
                Instantiate(mino5, pos, Quaternion.identity);
                break;
            case 6:
                Instantiate(mino6, pos, Quaternion.identity);
                break;
            case 7:
                Instantiate(mino9, pos, Quaternion.identity);
                break;
            default:
                Debug.Log("なんか変な値が出てるやでー");
                break;
        }
    }
}
