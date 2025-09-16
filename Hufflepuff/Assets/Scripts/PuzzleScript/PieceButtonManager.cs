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

    void Start()
    {
        buffSeter = FindAnyObjectByType<BuffSeter>();
        Pcreate = FindAnyObjectByType<PieceCreate>();
        shop = FindAnyObjectByType<ShopOpen>();
    }
    public void ShopClose()
    {
        buffSeter.ApplyBuffs(); // �o�t��K��������
        shop.ShopOpenAni(); // �V���b�v�̃J������؂�ւ���
    }

    public void minoClick(int number)
    {
        Pcreate.NewPiece(number);
    }
    public void PieceAddBuff(int BuffID)
    {
        Pcreate.PieceAddBuff((BuffForID)BuffID);
    }
}
