// PieceButtonManager.cs
// 
// �{�^�����͂���t�A���͂��ꂽ�s�[�X�𐶐����܂��B
// 

using UnityEngine;

public class PieceButtonManager : MonoBehaviour
{
    private PieceCreate Pcreate;    // �p�Y���s�[�X�𐶐�����X�N���v�g
    private ShopOpen shop;   // �V���b�v���J���X�N���v�g
    private BuffSeter buffSeter; // �o�t��K��������X�N���v�g
    private BuffManager buffManager; // �o�t���e���L�^����X�N���v�g
    private PlayrController playerController; // �v���C���[�̃R���g���[���[
    private GoldManager goldManager; // ���z�Ǘ����s���X�N���v�g
    private PuzzleController puzzleController; // �p�Y���S�̂��Ǘ�����X�N���v�g
    [SerializeField] private GameObject EnemySummoningManager; // �G�l�~�[�̔z�u���Ǘ�����X�N���v�g
    [SerializeField] private AudioClip normalBGM;

    void Start()
    {
        puzzleController = FindAnyObjectByType<PuzzleController>();
        goldManager = FindAnyObjectByType<GoldManager>();
        playerController = FindAnyObjectByType<PlayrController>();
        buffSeter = FindAnyObjectByType<BuffSeter>();
        buffManager = FindAnyObjectByType<BuffManager>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
        shop = FindAnyObjectByType<ShopOpen>();
    }
    public void ShopClose()
    {
        foreach (var buff in puzzleController.ProvisionalBuffs)
        {
            buffManager.AddBuff(buff.buffID, buff.value);
        }
        buffSeter.ApplyBuffs(); // �o�t��K��������
        playerController.CoinCount = goldManager.GetGold(); // ���������X�V
        EnemySummoningManager.GetComponent<AudioSource>().clip = normalBGM; // �ʏ�p��BGM��ݒ�
        EnemySummoningManager.GetComponent<AudioSource>().Play(); // BGM���Đ�
        shop.ShopOpenAni(); // �V���b�v�̃J������؂�ւ���
    }

    public void minoClick(int number)
    {
        Pcreate.NewPiece(number);
    }

    public void debugPresentClick()
    {
        Pcreate.PresentBox();
    }

    public void debugBlockClick()
    {
        Pcreate.BlockCreate();
    }
}
