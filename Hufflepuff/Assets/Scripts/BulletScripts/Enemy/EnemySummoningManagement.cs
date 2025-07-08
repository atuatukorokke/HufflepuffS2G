// EnemySummoningManagement.cs
//
// �G�l�~�[�̔z�u�⏢���Ȃǂ̊Ǘ����s���܂��B
//

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private List<EnemyDeployment> enemyDeployment; // �G�l�~�[�̔z�u�f�[�^���i�[���郊�X�g
    private bool waitingForMiddleBoss = false; // �r���Ń{�X���o�Ă��邩�ǂ����̃t���O


    private void Start()
    {
        StartCoroutine(enumerator()); // �G�l�~�[�̔z�u���J�n
    }

    /// <summary>
    /// �G�l�~�[�̔z�u���s���܂��B
    /// </summary>
    /// <returns></returns>
    public IEnumerator enumerator()
    {
        foreach(var deploment in enemyDeployment)
        {
            switch(deploment.GetState1)
            {
                case EnemyDeployment.state.Smallfry: // �G���G�̔z�u
                    for (int i = 0; i < deploment.EnemyCount; i++)
                    {
                        SpawnEnemy(deploment);
                        yield return new WaitForSeconds(deploment.DelayTime);
                    }
                    break;

                case EnemyDeployment.state.middleBoss: // ���{�X�̔z�u
                    GameObject middleBoss = SpawnEnemy(deploment);
                    waitingForMiddleBoss = true; // ���{�X���o�Ă���t���O�𗧂Ă�
                    EnemyHealth health = middleBoss.GetComponent<EnemyHealth>();
                    if (health != null)
                    {
                        health.OnDeath += () => waitingForMiddleBoss = false; // ���{�X���|���ꂽ��t���O��������
                    }
                    yield return new WaitUntil(() => !waitingForMiddleBoss); // ���{�X���|�����܂őҋ@
                    break;
                case EnemyDeployment.state.Boss: // �{�X�̔z�u
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
