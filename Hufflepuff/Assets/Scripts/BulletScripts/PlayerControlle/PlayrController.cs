// PlayerControlle.cs
//
// 矢印キーで移動・Zキーで弾幕
//

using System.Collections;
using UnityEngine;
using TMPro;
[System.Serializable]

public class PlayrController : MonoBehaviour
{
    [Header("Player Settings")]
    private Rigidbody2D myRigidbody;
    [SerializeField] private float Speed; // 移動速度
    [SerializeField] private float speedLate = 1.0f; // 移動速度の遅延
    [Range(1f, 5f)]
    [SerializeField] private float attack;
    [SerializeField] private GameObject straightBulletPrehab; // 直線弾幕のプレハブ
    [SerializeField] private GameObject diffusionBulletPrehab; // 拡散用の弾幕プレハブ
    [SerializeField] private float bulletSpeed = 20f; // 弾幕の速度
    [SerializeField] private Transform gunPort; // 弾幕の発射口
    [SerializeField] private float delayTime; // 発射してからのディレイ時間
    [SerializeField] private bool invincible = false; // 無敵判定
    [SerializeField] private float invincibleTime = 15f; // 無敵時間
    [SerializeField] private int presentCount = 0; // プレゼントの数
    [SerializeField] private PlayState playState; // プレイヤーの状態
    [SerializeField] private GameObject CutInnCanvas; // カットイン用のキャンバス
    [SerializeField] private PieceCreate pieceCreate; // ピース生成スクリプト
    [SerializeField] private float outPieceLate; // お邪魔ピースの出現率
    [SerializeField] private TextMeshProUGUI coinText; // 所持金テキスト
    [SerializeField] private TextMeshProUGUI pieceText; // ピースの数テキスト

    private AudioSource audio;
    [SerializeField] private AudioClip damageSE;
    [SerializeField] private AudioClip presentSE;
    [SerializeField] private AudioClip specialSE;


    [Header("Player Coin")]
    [SerializeField] private int pieceCount = 0; // ピースの数
    [SerializeField] private int coinCount = 0; // コインの数
    [SerializeField] private int defultCoinIncreaseCount = 20; // デフォルトのコイン増加数

    [Header("Player Bom")]
    [SerializeField] private GameObject PlayerBomObjerct; // プレイヤーのボムオブジェクト
    [SerializeField] private float bomAppearTime = 5f; // ボムの出現時間
    [SerializeField] private float playerBomDelayTime = 5f; // ボムのディレイ時間
    [SerializeField] private float maxChatgeTime; // ボムの最大チャージ時間
    [SerializeField] private float currentChargeTime; // ボムのチャージ時間
    [SerializeField] private bool isCharge = true; // ボムのチャージ状態
    [SerializeField] private bool isBom; // ボムの状態


    public bool isShooting = false;

    public float Attack { get => attack; set => attack = value; }
    public float InvincibleTime { get => invincibleTime; set => invincibleTime = value; }
    public PlayState Playstate { get => playState; set => playState = value; }
    public int CoinCount { get => coinCount; set => coinCount = value; }
    public int PieceCount { get => pieceCount; set => pieceCount = value; }
    public int DefultCoinIncreaseCount { get => defultCoinIncreaseCount; set => defultCoinIncreaseCount = value; }
    public float OutPieceLate { get => outPieceLate; set => outPieceLate = value; }

    void Start()
    {
        audio = GetComponent<AudioSource>();
        Playstate = PlayState.Shooting; // 初期状態をシューティングに設定
        myRigidbody = GetComponent<Rigidbody2D>();
        pieceCreate = FindAnyObjectByType<PieceCreate>();
        currentChargeTime = 0.0f;
        isCharge = true; // ボムのチャージ状態を開始
        isBom = false; // ボムの状態を初期化
        pieceText.text = $"ピース:<color=#ffd700>{PieceCount.ToString()}</color>";
        coinText.text = $"コイン:<color=#ffd700>{CoinCount.ToString()}</color>";
    }

    void Update()
    {
        if(Playstate == PlayState.Shooting) PlayerMove();

        if(isCharge)
        {
            currentChargeTime += Time.deltaTime; // ボムのチャージ時間を計測
            if (currentChargeTime >= playerBomDelayTime) // 一定時間経過したらボムのチャージ状態を解除
            {
                isCharge = false; // ボムのチャージ状態を解除
                isBom = true; // ボムの状態を有効にする
                currentChargeTime = 0.0f; // チャージ時間をリセット
            }
        }
    }

