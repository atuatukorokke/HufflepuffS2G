// EnemySummoningManagement.cs
//
// エネミーの配置や召喚などの管理を行います。
//

using System.Collections;
using UnityEngine;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private EnemyDeployment[] enemyDeployment; // エネミーの出現データの保管庫
    private int count; // enemyDeploymentの数
    private void Start()
    {
        count = 0;
        StartCoroutine(EnemySummoning());
    }

    IEnumerator EnemySummoning()
    {
        while(count < enemyDeployment.Length)
        {
            switch (enemyDeployment[count].GetState1)
            {
                case EnemyDeployment.state.Smallfry:
                    StartCoroutine(SmallfrySummoning(enemyDeployment[count].DelayTime, enemyDeployment[count].EnemyCount));
                    count++;
                    break;
                case EnemyDeployment.state.Boss:
                    BossSummoning();
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime); // ボス召喚後の待機時間(多分なくなる)
                    count++;
                    break;
                case EnemyDeployment.state.DelayTime:
                    Debug.Log(enemyDeployment[count].DelayTime + "秒間待機です。");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime); // 一個前の召喚が終わってからの待機時間
                    count++;
                    break;
            }

        }
        yield return null;
    }

    /// <summary>
    /// 雑魚敵の召喚を行います
    /// </summary>
    private IEnumerator SmallfrySummoning(float  delayTime, int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyDeployment[count].EnemyPrehab, enemyDeployment[count].GenerationPosition, Quaternion.identity); // エネミーを配置する
            // 移動方法の指定
            yield return new WaitForSeconds(delayTime); // １体召喚ごとにとる間隔
        }
    }
    /// <summary>
    /// ボスの召喚を行います。
    /// </summary>
    private void BossSummoning()
    {
        GameObject bossEnemy = Instantiate(enemyDeployment[count].EnemyPrehab, enemyDeployment[count].GenerationPosition, Quaternion.identity); // ボスエネミーを配置する
    }
}
