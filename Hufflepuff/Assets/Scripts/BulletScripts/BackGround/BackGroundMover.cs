// BackGroundMover.cs
//
// �w�i�𓮂���
//

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackGroundMover : MonoBehaviour
{
    private const float k_maxLength = 1f;           // �e�N�X�`���̍ő咷��
    private const string k_propName = "_MainTex";   // �}�e���A���̃v���p�e�B��

    [SerializeField]
    private Vector2 m_offsetSpeed;                  // �I�t�Z�b�g�̑��x

    private Material m_copiedMaterial;              // �R�s�[�����}�e���A��

    private void Start()
    {
        // �}�e���A���̃R�s�[�擾
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

        // �I�t�Z�b�g�̌v�Z
        var x = Mathf.Repeat(Time.time * m_offsetSpeed.x, k_maxLength);
        var y = Mathf.Repeat(Time.time * m_offsetSpeed.y, k_maxLength);
        var offset = new Vector2(x, y);
        m_copiedMaterial.SetTextureOffset(k_propName, offset);
    }

    /// <summary>
    /// �}�e���A���̔j��
    /// </summary>
    private void OnDestroy()
    {
        Destroy(m_copiedMaterial);
        m_copiedMaterial = null;
    }
}