using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public enum VolumeType
    {
        Master,
        BGM,
        SFX
    }

    [SerializeField] private VolumeType type;

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(Key, 1f));
        slider.onValueChanged.AddListener(OnChanged);
    }

    private void OnEnable()
    {
        if (slider != null)
            slider.SetValueWithoutNotify(PlayerPrefs.GetFloat(Key, 1f));
    }

    private void OnDestroy()
    {
        if (slider != null)
            slider.onValueChanged.RemoveListener(OnChanged);
    }

    private string Key => type switch
    {
        VolumeType.Master => "MasterVolume",
        VolumeType.BGM => "BGMVolume",
        _ => "SFXVolume"
    };

    private void OnChanged(float value)
    {
        SoundManager sm = SoundManager.instance;
        if (sm == null) return;

        switch (type)
        {
            case VolumeType.Master: sm.SetMasterVolume(value); break;
            case VolumeType.BGM: sm.SetBGMVolume(value); break;
            case VolumeType.SFX: sm.SetSFXVolume(value); break;
        }
    }
}
