// BackGroundMover.cs
//
// 背景を動かす
//

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackGroundMover : MonoBehaviour
{
    private const float k_maxLength = 1f;           // テクスチャの最大長さ
    private const string k_propName = "_MainTex";   // マテリアルのプロパティ名

    [SerializeField]
    private Vector2 m_offsetSpeed;                  // オフセットの速度

    private Material m_copiedMaterial;              // コピーしたマテリアル

    private void Start()
    {
        // マテリアルのコピー取得
        var image = GetComponent<Image>();
        m_copiedMaterial = image.material;
        Assert.IsNotNull(m_copiedMaterial);
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        // オフセットの計算
        var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
        var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
        var offset = new Vector2(x, y);
        m_copiedMaterial.SetTextureOffset(k_propName, offset);
    }

    /// <summary>
    /// マテリアルの破棄
    /// </summary>
    private void OnDestroy()
    {
        Destroy(m_copiedMaterial);
        m_copiedMaterial = null;
    }
}