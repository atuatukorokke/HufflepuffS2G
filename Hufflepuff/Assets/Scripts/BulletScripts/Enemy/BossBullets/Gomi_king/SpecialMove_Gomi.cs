using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一段階目-----------------------------------------------------------------------
[System.Serializable]
public class FastSpecialBom
{
    [SerializeField] public GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] public int ShotNum; // 弾幕を打つ回数
    [SerializeField] public float DelayTime; // 弾幕を打つ間隔
    [SerializeField] public float speed; // ゴミーの速さ
    [SerializeField] public float delayTime; // 弾幕を打つまでの待機時間
}
// 二段階目-----------------------------------------------------------------------
[System.Serializable]
public class SecondSpecialBom
{
    public GameObject RightBulletPrehab; // 右向きのハエ
    public GameObject LeftBulletPrehab; // 左向きのハエ
    public float delayTime;
    public int BulletNum; // 打つ数
    public float time; // 何秒後に後ろからハエを出すか
    public float speed; // 弾幕の速さ
    [Range(0, 360)]
    public float angle;

}
// 三段階目-----------------------------------------------------------------------
[System.Serializable]
public class ThirdSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float minSpeed;
    [SerializeField] public float delayTime;
 }
// 四段階目-----------------------------------------------------------------------
[System.Serializable]
public class FourSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float stopTime; // 止まるまでの時間
    [SerializeField] public int bulletNum; // 何発弾幕のまとまりを撃つか
    [SerializeField] public float circleDelayTime; // 円形の弾幕で何秒待機するか
    [SerializeField] public float speed; // 弾幕の速さ
    [SerializeField] public float angleOffset; // 弾幕の角度をずらすための変数
    [SerializeField] public float crossSpeed; // 交差上に弾を動かすときの速さ 
    [SerializeField] public float expandSpeed; // 拡散スピード
    [SerializeField] public float rotationSpeed; // 毎秒回転角度（度）
    [SerializeField] public float arcCount; // 弾幕の数（弾幕のまとまりの数）
    [SerializeField] public float arcAngle; // 弾幕のまとまりの角度
    [SerializeField] public float arcSpeed; // 弾幕のまとまりの速さ
    [SerializeField] public float movementSpeed; // 弾幕のまとまりの移動速度
    [SerializeField] public int arcLine; // 弾幕のまとまりのライン数（弾幕のまとまりの数）

}
// 最終段階目---------------------------------------------------------------------
[System.Serializable]
public class FinalSpecianBom
{
    [SerializeField] public GameObject BulletPrehab;
    [SerializeField] public float maxSpeed; // ランダムな弾幕の最大速さ
    [SerializeField] public float minSpeed; // ランダムな弾幕の最小速さ
    [SerializeField] public float randomSpeed; // ランダムな弾幕の速さ
    [SerializeField] public float randomBulletTime; // ランダムな弾幕を出す時間
    [SerializeField] public int radiationBulletNum; // 放射状に出す弾幕の数
    [SerializeField] public float radiationBulletSpeed; // 放射状に出す弾幕の速さ
    [SerializeField] public float radiationBulletDelayTime; // 放射状に出す弾幕の出す間隔
    [SerializeField] public float radiationBulletCount; // 放射状に出す弾幕の数（何回放射状に出すか）
    [SerializeField] public float radiationBulletAngle; // 放射状に出す弾幕の角度
    [SerializeField] public float breakTime; // 停止した弾幕を動かした後の待機時間
    [SerializeField] public Color bulletColor; // 弾幕の色
}
// セミファイナル-----------------------------------------------------------------
[System.Serializable]
public class SpecialFinalAttack
{
    public GameObject BulletPrehab; // 弾幕のプレハブ
    public int bulletNum; // 弾幕の数
    public float speed; // 弾幕のスピード
    public float delayTime; // 弾幕の出す間隔
    public float angleOffset;
 }

public class SpecialMove_Gomi : MonoBehaviour
{
    [Header("ボス全体を管理する変数")]
    [SerializeField] private Vector2 spellPos;// 必殺技・セミファイナルを打つときにこの座標に一旦戻る
    [SerializeField] private Boss1Bullet boss1Bullet;

