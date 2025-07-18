// PieceMoves.cs
// 
// �p�Y���s�[�X�̓����𐧌�����܂�
// 

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PieceMoves : MonoBehaviour
{
    [Header("�ړ����x")]
    [SerializeField] float moveSpeed;

    private PuzzleGrid Pgrid;   // �p�Y���s�[�X��z�u����X�N���v�g���Ăяo���p

    private int CountRotate = 0;
    /*
    bool isMoving;
    Vector2 input;
    */

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
    }

    // �ߋ��̃s�[�X�̈ړ��X�N���v�g
    /*
    /// <summary>
    /// �e����L�[�ňړ����\�ɂ��܂�
    /// </summary>
    public void PieceMove()
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
                // �ړ��ꏊ���͈͊O�Ȃ�Έړ��������Ȃ�if��
                if (Mathf.Pow(targetPos.x, 2) <= 64 && Mathf.Pow(targetPos.y, 2) <= 16)
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    /// <summary>
    /// �ړ�������X�N���v�g
    /// �}�X�ڂŎ~�܂�悤�ɐ���ł��܂�
    /// </summary>
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
    */

    /// <summary>
    /// PuzzleGrid���Ăяo���p�Y���s�[�X���z�u�\�����m�F���܂�
    /// </summary>
    public void PiecePossible()
    {
        bool banana;
        // PuzzleCheck ���Ăяo�����ƂŔz�u���Ă��悢������������
        banana = Pgrid.PuzzleCheck(gameObject, transform.position.x - 3, transform.position.y - 2, CountRotate);
        // �z�u�ł���Ȃ�ړ��p�X�N���v�g���폜���Ĕz�u����
        if (banana)
        {
            var thisPieceScript = GetComponent<PieceMoves>();
            Destroy(thisPieceScript);
            BoxCollider2D thisPieceCollider = GetComponent<BoxCollider2D>();
            Destroy(thisPieceCollider);
        }
    }

    /// <summary>
    /// �Ăяo�����ƂŃA�^�b�`���ꂽ�I�u�W�F�N�g�𔽎��v����ɉ�]�����܂�
    /// </summary>
    public void PieceRotation()
    {
        // �K�v�Œ�J�E���g����ݒ�
        switch (gameObject.tag)
        {
            // ��]����
            case "1mino":
            case "4mino":
            case "9mino":
                CountRotate = 0;
                break;
            // ��]�Q��
            case "2mino":
            case "5mino":
            case "6mino":
                CountRotate = (CountRotate + 1) % 2;
                break;
            // ��]�S��
            case "3mino":
                CountRotate = (CountRotate + 1) % 4;
                break;
            default:
                Debug.Log("�ςȃ^�O��ǂݎ���Ă܂�");
                break;

        }

        int test = CountRotate * -90;
        Debug.Log(CountRotate);
        transform.localRotation = Quaternion.Euler(0, 0, test);
    }
}
