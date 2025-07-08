// EnemySummoningManagement.cs
//
// エネミーの配置や召喚などの管理を行います。
//

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private List<EnemyDeployment> enemyDeployment; // エネミーの配置データを格納するリスト
    private bool waitingForMiddleBoss = false; // 途中でボスが出てくるかどうかのフラグ


    private void Start()
    {
        StartCoroutine(enumerator()); // エネミーの配置を開始
    }

    /// <summary>
    /// エネミーの配置を行います。
    /// </summary>
    /// <returns></returns>
    public IEnumerator enumerator()
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
                    EnemyHealth health = middleBoss.GetComponent<EnemyHealth>();
                    if (health != null)
                    {
                        health.OnDeath += () => waitingForMiddleBoss = false; // 中ボスが倒されたらフラグを下げる
                    }
                    yield return new WaitUntil(() => !waitingForMiddleBoss); // 中ボスが倒されるまで待機
                    break;
                case EnemyDeployment.state.Boss: // ボスの配置
                    Instantiate(deploment.EnemyPrehab, deploment.GenerationPosition, Quaternion.identity);
                    break;
                case EnemyDeployment.state.DelayTime:
                    yield return new WaitForSeconds(deploment.DelayTime);
                    break;
            }
        }
    }

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
}