    [Header("一段階目の必殺技の変数")]
    [SerializeField] private FastSpecialBom fastSpecialBom;
    [Header("二段階目の必殺技の変数")]
    [SerializeField] private SecondSpecialBom secondSpecialBom;
    [Header("三段階目の必殺技の変数")]
    [SerializeField] private ThirdSpecialBom thirdSpecialBom;
    [Header("四段階目の必殺技の変数")]
    [SerializeField] private FourSpecialBom fourSpecialBom;
    [Header("最終段階目の必殺技の変数")]
    [SerializeField] private FinalSpecianBom finalSpecianBom;
    [Header("セミファイナルの変数")]
    [SerializeField] private SpecialFinalAttack specialFinalAttack;
   
    /// <summary>
    /// どの必殺技を打つかの判定を行います
    /// </summary>
    /// <param name="state">今のボスの状態です</param>
    public void BomJudgement(State state)
    {
        switch(state)
        {
            case State.fast:
                StartCoroutine(FastSpecialBullet());
                break;
            case State.second:
                StartCoroutine(SecondSpecialBullet());
                break;
            case State.third:
                StartCoroutine(ThirdSpecialBullet());
                break;
            case State.four:
                StartCoroutine(FourSpecialBullet());
                break;
            case State.final:
                StartCoroutine(FinalSpecialBullet());
                break;

        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 一段階目の必殺技（高速ランダム弾幕）
    /// </summary>
    /// <returns>IEnumerator（コルーチン）</returns>
    private IEnumerator FastSpecialBullet()
    {
        // 指定位置（spellPos）へ移動（演出や配置意図あり）
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        // 状態が fast かつ spell 中の間、攻撃ループを継続
        while (boss1Bullet.State == State.fast && boss1Bullet.BulletState == BulletState.spell)
        {
            for (int i = 0; i < fastSpecialBom.ShotNum; i++)
            {
                // 現在位置から画面左寄り（最大 -8.5f まで）に向けてランダムな方向を生成
                float dirX = Random.Range(1.5f, -8.5f) - transform.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - transform.position.y;
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                // 弾プレハブを現在位置に生成し、速度を設定して発射
                GameObject gomi = Instantiate(fastSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = gomi.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * fastSpecialBom.speed;

                // 各弾発射ごとに遅延（間隔を調整可能）
                yield return new WaitForSeconds(fastSpecialBom.DelayTime);
            }

            // 一連の発射が終わったら小休止（テンポを調整）
            yield return new WaitForSeconds(fastSpecialBom.delayTime);
        }

        // 状態遷移などでループを抜けたら終了
        yield return null;
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 二段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator SecondSpecialBullet()
    {
        // 放射状に弾幕を生成する
        // 数秒後にプレイヤーの反対側からランダムなx座標に生成する
        // 上記の弾幕は右方向に直線的に飛ぶ

        float shotTime = 0; // 経過時間を初期化（角度変化に使う）

        // スペル発動位置に移動（演出と弾幕開始のトリガーとして）
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        // 状態が「second」かつ「spell」の間、弾幕を継続
        while (boss1Bullet.State == State.second && boss1Bullet.BulletState == BulletState.spell)
        {
            // 一定時間の間だけ、回転弾を発射し続ける
            while (shotTime < secondSpecialBom.delayTime)
            {
                // 状態変化があったら即時終了（安全対策）
                if (boss1Bullet.State != State.second || boss1Bullet.BulletState != BulletState.spell) break;

                // 左側から扇状に弾を発射
                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + secondSpecialBom.angle; // 基本角度（中央基準で間隔を空ける）
                    float incrementalAngle = shotTime * 10f; // 時間経過に応じて角度に変化を加える
                    float rad = (baseAngle + incrementalAngle) * Mathf.Deg2Rad; // ラジアンに変換

                    // 指定角度の方向ベクトルを計算
                    float dirX = Mathf.Cos(rad);
                    float dirY = Mathf.Sin(rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;

                    // 弾プレハブをボスの位置から生成
                    GameObject proj = Instantiate(secondSpecialBom.LeftBulletPrehab, transform.position, Quaternion.identity);

                    // ベクトル方向に速度を与えて発射（マイナス方向で外に向かって発射）
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection * -secondSpecialBom.speed;
                    proj.transform.rotation = Quaternion.Euler(0, 0, baseAngle); // 弾の向きを調整
                }

                // 次のループのために時間を加算＆ウェイト
                shotTime += 0.07f;
                yield return new WaitForSeconds(0.07f);
            }

            // 状態を再確認（ループの再突入前に安全にブレーク）
            if (boss1Bullet.State != State.second || boss1Bullet.BulletState != BulletState.spell) break;

            // 右側から直進弾をランダムY座標で発射（左右からの挟撃になる）
            for (int i = 0; i < secondSpecialBom.BulletNum; i++)
            {
                if (boss1Bullet.State != State.second || boss1Bullet.BulletState != BulletState.spell) continue;

                Vector2 randomPos = new Vector2(-9f, Random.Range(-4.5f, 4.5f)); // 左端からYランダム生成
                GameObject proj = Instantiate(secondSpecialBom.RightBulletPrehab, randomPos, Quaternion.identity);
                Vector3 moveDirection = new Vector3(-20f, 0, 0).normalized; // 画面左方向へ直進

                // 高速な直進弾で圧力をかける
                proj.GetComponent<Rigidbody2D>().linearVelocity = moveDirection * -secondSpecialBom.speed;
                yield return new WaitForSeconds(0.05f);
            }

            // shotTime をリセットして再ループへ（時計回りに角度増加の再開始）
            shotTime = 0f;
        }

        // 状態が変化したら終了
        yield return null;

    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 三段階目の必殺技（360度方向へランダム速度で弾を発射）
    /// </summary>
    /// <returns>IEnumerator（コルーチン）</returns>
    private IEnumerator ThirdSpecialBullet()
    {
        // スペルカード発動位置へ移動（演出的な効果や位置制御）
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        // ボスが「third」状態かつ「spell」状態の間、弾幕ループを継続
        while (boss1Bullet.State == State.third && boss1Bullet.BulletState == BulletState.spell)
        {
            // ランダムな角度（0～360度）を生成
            float angle = Random.Range(0f, 360f);

            // 弾速もランダムに設定（指定範囲内）
            float speed = Random.Range(thirdSpecialBom.minSpeed, thirdSpecialBom.maxSpeed);

            // 角度に応じた方向ベクトルを算出（Vector2.right を回転）
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            // 弾プレハブを現在位置に生成
            GameObject bullet = Instantiate(thirdSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 方向ベクトルに速度を掛けて弾速を設定
                rb.linearVelocity = direction * speed;
            }

            // 発射間隔を極小に設定（非常に高密度な弾幕を構成）
            yield return new WaitForSeconds(0.01f);
        }

        // 状態が変化したら終了
        yield return null;
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 四段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator FourSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));
        while (boss1Bullet.State == State.four && boss1Bullet.BulletState == BulletState.spell)
        {
            // 弾幕を円状に撃った後に
            // 弾を放射状に打つように変換する
            for (int i = 0; i < 3; i++)
            {
                List<GameObject> bullets = new List<GameObject>();
                for (int k = 0; k < 2; k++)
                {
                    if(boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;
                    float angleStep = 360f / fourSpecialBom.bulletNum;
                    float angle = fourSpecialBom.angleOffset; // 角度をずらす
                    for (int j = 0; j < fourSpecialBom.bulletNum; j++)
                    {
                        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                        Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                        GameObject proj = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                        rb.linearVelocity = moveDirection.normalized * fourSpecialBom.speed * (k + 1) * 0.7f;

                        bullets.Add(proj); // 弾をリストに追加
                        angle += angleStep; // 角度をずらす
                    }
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // 円形の弾幕で待機

                // ボスのステートが変わったらダンマクの強制終了
                if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;

                // 弾幕を停止する
                foreach (GameObject bullet in bullets)
                {
                    if(bullet == null) continue;
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = Vector2.zero; // 弾の速度をゼロにする   
                }
                yield return new WaitForSeconds(fourSpecialBom.stopTime); // 円形の弾幕で待機

                StartCoroutine(ExpandMove(bullets)); // 弾を拡散させる
                yield return null;
            }

            // ボスのステートが変わったらダンマクの強制終了
            if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;

            // ランダムな座標に移動しながら弾を飛ばす
            Vector2 randomPos = new Vector2(Random.Range(2.0f, 8.5f), Random.Range(-4.5f, 4.5f));
            StartCoroutine(PositionMove(randomPos)); // ランダムな座標へ移動
            for (int i = 0; i < 3; i++)
            {
                // ボスのステートが変わったらダンマクの強制終了
                if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) continue;

                // 弾幕生成
                List<GameObject> bullets = new List<GameObject>();
                // 扇状に弾幕を生成して
                float startAngle = 180f - fourSpecialBom.arcAngle / 2f; // 扇の開始角度
                float angleStep = fourSpecialBom.arcAngle / (fourSpecialBom.arcCount - 1);

                for(int j = 0; j < fourSpecialBom.arcCount; j++)
                {
                    for(int k = 0; k < fourSpecialBom.arcLine; k++)
                    {
                        // 弾幕を縦一列に扇状に作成
                        // 弾の角度を計算
                        float angle = startAngle + j * angleStep; // 弾の角度
                        float rad = angle * Mathf.Rad2Deg; // ラジアンに変換
                        Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // 弾の方向を計算

                        // 弾を生成
                        GameObject bullet = Instantiate(fourSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                        // 弾幕の速度はkの値によって変化
                        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                        if (rb != null)
                        {
                            rb.linearVelocity = direction * (fourSpecialBom.arcSpeed - (k * 0.1f)); // 弾の速度を設定
                        }
                        bullets.Add(bullet); // 弾をリストに追加
                    }
                    yield return new WaitForSeconds(0.01f); // 弾を飛ばす間隔
                }
                StartCoroutine(BulletMover(bullets));
                yield return new WaitForSeconds(0.7f);
            }
            yield return StartCoroutine(PositionMove(spellPos));
        }
        yield return null; // コルーチンの終了
    }

    /// <summary>
    /// 弾を拡散させて動かすコルーチン
    /// </summary>
    /// <param name="bullets">弾幕のリスト</param>
    /// <returns></returns>
    private IEnumerator ExpandMove(List<GameObject> bullets)
    {
        while (true)
        {
            float angleDelta = fourSpecialBom.rotationSpeed * Time.deltaTime;

            for (int j = 0; j < bullets.Count; j++)
            {
                GameObject bullet = bullets[j];

                if (bullet == null) continue; // 弾が存在しない場合はスキップ
                if (j % 2 == 0)
                {
                    // 弾の位置ベクトルを取得
                    Vector3 dir = bullet.transform.position - transform.position;

                    // 現在の角度 + 追加角度で回転
                    dir = Quaternion.Euler(0, 0, angleDelta) * dir;

                    // 拡散
                    dir += dir.normalized * fourSpecialBom.expandSpeed * Time.deltaTime;

                    bullet.transform.position = transform.position + dir;
                }
                else
                {
                    // 弾の位置ベクトルを取得
                    Vector3 dir = bullet.transform.position - transform.position;

                    // 現在の角度 + 追加角度で回転
                    dir = Quaternion.Euler(0, 0, -angleDelta) * dir;

                    // 拡散
                    dir += dir.normalized * fourSpecialBom.expandSpeed * Time.deltaTime;

                    bullet.transform.position = transform.position + dir;
                }

            }
            yield return null;
        }
    }

    /// <summary>
    /// 弾幕を自機狙いに飛ばすコルーチンコ
    /// </summary>
    /// <param name="bullets"></param>
    /// <returns></returns>
    private IEnumerator BulletMover(List<GameObject> bullets)
    {
        // 数秒停止状態にする
        yield return new WaitForSeconds(1f);
        foreach (GameObject bullet in bullets)
        {
            if(bullet == null) continue;
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero; // 弾の速度をゼロにする
        }
        if (boss1Bullet.State != State.four || boss1Bullet.BulletState != BulletState.spell) StopCoroutine(BulletMover(bullets));
        yield return new WaitForSeconds(1f); // 数秒間待機
                                                                         // 数秒後に自機狙いで弾を飛ばす
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets != null)
            {
                Vector3 direction = (GameObject.FindGameObjectWithTag("Player").transform.position - bullets[i].transform.position).normalized;
                bullets[i].GetComponent<Rigidbody2D>().linearVelocity = direction * 10f; // 弾の速さを設定
                yield return new WaitForSeconds(0.01f); // 弾を飛ばす間隔
            }
        }
        yield return null;
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 最終段階目の必殺技
    /// </summary>
    /// <returns></returns>
    public IEnumerator FinalSpecialBullet()
    {
        // 乱雑に弾幕を数秒間飛ばす〇
        // その後、画面内の弾幕の動きを止める〇
        // その後、移動しながら数秒間放射状に弾幕を飛ばす
        // 最後に画面内の弾幕をランダムな方向に一定の速度で飛ばす
        yield return StartCoroutine(FireSpecialPositionMove(spellPos));

        while(boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.spell) 
        {
            // 乱雑に弾幕を飛ばす
            float randomTime = 0; // 乱雑に弾幕を飛ばす時間
            List<GameObject> bullets = new List<GameObject>(); // 弾幕のリスト
            while (randomTime < finalSpecianBom.randomBulletTime) // 弾幕を乱雑に飛ばす時間が経過するまでループ
            {
                if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // 状態が変わったらループを抜ける
                float angle = Random.Range(0f, 360f); // ランダムな角度を生成
                float speed = Random.Range(finalSpecianBom.minSpeed, finalSpecianBom.maxSpeed); // ランダムな速度を生成
                Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // ランダムな方向を計算

                GameObject bullet = Instantiate(thirdSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.linearVelocity = direction * speed; // 弾の速度を設定
                }
                yield return new WaitForSeconds(0.01f);
                bullets.Add(bullet); // 弾をリストに追加
                randomTime += 0.01f; // 乱雑に弾幕を飛ばす時間を更新
            }
            if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // 状態が変わったらループを抜ける
            // 乱雑に飛ばした弾幕を停止する
            yield return new WaitForSeconds(1f); // 乱雑に弾幕を飛ばした後の待機時間
            foreach (GameObject bullet in bullets) // 乱雑に飛ばした弾幕を停止する
            {
                if (bullet != null)
                {
                    bullet.GetComponent<SpriteRenderer>().color = finalSpecianBom.bulletColor; // 弾の色を黒に変更
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = Vector2.zero; // 弾の速度をゼロにする
                }
            }
            // 放射状に弾幕を飛ばす
            if(boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // 状態が変わったらループを抜ける
            StartCoroutine(PositionMove(new Vector2(Random.Range(1.5f, 8.5f), Random.Range(-4.5f, 4.5f))));
            for (int i = 0; i < finalSpecianBom.radiationBulletCount; i++)
            {
                if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // 状態が変わったらループを抜ける
                float startAngle = 180f - finalSpecianBom.radiationBulletAngle / 2f; // 放射状の開始角度
                float angleStep = finalSpecianBom.radiationBulletAngle / (finalSpecianBom.radiationBulletNum - 1); // 放射状の角度ステップ
                for (int j = 0; j < finalSpecianBom.radiationBulletNum; j++)
                {
                    if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // 状態が変わったらループを抜ける
                    float angle = startAngle + j * angleStep; // 弾の角度を計算    
                    float rad = angle * Mathf.Deg2Rad; // ラジアンに変換

                    Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)); // 弾の方向を計算
                    GameObject bullet = Instantiate(finalSpecianBom.BulletPrehab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = direction * (finalSpecianBom.radiationBulletSpeed - (j * 0.1f)); // 弾の速度を設定
                    }
                }
                yield return new WaitForSeconds(finalSpecianBom.radiationBulletDelayTime); // 放射状に弾幕を飛ばす間隔
            }
            if (boss1Bullet.State != State.final || boss1Bullet.BulletState != BulletState.spell) break; // 状態が変わったらループを抜ける 
            // 放射状に弾幕を飛ばした後の待機時間
            yield return new WaitForSeconds(1f); // 放射状に弾幕を飛ばした後の待機時間
            // 画面内の弾幕をランダムな方向に一定の速度で飛ばす
            foreach (GameObject bullet in bullets)
            {
                if (bullet != null)
                {
                    // ランダムな方向を計算
                    float angle = Random.Range(0f, 360f);
                    Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right; // ランダムな方向を計算
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity = direction * finalSpecianBom.randomSpeed; // 弾の速度を設定
                    }
                }
            }
            yield return new WaitForSeconds(finalSpecianBom.breakTime); // 停止した弾幕を動かした後の待機時間 
        }
        yield return null;  
    }

    // ------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// セミファイナル
    /// </summary>
    /// <returns>IEnumerator for coroutine execution</returns>
    public IEnumerator FinalSpecialAttack()
    {
        // 弾の総数に基づいて円周の角度ステップを計算（360度を等分）
        float angleStep = 360 / specialFinalAttack.bulletNum;
        float angle = specialFinalAttack.angleOffset;

        // ボスの状態がファイナルかつスペル状態の間、攻撃を繰り返す
        while (boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.special)
        {
            // ランダムな生成位置を画面内から決定（やや広めの指定範囲）
            Vector3 randomPos = new Vector3(Random.Range(-8.4f, 8.5f), Random.Range(-4.5f, 4.5f), 0);

            // 弾幕を構成する個々の弾をループで発射
            List<GameObject> bullets = new List<GameObject>(); // 弾のリストを初期化
            for (int i = 0; i < specialFinalAttack.bulletNum; i++)
            {
                // 現在の角度に基づいて方向ベクトルを計算（ラジアンに変換）
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                // 弾プレハブをランダム位置に生成し、速度を与えて発射
                GameObject proj = Instantiate(specialFinalAttack.BulletPrehab, randomPos, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * specialFinalAttack.speed;

                // 弾をリストに追加
                bullets.Add(proj);

                // 次の弾の角度を設定
                angle += angleStep;
            }
            yield return new WaitForSeconds(0.2f); // 弾幕の発射間隔を待機

            // 弾幕のスピードを少し遅くする
            foreach(GameObject bullet in bullets)
            {
                if (bullet != null)
                {
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.linearVelocity *= 0.2f; // 弾の速度を少し遅くする
                    }
                }
            }

            // 弾幕を回転させるために角度オフセットを加算
            specialFinalAttack.angleOffset += 10f; // この値を変えると回転速度が変わる

            // オフセット角度が360度を超えたら0に戻す
            if (specialFinalAttack.angleOffset >= 360)
                specialFinalAttack.angleOffset -= 360f;

            // 一定時間待機してから次の波を実行
            yield return new WaitForSeconds(specialFinalAttack.delayTime);
        }

        // 条件を抜けた場合にコルーチンを終了
        yield return null;
    }

    /// <summary>
    /// 必殺技発動時にボスが特定の位置へ滑らかに移動する処理
    /// </summary>
    /// <returns>移動後に動作終了するコルーチン</returns>
    private IEnumerator FireSpecialPositionMove(Vector2 targetPos)
    {
        // 移動中はボスの被ダメージを一時的に無効化
        boss1Bullet.DamageLate = 0f;

        float limitTime = 1.5f; // 移動にかかる時間（秒）
        float elapsedTime = 0f; // 経過時間の初期化
        Vector2 startPosition = transform.position; // 移動開始位置を記録

        // 指定時間内でtargetPosまで線形補間（Lerp）を用いて滑らかに移動
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetPos.x, elapsedTime / limitTime), // X方向に補間
                Mathf.Lerp(startPosition.y, targetPos.y, elapsedTime / limitTime)  // Y方向に補間
            );

            // 時間を更新し、次のフレームへ
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 移動完了後、ボスの被ダメージを再び有効化（例：遅延0.2秒）
        boss1Bullet.DamageLate = 0.2f;

        // コルーチンを終了
        yield return null;
    }
    /// <summary>
    /// 必殺技発動時にボスが特定の位置へ滑らかに移動する処理
    /// </summary>
    /// <returns>移動後に動作終了するコルーチン</returns>
    private IEnumerator PositionMove(Vector2 targetPos)
    {

        float limitTime = 1.5f; // 移動にかかる時間（秒）
        float elapsedTime = 0f; // 経過時間の初期化
        Vector2 startPosition = transform.position; // 移動開始位置を記録

        // 指定時間内でtargetPosまで線形補間（Lerp）を用いて滑らかに移動
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, targetPos.x, elapsedTime / limitTime), // X方向に補間
                Mathf.Lerp(startPosition.y, targetPos.y, elapsedTime / limitTime)  // Y方向に補間
            );

            // 時間を更新し、次のフレームへ
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // コルーチンを終了
        yield return null;
    }
}
