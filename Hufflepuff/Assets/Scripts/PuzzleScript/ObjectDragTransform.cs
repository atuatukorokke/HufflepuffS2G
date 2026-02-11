// ========================================
//
// ObjectDragTransform.cs
//
// ========================================
//
// オブジェクトをドラッグ＆ドロップで移動させるクラス。
// ・マウスドラッグで移動
// ・Xキーで 90° 回転
// ・Cキーで売却（ピース削除・ゴールド加算・バフ削除）
// ・ドロップ時にグリッドへスナップ
//
// ========================================

using UnityEngine;

public class ObjectDragTransform : MonoBehaviour
{
    private Vector3 offset;                                     // マウス位置との差分
    private Camera mainCamera;                                  // メインカメラ
    private bool isDragging = false;                            // ドラッグ中かどうか

    float gridSize = 1.0f;                                      // グリッドサイズ

    [Header("ピース情報")]
    [SerializeField] private int pieceCount = 0;                // ピース数
    [SerializeField] private int pieceNumber = 0;               // ピース番号
    [SerializeField] private int sellGold = 0;                  // 売却時のゴールド

    [Header("スクリプト参照")]
    [SerializeField] private DeathCount deathCount;             // ピース数管理
    [SerializeField] private GoldManager goldManager;           // ゴールド管理
    [SerializeField] private PuzzleController puzzleController; // パズル管理
    [SerializeField] private Buff buff;                         // このピースが持つバフ

    private void Start()
    {
        mainCamera = Camera.main;

        // 必要なスクリプトを自動取得
        deathCount = Object.FindFirstObjectByType<DeathCount>();
        goldManager = Object.FindFirstObjectByType<GoldManager>();
        puzzleController = FindFirstObjectByType<PuzzleController>();

        // このピースのバフを仮バフリストに追加
        puzzleController.ProvisionalBuffs.Add(buff);
    }

    private void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            // ドラッグ中はマウス位置に追従
            transform.position = GetMouseWorldPosition() + offset;
        }

        // -----------------------------------------
        // Xキーで 90° 回転
        // -----------------------------------------
        if (isDragging & Input.GetKeyDown(KeyCode.X))
        {
            float currentZ = transform.eulerAngles.z;
            transform.eulerAngles = new Vector3(0, 0, currentZ + 90f);
        }

        // -----------------------------------------
        // Cキーで売却処理
        // -----------------------------------------
        if (isDragging & Input.GetKeyDown(KeyCode.C))
        {
            Destroy(gameObject);

            // ピース売却処理
            FindAnyObjectByType<ClearCountSet>().PieceSellCount(pieceNumber);
            deathCount.SetPieceCount(pieceCount * -1);
            goldManager.SetGoldCount(sellGold);

            // ブロック生成フラグ
            FindAnyObjectByType<PieceCreate>().isBlockCreate = true;

            // -----------------------------------------
            // 仮バフリストから一致するバフを削除
            // -----------------------------------------
            for (int i = 0; i <= puzzleController.ProvisionalBuffs.Count; i++)
            {
                if (buff.buffID == puzzleController.ProvisionalBuffs[i].buffID &
                    buff.value == puzzleController.ProvisionalBuffs[i].value)
                {
                    puzzleController.ProvisionalBuffs.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // -----------------------------------------
        // グリッドにスナップ
        // -----------------------------------------
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        transform.position = pos;
    }

    /// <summary>
    /// マウス位置をワールド座標に変換
    /// </summary>
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
