// ========================================
//
// EnemyDeployment.cs
//
// ========================================
//
// 敵の出現情報をまとめた ScriptableObject。
// ・雑魚 / 中ボス / ボス / 待機 / ショップ の5種類の状態を管理
// ・敵のプレハブ、HP、出現位置、出現数、待機時間、BGM などを保持
// ・WaveManager や StageManager がこれを読み取って敵を生成する
//
// ========================================

using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDeployment", menuName = "Scriptable Objects/EnemyDeployment")]
public class EnemyDeployment : ScriptableObject
{
    /// <summary>
    /// 敵の種類（出現状態）
    /// </summary>
    public enum state
    {
        Smallfry,   // 雑魚
        middleBoss, // 中ボス
        Boss,       // ボス
        DelayTime,  // 待機
        Shop,       // ショップ
    }

    [SerializeField] private state GetState;             // 敵の種類
    [SerializeField] private GameObject enemyPrehab;     // 敵のプレハブ
    [SerializeField] private float enemyHP;              // 敵のHP
    [SerializeField] private Vector2 generationPosition; // 出現位置
    [SerializeField] private int enemyCount;             // 出現数
    [SerializeField] private float delayTime;            // 待機時間（Wave間など）
    [SerializeField] private AudioClip bossBGM;          // ボス戦用BGM

    public state GetState1 => GetState;
    public GameObject EnemyPrehab => enemyPrehab;
    public int EnemyCount => enemyCount;
    public float DelayTime => delayTime;
    public Vector2 GenerationPosition => generationPosition;
    public float EnemyHP => enemyHP;
    public AudioClip BossBGM { get => bossBGM; set => bossBGM = value; }
}
