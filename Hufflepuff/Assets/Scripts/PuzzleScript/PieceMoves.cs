// PieceMoves.cs
// 
// �p�Y���s�[�X�̓����𐧌�����܂�
// 

using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class PieceMoves : MonoBehaviour
{
    private PuzzleGrid Pgrid;   // �p�Y���s�[�X��z�u����X�N���v�g���Ăяo��
    private PieceCreate Pcreate;    // �p�Y���s�[�X�𐶐�����X�N���v�g

    private void Start()
    {
        Pgrid = FindAnyObjectByType<PuzzleGrid>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// PuzzleGrid���Ăяo���p�Y���s�[�X���z�u�\�����m�F���܂�
    /// </summary>
    public void PiecePossible(int r)
    {
        // gameObject���擾
        GameObject thisPiece = this.gameObject;

        bool banana;
        // PuzzleCheck ���Ăяo�����ƂŔz�u���Ă��悢������������
        banana = Pgrid.PuzzleCheck(thisPiece, transform.position.x - 3, transform.position.y - 2, r);
        // �z�u�ł���Ȃ�ړ��p�X�N���v�g���폜���Ĕz�u����
        if (banana)
        {
            var thisPieceScript = GetComponent<PieceMoves>();
            Destroy(thisPieceScript, 0.1f);
            BoxCollider2D thisPieceCollider = GetComponent<BoxCollider2D>();
            Destroy(thisPieceCollider, 0.1f);
        }
    }

    /// <summary>
    /// �Ăяo�����ƂŃA�^�b�`���ꂽ�I�u�W�F�N�g�𔽎��v����ɉ�]�����܂�
    /// </summary>
    public int PieceRotation(int r)
    {
        // gameObject���擾
        GameObject thisPiece = this.gameObject;     // Destroy����Ɛ̂�gameObject��ǂݍ��ށ@�Ȃ��H

        switch (thisPiece.tag)
        {
            case "mino1":
            case "mino4":
            case "mino9":
                Debug.Log("��]���Ȃ��s�[�X�ł�");
                r = 0;
                break;
            case "mino2":
            case "mino5":
            case "mino6":
                r = (r == 0) ? 1 : 0;
                Pcreate.PieceRotationCreate(thisPiece, r);
                Destroy(thisPiece, 0.1f);
                thisPiece = null;
                break;
            case "mino3":
                r = r == 0 ? 1 : r == 1 ? 2 : r == 2 ? 3 : 0; // 0,1,2,3��4��]
                Pcreate.PieceRotationCreate(thisPiece, r);
                Destroy(thisPiece, 0.1f);
                thisPiece = null;
                break;
            default:
                Debug.Log("�^�O���ǂݎ��Ă܂��� PieceRotation()");
                break;
        }

        return r;
    }
}