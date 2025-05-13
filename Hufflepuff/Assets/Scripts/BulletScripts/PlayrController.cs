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
    /// プレイヤーの基本操作
    /// ・矢印キーによる移動
    /// ・Zキーを押してる間、弾幕を出す
    /// </summary>
    private void PlayerMove()
    {
        pos.x += Input.GetAxis("Horizontal");
        pos.y += Input.GetAxis("Vertical");
        transform.position = pos * Speed;
    }
}
