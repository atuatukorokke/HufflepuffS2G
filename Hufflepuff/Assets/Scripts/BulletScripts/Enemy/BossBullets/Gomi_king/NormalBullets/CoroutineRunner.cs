// ========================================
//
// CoroutineRunner.cs
//
// ========================================
//
// 非 MonoBehaviour クラスからでもコルーチンを実行できるようにするための補助クラス。
// ・シーンを跨いでも破棄されない専用オブジェクトを生成
// ・StartCoroutine / StopCoroutine を静的に呼び出せるようにする
//
// ========================================

using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    /// <summary>
    /// コルーチンを開始する。
    /// インスタンスが存在しない場合は生成して DontDestroyOnLoad にする。
    /// </summary>
    public static Coroutine Start(IEnumerator routine)
    {
        // インスタンスが存在しない場合は生成する
        if (instance == null)
        {
            GameObject obj = new GameObject("CoroutineRunner");
            instance = obj.AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(obj);
        }

        return instance.StartCoroutine(routine);
    }

    /// <summary>
    /// 実行中のコルーチンを停止する。
    /// </summary>
    public static void Stop(Coroutine routine)
    {
        // インスタンスが存在し、かつ routine が null でない場合のみ停止する
        if (instance != null && routine != null)
            instance.StopCoroutine(routine);
    }
}
