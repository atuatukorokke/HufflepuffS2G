using UnityEngine;

public class MinoCheck : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;     // 盤面が重なっていないかを確認するスクリプト

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // このスクリプトがアタッチされているオブジェクトを生成したときにオブジェクトを取得する
        pieceMoves = Object.FindFirstObjectByType<PieceMoves>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("当たった");
        pieceMoves.SetColliding(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pieceMoves.SetColliding(-1);
    }
}
