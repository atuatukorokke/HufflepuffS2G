// PieceMoves.cs
// 
// �p�Y���s�[�X�̓����𐧌�����܂�
// 

using System.Collections;
using UnityEngine;

public class PieceMoves : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    bool isMoving;
    Vector2 input;

    /// <summary>
    /// �e����L�[�ňړ����\�ɂ��܂�
    /// </summary>
    public void Move()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // ���͂���������
            if (input != Vector2.zero)
            {
                // ���͕���ǉ�
                Vector2 targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                StartCoroutine(Move(targetPos));
            }
        }
    }

    private IEnumerator Move(Vector3 inTargetPos)
    {
        isMoving = true;

        // targetPos�ƌ��݂�position�̍�������Ԃ́AMoveTowards��targetPos�ɋ߂�
        while ((inTargetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, inTargetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = inTargetPos;

        isMoving = false;
    }
}
