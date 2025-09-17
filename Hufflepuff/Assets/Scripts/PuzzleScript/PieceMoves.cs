// PieceMoves.cs
// 
// ���p�Y���s�[�X�̓����𐧌�����܂�
// �s�[�X���̏d�Ȃ���J�E���g���܂�
// 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PieceMoves : MonoBehaviour
{

    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g
    [SerializeField] private DestroyBlock destroyBlock; // �u���b�N�������X�N���v�g

    [Header("0�Ȃ�z�u�\")]
    [SerializeField] private int Colliding = 0;
    [Header("�z�u�s�̎���false, �z�u�\�Ȃ�true")]
    [SerializeField] private bool isPiesePossible = false;

    private void Start()
    {

    }

    /// <summary>
    /// �R���C�_�[���m�F���āA�d�Ȃ��Ă�������Colliding��true�ɂ���
    /// </summary>
    public void PiecePossible()
    {
        if (Colliding == 0)
        {
            Debug.Log("�z�u�\");
            isPiesePossible = true;
        }
        else
        {
            Debug.Log("�z�u�s��");
            isPiesePossible = false;
        }

    }

    public bool GetPiecePossible()
    {
        if (Colliding == 0)
        {
            Debug.Log("�z�u�\");
            isPiesePossible = true;
        }
        else
        {
            Debug.Log("�z�u�s��");
            isPiesePossible = false;
        }
        return isPiesePossible;
    }

    public void SetColliding(int newColliding)
    {
        Colliding = Colliding + newColliding;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("��������");
        Colliding = Colliding + 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Colliding = Colliding - 1;
    }
    */
}