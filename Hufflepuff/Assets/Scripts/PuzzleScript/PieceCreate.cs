// PieceCreate.cs
// 
// �X�N���v�g�Ăяo�������Ƀp�Y���s�[�X�������_�������i�p�Y���ɂ͕K�����Ă͂߂邱�Ƃ��\�j����
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("�e��s�[�X")]
    [SerializeField] public GameObject Imino;
    [SerializeField] public GameObject Jmino;
    [SerializeField] public GameObject Lmino;
    [SerializeField] public GameObject Omino;
    [SerializeField] public GameObject Smino;
    [SerializeField] public GameObject Tmino;
    [SerializeField] public GameObject Zmino;
    public void NewPiece()
    {
        // 1~7�̐����������_���Ő���
        int rndMino = Random.Range(1, 8);

        // �����ʒu
        Vector3 pos = new Vector3(6.5f, 0.5f, 0.0f);

        // �v���n�u���w��ʒu�ɐ���
        switch (rndMino)
        {
            case 1:
                Instantiate(Imino, pos, Quaternion.identity);
                break;
            case 2:
                Instantiate(Jmino, pos, Quaternion.identity);
                break;
            case 3:
                Instantiate(Lmino, pos, Quaternion.identity);
                break;
            case 4:
                Instantiate(Omino, pos, Quaternion.identity);
                break;
            case 5:
                Instantiate(Smino, pos, Quaternion.identity);
                break;
            case 6:
                Instantiate(Imino, pos, Quaternion.identity);
                break;
            case 7:
                Instantiate(Zmino, pos, Quaternion.identity);
                break;
            default:
                Debug.Log("�Ȃ񂩕ςȒl���o�Ă��Ł[");
                break;
        }
    }
}
