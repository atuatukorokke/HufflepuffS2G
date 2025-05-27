// EnemySummoningManagement.cs
//
// �G�l�~�[�̔z�u�⏢���Ȃǂ̊Ǘ����s���܂��B
//

using System.Collections;
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
                    StartCoroutine(SmallfrySummoning(enemyDeployment[count].DelayTime, enemyDeployment[count].EnemyCount));
                    count++;
                    break;
                case EnemyDeployment.state.Boss:
                    BossSummoning();
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime); // �{�X������̑ҋ@����(�����Ȃ��Ȃ�)
                    count++;
                    break;
                case EnemyDeployment.state.DelayTime:
                    Debug.Log(enemyDeployment[count].DelayTime + "�b�ԑҋ@�ł��B");
                    yield return new WaitForSeconds(enemyDeployment[count].DelayTime); // ��O�̏������I����Ă���̑ҋ@����
                    count++;
                    break;
            }

        }
        yield return null;
    }

    /// <summary>
    /// �G���G�̏������s���܂�
    /// </summary>
    private IEnumerator SmallfrySummoning(float  delayTime, int count)
    {
        for(int i = 0; i < count; i++)
        {
            GameObject enemy = Instantiate(enemyDeployment[count].EnemyPrehab, enemyDeployment[count].GenerationPosition, Quaternion.identity); // �G�l�~�[��z�u����
            // �ړ����@�̎w��
            yield return new WaitForSeconds(delayTime); // �P�̏������ƂɂƂ�Ԋu
        }
    }
    /// <summary>
    /// �{�X�̏������s���܂��B
    /// </summary>
    private void BossSummoning()
    {
        GameObject bossEnemy = Instantiate(enemyDeployment[count].EnemyPrehab, enemyDeployment[count].GenerationPosition, Quaternion.identity); // �{�X�G�l�~�[��z�u����
    }
}
