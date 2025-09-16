// PieceCreate.cs
// 
// �X�N���v�g�Ăяo�������Ƀp�Y���s�[�X�������_�������i�p�Y���ɂ͕K�����Ă͂߂邱�Ƃ��\�j����
// 

using UnityEngine;

public class PieceCreate : MonoBehaviour
{
    [Header("�e��s�[�X")]
    [SerializeField] public GameObject mino1;
    [SerializeField] public GameObject mino2;
    [SerializeField] public GameObject mino3;
    [SerializeField] public GameObject mino4;
    [SerializeField] public GameObject mino5;
    [SerializeField] public GameObject mino6;
    [SerializeField] public GameObject mino9;

    private bool isCreate = false;
    private int pieceNumber;
    private BuffForID buffID;
    private float buffValue;

    public bool IsCreate { get => isCreate; set => isCreate = value; }
    public BuffForID BuffID { get => buffID; private set => buffID = value; }
    public float BuffValue { get => buffValue; private set => buffValue = value; }

    /// <summary>
    /// �V�����s�[�X�𐶐����܂�
    /// </summary>
    /// <param name="pieceNumber">0 = �����_������, 1 ~ 7 = �Ή������s�[�X�𐶐�</param>
    public void NewPiece(int pieceNumber)
    { 
        this.pieceNumber = pieceNumber;
        // ���͂��O�̎�1~7�̐����������_���Ő���
        if (pieceNumber == 0)
        {
            //int rndMino = Random.Range(1, 8);
            pieceNumber = 3; // �f�o�b�O�p��2���Œ�
        }

        // �����ʒu
        Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

        if(!IsCreate)
        {
            IsCreate = true;
            // �v���n�u���w��ʒu�ɐ���
            switch (pieceNumber)
            {
                case 1:
                    Instantiate(mino1, pos, Quaternion.identity);
                    break;
                case 2:
                    Instantiate(mino2, pos, Quaternion.identity);
                    break;
                case 3:
                    Instantiate(mino3, pos, Quaternion.identity);
                    break;
                case 4:
                    Instantiate(mino4, pos, Quaternion.identity);
                    break;
                case 5:
                    Instantiate(mino5, pos, Quaternion.identity);
                    break;
                case 6:
                    Instantiate(mino6, pos, Quaternion.identity);
                    break;
                case 7:
                    Instantiate(mino9, pos, Quaternion.identity);
                    break;
                default:
                    break;
            }
        }
        
    }

    public void PieceAddBuff(BuffForID buffForID)
    {
        if(IsCreate)
        {
            BuffID = buffForID;
            BuffValue = pieceNumber * 0.1f; // �f�o�b�O�p�Ƀs�[�X�ԍ���10%���o�t�l�ɂ���
        }
    }
}
