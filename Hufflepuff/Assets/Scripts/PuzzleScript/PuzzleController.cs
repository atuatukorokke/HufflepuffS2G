// PuzzleController.cs
// 
// �v���C���[����̓��͂��󂯎��A
// ���̃X�N���v�g�𔭉΂��܂��B
// 

using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        // ���L�[�Ńp�Y���s�[�X���ړ�
        if (Input.GetKey(KeyCode.RightArrow)) ;
        if (Input.GetKey(KeyCode.LeftArrow)) ;
        if (Input.GetKey(KeyCode.UpArrow)) ;
        if (Input.GetKey(KeyCode.DownArrow)) ;

        // z�L�[�Ńp�Y���s�[�X��ݒu
        if (Input.GetKeyDown(KeyCode.Z)) ;

        // x�L�[�Ńp�Y���s�[�X����]
        if (Input.GetKeyDown(KeyCode.X)) ;
    }
}
