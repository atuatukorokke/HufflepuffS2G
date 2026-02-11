// ========================================
//
// EnemySummoningManagement.cs
//
// ========================================
//
// ステージ進行に応じて敵を出現させる管理クラス。
// ・EnemyDeployment（ScriptableObject）を順番に読み取り、敵・中ボス・ボス・ショップを制御
// ・中ボス撃破待ち、ショップ待ちなどのフロー管理
// ・ボス撃破後の演出（カメラズーム、BGMフェードアウト、クリア演出）も担当
//
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private List<EnemyDeployment> enemyDeployment; // エネミーの配置データを格納するリスト
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject TitleButton;
    [SerializeField] private GameObject CanvasMaster;
    [SerializeField] private TextMeshProUGUI coinText;              // 所持金テキスト
    [SerializeField] private TextMeshProUGUI pieceText;             // ピースの数テキスト
    [SerializeField] private TextMeshProUGUI deathLateText;         // 死亡率テキスト 
    [SerializeField] private Animator ClearAnimator;
    [SerializeField] private PlayrController playerController;      // プレイヤーのコントローラー
    [SerializeField] private GoldManager goldManager;               // 金額管理を行うスクリプト
    [SerializeField] private DeathCount deathCount;                 // 死ぬかの判定を行うスクリプト
    private bool waitingForMiddleBoss = false;                      // 途中でボスが出てくるかどうかのフラグ
    private bool waitingForShop = false;                            // パズル画面が閉じられるまでの待機フラグ
    public bool isPuzzle = false;                                   // パズル中かどうか(パズル画面を閉じるボタンの二度押し防止のために使う)
    private AudioSource audioSource;                                // BGMの再生用オーディオソース
    private Camera mainCamera;                                      // メインカメラ

    [SerializeField] private AudioClip OpenPuzzle;
    [SerializeField] private AudioClip puzzleBGM;

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayrController>();
        goldManager = FindAnyObjectByType<GoldManager>();
        deathCount = FindAnyObjectByType<DeathCount>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        TitleButton.SetActive(false);
        ClearPanel.SetActive(false);

        StartCoroutine(Enumerator());
    }

    /// <summary>
    /// EnemyDeployment の内容に従って敵を順番に出現させる。
    /// </summary>
    public IEnumerator Enumerator()
    {
        foreach (var deploment in enemyDeployment)
        {
            switch (deploment.GetState1)
            {
                // -----------------------------------------
                // 雑魚の出現
                // -----------------------------------------
                case EnemyDeployment.state.Smallfry:
                    for (int i = 0; i < deploment.EnemyCount; i++)
                    {
                        SpawnEnemy(deploment);
                        yield return new WaitForSeconds(deploment.DelayTime);
                    }
                    break;

                // -----------------------------------------
                // 中ボスの出現
                // -----------------------------------------
                case EnemyDeployment.state.middleBoss:
                    GameObject middleBoss = SpawnEnemy(deploment);

                    waitingForMiddleBoss = true;
                    BossHealth health = middleBoss.GetComponent<BossHealth>();

                    health.OnDeath += () => waitingForMiddleBoss = false;

                    yield return new WaitUntil(() => !waitingForMiddleBoss);
                    break;

                // -----------------------------------------
                // ボスの出現
                // -----------------------------------------
                case EnemyDeployment.state.Boss:
                    GameObject bossObj = Instantiate(
                        deploment.EnemyPrehab,
                        deploment.GenerationPosition,
                        Quaternion.identity
                    );

                    audioSource.clip = deploment.BossBGM;
                    audioSource.Play();

                    Boss1Bullet bossBullet = bossObj.GetComponent<Boss1Bullet>();
                    bossBullet.Ondeath += () => StartCoroutine(BossDeath());
                    break;

                // -----------------------------------------
                // 待機時間
                // -----------------------------------------
                case EnemyDeployment.state.DelayTime:
                    yield return new WaitForSeconds(deploment.DelayTime);
                    break;

                // -----------------------------------------
                // ショップの出現
                // -----------------------------------------
                case EnemyDeployment.state.Shop:
                    playerController.isShooting = false;
                    PuzzleSet();

                    var shop = FindAnyObjectByType<ShopOpen>();
                    goldManager.SetGoldCount(playerController.CoinCount);
                    shop.ShopOpenAni();

                    waitingForShop = true;
                    isPuzzle = true;

                    shop.OnShop += () => waitingForShop = false;

                    yield return new WaitUntil(() => !waitingForShop);
                    yield return new WaitForSeconds(2f);

                    PuzzleOut();
                    break;
            }
        }
    }

    /// <summary>
    /// 敵を生成し、HPを設定する。
    /// </summary>
    private GameObject SpawnEnemy(EnemyDeployment deployment)
    {
        Vector2 position = deployment.GenerationPosition;
        GameObject enemy = Instantiate(deployment.EnemyPrehab, position, Quaternion.identity);

        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.SetHealth(deployment.EnemyHP);
        }

        return enemy;
    }

    /// <summary>
    /// パズルモードへ移行（UI非表示・BGM変更）
    /// </summary>
    private void PuzzleSet()
    {
        coinText.gameObject.SetActive(false);
        pieceText.gameObject.SetActive(false);
        deathLateText.gameObject.SetActive(false);

        audioSource.PlayOneShot(OpenPuzzle);
        audioSource.clip = puzzleBGM;
        audioSource.Play();
    }

    /// <summary>
    /// パズル終了 → 通常UIへ戻す
    /// </summary>
    private void PuzzleOut()
    {
        coinText.gameObject.SetActive(true);
        pieceText.gameObject.SetActive(true);
        deathLateText.gameObject.SetActive(true);

        coinText.text = $"コイン:<color=#ffd700>{playerController.CoinCount}</color>";
        pieceText.text = $"ピース:<color=#ffd700>{playerController.PieceCount}</color>";
        deathLateText.text =
            $"死亡率:<color=#ff0000>{((int)((float)deathCount.BlockCount / (float)deathCount.PieceCount * 100))}%</color>";
    }

    /// <summary>
    /// ボス撃破後の演出（カメラズーム → BGMフェード → クリア演出）
    /// </summary>
    private IEnumerator BossDeath()
    {
        FindAnyObjectByType<PlayrController>().Playstate = PlayState.Clear;

        coinText.gameObject.SetActive(false);
        pieceText.gameObject.SetActive(false);
        deathLateText.gameObject.SetActive(false);

        yield return StartCoroutine(CameraZoomToPlayer());

        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1f);

        CanvasMaster.GetComponent<Animator>().SetTrigger("GameClear");
    }

    /// <summary>
    /// カメラをプレイヤーへズームさせる演出
    /// </summary>
    private IEnumerator CameraZoomToPlayer()
    {
        float duration = 2f;
        float elapsed = 0f;

        float startSize = mainCamera.orthographicSize;
        float targetSize = 2.5f;

        Vector3 startPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3(
            playerController.transform.position.x,
            playerController.transform.position.y,
            mainCamera.transform.position.z
        );

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPos;
        mainCamera.orthographicSize = targetSize;
    }
}
