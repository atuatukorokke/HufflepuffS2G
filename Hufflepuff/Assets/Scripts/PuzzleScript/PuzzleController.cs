// PuzzleController.cs
// 
// �v���C���[����̓��͂��󂯎��A
// ���̃p�Y���p�X�N���v�g�𔭉΂��܂��B
// 

using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PieceMoves Pmove;  // �p�Y���s�[�X�𓮂����X�N���v�g���Ăяo���p

    private void Start()
    {
        Pmove = FindAnyObjectByType<PieceMoves>();
    }

    void Update()
    {
        // ���L�[�Ńp�Y���s�[�X���ړ�
        if (Input.GetKey(KeyCode.RightArrow)) Pmove.Move();
        if (Input.GetKey(KeyCode.LeftArrow)) Pmove.Move();
        if (Input.GetKey(KeyCode.UpArrow)) Pmove.Move();
        if (Input.GetKey(KeyCode.DownArrow)) Pmove.Move();

        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z)) Debug.Log("z");

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X)) Debug.Log("x");
    }
}
