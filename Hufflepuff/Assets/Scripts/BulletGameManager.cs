using UnityEngine;

public class BulletGameManager : MonoBehaviour
{
    [SerializeField] private float gameTime = 0.0f; // �Q�[���̌o�ߎ��Ԃ��Ǘ�����ϐ�
    private bool isStart = false;�@ // �Q�[�����J�n����Ă��邩�ǂ������Ǘ�����t���O

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameTime = 0.0f;
        Cursor.visible = false; // �J�[�\�����\���ɂ���
        Cursor.lockState = CursorLockMode.Locked; // �J�[�\������ʒ����ɌŒ肷��
    }

    // Update is called once per frame
    void Update()
    {
        if(isStart)
        {
            gameTime += Time.deltaTime; // �Q�[���̌o�ߎ��Ԃ��X�V
        }
    }
    /// <summary>
    /// �Q�[�����J�n���郁�\�b�h�ł��B
    /// </summary>
    public void StartGame()
    {
        isStart = true; // �Q�[�����J�n����
        gameTime = 0.0f; // �Q�[�����Ԃ����Z�b�g
    }
}
