// Boss1Bulletrs.cs
//
// �{�X�̒e���𐶐�����
// �ړ��̍ۂ͉�ʂ̍������͓���Ȃ�
//


using UnityEngine;

public enum BulletState
{
    normal,
    spell
}

public enum State
{
    fast = 0,
    second = 1,
    third = 2,
    four = 3,
    final = 4
}

public class Boss1Bullet : MonoBehaviour
{
    [SerializeField] private State state = State.fast; // ���̍U�������i�K�ڂ��̔���
    [SerializeField] private BulletState bulletState = BulletState.normal; // HP���ʂłǂ̒e�����������ʂ���
    [SerializeField] private float maxHP = 100f; // �G�̍ő�HP
    [Range(0, 100)]
    [SerializeField] private float currentHP; // ����HP
    private bool isSpecialBulletActive = false; // HP��0�ɂȂ�����true�ɂȂ遨���X�g���[�h����
    private float specialBulletDuration = 15f; // ���b���X�g���[�h������
    private float timer = 0f; // �����b��������
    private float damageLate = 1f;
    [SerializeField] private float attak = 1f;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (currentHP > 0 && bulletState == BulletState.normal)
        {
            HandleBulletPattern();
        }
        else if(currentHP <= 0)
        {
            if (state == State.final)
            {
                SpecialFinalBullet();
            }
            else
            {
                TransitionToNextState();
            }
        }

        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;
            if (timer >= specialBulletDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    private void HandleBulletPattern()
    {
        // ���̏�Ԃɂ���Ēʏ�̒e����ω�������
        // �{�X�ɂ���ĕω�������̂ł��Ȃ���
        // �y�����̂Ń��[�X
        switch (state)
        {
            case State.fast:
                FireFastBullet();
                break;
            case State.second:
                FireSecondBullet();
                break;
            case State.third:
                FireThirdBullet();
                break;
            case State.four:
                FireFourBullet();
                break;
            case State.final:
                FireFinalBullet();
                break;
        }
    }
    /// <summary>
    /// ��i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private void FireFastBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
    }
    /// <summary>
    /// ��i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private void FireSecondBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
    }
    /// <summary>
    /// �O�i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private void FireThirdBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
    }
    /// <summary>
    /// �l�i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private void FireFourBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
    }
    /// <summary>
    /// �ŏI�i�K�̒ʏ�e���ł�
    /// </summary>
    private void FireFinalBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
    }
    // LastWard
    private void FireSpecialBullet()
    {
        Debug.Log("����e������: " + state);
    }

    /// <summary>
    /// �Ō�̑�Z���o���܂�
    /// </summary>
    private void SpecialFinalBullet()
    {
        if (!isSpecialBulletActive)
        {
            isSpecialBulletActive = true;
            Debug.Log("Final���: ���ʂȒe���𔭎�");
        }
    }

    /// <summary>
    /// �G�l�~�[�̏�ԉ񕜂�State�̍X�V�����܂�
    /// </summary>
    private void TransitionToNextState()
    {
        if (state < State.final)
        {
            state++;
            bulletState = BulletState.normal; // �e���̕ύX
            damageLate = 1f;
            currentHP = maxHP; // HP����
            Debug.Log("State���ύX����܂���: " + state);
        }
    }

    /// <summary>
    /// �G�l�~�[�Ƀ_���[�W��^���܂�
    /// </summary>
    /// <param name="damage">�^�_���[�W</param>
    private void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= maxHP * 0.2f)
        {
            damageLate = 0.2f; // HP�̌����X�s�[�h�̕ύX
            bulletState = BulletState.spell; // �e���̕ύX
            FireSpecialBullet();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            TakeDamage(attak * damageLate); // �_���[�W�v�Z
            Destroy(collision.gameObject); // �e������
        }
    }
}
