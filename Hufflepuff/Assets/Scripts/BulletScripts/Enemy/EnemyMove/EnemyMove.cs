using UnityEngine;

public enum FiringType
{
    Circle, // �~�`�̒e����łG�i�������ړ��j
    Following, // �ǔ��e��łG�i�����ړ��j
    Winder // �T�C���g�̒e����łG�i�������j
}

public class EnemyMove : MonoBehaviour
{
    [Header("�S�ړ����@���ʕϐ�")]
    [SerializeField] private float moveSpeed; // �ړ��X�s�[�h
    [SerializeField] private float TimingFiring; // ���˃^�C�~���O�i���g�p�j
    [SerializeField] private FiringType firingType; // �ړ��^�C�v�ɂ���ċ�����ς���

    [Header("�������ړ��p�ϐ�")]
    [SerializeField] private Vector3 baseTargetPoint; // ��{�̖ړI�n�i�ړ���j
    private Vector3 targetPoint; // �e�I�u�W�F�N�g���Ƃɒ������ꂽ�ړI�n
    [SerializeField] private float height = 2.0f; // �������̍���
    [SerializeField] private float duration = 2.0f; // �������ړ��ɂ����鎞��
    private float elapsedTime = 0f; // �o�ߎ��Ԃ̊Ǘ�
    private bool slowMovePhase = false; // �������ړ���̂������ړ��t���O

    [Header("�����ړ��p�ϐ�")]
    [SerializeField] private float decelerateFactor = 0.98f; // �������i0.98 = ���t���[�����x��98%�Ɂj
    [SerializeField] private float slowMoveDuration = 2f; // �������ړ���̂������ړ�����
    [SerializeField] private float exitSpeed = 10f; // ��ʊO�֔�ԑ��x
    private bool waitBeforeExit = false; // ��ʊO�ړ��̑ҋ@�t�F�[�Y
    private bool exitPhase = false; // ��ʊO�ړ��J�n�t���O
    [SerializeField] private float waitTimeBeforeExit = 3f; // ��ʊO�ֈړ�����܂ł̑ҋ@����

    private Vector3 startPoint; // �ړ��J�n�n�_
    private Vector3 velocity; // �����ړ��̑��x�x�N�g��
    private float positionOffset; // �ړI�n�������_���ɂ��炷�I�t�Z�b�g

    private void Start()
    {
        startPoint = transform.position;

        // �e�I�u�W�F�N�g��Y���W���l�����A�ړI�n�������_���ɂ��炷
        positionOffset = Mathf.Sin(transform.position.y) * Random.Range(-1f, 1f);
        targetPoint = baseTargetPoint + new Vector3(positionOffset, 0, 0);

        // ��ʊO���猸�����Ȃ���ړ����邽�߂̏������x�ݒ�
        velocity = new Vector3(-moveSpeed, 0, 0);
    }

    private void Update()
    {
        // firingType �ɉ������ړ����������s
        switch (firingType)
        {
            case FiringType.Circle:
                CurveMovement(); // �������ړ�
                break;
            case FiringType.Following:
                DecelerateMove(); // �����ړ�
                break;
        }

        // �w���y���W�ŃG�l�~�[�폜
        if(transform.position.y < -30 || transform.position.y > 30)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// �������ړ��i�ړI�n�փW�����v����悤�ȓ����j
    /// </summary>
    private void CurveMovement()
    {
        if (!slowMovePhase)
        {
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                // �C�[�W���O���g���ăX���[�Y�ɕ������ړ�
                float linearT = elapsedTime / duration;
                float easedT = 1 - Mathf.Pow(1 - linearT, 2);

                // X���W�̈ړ�
                Vector3 horizontalPos = Vector3.Lerp(startPoint, targetPoint, easedT);

                // Y���W�ɕ������̍����I�t�Z�b�g��ǉ�
                float heightOffset = 4 * height * easedT * (1 - easedT);
                transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(startPoint.y, targetPoint.y, easedT) + heightOffset, horizontalPos.z);
            }
            else
            {
                // �������ړ�������������A�������ړ��t�F�[�Y��
                slowMovePhase = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime < slowMoveDuration)
            {
                // ���b�Ԃ�����荶�Ɉړ�
                transform.position += new Vector3(-0.5f * Time.deltaTime, 0, 0);
            }
            else
            {
                // ��ʊO�ֈړ��J�n
                waitBeforeExit = true;
                exitPhase = true;
            }
        }
        if (waitBeforeExit)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= waitTimeBeforeExit)
            {
                exitPhase = true;
            }
        }

        if (exitPhase)
        {
            MoveOutOfScreen();
        }
    }

    /// <summary>
    /// ��ʊO���猸�����Ȃ���ړI�n�֌������ړ�
    /// </summary>
    private void DecelerateMove()
    {
        transform.position += velocity * Time.deltaTime;
        velocity *= decelerateFactor; // ���X�Ɍ���

        if (velocity.magnitude < 1f)
        {
            // firingType������Following�Ȃ�ύX���Ȃ�
            if (firingType == FiringType.Following)
            {
                return;
            }
            else
            {
                // �����łȂ���΁ACircle�֑J��
                firingType = FiringType.Circle;
                Start();
            }
        }
    }

    /// <summary>
    /// ��ʊO�ֈړ��iY���W�ɂ��ړ���������j
    /// </summary>
    private void MoveOutOfScreen()
    {
        float exitDirectionY = transform.position.y >= 0 ? 1f : -1f;
        transform.position += new Vector3(exitSpeed * Time.deltaTime, exitSpeed * exitDirectionY * Time.deltaTime, 0);
    }
}
