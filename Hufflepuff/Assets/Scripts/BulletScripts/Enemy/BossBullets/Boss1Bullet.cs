// Boss1Bulletrs.cs
//
// �{�X�̒e���𐶐�����
// �ړ��̍ۂ͉�ʂ̍������͓���Ȃ�
//

using System.Collections;
using UnityEngine;

// ��i�K�ڂ̒ʏ�e���̕ϐ�
[System.Serializable]
public class FastBullet
{
    [SerializeField] public GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] public int FlyingNum; // ���˂��鐔
    [SerializeField] public int frequency; // ���ˉ�
    [SerializeField] public float speed; // �e���̃X�s�[�h
    [SerializeField] public float DeleteTime; // �폜���鎞��
    [SerializeField] public float delayTime; // �e�����o���Ԋu
    public float angleOffset = 0f; // ���炵�p�̊p�x
    [SerializeField] public float moveSpeed;
}

// ��i�K�ڂ̒ʏ�e���̕ϐ�
[System.Serializable] 
public class SecondBullet
{
    [SerializeField] public GameObject RevolutionBulletPrehab; // �e���̃v���n�u
    [SerializeField] public int FevolutionFlyingNum; // ���˂��鐔
    [SerializeField] public int FevolutionFrequency; // ���ˉ�
    [SerializeField] public float FevolutionSpeed; // �e���̃X�s�[�h
    [SerializeField] public float FevolutionDeleteTime; // �폜���鎞��
    [SerializeField] public float FevolutionDelayTime; // �e�����o���Ԋu
    public float FevolutionAngleOffset = 0;
}

// �O�i�K�ڂ̒ʏ�e���̕ϐ�
[System.Serializable] 
public class ThirdBullet
{
    [SerializeField] public GameObject RotationBulletPrehab; // �e���̃v���n�u
    [SerializeField] public int RotationFlyingNum; // ���˂��鐔
    [SerializeField] public int RotationFrequency; // ���ˉ�
    [SerializeField] public float RotationSpeed; // �e���̃X�s�[�h
    [SerializeField] public float RotationDeleteTime; // �폜���鎞��
    [SerializeField] public float RotationDelayTime; // �e�����o���Ԋu
    public float RotationAngleOffset = 0;
}

// �l�i�K�ڂ̒ʏ�e���̕ϐ�
[System.Serializable]
public class FourBullet
{
    [SerializeField] private GameObject Prehab;
}


//�G�̒e���̏��
public enum BulletState
{
    normal,
    spell
}

