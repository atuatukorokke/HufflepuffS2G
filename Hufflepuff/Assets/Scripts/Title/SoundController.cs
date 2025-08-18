using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider BGMSlider;
    [SerializeField] private Slider SESlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("BGMVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetBGMVolume();
            SetSEVolume();
        }
        
    }

    public void SetBGMVolume()
    {
        float volume = BGMSlider.value;
        myMixer.SetFloat("BGMVol",Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }

    public void SetSEVolume()
    {
        float volume = SESlider.value;
        myMixer.SetFloat("SEVol", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

    private void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
        SESlider.value = PlayerPrefs.GetFloat("SEVolume");
        SetBGMVolume();
        SetSEVolume();
    }
}
