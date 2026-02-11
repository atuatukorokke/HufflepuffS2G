// ========================================
//
// INormalBulletPattern.cs
//
// ========================================
//
// 通常弾幕パターンの共通インターフェース。
// すべての弾幕パターンはこの 3 メソッドを実装する。
// ・Initialize … パターン開始時の初期化
// ・Fire        … 弾幕のメイン処理（コルーチン）
// ・Clear       … 弾幕の停止・削除処理
//
// ========================================

using System.Collections;

public interface INormalBulletPattern
{
    /// <summary>
    /// 弾幕パターン開始時の初期化処理。
    /// 角度リセットやリスト初期化などを行う。
    /// </summary>
    void Initialize();

    /// <summary>
    /// 弾幕のメイン処理。
    /// 発射・移動などのループ処理をコルーチンで実行する。
    /// </summary>
    IEnumerator Fire();

    /// <summary>
    /// 弾幕の停止処理。
    /// コルーチン停止や生成済み弾の削除を行う。
    /// </summary>
    void Clear();
}
