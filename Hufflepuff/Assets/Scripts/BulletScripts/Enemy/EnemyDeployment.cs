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

[System.Serializable]
public class EnemyDeployment
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

    public state GetState1;             // 敵の種類
    public GameObject EnemyPrehab;     // 敵のプレハブ
    public float EnemyHP;              // 敵のHP
    public Vector2 GenerationPosition; // 出現位置
    public int EnemyCount;             // 出現数
    public float DelayTime;            // 待機時間（Wave間など）
    public AudioClip BossBGM;          // ボス戦用BGM
}
