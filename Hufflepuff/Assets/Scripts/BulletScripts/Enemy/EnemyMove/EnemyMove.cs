using UnityEngine;

public enum FiringType
{
    Circle, // 円形の弾幕を打つ敵
    Following, // 追尾弾を打つ敵
    Winder // サイン波の弾幕を打つ敵
}

public class EnemyMove : MonoBehaviour
{
    [Header("全移動方法共通変数")]
    [SerializeField] private float moveSpeed; // 移動スピード
    [SerializeField] private float TimingFiring; // 発射タイミング
    [SerializeField] private FiringType firingType; // 弾幕のタイプによって移動方法を変える

    [Header("放物線移動用変数")]
    [SerializeField] private Vector3 baseTargetPoint; // 基本の目的地
    private Vector3 targetPoint; // 調整後の目的地
    [SerializeField] private float height = 2.0f;
    [SerializeField] private float duration = 2.0f;
    private float elapsedTime = 0f;
    private bool slowMovePhase = false;

    [Header("直線移動用変数")]
    [SerializeField] private float decelerateFactor = 0.98f; // 減速率
    [SerializeField] private float slowMoveDuration = 2f; // ゆっくり移動の時間
    [SerializeField] private float exitSpeed = 10f; // 画面外へ移動する速度
    private bool exitPhase = false; // 画面外移動開始フラグ

    private Vector3 startPoint;
    private Vector3 velocity;
    private float positionOffset; // 目的地をずらすオフセット

    private void Start()
    {
        startPoint = transform.position;

        // 各オブジェクトのY座標を基準にランダムなオフセットを設定
        positionOffset = Mathf.Sin(transform.position.y) * Random.Range(-1f, 1f);
        targetPoint = baseTargetPoint + new Vector3(positionOffset, 0, 0);

        // 画面外から減速しながら目的地へ向かう初期速度設定
        velocity = new Vector3(-moveSpeed, 0, 0);
    }

    private void Update()
    {
        switch (firingType)
        {
            case FiringType.Circle:
                CurveMovement();
                break;
            case FiringType.Following:
                DecelerateMove();
                break;
        }
    }

    private void CurveMovement()
    {
        if (!slowMovePhase)
        {
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                float linearT = elapsedTime / duration;
                float easedT = 1 - Mathf.Pow(1 - linearT, 2);

                Vector3 horizontalPos = Vector3.Lerp(startPoint, targetPoint, easedT);
                float heightOffset = 4 * height * easedT * (1 - easedT);

                transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(startPoint.y, targetPoint.y, easedT) + heightOffset, horizontalPos.z);
            }
            else
            {
                slowMovePhase = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < slowMoveDuration)
            {
                transform.position += new Vector3(-0.5f * Time.deltaTime, 0, 0);
            }
            else
            {
                exitPhase = true;
            }
        }

        if (exitPhase)
        {
            MoveOutOfScreen();
        }
    }

    private void DecelerateMove()
    {
        transform.position += velocity * Time.deltaTime;
        velocity *= decelerateFactor;

        if (velocity.magnitude < 1f)
        {
            firingType = FiringType.Circle;
            Start();
        }
    }

    private void MoveOutOfScreen()
    {
        float exitDirectionY = transform.position.y >= 0 ? 1f : -1f;
        transform.position += new Vector3(exitSpeed * Time.deltaTime, exitSpeed * exitDirectionY * Time.deltaTime, 0);
    }
}
