using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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
    [SerializeField] public GameObject BulletPrehab;
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
        yield return StartCoroutine(FireSpecialBullet());
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
        yield return StartCoroutine(FireSpecialBullet());
        while (boss1Bullet.State == State.second && boss1Bullet.BulletState == BulletState.spell)
        {
            for(int i = -3; i < 3; i++)
            {
                float baseAngle = i * 20 + secondSpecialBom.angle;
                float rad = baseAngle * Mathf.Rad2Deg;

                yield return new WaitForSeconds(0.5f);
            }
        }
        yield return null;
    }

    /// <summary>
    /// 三段階目の必殺技
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThirdSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialBullet());
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
        yield return StartCoroutine(FireSpecialBullet());
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
    private IEnumerator FinalSpecialBullet()
    {
        yield return StartCoroutine(FireSpecialBullet());
        while (boss1Bullet.State == State.final && boss1Bullet.BulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// 必殺技を打つときの移動をします
    /// </summary>
    /// <returns>動作を終了させます</returns>
    private IEnumerator FireSpecialBullet()
    {
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
        yield return null;
    }
}
