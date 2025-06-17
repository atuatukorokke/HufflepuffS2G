using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

// 一段階目-----------------------------------------------------------------------
[System.Serializable]
public class FastSpecialBom
{
    [SerializeField] public GameObject BulletPrehab; // 弾幕のプレハブ
    [SerializeField] public int ShotNum; // 弾幕を打つ回数
    [SerializeField] public float DelayTime; // 弾幕を打つ間隔
    [SerializeField] public float speed; // ゴミーの速さ
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
 }
// 四段階目-----------------------------------------------------------------------
[System.Serializable]
public class FourSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
}
// 最終段階目---------------------------------------------------------------------
[System.Serializable]
public class FinalSpecianBom
{
    [SerializeField] public GameObject BulletPrehab;
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

    /// <summary>
    /// 一段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator FastSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove());
        while(boss1Bullet.State == State.fast && boss1Bullet.BulletState == BulletState.spell)
        {
            for(int i = 0; i < fastSpecialBom.ShotNum; i++)
            {
                float dirX = Random.Range(1.5f, -8.5f) - transform.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - transform.position.y;
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);
                GameObject gomi = Instantiate(fastSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = gomi.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * fastSpecialBom.speed;
                yield return new WaitForSeconds(fastSpecialBom.DelayTime);
            }
            yield return new WaitForSeconds(2.5f);
        }
        yield return null;
    }

    /// <summary>
    /// 二段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator SecondSpecialBullet()
    {
        // 放射状に弾幕を生成する
        // 数秒後にプレイヤーの反対側からランダムなx座標に生成する
        // 上記の弾幕は右方向に直線的に飛ぶ

        float shotTime = 0;
        yield return StartCoroutine(FireSpecialPositionMove());
        while (boss1Bullet.State == State.second && boss1Bullet.BulletState == BulletState.spell)
        {
            while (shotTime < secondSpecialBom.delayTime)
            {
                for (int i = -3; i < 3; i++)
                {
                    float baseAngle = i * 20 + secondSpecialBom.angle;
                    float incrementalAngle = shotTime * 10f; // 時間経過で角度を変化させる

                    float rad = (baseAngle + incrementalAngle) * Mathf.Deg2Rad;

                    float dirX = Mathf.Cos(rad);
                    float dirY = Mathf.Sin(rad);

                    Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;

                    GameObject proj = Instantiate(secondSpecialBom.LeftBulletPrehab, transform.position, Quaternion.identity);

                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection * -secondSpecialBom.speed;


                }
                shotTime += 0.07f;
                yield return new WaitForSeconds(0.07f);
            }

            // 反対側から弾幕を飛ばす（ハエ）
            for(int i = 0; i < secondSpecialBom.BulletNum; i++)
            {
                Vector2 randomPos = new Vector2(-9f, Random.Range(-4.5f, 4.5f)); // 生成座標の設定
                GameObject proj = Instantiate(secondSpecialBom.RightBulletPrehab, randomPos, Quaternion.identity); // 弾幕の生成
                Vector3 moveDirection = new Vector3(-20f, 0, 0).normalized; // 方向の設定
                proj.GetComponent<Rigidbody2D>().linearVelocity = moveDirection * -secondSpecialBom.speed; // 飛ばす
                yield return new WaitForSeconds(0.05f);
            }
            shotTime = 0f;

        }
        yield return null;
    }

    /// <summary>
    /// 三段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThirdSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove());
        while (boss1Bullet.State == State.third && boss1Bullet.BulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// 四段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator FourSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove());
        while (boss1Bullet.State == State.four && boss1Bullet.BulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// 最終段階目の必殺技
    /// </summary>
    /// <returns></returns>
    public IEnumerator FinalSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialPositionMove());

        float angleStep = 360 / specialFinalAttack.bulletNum;
        float angle = specialFinalAttack.angleOffset;
        while (boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.spell)
        {
            Vector3 randomPos = new Vector3(Random.Range(-8.4f, 8.5f), Random.Range(-4.5f, 4.5f), 0);
            Debug.Log(randomPos);
            for(int i = 0; i < specialFinalAttack.bulletNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = Instantiate(specialFinalAttack.BulletPrehab, randomPos, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * specialFinalAttack.speed;

                angle += angleStep;
            }
            specialFinalAttack.angleOffset += 10f; // ここを変えれば回転速度が変わる
            if (specialFinalAttack.angleOffset >= 360) specialFinalAttack.angleOffset -= 360f; // 範囲内を保つ
            yield return new WaitForSeconds(specialFinalAttack.delayTime);

        }
        yield return null;
    }

    /// <summary>
    /// 必殺技を打つときの移動をします
    /// </summary>
    /// <returns>動作を終了させます</returns>
    private IEnumerator FireSpecialPositionMove()
    {
        boss1Bullet.DamageLate = 0f;
        float limitTime = 1.5f; // 移動にかける時間
        float elapsedTime = 0f; // 移動にかかった時間
        Vector2 startPosition = transform.position;
        // randomPosにlimitTimeかけて移動する
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, spellPos.x, elapsedTime / limitTime),
                Mathf.Lerp(startPosition.y, spellPos.y, elapsedTime / limitTime)
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        boss1Bullet.DamageLate = 0.2f;
        yield return null;
    }
}
