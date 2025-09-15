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

    void Start()
    {
        mainCamera = Camera.main;
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

        if (isDragging & Input.GetKeyDown(KeyCode.X))
        {
            float currentZ = transform.eulerAngles.z;
            transform.eulerAngles = new Vector3(0, 0, currentZ + 90f);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;

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
