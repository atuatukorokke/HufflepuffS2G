// SoundController.cs
//
// 音声関係を管理します。
//

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;    // オーディオミキサー
    [SerializeField] private Slider BGMSlider;      // BGMスライダー
    [SerializeField] private Slider SESlider;       // SEスライダー

    private void Start()
    {
        // 保存されている音量データがある場合は読み込み、ない場合はデフォルト音量を設定
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
    /// BGM音量を設定します
    /// </summary>
    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGMVol",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    /// <summary>
    /// SE音量を設定します
    /// </summary>
    public void SetSEVolume()
    {
        float volume = SESlider.value;
        myMixer.SetFloat("SEVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

    /// <summary>
    /// 保存されている音量データを読み込みます
    /// </summary>
    private void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SESlider.value = PlayerPrefs.GetFloat("SEVolume");
        SetBGMVolume();
        SetSEVolume();
    }
}
