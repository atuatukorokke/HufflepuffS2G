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

    [SerializeField] public GameObject block;

    [Header("�X�N���v�g���A�^�b�`")]
    [SerializeField] private DeathCount deathCount;     // ���ʂ��̔�����s���X�N���v�g
    [SerializeField] private GoldManager goldManager;     // ���z�Ǘ����s���X�N���v�g
    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g
    [SerializeField] private DestroyBlock destroyBlock; // �u���b�N�������X�N���v�g
    [SerializeField] private PlayrController playerController; // �v���C���[�̃R���g���[���[

    [Header("�u���b�N��u���邩�̔���")]
    public bool isBlockCreate = true; 

    private int[,] BlockBoard = new int[5, 5]
    {{1, 0, 0, 0, 1},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {1, 0, 0, 0, 1}};

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayrController>();
    }

    /// <summary>
    /// �V�����s�[�X�𐶐����܂�
    /// </summary>
    /// <param name="x">0 = �����_������, 1 ~ 7 = �Ή������s�[�X�𐶐�</param>
    public void NewPiece(int x)
    {
        int rndMino = x;
        // ���͂��O�̎�1~7�̐����������_���Ő���
        if (x == 0)
        {
            //int rndMino = Random.Range(1, 8);
            rndMino = 3; // �f�o�b�O�p��2���Œ�
        }

        // �����ʒu
        Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

        if(isBlockCreate)
        {
            isBlockCreate = false;
            // �v���n�u���w��ʒu�ɐ���
            switch (rndMino)
            {
                case 1:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino1, pos, Quaternion.identity);
                        deathCount.SetPieceCount(1);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                case 2:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino2, pos, Quaternion.identity);
                        deathCount.SetPieceCount(2);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                case 3:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino3, pos, Quaternion.identity);
                        deathCount.SetPieceCount(3);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                case 4:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino4, pos, Quaternion.identity);
                        deathCount.SetPieceCount(4);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                case 5:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino5, pos, Quaternion.identity);
                        deathCount.SetPieceCount(5);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                case 6:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino6, pos, Quaternion.identity);
                        deathCount.SetPieceCount(6);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                case 7:
                    if (goldManager.GetGold() >= 10)
                    {
                        Debug.Log("�w���ł��܂���");
                        goldManager.SetGoldCount(-10);
                        Instantiate(mino9, pos, Quaternion.identity);
                        deathCount.SetPieceCount(9);
                    }
                    else
                    {
                        Debug.Log("��������܂���");
                    }
                    break;
                default:
                    Debug.Log("�Ȃ񂩕ςȒl���o�Ă��Ł[");
                    break;
            }
        }
    }

    /// <summary>
    /// �����Ńs�[�X���쐬���܂�
    /// </summary>
    public void PresentBox()
    {
        if(playerController.PieceCount > 0 && isBlockCreate)
        {
            isBlockCreate = false;
            playerController.PieceCount -= 1;
            int rndMino = Random.Range(1, 8);

            // �����ʒu
            Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

            // �v���n�u���w��ʒu�ɐ���
            switch (rndMino)
            {
                case 1:
                    Instantiate(mino1, pos, Quaternion.identity);
                    deathCount.SetPieceCount(1);
                    break;
                case 2:
                    Instantiate(mino2, pos, Quaternion.identity);
                    deathCount.SetPieceCount(2);
                    break;
                case 3:
                    Instantiate(mino3, pos, Quaternion.identity);
                    deathCount.SetPieceCount(3);
                    break;
                case 4:
                    Instantiate(mino4, pos, Quaternion.identity);
                    deathCount.SetPieceCount(4);
                    break;
                case 5:
                    Instantiate(mino5, pos, Quaternion.identity);
                    deathCount.SetPieceCount(5);
                    break;
                case 6:
                    Instantiate(mino6, pos, Quaternion.identity);
                    deathCount.SetPieceCount(6);
                    break;
                case 7:
                    Instantiate(mino9, pos, Quaternion.identity);
                    deathCount.SetPieceCount(9);
                    break;
                default:
                    break;
            }
        }
        
    }

    // ���ז��u���b�N�𐶐�(�Ăяo���悤�ɕύX)
    public void BlockCreate()
    {
        bool isBlock = false;

        int BlockRndX = 0;
        int BlockRndY = 0;

        while (isBlock == false)
        {
            isBlock = true;

            BlockRndX = Random.Range(0, 5);
            BlockRndY = Random.Range(0, 5);

            if (BlockBoard[BlockRndY, BlockRndX] == 1)
            {
                isBlock = false;
            }

            if (BlockRndX == 0 && BlockRndY == 0)
            {
                isBlock = false;
            }

            if (BlockRndX == 0 && BlockRndY == 4)
            {
                isBlock = false;
            }

            if (BlockRndX == 4 && BlockRndY == 0)
            {
                isBlock = false;
            }

            if (BlockRndX == 4 && BlockRndY == 4)
            {
                isBlock = false;
            }
        }

        Debug.Log(BlockRndX);
        Debug.Log(BlockRndY);

        Debug.Log(BlockBoard[BlockRndY, BlockRndX]);

        BlockBoard[BlockRndY, BlockRndX] = 1;

        // �����ʒu �Ֆʂ̍�����w��
        Vector3 pos = new Vector3(-28.0f - BlockRndX, -7.0f - BlockRndY, 0.0f);
        Instantiate(block, pos, Quaternion.identity);

        deathCount.SetBlockCount(1);

        if (pieceMoves.GetPiecePossible())
        {
            destroyBlock.DestroyPieceBlock();
        }
    }

    // ���ז��u���b�N�̔z�u����������
    public void BlockBoardInitialize()
    {
        BlockBoard = new int[5, 5]
        {{1, 0, 0, 0, 1},
         {0, 0, 0, 0, 0},
         {0, 0, 0, 0, 0},
         {0, 0, 0, 0, 0},
         {1, 0, 0, 0, 1}};
    }
}