// ObjectDragTransform
// 
// オブジェクトをドラッグアンドドロップで移動させます
// 

using System.Collections;
using UnityEngine;

public class ObjectDragTransform : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    float gridSize = 1.0f;

    [Header("ピース情報")]
    [SerializeField] private int pieceCount = 0;    // ピース数
    [SerializeField] private int sellGold = 0;     // 金額

    [Header("スクリプトを動的にアタッチされる")]
    [SerializeField] private DeathCount deathCount;     // 死ぬかの判定を行うスクリプト
    [SerializeField] private GoldManager goldManager;     // 金額管理を行うスクリプト
    [SerializeField] private PuzzleController puzzleController; // パズル全体を管理するスクリプト
    [SerializeField] private Buff buff;    // バフ管理を行うスクリプト

    void Start()
    {
        mainCamera = Camera.main;

        // このスクリプトがアタッチされているオブジェクトを生成したときにオブジェクトを取得する
        deathCount = Object.FindFirstObjectByType<DeathCount>();
        goldManager = Object.FindFirstObjectByType<GoldManager>();
        puzzleController = FindFirstObjectByType<PuzzleController>();
        puzzleController.ProvisionalBuffs.Add(buff);
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        // 現在ドラッグしているオブジェクトをXキーで90度回転
        if (isDragging & Input.GetKeyDown(KeyCode.X))
        {
            float currentZ = transform.eulerAngles.z;
            transform.eulerAngles = new Vector3(0, 0, currentZ + 90f);
        }

        // 現在ドラッグしているオブジェクトをCキーで売却
        if (isDragging & Input.GetKeyDown(KeyCode.C))
        {
            Destroy(gameObject);

            deathCount.SetPieceCount(pieceCount * -1);

            goldManager.SetGoldCount(sellGold);

            for(int i = 0; i <= puzzleController.ProvisionalBuffs.Count; i++)
            {
                if(buff.buffID == puzzleController.ProvisionalBuffs[i].buffID & buff.value == puzzleController.ProvisionalBuffs[i].value)
                {
                    puzzleController.ProvisionalBuffs.RemoveAt(i);
                    break;
                }
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // ピース同士が重なっていたら元の位置に戻す

        // 位置を取得して、xとyを四捨五入して整数にスナップ
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        transform.position = pos;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
