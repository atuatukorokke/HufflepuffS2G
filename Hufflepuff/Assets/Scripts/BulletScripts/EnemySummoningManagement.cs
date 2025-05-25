using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private EnemyDeployment[] enemyDeployment; // �G�l�~�[�̏o���f�[�^�̕ۊǌ�
    private int count; // enemyDeployment�̐�
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
                    Debug.Log("�G���G�ł��B");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime);
                    count++;
                    break;
                case EnemyDeployment.state.Boss:
                    Debug.Log("�{�X�ł�");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime);
                    count++;
                    break;
                case EnemyDeployment.state.DelayTime:
                    Debug.Log(enemyDeployment[count].DelayTime + "�b�ԑҋ@�ł��B");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime);
                    count++;
                    break;
            }

        }
        yield return null;
    }
}
