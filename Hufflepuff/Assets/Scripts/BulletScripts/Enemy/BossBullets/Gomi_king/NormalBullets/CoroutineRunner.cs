using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    public static Coroutine Start(IEnumerator routine)
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("CoroutineRunner");
            instance = obj.AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(obj);
        }
        return instance.StartCoroutine(routine);
    }

    public static void Stop(Coroutine routine)
    {
        if (instance != null && routine != null)
            instance.StopCoroutine(routine);
    }
}
