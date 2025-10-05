// PieceCreate.cs
// 
// �X�N���v�g�Ăяo�������Ƀp�Y���s�[�X�������_�������i�p�Y���ɂ͕K�����Ă͂߂邱�Ƃ��\�j����
// 

using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

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

    private AudioSource audio;
    [SerializeField] private AudioClip blockCreateSE;
    [SerializeField] private AudioClip notBlockCreateSE;

    [SerializeField] private TextMeshProUGUI goldText;

    [Header("�u���b�N��u���邩�̔���")]
    public bool isBlockCreate = true; 

    public int[,] BlockBoard = new int[5, 5]
    {{1, 0, 0, 0, 1},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {0, 0, 0, 0, 0},
     {1, 0, 0, 0, 1}};

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        playerController = FindAnyObjectByType<PlayrController>();
        goldText.text = $"�c��̃R�C��:<color=#ffd700>{goldManager.GetGold().ToString()}</color>";
    }

    /// <summary>
    /// �V�����s�[�X�𐶐����܂�
    /// </summary>
    /// <param name="pieceNumber">0 = �����_������, 1 ~ 7 = �Ή������s�[�X�𐶐�</param>
    public void NewPiece(int pieceNumber, int buyLate)
    {
        int rndMino = pieceNumber;
        // ���͂��O�̎�1~7�̐����������_���Ő���
        if (pieceNumber == 0)
        {
            //int rndMino = Random.Range(1, 8);
            rndMino = 3; // �f�o�b�O�p��2���Œ�
        }

        // �����ʒu
        Vector3 pos = new Vector3(-40.0f, -11.0f, 0.0f);

        if(isBlockCreate)
        {
            // �v���n�u���w��ʒu�ɐ���
            switch (rndMino)
            {
                case 1:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino1, pos, Quaternion.identity);
                        deathCount.SetPieceCount(1);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 2:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino2, pos, Quaternion.identity);
                        deathCount.SetPieceCount(2);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 3:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino3, pos, Quaternion.identity);
                        deathCount.SetPieceCount(3);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 4:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino4, pos, Quaternion.identity);
                        deathCount.SetPieceCount(4);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 5:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino5, pos, Quaternion.identity);
                        deathCount.SetPieceCount(5);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 6:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino6, pos, Quaternion.identity);
                        deathCount.SetPieceCount(6);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                case 7:
                    if (goldManager.GetGold() >= buyLate)
                    {
                        isBlockCreate = false;
                        audio.PlayOneShot(blockCreateSE);
                        goldManager.SetGoldCount(-buyLate);
                        Instantiate(mino9, pos, Quaternion.identity);
                        deathCount.SetPieceCount(9);
                    }
                    else
                    {
                        audio.PlayOneShot(notBlockCreateSE);
                    }
                    break;
                default:
                    Debug.Log("�Ȃ񂩕ςȒl���o�Ă��Ł[");
                    break;
            }
            goldText.text = $"�c��̃R�C��:<color=#ffd700>{goldManager.GetGold().ToString()}</color>";
        }
    }

    /// <summary>
    /// �����Ńs�[�X���쐬���܂�
    /// </summary>
    public void PresentBox()
    {
        if(playerController.PieceCount > 0 && isBlockCreate)
        {
            audio.PlayOneShot(blockCreateSE);
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
        else
        {
            audio.PlayOneShot(notBlockCreateSE);
        }
        
    }

    // ���ז��u���b�N�𐶐�(�Ăяo���悤�ɕύX)
    public void BlockCreate()
    {
        List<Vector2Int> blockPositions = new List<Vector2Int>(); // �󂢂Ă���ʒu�̃��X�g

        // �󂢂Ă���ʒu������->���X�g�ɒǉ�
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (BlockBoard[i, j] == 0)
                {
                    blockPositions.Add(new Vector2Int(i, j));
                }
            }
        }

        Vector2Int randomIndex = blockPositions[Random.Range(0, blockPositions.Count)]; // ���X�g���烉���_���ɑI��

        /*while (isBlock == false)
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
        }*/

        BlockBoard[randomIndex.x, randomIndex.y] = 1; // �Ֆʏ����X�V

        // �����ʒu �Ֆʂ̍�����w��
        Vector3 pos = new Vector3(-28.0f - randomIndex.x, -7.0f - randomIndex.y, 0.0f);
        GameObject Trash = Instantiate(block, pos, Quaternion.identity);
        Trash.GetComponent<BlockOverlap>().blockPosition = new Vector2Int(randomIndex.x, randomIndex.y);

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