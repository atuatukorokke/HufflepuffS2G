using System.Security.Cryptography;
using UnityEngine;

public enum FiringType
{
    Circle, // �~�`�̒e����łG
    Following, // �ǔ��e��łG
    Winder // �T�C���g�̒e����łG
}


public class EnemyMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed; // �ړ��X�s�[�h
    [SerializeField] private float TimingFiring; // ���˃^�C�~���O
    [SerializeField] private FiringType firingType; // �e���̃^�C�v�ɂ���Ĉړ����@��ς���

    public Vector3 startPoint;
    public Vector3 targetPoint;
    public float height = 2.0f;
    public float duration = 2.0f;

    private float elapsedTime = 0f;


    private void Start()
    {
        startPoint = transform.position;
    }


    private void Update()
    {
        switch (firingType)
        {
            case FiringType.Circle:
                CurveMovement();
                break;
        }
    }
    private void CurveMovement()
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
    }
}
