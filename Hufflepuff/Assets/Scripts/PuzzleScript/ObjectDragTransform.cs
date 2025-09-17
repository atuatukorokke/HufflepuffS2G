// ObjectDragTransform
// 
// �I�u�W�F�N�g���h���b�O�A���h�h���b�v�ňړ������܂�
// 

using System.Collections;
using UnityEngine;

public class ObjectDragTransform : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    private bool isDragging = false;

    float gridSize = 1.0f;

    [Header("�s�[�X���")]
    [SerializeField] private int pieceCount = 0;    // �s�[�X��
    [SerializeField] private int sellGold = 0;     // ���z

    [Header("�X�N���v�g�𓮓I�ɃA�^�b�`�����")]
    [SerializeField] private DeathCount deathCount;     // ���ʂ��̔�����s���X�N���v�g
    [SerializeField] private GoldManager goldManager;     // ���z�Ǘ����s���X�N���v�g
    [SerializeField] private PuzzleController puzzleController; // �p�Y���S�̂��Ǘ�����X�N���v�g
    [SerializeField] private Buff buff;    // �o�t�Ǘ����s���X�N���v�g

    void Start()
    {
        mainCamera = Camera.main;

        // ���̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g�𐶐������Ƃ��ɃI�u�W�F�N�g���擾����
        deathCount = Object.FindFirstObjectByType<DeathCount>();
        goldManager = Object.FindFirstObjectByType<GoldManager>();
        puzzleController = FindFirstObjectByType<PuzzleController>();
        puzzleController.ProvisionalBuffs.Add(buff);
    }

    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }

        // ���݃h���b�O���Ă���I�u�W�F�N�g��X�L�[��90�x��]
        if (isDragging & Input.GetKeyDown(KeyCode.X))
        {
            float currentZ = transform.eulerAngles.z;
            transform.eulerAngles = new Vector3(0, 0, currentZ + 90f);
        }

        // ���݃h���b�O���Ă���I�u�W�F�N�g��C�L�[�Ŕ��p
        if (isDragging & Input.GetKeyDown(KeyCode.C))
        {
            Destroy(gameObject);

            deathCount.SetPieceCount(pieceCount * -1);

            goldManager.SetGoldCount(sellGold);

            for(int i = 0; i <= puzzleController.ProvisionalBuffs.Count; i++)
            {
                if(buff.buffID == puzzleController.ProvisionalBuffs[i].buffID & buff.value == puzzleController.ProvisionalBuffs[i].value)
                {
                    puzzleController.ProvisionalBuffs.RemoveAt(i);
                    break;
                }
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

        // �s�[�X���m���d�Ȃ��Ă����猳�̈ʒu�ɖ߂�

        // �ʒu���擾���āAx��y���l�̌ܓ����Đ����ɃX�i�b�v
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        transform.position = pos;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mainCamera.WorldToScreenPoint(transform.position).z;
        return mainCamera.ScreenToWorldPoint(mousePoint);
    }
}
