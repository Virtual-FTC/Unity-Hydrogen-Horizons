using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

//i think a better way to do this would be an event / interface that could be triggered on a 'setprefs' event.

public enum AudioType { SFX, BGM };
public class setAudioLevels : MonoBehaviour
{
    public AudioMixer mixer;
    public string prefName;
    public AudioType audioType;
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        slider = GetComponent<Slider>();
        float val = PlayerPrefs.GetFloat(prefName, 1);
        if (slider)
        {
            slider.value = val;
            slider.onValueChanged.AddListener(SetVolume);
        }
        mixer.SetFloat("volume", getVolumeVal(val));
    }

    public float getVolumeVal(float sliderValue)
    {
        if(sliderValue == 0) { return -80; }
        else { return Mathf.Log10(sliderValue) * 20; }
    }

    public void SetVolume(float val)
    {
        mixer.SetFloat("volume", getVolumeVal(val));
        PlayerPrefs.SetFloat(prefName, val);
    }
}