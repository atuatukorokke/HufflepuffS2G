// EnemySummoningManagement.cs
//
// エネミーの配置や召喚などの管理を行います。
//

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private List<EnemyDeployment> enemyDeployment; // エネミーの配置データを格納するリスト
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject TitleButton;
    [SerializeField] private Animator animator;
    private bool waitingForMiddleBoss = false; // 途中でボスが出てくるかどうかのフラグ
    private bool waitingForShop = false; // ショップを開いているかどうかのフラグ
    private AudioSource audioSource; // BGMの再生用オーディオソース
    private float fadeInTime = 2;
    private float fadeOutTime = 0;
    private bool isFateIn = false;

    private void Start()
    {
        TitleButton.SetActive(false);
        ClearPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
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
                case EnemyDeployment.state.Smallfry: // 雑魚敵の配置
                    for (int i = 0; i < deploment.EnemyCount; i++)
                    {
                        SpawnEnemy(deploment);
                        yield return new WaitForSeconds(deploment.DelayTime);
                    }
                    break;

                case EnemyDeployment.state.middleBoss: // 中ボスの配置
                    GameObject middleBoss = SpawnEnemy(deploment);
                    waitingForMiddleBoss = true; // 中ボスが出てくるフラグを立てる
                    BossHealth health = middleBoss.GetComponent<BossHealth>();
                    health.OnDeath += () => waitingForMiddleBoss = false; // 中ボスが倒されたらフラグを下げる
                    yield return new WaitUntil(() => !waitingForMiddleBoss); // 中ボスが倒されるまで待機
                    break;
                case EnemyDeployment.state.Boss: // ボスの配置
                    GameObject Bosss = Instantiate(deploment.EnemyPrehab, deploment.GenerationPosition, Quaternion.identity);
                    audioSource.clip = deploment.BossBGM; // ボス戦用のBGMを設定
                    audioSource.Play(); // BGMを再生

                    // ボスが倒された時の処理
                    Boss1Bullet BossBullet = Bosss.GetComponent<Boss1Bullet>();
                    BossBullet.Ondeath += () => StartCoroutine(BossDeath());  
                    break;
                case EnemyDeployment.state.DelayTime:
                    yield return new WaitForSeconds(deploment.DelayTime);
                    break;
                case EnemyDeployment.state.Shop:
                    var shop = FindAnyObjectByType<ShopOpen>(); // ショップのオブジェクトを取得
                    shop.ShopOpenAni();
                    waitingForShop = true; // ショップが開いているフラグを立てる
                    // ここで現在のバフ情報を渡す
                    // BuffHandOver(); 仮
                    shop.OnShop += () => waitingForShop = false; // ショップが閉じられたらフラグを下げる
                    yield return new WaitUntil(() => !waitingForShop); // ショップが閉じられるまで待機
                    yield return new WaitForSeconds(2f);
                    break;
            }
        }
    }

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

    private IEnumerator BossDeath()
    {
        FindAnyObjectByType<PlayrController>().PlayState = PlayState.Clear;
        ClearPanel.SetActive(true);
        while(audioSource.volume > 0)
        {
            audioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        animator.SetBool("EndGame", true);
        yield return new WaitForSeconds(1.7f);
        TitleButton.SetActive(true);
    }
}