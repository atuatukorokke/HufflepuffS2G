using System.Collections;
using Unity.VisualScripting;
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
                    Debug.Log("雑魚敵です。");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime);
                    count++;
                    break;
                case EnemyDeployment.state.Boss:
                    Debug.Log("ボスです");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime);
                    count++;
                    break;
                case EnemyDeployment.state.DelayTime:
                    Debug.Log(enemyDeployment[count].DelayTime + "秒間待機です。");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime);
                    count++;
                    break;
            }

        }
        yield return null;
    }
}
