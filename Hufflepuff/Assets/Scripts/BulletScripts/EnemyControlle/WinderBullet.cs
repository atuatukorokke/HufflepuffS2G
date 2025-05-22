// Winderbullet.cs
//
// ���C���_�[��ɒe���𐶐�
//


using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WinderBullet : MonoBehaviour
{
    private GameObject targetObj; // �^�[�Q�b�g(�v���C���[)
    private Vector2 targetPos; // �^�[�Q�b�g(�v���C���[)�Ƃ̈ʒu�֌W������
    [SerializeField] private GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] private int ShotNum; // �e�����o����
    [SerializeField] private float speed; // �e���̃X�s�[�h
    [SerializeField] private float delayTime; // �e�����o���Ԋu
    [SerializeField] private float destroyTime; // �e������������
    GameObject proj;
    void Start()
    {
        InvokeRepeating("WindBulletUpdate", 0, delayTime + 2.0f);
    }

    private void WindBulletUpdate()
    {
        StartCoroutine(WinderBulletCreat());
    }

    private IEnumerator WinderBulletCreat()
    {
        int count = 0;
        while(count < 20)
        {
            targetPos = GetAnim(transform.position);
            for (int i = 0; i < 3; i++)
            {
                Debug.Log(targetPos);
            }
            count++;
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// �v���C���[�̍��W���擾����
    /// </summary>
    /// <param name="E_position">�G�l�~�[�̍��W</param>
    /// <returns>�v���C���[�ƃG�l�~�[�̈ʒu�֌W��Ԃ�</returns>
    private Vector2 GetAnim(Vector2 E_position)
    {
        targetObj = GameObject.Find("Player");
        float dx = targetObj.transform.position.x - E_position.x;
        float dy = targetObj.transform.position.y - E_position.y;
        return new Vector2(dx, dy);
    }
}
