using UnityEngine;

public enum FiringType
{
    Circle, // �~�`�̒e����łG
    Following, // �ǔ��e��łG
    Winder // �T�C���g�̒e����łG
}

public class EnemyMove : MonoBehaviour
{
    [Header("�S�ړ����@���ʕϐ�")]
    [SerializeField] private float moveSpeed; // �ړ��X�s�[�h
    [SerializeField] private float TimingFiring; // ���˃^�C�~���O
    [SerializeField] private FiringType firingType; // �e���̃^�C�v�ɂ���Ĉړ����@��ς���

    [Header("�������ړ��p�ϐ�")]
    [SerializeField] private Vector3 baseTargetPoint; // ��{�̖ړI�n
    private Vector3 targetPoint; // ������̖ړI�n
    [SerializeField] private float height = 2.0f;
    [SerializeField] private float duration = 2.0f;
    private float elapsedTime = 0f;
    private bool slowMovePhase = false;

    [Header("�����ړ��p�ϐ�")]
    [SerializeField] private float decelerateFactor = 0.98f; // ������
    [SerializeField] private float slowMoveDuration = 2f; // �������ړ��̎���
    [SerializeField] private float exitSpeed = 10f; // ��ʊO�ֈړ����鑬�x
    private bool exitPhase = false; // ��ʊO�ړ��J�n�t���O

    private Vector3 startPoint;
    private Vector3 velocity;
    private float positionOffset; // �ړI�n�����炷�I�t�Z�b�g

    private void Start()
    {
        startPoint = transform.position;

        // �e�I�u�W�F�N�g��Y���W����Ƀ����_���ȃI�t�Z�b�g��ݒ�
        positionOffset = Mathf.Sin(transform.position.y) * Random.Range(-1f, 1f);
        targetPoint = baseTargetPoint + new Vector3(positionOffset, 0, 0);

        // ��ʊO���猸�����Ȃ���ړI�n�֌������������x�ݒ�
        velocity = new Vector3(-moveSpeed, 0, 0);
    }

    private void Update()
    {
        switch (firingType)
        {
            case FiringType.Circle:
                CurveMovement();
                break;
            case FiringType.Following:
                DecelerateMove();
                break;
        }
    }

    private void CurveMovement()
    {
        if (!slowMovePhase)
        {
            if (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                float linearT = elapsedTime / duration;
                float easedT = 1 - Mathf.Pow(1 - linearT, 2);

                Vector3 horizontalPos = Vector3.Lerp(startPoint, targetPoint, easedT);
                float heightOffset = 4 * height * easedT * (1 - easedT);

                transform.position = new Vector3(horizontalPos.x, Mathf.Lerp(startPoint.y, targetPoint.y, easedT) + heightOffset, horizontalPos.z);
            }
            else
            {
                slowMovePhase = true;
                elapsedTime = 0f;
            }
        }
        else
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime < slowMoveDuration)
            {
                transform.position += new Vector3(-0.5f * Time.deltaTime, 0, 0);
            }
            else
            {
                exitPhase = true;
            }
        }

        if (exitPhase)
        {
            MoveOutOfScreen();
        }
    }

    private void DecelerateMove()
    {
        transform.position += velocity * Time.deltaTime;
        velocity *= decelerateFactor;

        if (velocity.magnitude < 1f)
        {
            firingType = FiringType.Circle;
            Start();
        }
    }

    private void MoveOutOfScreen()
    {
        float exitDirectionY = transform.position.y >= 0 ? 1f : -1f;
        transform.position += new Vector3(exitSpeed * Time.deltaTime, exitSpeed * exitDirectionY * Time.deltaTime, 0);
    }
}
