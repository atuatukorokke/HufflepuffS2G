// PieceCreate.cs
// 
// �X�N���v�g�Ăяo�������Ƀp�Y���s�[�X�������_�������i�p�Y���ɂ͕K�����Ă͂߂邱�Ƃ��\�j����
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("�e��s�[�X")]
    [SerializeField] public GameObject mino1;
    [SerializeField] public GameObject mino2_1;
    [SerializeField] public GameObject mino2_2;
    [SerializeField] public GameObject mino3_1;
    [SerializeField] public GameObject mino3_2;
    [SerializeField] public GameObject mino3_3;
    [SerializeField] public GameObject mino3_4;
    [SerializeField] public GameObject mino4;
    [SerializeField] public GameObject mino5_1;
    [SerializeField] public GameObject mino5_2;
    [SerializeField] public GameObject mino6_1;
    [SerializeField] public GameObject mino6_2;
    [SerializeField] public GameObject mino9;

    public void NewPiece()
    {
        // 1~7�̐����������_���Ő���
        //int rndMino = Random.Range(1, 8);
        int rndMino = 3; // �f�o�b�O�p��2���Œ�

        // �����ʒu
        Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);

        // �v���n�u���w��ʒu�ɐ���
        switch (rndMino)
        {
            case 1:
                Instantiate(mino1, pos, Quaternion.identity);
                break;
            case 2:
                Instantiate(mino2_1, pos, Quaternion.identity);
                break;
            case 3:
                Instantiate(mino3_1, pos, Quaternion.identity);
                break;
            case 4:
                Instantiate(mino4, pos, Quaternion.identity);
                break;
            case 5:
                Instantiate(mino5_1, pos, Quaternion.identity);
                break;
            case 6:
                Instantiate(mino6_1, pos, Quaternion.identity);
                break;
            case 7:
                Instantiate(mino9, pos, Quaternion.identity);
                break;
            default:
                Debug.Log("�Ȃ񂩕ςȒl���o�Ă��Ł[");
                break;
        }
    }

    /// <summary>
    /// �s�[�X�̉�]���s���܂�
    /// </summary>
    /// <param name="inGameObject">�Ώۂ̃Q�[���I�u�W�F�N�g�����Ă�������</param>
    /// <param name="r">�����l����̉�]�������Ă�������</param>
    public void PieceRotationCreate(GameObject inGameObject, int r)
    {
        Debug.Log(r);
        // �K�v�Œ�J�E���g����ݒ�
        switch (inGameObject.tag)
        {
            // ��]�Q��
            case "mino2":
                switch (r)
                {
                    case 0:
                        Instantiate(mino2_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino2_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            case "mino5":
                switch (r)
                {
                    case 0:
                        Instantiate(mino5_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino5_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            case "mino6":
                switch (r)
                {
                    case 0:
                        Instantiate(mino6_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino6_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            // ��]�S��
            case "mino3":
                switch (r)
                {
                    case 0:
                        Instantiate(mino3_1, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(mino3_2, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(mino3_3, inGameObject.transform.position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(mino3_4, inGameObject.transform.position, Quaternion.identity);
                        break;
                }
                break;
            default:
                Debug.Log("�ςȃ^�Oor��]������Ȃ��^�O��ǂݎ���Ă܂�");
                break;
        }
    }
}
