// ========================================
//
// ISpellPattern.cs
//
// ========================================
//
// 必殺技（スペルカード）パターンの共通インターフェース。
// ・Initialize … スペル開始時の初期化
// ・Execute    … スペル本体の処理（コルーチン）
// ・Clear      … 弾の削除や後処理
//
// すべてのスペルパターンはこの3つを実装する。
//
// ========================================

using System.Collections;

public interface ISpellPattern
{
    /// <summary>
    /// スペル開始時の初期化処理。
    /// </summary>
    void Initialize();

    /// <summary>
    /// スペル本体の処理。
    /// 弾生成・移動・演出などをコルーチンで実行する。
    /// </summary>
    IEnumerator Execute();

    /// <summary>
    /// スペル終了時の後処理。
    /// 生成済み弾の削除などを行う。
    /// </summary>
    void Clear();
}
