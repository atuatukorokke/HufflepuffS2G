// PieceCreate.cs
// 
// スクリプト呼び出した時にパズルピースをランダム生成（パズルには必ず当てはめることが可能）する
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("各種ピース")]
    [SerializeField] public GameObject mino1;
    [SerializeField] public GameObject mino2_1;
    [SerializeField] public GameObject mino2_2;
    [SerializeField] public GameObject mino3_1;
    [SerializeField] public GameObject mino3_2;
    [SerializeField] public GameObject mino3_3;
    [SerializeField] public GameObject mino3_4;
    [SerializeField] public GameObject mino4;
    [SerializeField] public GameObject mino5_1;
    [SerializeField] public GameObject mino5_2;
    [SerializeField] public GameObject mino6_1;
    [SerializeField] public GameObject mino6_2;
    [SerializeField] public GameObject mino9;

    public void NewPiece()
    {
        // 1~7の整数をランダムで生成
        //int rndMino = Random.Range(1, 8);
        int rndMino = 3; // デバッグ用に2を固定

        // 生成位置
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);

        // プレハブを指定位置に生成
        switch (rndMino)
        {
            case 1:
                Instantiate(mino1, pos, Quaternion.identity);
                break;
            case 2:
                Instantiate(mino2_1, pos, Quaternion.identity);
                break;
            case 3:
                Instantiate(mino3_1, pos, Quaternion.identity);
                break;
            case 4:
                Instantiate(mino4, pos, Quaternion.identity);
                break;
            case 5:
                Instantiate(mino5_1, pos, Quaternion.identity);
                break;
            case 6:
                Instantiate(mino6_1, pos, Quaternion.identity);
                break;
            case 7:
                Instantiate(mino9, pos, Quaternion.identity);
                break;
            default:
                Debug.Log("なんか変な値が出てるやでー");
                break;
        }
    }

    /// <summary>
    /// ピースの回転を行います
    /// </summary>
    /// <param name="inGameObject">対象のゲームオブジェクトを入れてください</param>
    /// <param name="r">初期値からの回転数を入れてください</param>
    public void PieceRotationCreate(GameObject inGameObject, int r)
    {
        Debug.Log(r);
        // 必要最低カウント数を設定
        switch (inGameObject.tag)
        {
            // 回転２つ
            case "mino2":
                switch (r)
                {
                    case 0:
                        Instantiate(mino2_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino2_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            case "mino5":
                switch (r)
                {
                    case 0:
                        Instantiate(mino5_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino5_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            case "mino6":
                switch (r)
                {
                    case 0:
                        Instantiate(mino6_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino6_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            // 回転４つ
            case "mino3":
                switch (r)
                {
                    case 0:
                        Instantiate(mino3_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino3_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(mino3_3, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(mino3_4, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            default:
                Debug.Log("変なタグor回転がいらないタグを読み取ってます");
                break;
        }
    }
}
