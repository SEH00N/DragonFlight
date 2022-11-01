using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer = null;
    [SerializeField] string volumeKey = "";
    private Slider slider = null;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }
    
    private void Start()
    {
        slider.value = DataManager.Instance.userSetting.volumes[volumeKey];
        audioMixer.SetFloat(volumeKey, DataManager.Instance.userSetting.volumes[volumeKey]);
    }

    public void SetVolume()
    {
        float volume = slider.value <= -40f ? -80f : slider.value;
        audioMixer.SetFloat(volumeKey, volume);
        DataManager.Instance.userSetting.volumes[volumeKey] = volume;
    }
}
