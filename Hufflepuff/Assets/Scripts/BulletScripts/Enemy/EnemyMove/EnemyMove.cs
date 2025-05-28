using UnityEngine;

public enum FiringType
{
    Circle, // 円形の弾幕を打つ敵（放物線移動）
    Following, // 追尾弾を打つ敵（減速移動）
    Winder // サイン波の弾幕を打つ敵（未実装）
}

public class EnemyMove : MonoBehaviour
{
    [Header("全移動方法共通変数")]
    [SerializeField] private float moveSpeed; // 移動スピード
    [SerializeField] private float TimingFiring; // 発射タイミング（未使用）
    [SerializeField] private FiringType firingType; // 移動タイプによって挙動を変える

    [Header("放物線移動用変数")]
    [SerializeField] private Vector3 baseTargetPoint; // 基本の目的地（移動先）
    private Vector3 targetPoint; // 各オブジェクトごとに調整された目的地
    [SerializeField] private float height = 2.0f; // 放物線の高さ
    [SerializeField] private float duration = 2.0f; // 放物線移動にかかる時間
    private float elapsedTime = 0f; // 経過時間の管理
    private bool slowMovePhase = false; // 放物線移動後のゆっくり移動フラグ

    [Header("直線移動用変数")]
    [SerializeField] private float decelerateFactor = 0.98f; // 減速率（0.98 = 毎フレーム速度を98%に）
    [SerializeField] private float slowMoveDuration = 2f; // 放物線移動後のゆっくり移動時間
    [SerializeField] private float exitSpeed = 10f; // 画面外へ飛ぶ速度
    private bool waitBeforeExit = false; // 画面外移動の待機フェーズ
    private bool exitPhase = false; // 画面外移動開始フラグ
    [SerializeField] private float waitTimeBeforeExit = 3f; // 画面外へ移動するまでの待機時間

    private Vector3 startPoint; // 移動開始地点
    private Vector3 velocity; // 直線移動の速度ベクトル
    private float positionOffset; // 目的地をランダムにずらすオフセット

    private void Start()
    {
        startPoint = transform.position;

        // 各オブジェクトのY座標を考慮し、目的地をランダムにずらす
        positionOffset = Mathf.Sin(transform.position.y) * Random.Range(-1f, 1f);
        targetPoint = baseTargetPoint + new Vector3(positionOffset, 0, 0);

        // 画面外から減速しながら移動するための初期速度設定
        velocity = new Vector3(-moveSpeed, 0, 0);
    }

    private void Update()
    {
        // firingType に応じた移動処理を実行
        switch (firingType)
        {
            case FiringType.Circle:
                CurveMovement(); // 放物線移動
                break;
            case FiringType.Following:
                DecelerateMove(); // 減速移動
                break;
        }

        // 指定のy座標でエネミー削除
        if(transform.position.y < -30 || transform.position.y > 30)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 放物線移動（目的地へジャンプするような動き）
    /// </summary>
    private void CurveMovement()
    {
        if (!slowMovePhase)
        {
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // イージングを使ってスムーズに放物線移動
                float linearT = elapsedTime / duration;
                float easedT = 1 - Mathf.Pow(1 - linearT, 2);

                // X座標の移動
                Vector3 horizontalPos = Vector3.Lerp(startPoint, targetPoint, easedT);

                // Y座標に放物線の高さオフセットを追加
                float heightOffset = 4 * height * easedT * (1 - easedT);
                transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(startPoint.y, targetPoint.y, easedT) + heightOffset, horizontalPos.z);
            }
            else
            {
                // 放物線移動が完了したら、ゆっくり移動フェーズへ
                slowMovePhase = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < slowMoveDuration)
            {
                // 数秒間ゆっくり左に移動
                transform.position += new Vector3(-0.5f * Time.deltaTime, 0, 0);
            }
            else
            {
                // 画面外へ移動開始
                waitBeforeExit = true;
                exitPhase = true;
            }
        }
        if (waitBeforeExit)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTimeBeforeExit)
            {
                exitPhase = true;
            }
        }

        if (exitPhase)
        {
            MoveOutOfScreen();
        }
    }

    /// <summary>
    /// 画面外から減速しながら目的地へ向かう移動
    /// </summary>
    private void DecelerateMove()
    {
        transform.position += velocity * Time.deltaTime;
        velocity *= decelerateFactor; // 徐々に減速

        if (velocity.magnitude < 1f)
        {
            // firingTypeが既にFollowingなら変更しない
            if (firingType == FiringType.Following)
            {
                return;
            }
            else
            {
                // そうでなければ、Circleへ遷移
                firingType = FiringType.Circle;
                Start();
            }
        }
    }

    /// <summary>
    /// 画面外へ移動（Y座標による移動方向決定）
    /// </summary>
    private void MoveOutOfScreen()
    {
        float exitDirectionY = transform.position.y >= 0 ? 1f : -1f;
        transform.position += new Vector3(exitSpeed * Time.deltaTime, exitSpeed * exitDirectionY * Time.deltaTime, 0);
    }
}
