// EnemySummoningManagement.cs
//
// エネミーの配置や召喚などの管理を行います。
//

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
        StartCoroutine(Enumerator()); // エネミーの配置を開始
    }

    /// <summary>
    /// エネミーの配置を行います。
    /// </summary>
    /// <returns></returns>
    public IEnumerator Enumerator()
    {
        foreach(var deploment in enemyDeployment)
        {
            switch(deploment.GetState1)
            {
                // 雑魚敵の配置
                case EnemyDeployment.state.Smallfry:

                    // 指定された数だけ生成
                    for (int i = 0; i < deploment.EnemyCount; i++)
                    {
                        SpawnEnemy(deploment);
                        yield return new WaitForSeconds(deploment.DelayTime);
                    }
                    break;
                // 中ボスの配置
                case EnemyDeployment.state.middleBoss:

                    // 中ボスの生成
                    GameObject middleBoss = SpawnEnemy(deploment);

                    // 中ボスが出てくるフラグを立てて、中ボスが倒されるまで待機
                    waitingForMiddleBoss = true;
                    BossHealth health = middleBoss.GetComponent<BossHealth>();
                    health.OnDeath += () => waitingForMiddleBoss = false; 
                    yield return new WaitUntil(() => !waitingForMiddleBoss); 
                    break;
                // ボスの配置
                case EnemyDeployment.state.Boss:
                    GameObject Bosss = Instantiate(deploment.EnemyPrehab, deploment.GenerationPosition, Quaternion.identity);

                    // ボス戦用のBGMを設定して再生
                    audioSource.clip = deploment.BossBGM; 
                    audioSource.Play(); 

                    // ボスが倒された時の処理
                    Boss1Bullet BossBullet = Bosss.GetComponent<Boss1Bullet>();
                    BossBullet.Ondeath += () => StartCoroutine(BossDeath());  
                    break;
                // 配置後の待ち時間
                case EnemyDeployment.state.DelayTime:
                    yield return new WaitForSeconds(deploment.DelayTime);
                    break;
                // パズル画面を開く
                case EnemyDeployment.state.Shop:

                    // ショップ中は攻撃できないようにする
                    playerController.isShooting = false;
                    PuzzleSet();

                    // ショップのオブジェクトを取得して所持金の更新
                    var shop = FindAnyObjectByType<ShopOpen>(); 
                    goldManager.SetGoldCount(playerController.CoinCount);
                    shop.ShopOpenAni();

                    // ショップが開いているフラグを立てる
                    waitingForShop = true;
                    isPuzzle = true;

                    // ショップが閉じられたらフラグを下げる動作を与えて、ショップが閉じられるまで待機
                    shop.OnShop += () => waitingForShop = false;
                    yield return new WaitUntil(() => !waitingForShop);
                    yield return new WaitForSeconds(2f);
                    PuzzleOut();
                    break;
            }
        }
    }

    /// <summary>
    /// エネミーを生成します。
    /// </summary>
    /// <param name="deployment">エネミーを召喚する際の設定</param>
    /// <returns></returns>
    private GameObject SpawnEnemy(EnemyDeployment deployment)
    {
        Vector2 position = deployment.GenerationPosition;
        GameObject enemy = Instantiate(deployment.EnemyPrehab, position, Quaternion.identity);
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if(health != null)
        {
            health.SetHealth(deployment.EnemyHP);
        }

        return enemy;
    }

    /// <summary>
    /// パズルモードに切り替えます。
    /// </summary>
    private void PuzzleSet()
    {
        coinText.gameObject.SetActive(false); // 所持金テキストを非表示
        pieceText.gameObject.SetActive(false); // ピースの数テキストを非表示
        deathLateText.gameObject.SetActive(false); // 死亡率テキストを非表示
        audioSource.PlayOneShot(OpenPuzzle);
        audioSource.clip = puzzleBGM; // パズル用のBGMを設定
        audioSource.Play(); // BGMを再生
    }

    /// <summary>
    /// シューティングモードに切り替えます。
    /// </summary>
    private void PuzzleOut()
    {
        coinText.gameObject.SetActive(true); // 所持金テキストを表示
        pieceText.gameObject.SetActive(true); // ピースの数テキストを表示
        deathLateText.gameObject.SetActive(true); // 死亡率テキストを表示
        coinText.text = $"コイン:<color=#ffd700>{playerController.CoinCount.ToString()}</color>";
        pieceText.text = $"ピース:<color=#ffd700>{playerController.PieceCount.ToString()}</color>";
        deathLateText.text = $"お邪魔:<color=#ff0000>{((int)((float)deathCount.BlockCount / (float)deathCount.PieceCount * 100)).ToString()}%</color>";
    }

    /// <summary>
    /// ボスが倒された時の処理を行います。
    /// </summary>
    private IEnumerator BossDeath()
    {
        FindAnyObjectByType<PlayrController>().Playstate = PlayState.Clear;
        coinText.gameObject.SetActive(false);
        pieceText.gameObject.SetActive(false);
        deathLateText.gameObject.SetActive(false);
        yield return StartCoroutine(CameraZoomToPlayer());
        // BGMをフェードアウト
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        CanvasMaster.GetComponent<Animator>().SetTrigger("GameClear");
    }

    private IEnumerator CameraZoomToPlayer()
    {
        float duration = 2f;
        float elaapsed = 0f;

        float startSize = mainCamera.orthographicSize;
        float targetSize = 2.5f;

        Vector3 startPos = mainCamera.transform.position;
        Vector3 targetPos = new Vector3(
            playerController.transform.position.x,
            playerController.transform.position.y,
            mainCamera.transform.position.z);

        while(elaapsed < duration)
        {
            float t = elaapsed / duration;
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, t);
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, t);
            elaapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.position = targetPos;
        mainCamera.orthographicSize = targetSize;
    }
}