    /// <summary>
    /// プレイヤーの基本操作
    /// ・矢印キーによる移動
    /// ・Zキーを押してる間、弾幕を出す
    /// </summary>
    private void PlayerMove()
    {
        // プレイヤーの移動速度を制御する
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedLate = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedLate = 1.0f; // スペースキーを離したら通常の速度に戻す
        }

        float x = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime * speedLate;
        float y = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime * speedLate;
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + x, -8.5f, 8.0f),
            Mathf.Clamp(transform.position.y + y, -4.5f, 4.5f));
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(!isShooting)
            {
                isShooting = true;
                // 直線状に弾幕を飛ばす
                StartCoroutine(BulletCreat());
                // 拡散するように弾幕を飛ばす
                if (Attack >= 2) StartCoroutine(DiffusionBullet(3));
            }
        }

        // Xキーを押したらカットインからのボムの発動
        // ボム発動中は無敵
        // 一定時間後にボムが消えて、無敵も解除
        // 発動が終わったらボムのチャージを開始する
        if(Input.GetKeyDown(KeyCode.X) && isBom)
        {
            StartCoroutine(Bom());
        }

        if(Input.GetKeyUp(KeyCode.Z))
        {
            isShooting = false;
        }
    }

    private IEnumerator Bom()
    {
        audio.PlayOneShot(specialSE);
        isBom = false; // ボムの二度撃ちを防止
        Instantiate(CutInnCanvas, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject Bom = Instantiate(PlayerBomObjerct, transform.position, Quaternion.identity);
        Destroy(Bom, bomAppearTime); // 一定時間後にボムを削除
        invincible = true; // ボム発動中は無敵
        float time = 0;
        while(time >= bomAppearTime)
        {
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }
        invincible = false; // 一定時間後に無敵を解除
        isCharge = true; // ボムのチャージ状態を開始
    }
    /// <summary>
    /// 直線状に弾幕を出します
    /// </summary>
    /// <returns>nullを返す</returns>
    private IEnumerator BulletCreat()
    {
        while(isShooting)
        {
            GameObject bullet = Instantiate(
            straightBulletPrehab, //弾幕
            gunPort.position, // 位置
            straightBulletPrehab.transform.rotation //回転                  
            );

            bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(1, 0, 0) * bulletSpeed;
            yield return new WaitForSeconds(delayTime); //1発打ったら待ち
        }
        
        yield return null;
    }

    /// <summary>
    /// 放射状に弾幕を出します
    /// </summary>
    private IEnumerator DiffusionBullet(int bulletCount)
    {
        while(isShooting)
        {
            Vector2 player = transform.up.normalized; // プレイヤーの向き

            float spreadAngle = 30f; // 放射状の角度

            float baseAbgle = Mathf.Atan2(player.x, player.y) * Mathf.Rad2Deg; // プレイヤーの向きの角度
            float angleStep = spreadAngle / (bulletCount - 1); // 弾幕の間隔
            float startAngle = baseAbgle - (spreadAngle / 2); // 開始角度

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + angleStep * i; // 弾幕の角度
                float rad = angle * Mathf.Deg2Rad; // ラジアンに変換
                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // 方向ベクトル

                GameObject bullet = Instantiate(diffusionBulletPrehab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed; // 弾幕の速度を設定
            }
            yield return new WaitForSeconds(delayTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "E_Bullet":
                if(!invincible)
                {
                    invincible = true;
                    audio.PlayOneShot(damageSE);
                    Destroy(collision.gameObject);
                    StartCoroutine(ResetInvincibility()); // 一定時間後に無敵解除
                }
                break;
            case "Present":
                audio.PlayOneShot(presentSE);
                PieceCount++; // ピースの数を増やす
                CoinCount += DefultCoinIncreaseCount + Random.Range(0, 5); // コインの数を増やす
                Destroy(collision.gameObject); // プレゼントを削除
                pieceText.text = $"ピース:<color=#ffd700>{PieceCount.ToString()}</color>";
                coinText.text = $"コイン:<color=#ffd700>{CoinCount.ToString()}</color>";
                break;  
        }
    }
    private IEnumerator ResetInvincibility()
    {
        if(Random.Range(0, 100) < OutPieceLate) pieceCreate.BlockCreate(); // ブロックを生成
        for (int i = 0; i < InvincibleTime; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); // 赤く点滅
            yield return new WaitForSeconds(0.05f); // 0.1秒ごとに無敵状態を維持
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1); // 元の色に戻す
            yield return new WaitForSeconds(0.05f); // 0.1秒ごとに無敵状態を維持
        }
        invincible = false;
        yield return null;
    }
}

public enum  PlayState
{
    Shooting, // シューティング中
    Puzzle, // パズル中
    Clear, // ゲームクリア時のアニメーション中
}
