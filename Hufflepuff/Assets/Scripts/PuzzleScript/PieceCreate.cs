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

    private bool isCreate = false;
    private int pieceNumber;
    private BuffForID buffID;
    private float buffValue;

    public bool IsCreate { get => isCreate; set => isCreate = value; }
    public BuffForID BuffID { get => buffID; private set => buffID = value; }
    public float BuffValue { get => buffValue; private set => buffValue = value; }

    /// <summary>
    /// 新しいピースを生成します
    /// </summary>
    /// <param name="pieceNumber">0 = ランダム生成, 1 ~ 7 = 対応したピースを生成</param>
    public void NewPiece(int pieceNumber)
    { 
        this.pieceNumber = pieceNumber;
        // 入力が０の時1~7の整数をランダムで生成
        if (pieceNumber == 0)
        {
            //int rndMino = Random.Range(1, 8);
            pieceNumber = 3; // デバッグ用に2を固定
        }

        // 生成位置
        Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

        if(!IsCreate)
        {
            IsCreate = true;
            // プレハブを指定位置に生成
            switch (pieceNumber)
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
                    break;
            }
        }
        
    }

    public void PieceAddBuff(BuffForID buffForID)
    {
        if(IsCreate)
        {
            BuffID = buffForID;
            BuffValue = pieceNumber * 0.1f; // デバッグ用にピース番号の10%をバフ値にする
        }
    }
}
