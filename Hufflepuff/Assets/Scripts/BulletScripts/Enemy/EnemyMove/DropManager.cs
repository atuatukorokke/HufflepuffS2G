// DropManager.cs
//
// ���𗎂Ƃ��m�����Ǘ����܂�
//

using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private int dropLate = 10; // ���𗎂Ƃ��m��

    public int DropLate { get => dropLate; set => dropLate = value; }

    /// <summary>
    /// �h���b�v�m���̍X�V�����܂�
    /// </summary>
    /// <returns>�V�����m���ł�</returns>
    public int LateChange()
    {
        DropLate--;
        return DropLate;
    }

    /// <summary>
    /// �h���b�v�m���̃��Z�b�g�����܂�
    /// </summary>
    /// <returns>�V�����m���ł�</returns>
    public int LateReset()
    {
        DropLate = 10;
        return DropLate;
    }
}
