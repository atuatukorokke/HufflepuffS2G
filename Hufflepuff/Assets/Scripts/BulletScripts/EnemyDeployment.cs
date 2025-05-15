using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeployment", menuName = "Scriptable Objects/EnemyDeployment")]

public class EnemyDeployment : ScriptableObject
{
	enum state
	{
        Smallfry, // �G���G
        Boss, // �{�X
        DelayTime // �҂�����
    }
    [SerializeField] private state GetState; // �E�G���G�@�E�{�X�@�E�E�F�[�u�̑҂�����
    [SerializeField] private GameObject enemyPrehab; // �G�l�~�[�̃v���n�u
    [SerializeField] private Vector2 generationPosition; // �����ʒu
    [SerializeField] private int enemyCount; //�G�l�~�[�̐��@
    [SerializeField] private int delayTime; // �E�F�[�u�̑҂�����

    private state GetState1 { get => GetState;}
    public GameObject EnemyPrehab { get => enemyPrehab;}
    public int EnemyCount { get => enemyCount; }
    public int DelayTime { get => delayTime; }
}
