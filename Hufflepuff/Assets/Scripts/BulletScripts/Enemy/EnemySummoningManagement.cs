// EnemySummoningManagement.cs
//
// �G�l�~�[�̔z�u�⏢���Ȃǂ̊Ǘ����s���܂��B
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySummoningManagement : MonoBehaviour
{
    [SerializeField] private List<EnemyDeployment> enemyDeployment; // �G�l�~�[�̔z�u�f�[�^���i�[���郊�X�g
    [SerializeField] private GameObject ClearPanel;
    [SerializeField] private GameObject TitleButton;
    [SerializeField] private TextMeshProUGUI coinText; // �������e�L�X�g
    [SerializeField] private TextMeshProUGUI pieceText; // �s�[�X�̐��e�L�X�g
    [SerializeField] private TextMeshProUGUI deathLateText; // ���S���e�L�X�g 
    [SerializeField] private Animator ClearAnimator;
    [SerializeField] private Animator PuzzleAnimetor;
    [SerializeField] private PlayrController playerController; // �v���C���[�̃R���g���[���[
    [SerializeField] private GoldManager goldManager; // ���z�Ǘ����s���X�N���v�g
    [SerializeField] private DeathCount deathCount; // ���ʂ��̔�����s���X�N���v�g
    private bool waitingForMiddleBoss = false; // �r���Ń{�X���o�Ă��邩�ǂ����̃t���O
    private bool waitingForShop = false; // �V���b�v���J���Ă��邩�ǂ����̃t���O
    private AudioSource audioSource; // BGM�̍Đ��p�I�[�f�B�I�\�[�X
    private float fadeInTime = 2;
    private float fadeOutTime = 0;
    private bool isFateIn = false;

    [SerializeField] private AudioClip OpenPuzzle;
    [SerializeField] private AudioClip puzzleBGM;

    private void Start()
    {
        playerController = FindAnyObjectByType<PlayrController>();
        goldManager = FindAnyObjectByType<GoldManager>();
        deathCount = FindAnyObjectByType<DeathCount>();
        TitleButton.SetActive(false);
        ClearPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
        StartCoroutine(Enumerator()); // �G�l�~�[�̔z�u���J�n
    }

    /// <summary>
    /// �G�l�~�[�̔z�u���s���܂��B
    /// </summary>
    /// <returns></returns>
    public IEnumerator Enumerator()
    {
        foreach(var deploment in enemyDeployment)
        {
            switch(deploment.GetState1)
            {
                case EnemyDeployment.state.Smallfry: // �G���G�̔z�u
                    for (int i = 0; i < deploment.EnemyCount; i++)
                    {
                        SpawnEnemy(deploment);
                        yield return new WaitForSeconds(deploment.DelayTime);
                    }
                    break;

                case EnemyDeployment.state.middleBoss: // ���{�X�̔z�u
                    GameObject middleBoss = SpawnEnemy(deploment);
                    waitingForMiddleBoss = true; // ���{�X���o�Ă���t���O�𗧂Ă�
                    BossHealth health = middleBoss.GetComponent<BossHealth>();
                    health.OnDeath += () => waitingForMiddleBoss = false; // ���{�X���|���ꂽ��t���O��������
                    yield return new WaitUntil(() => !waitingForMiddleBoss); // ���{�X���|�����܂őҋ@
                    break;
                case EnemyDeployment.state.Boss: // �{�X�̔z�u
                    GameObject Bosss = Instantiate(deploment.EnemyPrehab, deploment.GenerationPosition, Quaternion.identity);
                    audioSource.clip = deploment.BossBGM; // �{�X��p��BGM��ݒ�
                    audioSource.Play(); // BGM���Đ�

                    // �{�X���|���ꂽ���̏���
                    Boss1Bullet BossBullet = Bosss.GetComponent<Boss1Bullet>();
                    BossBullet.Ondeath += () => StartCoroutine(BossDeath());  
                    break;
                case EnemyDeployment.state.DelayTime:
                    yield return new WaitForSeconds(deploment.DelayTime);
                    break;
                case EnemyDeployment.state.Shop:
                    playerController.isShooting = false; // �V���b�v���͍U���ł��Ȃ��悤�ɂ���
                    PuzzleSet();
                    var shop = FindAnyObjectByType<ShopOpen>(); // �V���b�v�̃I�u�W�F�N�g���擾
                    goldManager.SetGoldCount(playerController.CoinCount); // ���������X�V
                    shop.ShopOpenAni();
                    waitingForShop = true; // �V���b�v���J���Ă���t���O�𗧂Ă�
                    shop.OnShop += () => waitingForShop = false; // �V���b�v������ꂽ��t���O��������
                    yield return new WaitUntil(() => !waitingForShop); // �V���b�v��������܂őҋ@
                    PuzzleAnimetor.SetBool("SetInstructions", false);
                    yield return new WaitForSeconds(2f);
                    PuzzleOut();
                    break;
            }
        }
    }

    /// <summary>
    /// �G�l�~�[�𐶐����܂��B
    /// </summary>
    /// <param name="deployment">�G�l�~�[����������ۂ̐ݒ�</param>
    /// <returns></returns>
    private GameObject SpawnEnemy(EnemyDeployment deployment)
    {
        Vector2 position = deployment.GenerationPosition;
        GameObject enemy = Instantiate(deployment.EnemyPrehab, position, Quaternion.identity);
        EnemyHealth health = enemy.GetComponent<EnemyHealth>();
        if(health != null)
        {
            health.SetHealth(deployment.EnemyHP);
        }

        return enemy;
    }

    /// <summary>
    /// �p�Y�����[�h�ɐ؂�ւ��܂��B
    /// </summary>
    private void PuzzleSet()
    {
        coinText.gameObject.SetActive(false); // �������e�L�X�g���\��
        pieceText.gameObject.SetActive(false); // �s�[�X�̐��e�L�X�g���\��
        deathLateText.gameObject.SetActive(false); // ���S���e�L�X�g���\��
        audioSource.PlayOneShot(OpenPuzzle);
        audioSource.clip = puzzleBGM; // �p�Y���p��BGM��ݒ�
        audioSource.Play(); // BGM���Đ�
        PuzzleAnimetor.SetBool("SetInstructions", true);
    }

    /// <summary>
    /// �V���[�e�B���O���[�h�ɐ؂�ւ��܂��B
    /// </summary>
    private void PuzzleOut()
    {
        coinText.gameObject.SetActive(true); // �������e�L�X�g��\��
        pieceText.gameObject.SetActive(true); // �s�[�X�̐��e�L�X�g��\��
        deathLateText.gameObject.SetActive(true); // ���S���e�L�X�g��\��
        coinText.text = $"�R�C��:<color=#ffd700>{playerController.CoinCount.ToString()}</color>";
        pieceText.text = $"�s�[�X:<color=#ffd700>{playerController.PieceCount.ToString()}</color>";
        deathLateText.text = $"���ז�:<color=#ff0000>{((int)((float)deathCount.BlockCount / (float)deathCount.PieceCount * 100)).ToString()}%</color>";
    }

    /// <summary>
    /// �{�X���|���ꂽ���̏������s���܂��B
    /// </summary>
    private IEnumerator BossDeath()
    {
        FindAnyObjectByType<PlayrController>().Playstate = PlayState.Clear;
        ClearPanel.SetActive(true);
        // BGM���t�F�[�h�A�E�g
        while (audioSource.volume > 0)
        {
            audioSource.volume -= 0.05f;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        ClearAnimator.SetBool("EndGame", true);
        yield return new WaitForSeconds(1.7f);
        TitleButton.SetActive(true);
    }
}