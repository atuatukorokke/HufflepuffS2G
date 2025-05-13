using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayrController : MonoBehaviour
{
    private Vector2 pos;
    [SerializeField] private float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    /// <summary>
    /// �v���C���[�̊�{����
    /// �E���L�[�ɂ��ړ�
    /// �EZ�L�[�������Ă�ԁA�e�����o��
    /// </summary>
    private void PlayerMove()
    {
        pos.x += Input.GetAxis("Horizontal");
        pos.y += Input.GetAxis("Vertical");
        transform.position = pos * Speed;
    }
}
