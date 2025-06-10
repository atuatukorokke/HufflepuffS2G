using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// ��i�K��
[System.Serializable]
public class FastSpecialBom
{
    [SerializeField] public GameObject BulletPrehab; // �e���̃v���n�u
    [SerializeField] public int ShotNum; // �e����ł�
    [SerializeField] public float DelayTime; // �e����łԊu
    [SerializeField] public float speed; // �S�~�[�̑���

}
// ��i�K��
[System.Serializable]
public class SecondSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
}
// �O�i�K��
[System.Serializable]
public class ThirdSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
}
// �l�i�K��
[System.Serializable]
public class FourSpecialBom
{
    [SerializeField] public GameObject BulletPrehab;
}
// �ŏI�i�K��
[System.Serializable]
public class FinalSpecianBom
{
    [SerializeField] public GameObject BulletPrehab;
}

// �Z�~�t�@�C�i��
[System.Serializable]
public class SpecialFinalAttack
{
    [SerializeField] public GameObject BulletPrehab;
 }



public class SpecialMove_Gomi : MonoBehaviour
{
    [Header("�{�X�S�̂��Ǘ�����ϐ�")]
    [SerializeField] private Vector2 spellPos;// �K�E�Z�E�Z�~�t�@�C�i����łƂ��ɂ��̍��W�Ɉ�U�߂�

    [Header("��i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private FastSpecialBom fastSpecialBom;
    [Header("��i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private SecondSpecialBom secondSpecialBom;
    [Header("�O�i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private ThirdSpecialBom thirdSpecialBom;
    [Header("�l�i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private FourSpecialBom fourSpecialBom;
    [Header("�ŏI�i�K�ڂ̕K�E�Z�̕ϐ�")]
    [SerializeField] private FinalSpecianBom finalSpecianBom;
    [Header("�Z�~�t�@�C�i���̕ϐ�")]
    [SerializeField] private SpecialFinalAttack specialFinalAttack;
   
    /// <summary>
    /// �ǂ̕K�E�Z��ł��̔�����s���܂�
    /// </summary>
    /// <param name="state">���̃{�X�̏�Ԃł�</param>
    public void BomJudgement(State state, BulletState bulletState)
    {
        switch(state)
        {
            case State.fast:
                StartCoroutine(FastSpecialBullet(state, bulletState));
                break;
            case State.second:
                StartCoroutine(SecondSpecialBullet(state, bulletState));
                break;
            case State.third:
                StartCoroutine(ThirdSpecialBullet(state, bulletState));
                break;
            case State.four:
                StartCoroutine(FourSpecialBullet(state, bulletState));
                break;
            case State.final:
                StartCoroutine(FinalSpecialBullet(state, bulletState));
                break;

        }
    }

    /// <summary>
    /// ��i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator FastSpecialBullet(State state, BulletState bulletState)
    {
        yield return StartCoroutine(FireSpecialBullet());
        while(state == State.fast && bulletState == BulletState.spell)
        {
            for(int i = 0; i < fastSpecialBom.ShotNum; i++)
            {
                float dirX = Random.Range(1.5f, -8.5f) - transform.position.x;
                float dirY = Random.Range(-4.5f, 4.5f) - transform.position.y;
                Vector3 moveDirection = new Vector3(dirX, dirY, 0);
                GameObject gomi = Instantiate(fastSpecialBom.BulletPrehab, transform.position, Quaternion.identity);
                Rigidbody2D rb = gomi.GetComponent<Rigidbody2D>();
                rb.linearVelocity = moveDirection * fastSpecialBom.speed;
                yield return new WaitForSeconds(fastSpecialBom.DelayTime);
            }
            yield return new WaitForSeconds(2.5f);
        }
        yield return null;
    }

    /// <summary>
    /// ��i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator SecondSpecialBullet(State state, BulletState bulletState)
    {
        while(state == State.second && bulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// �O�i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator ThirdSpecialBullet(State state, BulletState bulletState)
    {
        while(state == State.third && bulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// �l�i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator FourSpecialBullet(State state, BulletState bulletState)
    {
        while(state == State.four && bulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// �ŏI�i�K�ڂ̕K�E�Z
    /// </summary>
    /// <returns></returns>
    private IEnumerator FinalSpecialBullet(State state, BulletState bulletState)
    {
        while(state == State.final && bulletState == BulletState.spell)
        {
            yield return new WaitForSeconds(20);
        }
        yield return null;
    }

    /// <summary>
    /// �K�E�Z��łƂ��̈ړ������܂�
    /// </summary>
    /// <returns>������I�������܂�</returns>
    private IEnumerator FireSpecialBullet()
    {
        float limitTime = 1.5f; // �ړ��ɂ����鎞��
        float elapsedTime = 0f; // �ړ��ɂ�����������
        Vector2 startPosition = transform.position;
        // randomPos��limitTime�����Ĉړ�����
        while (elapsedTime < limitTime)
        {
            transform.position = new Vector2(
                Mathf.Lerp(startPosition.x, spellPos.x, elapsedTime / limitTime),
                Mathf.Lerp(startPosition.y, spellPos.y, elapsedTime / limitTime)
                );
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
