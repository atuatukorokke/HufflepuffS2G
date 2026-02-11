// EffectDelete.cs
// 
// アニメーションから自身を削除します
// 

using UnityEngine;

public class EffectDelete : MonoBehaviour
{
    public void OnDelete()
    {
        Destroy(gameObject);
    }
}
