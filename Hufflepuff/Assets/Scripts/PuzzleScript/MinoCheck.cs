using UnityEngine;

public class MinoCheck : MonoBehaviour
{
    [SerializeField] private PieceMoves pieceMoves;     // �Ֆʂ��d�Ȃ��Ă��Ȃ������m�F����X�N���v�g

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�𐶐������Ƃ��ɃI�u�W�F�N�g���擾����
        pieceMoves = Object.FindFirstObjectByType<PieceMoves>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("��������");
        pieceMoves.SetColliding(1);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pieceMoves.SetColliding(-1);
    }
}
