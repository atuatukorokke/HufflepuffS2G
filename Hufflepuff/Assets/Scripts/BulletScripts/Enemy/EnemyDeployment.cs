// EnemyDeployment.cs
//
// �G�l�~�[���ł�
//

using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeployment", menuName = "Scriptable Objects/EnemyDeployment")]

public class EnemyDeployment : ScriptableObject
{
    /// <summary>
    /// �e�f�[�^�̎��
    /// </summary>
	public enum state
	{
        Smallfry, // �G���G
        middleBoss, // ���{�X
        Boss, // �{�X
        DelayTime, // �҂�����
        Shop, // �V���b�v
    }

    [SerializeField] private state GetState; // �E�G���G�@�E�{�X�@�E�E�F�[�u�̑҂�����
    [SerializeField] private GameObject enemyPrehab; // �G�l�~�[�̃v���n�u
    [SerializeField] private float enemyHP; // �G�l�~�[��HP
    [SerializeField] private Vector2 generationPosition; // �����ʒu
    [SerializeField] private int enemyCount; //�G�l�~�[�̐��@
    [SerializeField] private float delayTime; // �E�F�[�u�̑҂�����
    [SerializeField] private AudioClip bossBGM; // �{�X��p��BGM

    public state GetState1 { get => GetState;}
    public GameObject EnemyPrehab { get => enemyPrehab;}
    public int EnemyCount { get => enemyCount; }
    public float DelayTime { get => delayTime; }
    public Vector2 GenerationPosition { get => generationPosition; }
    public float EnemyHP { get => enemyHP;}
    public AudioClip BossBGM { get => bossBGM; set => bossBGM = value; }
}
