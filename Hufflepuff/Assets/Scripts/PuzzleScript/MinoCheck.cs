// MinoCheck.cs
// 
// �s�[�X���ՖʊO��ق��̃s�[�X���d�Ȃ��Ă��鎞��PieceMoves�ɒl�𑗂�܂�
// 

using UnityEngine;

public class MinoCheck : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g

    void Start()
    {
        // ���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�𐶐������Ƃ��ɃI�u�W�F�N�g���擾����
        pieceMoves = Object.FindFirstObjectByType<PieceMoves>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pieceMoves.SetColliding(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pieceMoves.SetColliding(-1);
    }
}
