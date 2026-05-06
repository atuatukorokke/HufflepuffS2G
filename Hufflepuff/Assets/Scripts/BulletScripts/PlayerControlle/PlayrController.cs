// ========================================
//
// PlayrController.cs
//
// ========================================
//
// プレイヤーの操作・攻撃・被弾処理・ボム・コイン/ピース管理など、
// シューティングパートの中心となるクラス。
// ・矢印キーで移動（Shiftで低速移動）
// ・Zキーで弾幕発射（直線＋攻撃力に応じて拡散）
// ・Xキーでボム発動（無敵＋広範囲攻撃）
// ・プレゼント取得でコイン/ピース増加
// ・被弾時は無敵時間＋点滅演出
//
// ========================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PlayrController : MonoBehaviour
{
    [Header("Player Settings")]
    private Rigidbody2D myRigidbody;
    [SerializeField] private float Speed;
    [SerializeField] private float speedLate = 1.0f;

    [Range(1f, 5f)]
    [SerializeField] private float attack;

    [SerializeField] private GameObject straightBulletPrehab;
    [SerializeField] private GameObject diffusionBulletPrehab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private Transform gunPort;
    [SerializeField] private float delayTime;

    [SerializeField] private bool invincible = false;
    [SerializeField] private float invincibleTime = 15f;

    [SerializeField] private int presentCount = 0;
    [SerializeField] private PlayState playState;

    [SerializeField] private GameObject CutInnCanvas;
    [SerializeField] private PieceCreate pieceCreate;
    [SerializeField] private float outPieceLate;

    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI pieceText;
    [SerializeField] private TextMeshProUGUI bombText;

    private AudioSource audio;
    [SerializeField] private AudioClip damageSE;
    [SerializeField] private AudioClip presentSE;
    [SerializeField] private AudioClip specialSE;

    [Header("Player Coin")]
    [SerializeField] private int pieceCount = 0;
    [SerializeField] private int coinCount = 0;
    [SerializeField] private int defultCoinIncreaseCount = 20;

    [Header("Player Bom")]
    [SerializeField] private GameObject PlayerBomObjerct;
    [SerializeField] private float bomAppearTime = 5f;
    [SerializeField] private int bombCount = 2;

    [Header("Bomb Follower Settings")]
    [SerializeField] private GameObject bombFollowerPrefab;
    [SerializeField] private int followDelayFrames = 10;
    private List<GameObject> activeBombFollowers = new List<GameObject>();
    private List<Vector3> positionHistory = new List<Vector3>();
    private int maxHistorySize = 100;

    [Header("Homing Bullet Settings")]
    [SerializeField] private GameObject homingBulletPrehab;

    public bool isShooting = false;

    public float Attack { get => attack; set => attack = value; }
    public float InvincibleTime { get => invincibleTime; set => invincibleTime = value; }
    public PlayState Playstate { get => playState; set => playState = value; }
    public int CoinCount { get => coinCount; set => coinCount = value; }
    public int PieceCount { get => pieceCount; set => pieceCount = value; }
    public int BombCount { get => bombCount; set { bombCount = value; UpdateBombText(); } }
    public int DefultCoinIncreaseCount { get => defultCoinIncreaseCount; set => defultCoinIncreaseCount = value; }
    public float OutPieceLate { get => outPieceLate; set => outPieceLate = value; }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        Playstate = PlayState.Shooting;

        myRigidbody = GetComponent<Rigidbody2D>();
        pieceCreate = FindAnyObjectByType<PieceCreate>();

        pieceText.text = $"ピース:<color=#ffd700>{PieceCount}</color>";
        coinText.text = $"コイン:<color=#ffd700>{CoinCount}</color>";
        UpdateBombText();
    }

    private void Update()
    {
        if (Playstate == PlayState.Shooting)
            PlayerMove();
    }

    private void UpdateBombText()
    {
        if (bombText != null)
        {
            bombText.gameObject.SetActive(false);
        }
        UpdateBombFollowers();
    }

    private void UpdateBombFollowers()
    {
        if (bombFollowerPrefab == null) return;

        // ボムの数と現在の追従オブジェクトの数を合わせる
        while (activeBombFollowers.Count < bombCount)
        {
            GameObject follower = Instantiate(bombFollowerPrefab, transform.position, Quaternion.identity);
            activeBombFollowers.Add(follower);
        }

        while (activeBombFollowers.Count > bombCount)
        {
            int lastIndex = activeBombFollowers.Count - 1;
            Destroy(activeBombFollowers[lastIndex]);
            activeBombFollowers.RemoveAt(lastIndex);
        }
    }

    private void FixedUpdate()
    {
        if (Playstate != PlayState.Shooting) return;

        // 履歴の先頭に現在の位置を追加
        positionHistory.Insert(0, transform.position);

        // 必要な履歴の最大サイズを計算 (ボムの最大予想数 * 間隔 + 余裕)
        maxHistorySize = Mathf.Max(maxHistorySize, (bombCount + 1) * followDelayFrames);

        // 古い履歴を削除
        if (positionHistory.Count > maxHistorySize)
        {
            positionHistory.RemoveAt(positionHistory.Count - 1);
        }

        // 追従オブジェクトの位置を更新
        for (int i = 0; i < activeBombFollowers.Count; i++)
        {
            if (activeBombFollowers[i] == null) continue;

            int targetIndex = (i + 1) * followDelayFrames;
            
            // 履歴が足りない場合は一番古い位置を参照
            if (targetIndex >= positionHistory.Count)
            {
                targetIndex = positionHistory.Count - 1;
            }

            if (targetIndex >= 0)
            {
                // Lerpを使って滑らかに追従
                activeBombFollowers[i].transform.position = Vector3.Lerp(
                    activeBombFollowers[i].transform.position,
                    positionHistory[targetIndex],
                    Time.fixedDeltaTime * 15f
                );
            }
        }
    }

    /// <summary>
    /// プレイヤーの移動・攻撃・ボム操作
    /// </summary>
    private void PlayerMove()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speedLate = 2f;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            speedLate = 1.0f;

        float x = Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime * speedLate;
        float y = Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime * speedLate;

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x + x, -8.5f, 8.0f),
            Mathf.Clamp(transform.position.y + y, -4.5f, 4.5f)
        );

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!isShooting)
            {
                isShooting = true;
                StartCoroutine(BulletCreat());
                if (Attack >= 2) StartCoroutine(DiffusionBullet(3));
                if (Attack >= 3) StartCoroutine(ThirdBulletPattern());
            }
        }

        if (Input.GetKeyDown(KeyCode.X) && bombCount > 0)
        {
            BombCount--;
            StartCoroutine(Bom());
        }

        if (Input.GetKeyUp(KeyCode.Z))
            isShooting = false;
    }

    /// <summary>
    /// ボム発動（無敵＋広範囲攻撃）
    /// </summary>
    private IEnumerator Bom()
    {
        audio.PlayOneShot(specialSE);

        Instantiate(CutInnCanvas, Vector3.zero, Quaternion.identity);

        GameObject Bom = Instantiate(PlayerBomObjerct, transform.position, Quaternion.identity);
        Destroy(Bom, bomAppearTime);

        invincible = true;

        float time = 0;
        while (time >= bomAppearTime)
        {
            yield return new WaitForSeconds(0.1f);
            time += 0.1f;
        }

        invincible = false;
    }

    /// <summary>
    /// 直線弾幕
    /// </summary>
    private IEnumerator BulletCreat()
    {
        while (isShooting)
        {
            GameObject bullet = Instantiate(
                straightBulletPrehab,
                gunPort.position,
                straightBulletPrehab.transform.rotation
            );

            bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(1, 0, 0) * bulletSpeed;

            yield return new WaitForSeconds(delayTime);
        }
    }

    /// <summary>
    /// 拡散弾幕
    /// </summary>
    private IEnumerator DiffusionBullet(int bulletCount)
    {
        while (isShooting)
        {
            Vector2 player = transform.up.normalized;

            float spreadAngle = 30f;
            float baseAngle = Mathf.Atan2(player.x, player.y) * Mathf.Rad2Deg;
            float angleStep = spreadAngle / (bulletCount - 1);
            float startAngle = baseAngle - (spreadAngle / 2);

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + angleStep * i;
                float rad = angle * Mathf.Deg2Rad;

                Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                GameObject bullet = Instantiate(diffusionBulletPrehab, transform.position, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

    /// <summary>
    /// ホーミング弾 または 斜め弾
    /// </summary>
    private IEnumerator ThirdBulletPattern()
    {
        while (isShooting)
        {
            GameObject targetEnemy = FindClosestEnemy();

            GameObject prefabToUse = homingBulletPrehab != null ? homingBulletPrehab : straightBulletPrehab;

            if (targetEnemy != null)
            {
                // 敵がいる場合はホーミング弾を生成
                GameObject bullet = Instantiate(prefabToUse, transform.position, Quaternion.identity);
                
                // ホーミングスクリプトを追加または取得
                PlayerHomingBullet homingScript = bullet.GetComponent<PlayerHomingBullet>();
                if (homingScript == null) homingScript = bullet.AddComponent<PlayerHomingBullet>();
                
                homingScript.Initialize(targetEnemy.transform, bulletSpeed);
            }
            else
            {
                // 敵がいない場合は斜め上下に発射
                float[] angles = { 30f, -30f }; // 斜め上30度と斜め下30度
                foreach (float angle in angles)
                {
                    float rad = angle * Mathf.Deg2Rad;
                    Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

                    GameObject bullet = Instantiate(prefabToUse, transform.position, Quaternion.identity);
                    
                    // ホーミングスクリプトがついていたら無効化（直線移動のみにする）
                    PlayerHomingBullet homingScript = bullet.GetComponent<PlayerHomingBullet>();
                    if (homingScript != null) Destroy(homingScript);

                    bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * bulletSpeed;
                }
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        
        GameObject closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, currentPos);
            if (dist < minDistance)
            {
                closest = enemy;
                minDistance = dist;
            }
        }

        foreach (GameObject boss in bosses)
        {
            float dist = Vector3.Distance(boss.transform.position, currentPos);
            if (dist < minDistance)
            {
                closest = boss;
                minDistance = dist;
            }
        }

        return closest;
    }

    /// <summary>
    /// 被弾・プレゼント取得処理
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "E_Bullet":
                if (!invincible)
                {
                    invincible = true;
                    audio.PlayOneShot(damageSE);
                    Destroy(collision.gameObject);
                    StartCoroutine(ResetInvincibility());
                }
                break;

            case "Present":
                audio.PlayOneShot(presentSE);

                PieceCount++;
                CoinCount += DefultCoinIncreaseCount + Random.Range(0, 5);

                Destroy(collision.gameObject);

                pieceText.text = $"ピース:<color=#ffd700>{PieceCount}</color>";
                coinText.text = $"コイン:<color=#ffd700>{CoinCount}</color>";
                break;
        }
    }

    /// <summary>
    /// 無敵時間の点滅演出
    /// </summary>
    private IEnumerator ResetInvincibility()
    {
        if (Random.Range(0, 100) < OutPieceLate)
            pieceCreate.BlockCreate();

        for (int i = 0; i < InvincibleTime; i++)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.05f);

            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.05f);
        }

        invincible = false;
    }
}

public enum PlayState
{
    Shooting,
    Puzzle,
    Clear,
}
