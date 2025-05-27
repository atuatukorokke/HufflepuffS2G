// PuzzleController.cs
// 
// �v���C���[����̓��͂��󂯎��A
// ���̃p�Y���p�X�N���v�g�𔭉΂��܂��B
// 

using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    private PieceMoves Pmoves;  // �p�Y���s�[�X�𓮂����X�N���v�g���Ăяo���p

    private void Start()
    {
        Pmoves = FindAnyObjectByType<PieceMoves>();
    }

    void Update()
    {
        // ���L�[�Ńp�Y���s�[�X���ړ�
        if (Input.GetKey(KeyCode.RightArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.LeftArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.UpArrow)) Pmoves.PieceMove();
        if (Input.GetKey(KeyCode.DownArrow)) Pmoves.PieceMove();

        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z)) Debug.Log("z");

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X)) Pmoves.PieceRotation();
    }
}
