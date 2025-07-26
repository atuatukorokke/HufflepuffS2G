// EnemySummoningManagement.cs
//
// �G�l�~�[�̔z�u�⏢���Ȃǂ̊Ǘ����s���܂��B
//

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private List<EnemyDeployment> enemyDeployment; // �G�l�~�[�̔z�u�f�[�^���i�[���郊�X�g
    private bool waitingForMiddleBoss = false; // �r���Ń{�X���o�Ă��邩�ǂ����̃t���O
    private bool waitingForShop = false; // �V���b�v���J���Ă��邩�ǂ����̃t���O
    private AudioSource audioSource; // BGM�̍Đ��p�I�[�f�B�I�\�[�X

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
        StartCoroutine(Enumerator()); // �G�l�~�[�̔z�u���J�n
    }

    /// <summary>
    /// �G�l�~�[�̔z�u���s���܂��B
    /// </summary>
    /// <returns></returns>
    public IEnumerator Enumerator()
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
                    BossHealth health = middleBoss.GetComponent<BossHealth>();
                    health.OnDeath += () => waitingForMiddleBoss = false; // ���{�X���|���ꂽ��t���O��������
                    yield return new WaitUntil(() => !waitingForMiddleBoss); // ���{�X���|�����܂őҋ@
                    break;
                case EnemyDeployment.state.Boss: // �{�X�̔z�u
                    Instantiate(deploment.EnemyPrehab, deploment.GenerationPosition, Quaternion.identity);
                    audioSource.clip = deploment.BossBGM; // �{�X��p��BGM��ݒ�
                    audioSource.Play(); // BGM���Đ�
                    break;
                case EnemyDeployment.state.DelayTime:
                    yield return new WaitForSeconds(deploment.DelayTime);
                    break;
                case EnemyDeployment.state.Shop:
                    var shop = FindAnyObjectByType<ShopOpen>(); // �V���b�v�̃I�u�W�F�N�g���擾
                    shop.ShopOpenAni();
                    waitingForShop = true; // �V���b�v���J���Ă���t���O�𗧂Ă�
                    shop.OnShop += () => waitingForShop = false; // �V���b�v������ꂽ��t���O��������
                    yield return new WaitUntil(() => !waitingForShop); // �V���b�v��������܂őҋ@
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
}
