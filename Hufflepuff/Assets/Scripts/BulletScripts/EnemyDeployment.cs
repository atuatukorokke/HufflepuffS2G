using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeployment", menuName = "Scriptable Objects/EnemyDeployment")]

public class EnemyDeployment : ScriptableObject
{
	enum state
	{
        Smallfry, // 雑魚敵
        Boss, // ボス
        DelayTime // 待ち時間
    }
    [SerializeField] private state GetState; // ・雑魚敵　・ボス　・ウェーブの待ち時間
    [SerializeField] private GameObject enemyPrehab; // エネミーのプレハブ
    [SerializeField] private Vector2 generationPosition; // 生成位置
    [SerializeField] private int enemyCount; //エネミーの数　
    [SerializeField] private int delayTime; // ウェーブの待ち時間

    private state GetState1 { get => GetState;}
    public GameObject EnemyPrehab { get => enemyPrehab;}
    public int EnemyCount { get => enemyCount; }
    public int DelayTime { get => delayTime; }
}
