// SoundController.cs
//
// �����֌W���Ǘ����܂��B
//

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;    // �I�[�f�B�I�~�L�T�[
    [SerializeField] private Slider BGMSlider;      // BGM�X���C�_�[
    [SerializeField] private Slider SESlider;       // SE�X���C�_�[

    private void Start()
    {
        // �ۑ�����Ă��鉹�ʃf�[�^������ꍇ�͓ǂݍ��݁A�Ȃ��ꍇ�̓f�t�H���g���ʂ�ݒ�
        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetSEVolume();
        }
        
    }

    /// <summary>
    /// BGM���ʂ�ݒ肵�܂�
    /// </summary>
    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGMVol",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    /// <summary>
    /// SE���ʂ�ݒ肵�܂�
    /// </summary>
    public void SetSEVolume()
    {
        float volume = SESlider.value;
        myMixer.SetFloat("SEVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

    /// <summary>
    /// �ۑ�����Ă��鉹�ʃf�[�^��ǂݍ��݂܂�
    /// </summary>
    private void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SESlider.value = PlayerPrefs.GetFloat("SEVolume");
        SetBGMVolume();
        SetSEVolume();
    }
}
