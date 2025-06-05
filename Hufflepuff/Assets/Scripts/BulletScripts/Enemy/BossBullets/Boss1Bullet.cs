// Boss1Bulletrs.cs
//
// �{�X�̒e���𐶐�����
// �ړ��̍ۂ͉�ʂ̍������͓���Ȃ�
//

using System.Collections;
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
    [Header("�{�X�̑S�̂��Ǘ�����ϐ�")]
    [SerializeField] private State state = State.fast; // ���̍U�������i�K�ڂ��̔���
    [SerializeField] private BulletState bulletState = BulletState.normal; // HP���ʂłǂ̒e�����������ʂ���
    [SerializeField] private float maxHP = 100f; // �G�̍ő�HP
    [Range(0, 100)]
    [SerializeField] private float currentHP; // ����HP
    private bool isSpecialBulletActive = false; // HP��0�ɂȂ�����true�ɂȂ遨���X�g���[�h����
    private float specialBulletDuration = 15f; // ���b���X�g���[�h������
    private float timer = 0f; // �����b��������
    private float damageLate = 1f; // �_���[�W��^���銄��
    [SerializeField] private float attak = 1f; // �U����
    [SerializeField] private Vector2 spellPos; // �K�E�Z�E�Z�~�t�@�C�i����łƂ��ɂ��̍��W�Ɉ�U�߂�

    [Header("�m�[�}���~�`�e���̕ϐ�")]
    [SerializeField] private GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] private int FlyingNum; // ���˂��鐔
    [SerializeField] private int frequency; // ���ˉ�
    [SerializeField] private float speed; // �e���̃X�s�[�h
    [SerializeField] private float DeleteTime; // �폜���鎞��
    [SerializeField] private float delayTime; // �e�����o���Ԋu
    private float angleOffset = 0f; // ���炵�p�̊p�x
    [SerializeField] private float moveSpeed;

    [Header("�m�[�}����]�~�`�e���̕ϐ�")]
    [SerializeField] private GameObject RevolutionBulletPrehab;
    [SerializeField] private int FevolutionFlyingNum;
    [SerializeField] private int FevolutionFrequency;
    [SerializeField] private float FevolutionSpeed;
    [SerializeField] private float FevolutionDeleteTime;
    [SerializeField] private float FevolutionDelayTime;
    private float FevolutionAngleOffset = 0;

    void Start()
    {
        currentHP = maxHP;
        StartCoroutine(BulletUpdate());
    }


    private void Update()
    {
        // ���b�ԃZ�~�t�@�C�i����ł�������Ə�����
        if (isSpecialBulletActive)
        {
            timer += Time.deltaTime;
            if (timer >= specialBulletDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// �{�X���ǂ̒e����ł��̔��ʂ��s���܂�
    /// </summary>
    /// <returns></returns>
    IEnumerator BulletUpdate()
    {
        if (currentHP > 0 && bulletState == BulletState.normal)
        {
            StartCoroutine(HandleBulletPattern());
        }

        
        yield return null;
    }

    /// <summary>
    /// �ʏ�e���̔��ʂ��s���܂�
    /// </summary>
    private IEnumerator HandleBulletPattern()
    {
        // ���̏�Ԃɂ���Ēʏ�̒e����ω�������
        // �{�X�ɂ���ĕω�������̂ł��Ȃ���
        // �y�����̂Ń��[�X
        switch (state)
        {
            case State.fast:
                yield return StartCoroutine(FireFastBullet());
                break;
            case State.second:
                yield return StartCoroutine(FireSecondBullet());
                break;
            case State.third:
                yield return StartCoroutine(FireThirdBullet());
                break;
            case State.four:
                yield return StartCoroutine(FireFourBullet());
                break;
            case State.final:
                yield return StartCoroutine(FireFinalBullet());
                break;
        }
    }
    /// <summary>
    /// ��i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private IEnumerator FireFastBullet()
    {
        // ��ʉE�����������_���Ɉړ����Ă���
        // �~�`�̒e����ł�
        while(state == State.fast && bulletState == BulletState.normal)
        {
            // �e�̉��Ԋu�̌v�Z
            float angleStep = 360f / FlyingNum;
            float angle = angleOffset;

            // frequency�̉񐔂����e���𐶐�����
            // FlyingNum�͈��̐����ŉ��e�������o����
            for (int i = 0; i < frequency; i++)
            {
                for (int j = 0; j < FlyingNum; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                    GameObject proj = Instantiate(BulletPrehab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection.normalized * speed;

                    angle += angleStep;

                    Destroy(proj, DeleteTime); // ���b��ɒe��������

                }
                angleOffset += 10f; // ������ς���Ή�]���x���ς��
                if (angleOffset >= 360) angleOffset -= 360f; // �͈͓���ۂ�
                yield return new WaitForSeconds(delayTime);
            }
            Vector2 randomPos = RandomPos(); // �����_���Ȉړ���̔r�o
            float limitTime = 0.5f; // �ړ��ɂ����鎞��
            float elapsedTime = 0f; // �ړ��ɂ�����������
            Vector2 startPosition = transform.position;
            // randomPos��limitTime�����Ĉړ�����
            while (elapsedTime < limitTime)
            {
                transform.position = new Vector2(
                    Mathf.Lerp(startPosition.x, randomPos.x, elapsedTime / limitTime),
                    Mathf.Lerp(startPosition.y, randomPos.y, elapsedTime / limitTime)
                    );
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return null;
        }
    }
    /// <summary>
    /// ��i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private IEnumerator FireSecondBullet()
    {
        // �~�`�̒e������]�����Ȃ���ł��܂�

        while (state == State.second && bulletState == BulletState.normal)
        {
            // �e�̉��Ԋu�̌v�Z
            float angleStep = 360f / FevolutionFlyingNum;
            float angle = FevolutionAngleOffset;
            for (int i = 0; i < FevolutionFlyingNum; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                GameObject proj = Instantiate(RevolutionBulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * FevolutionSpeed;

                angle += angleStep;

                Destroy(proj, FevolutionDeleteTime); // ���b��ɒe��������
            }
            FevolutionAngleOffset += 20f; // ������ς���Ή�]���x���ς��
            if (FevolutionAngleOffset >= 360) FevolutionAngleOffset -= 360f; // �͈͓���ۂ�
            yield return new WaitForSeconds(FevolutionDelayTime);
        }
        yield return null;
    }
    /// <summary>
    /// �O�i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private IEnumerator FireThirdBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
        yield return null;
    }
    /// <summary>
    /// �l�i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private IEnumerator FireFourBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
        yield return null;
    }
    /// <summary>
    /// �ŏI�i�K�̒ʏ�e���ł�
    /// </summary>
    private IEnumerator FireFinalBullet()
    {
        Debug.Log("�ʏ�e������: " + state);
        yield return null;
    }
    // LastWard
    private IEnumerator FireSpecialBullet()
    {
        float limitTime = 1f; // �ړ��ɂ����鎞��
        float elapsedTime = 0f; // �ړ��ɂ�����������
        Vector2 startPosition = transform.position;
        // randomPos��limitTime�����Ĉړ�����
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, spellPos.x, elapsedTime / limitTime),
                Mathf.Lerp(startPosition.y, spellPos.y, elapsedTime / limitTime)
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("����e������: " + state);
        yield return null;
    }

    /// <summary>
    /// �Ō�̑�Z���o���܂�
    /// </summary>
    private IEnumerator SpecialFinalBullet()
    {
        if (!isSpecialBulletActive)
        {
            isSpecialBulletActive = true;
            Debug.Log("Final���: ���ʂȒe���𔭎�");
            yield return null;
        }
    }

    /// <summary>
    /// �G�l�~�[�̏�ԉ񕜂�State�̍X�V�����܂�
    /// </summary>
    private IEnumerator TransitionToNextState()
    {
        if (state < State.final)
        {
            state++;
            bulletState = BulletState.normal; // �e���̕ύX
            damageLate = 1f;
            currentHP = maxHP; // HP����
            Debug.Log("State���ύX����܂���: " + state);
            yield return StartCoroutine(BulletUpdate());
        }
    }

    /// <summary>
    /// �G�l�~�[�Ƀ_���[�W��^���܂�
    /// </summary>
    /// <param name="damage">�^�_���[�W</param>
    private IEnumerator TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= maxHP * 0.2f && bulletState == BulletState.normal)
        {
            damageLate = 0.2f; // HP�̌����X�s�[�h�̕ύX
            bulletState = BulletState.spell; // �e���̕ύX
            yield return StartCoroutine(FireSpecialBullet());
        }
        else if (currentHP <= 0)
        {
            if (state == State.final)
            {
                StartCoroutine(SpecialFinalBullet());
            }
            else
            {
                StartCoroutine(TransitionToNextState());
            }
        }
    }
    /// <summary>
    /// �����_���ړ��̈ړ�����v�Z���܂�
    /// </summary>
    /// <returns>�ړ����Ԃ��܂�</returns>
    private Vector2 RandomPos()
    {
        return new Vector2(Random.Range(1.5f, 8.5f), Random.Range(-4.5f, 4.5f));
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            StartCoroutine(TakeDamage(attak * damageLate)); // �_���[�W�v�Z
            Destroy(collision.gameObject); // �e������
        }
    }
}
