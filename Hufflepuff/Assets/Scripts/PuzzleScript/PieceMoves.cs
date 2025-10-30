// PieceMoves.cs
// 
// ���p�Y���s�[�X�̓����𐧌�����܂�
// �s�[�X���̏d�Ȃ���J�E���g���܂�
// 

using UnityEngine;

public class PieceMoves : MonoBehaviour
{

    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g
    [SerializeField] private PieceCreate pieceCreate; // �s�[�X�𐶐�����X�N���v�g
    [SerializeField] private DestroyBlock destroyBlock; // �u���b�N�������X�N���v�g
    private AudioSource audio;
    [SerializeField] private AudioClip putSE;
    [SerializeField] private AudioClip removeSE;

    [Header("0�Ȃ�z�u�\")]
    [SerializeField] private int Colliding = 0;
    [Header("�z�u�s�̎���false, �z�u�\�Ȃ�true")]
    [SerializeField] private bool isPiesePossible = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        pieceCreate = FindAnyObjectByType<PieceCreate>();
    }

    /// <summary>
    /// �R���C�_�[���m�F���āA�d�Ȃ��Ă�������Colliding��true�ɂ���
    /// </summary>
    public void PiecePossible()
    {
        if (Colliding == 0)
        {
            audio.PlayOneShot(putSE);
            pieceCreate.isBlockCreate = true;
            isPiesePossible = true;
        }
        else
        {
            audio.PlayOneShot(removeSE);
            isPiesePossible = false;
        }

    }

    public bool GetPiecePossible()
    {
        if (Colliding == 0)
        {
            audio.PlayOneShot(putSE);
            isPiesePossible = true;
        }
        else
        {
            audio.PlayOneShot(removeSE);
            isPiesePossible = false;
        }
        return isPiesePossible;
    }

    public void SetColliding(int newColliding)
    {
        Colliding = Colliding + newColliding;
    }
}