// �G�������i�K�ڂ�
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

    [Header("��i�K�ڂ̒ʏ�e���̕ϐ�")]
    [SerializeField] private FastBullet fastBulletValue;

    [Header("��i�K�ڂ̒ʏ�e���̕ϐ�")]
    [SerializeField] private SecondBullet secondBulletValue;

    [Header("�O�i�K�ڂ̒ʏ�e���̕ϐ�")]
    [SerializeField] private ThirdBullet thirdBulletValue;

    [Header("�l�i�K�ڂ̒ʏ�e���̕ϐ�")]
    [SerializeField] private FourBullet FourBulletValue;

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
            float angleStep = 360f / fastBulletValue.FlyingNum;
            float angle = fastBulletValue.angleOffset;

            // frequency�̉񐔂����e���𐶐�����
            // FlyingNum�͈��̐����ŉ��e�������o����
            for (int i = 0; i < fastBulletValue.frequency; i++)
            {
                for (int j = 0; j < fastBulletValue.FlyingNum; j++)
                {
                    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0);

                    GameObject proj = Instantiate(fastBulletValue.BulletPrehab, transform.position, Quaternion.identity);
                    Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                    rb.linearVelocity = moveDirection.normalized * fastBulletValue.speed;

                    angle += angleStep;

                    Destroy(proj, fastBulletValue.DeleteTime); // ���b��ɒe��������

                }
                fastBulletValue.angleOffset += 10f; // ������ς���Ή�]���x���ς��
                if (fastBulletValue.angleOffset >= 360) fastBulletValue.angleOffset -= 360f; // �͈͓���ۂ�
                yield return new WaitForSeconds(fastBulletValue.delayTime);
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
            // �e�̉��Ԋu�̌v�Z�i1��360�x���w��̒e���ŋϓ��ɕ����j
            float angleStep = 360f / secondBulletValue.FevolutionFlyingNum;
            float angle = secondBulletValue.FevolutionAngleOffset;

            // �w�肳�ꂽ�e�������[�v
            for (int i = 0; i < secondBulletValue.FevolutionFlyingNum; i++)
            {
                // ���˕����ix, y���W�j���v�Z
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad); // X�����̑��x������
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad); // Y�����̑��x������
                Vector3 moveDirection = new Vector3(dirX, dirY, 0); // �e�̈ړ��������쐬

                // �e�𐶐��i�v���n�u�����ɃC���X�^���X���j
                GameObject proj = Instantiate(secondBulletValue.RevolutionBulletPrehab, transform.position, Quaternion.identity);

                // �e�� Rigidbody2D �R���|�[�l���g���擾���A���x��ݒ�
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * secondBulletValue.FevolutionSpeed; // ���x�𐳋K�����ēK�p

                // ���̒e�̔��˕�����ݒ�
                angle += angleStep;

                // ��莞�Ԍ�ɒe���폜
                Destroy(proj, secondBulletValue.FevolutionDeleteTime);
            }

            // �e�̉�]�p�x���X�V�i��]���x�𒲐��j
            secondBulletValue.FevolutionAngleOffset += 20f;
            if (secondBulletValue.FevolutionAngleOffset >= 360) secondBulletValue.FevolutionAngleOffset -= 360f; // 360�x�𒴂��Ȃ��悤����

            // ��莞�ԑҋ@
            yield return new WaitForSeconds(secondBulletValue.FevolutionDelayTime);
        }

        // �ŏI�I�ɏ������I��
        yield return null;

    }
    /// <summary>
    /// �O�i�K�ڂ̒ʏ�e���ł�
    /// </summary>
    private IEnumerator FireThirdBullet()
    {
        while(state == State.third && bulletState == BulletState.normal)
        {
            // �e�̉��Ԋu�̌v�Z�i360�x���w��̒e���ŋϓ��ɕ����j
            float angleStep = 360f / thirdBulletValue.RotationFlyingNum;
            float angle = thirdBulletValue.RotationAngleOffset;

            for (int i = 0; i < thirdBulletValue.RotationFlyingNum; i++)
            {
                // �e�̔��˕����ix, y���W�j���v�Z
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad); // X�����̑��x������
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad); // Y�����̑��x������
                Vector3 moveDirection = new Vector3(dirX, dirY, 0); // �e�̈ړ��������쐬

                // �e�𐶐��i�v���n�u�����ɃC���X�^���X���j
                GameObject proj = Instantiate(thirdBulletValue.RotationBulletPrehab, transform.position, Quaternion.identity);

                // �e�� Rigidbody2D �R���|�[�l���g���擾���A���x��ݒ�
                Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection.normalized * thirdBulletValue.RotationSpeed; // ���x�𐳋K�����ēK�p

                // ���̒e�̔��˕�����ݒ�
                angle += angleStep;

                // ��莞�Ԍ�ɒe���폜
                Destroy(proj, thirdBulletValue.RotationDeleteTime);
            }

            // �e�̉�]�p�x���X�V�i��]���x�𒲐��j
            thirdBulletValue.RotationAngleOffset += 10f;
            if (thirdBulletValue.RotationAngleOffset >= 360) thirdBulletValue.RotationAngleOffset -= 360f; // 360�x�𒴂��Ȃ��悤����

            // ��莞�ԑҋ@
            yield return new WaitForSeconds(thirdBulletValue.RotationDelayTime);

        }
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
    /// <summary>
    /// �v���C���[�̒e���ɓ��������ۂɍ쓮���܂�
    /// </summary>
    /// <param name="collision">�v���C���[�̒e���̃^�O�̖��O�ł��B</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P_Bullet"))
        {
            StartCoroutine(TakeDamage(attak * damageLate)); // �_���[�W�v�Z
            Destroy(collision.gameObject); // �e������
        }
    }
}
