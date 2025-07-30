using System;
using MoreMountains.Tools;
using Sirenix.OdinInspector;
using Tools.DataPersistence.PlayerPref;
using UnityEngine;
using UnityEngine.UI;

public class MMSMAudioSetting : MonoBehaviour
{
    // public MMSMAudioSettingInfo MasterSetting;
    // public MMSMAudioSettingInfo MusicSetting;
    // public MMSMAudioSettingInfo SFXSetting;
    // public MMSMAudioSettingInfo UISetting;

    public PlayerPrefSavableMMSoundManagerTrackSetting MasterSetting;
    public PlayerPrefSavableMMSoundManagerTrackSetting MusicSetting;
    public PlayerPrefSavableMMSoundManagerTrackSetting SFXSetting;
    public PlayerPrefSavableMMSoundManagerTrackSetting UISetting;

    void Start()
    {
        MasterSetting.InitTrackSetting();
        MusicSetting.InitTrackSetting();
        SFXSetting.InitTrackSetting();
        UISetting.InitTrackSetting();
    }

    public void SaveSettings()
    {
        MasterSetting.SaveTrackSetting();
        MusicSetting.SaveTrackSetting();
        SFXSetting.SaveTrackSetting();
        UISetting.SaveTrackSetting();
    }

    public void ResetSettings()
    {
        MasterSetting.ResetTrackSetting();
        MusicSetting.ResetTrackSetting();
        SFXSetting.ResetTrackSetting();
        UISetting.ResetTrackSetting();
    }
}

// [Serializable]
// public class MMSMAudioSettingInfo
// {
//     [EnumToggleButtons]
//     public MMSoundManager.MMSoundManagerTracks Track;
//     public GameObject UIObject;

//     public bool DefaultMute = false;
//     public float DefaultVolume = 1f;

//     private float _volume;
//     private bool _mute;

//     private Slider VolumeSlider;
//     private Toggle MuteToggle;

//     private string VolumeKey { get { return Track.ToString() + "_volume"; } }
//     private string MuteKey { get { return Track.ToString() + "_mute"; } }

//     public void InitTrackSetting()
//     {
//         _volume = DefaultVolume;
//         _mute = DefaultMute;

//         VolumeSlider = UIObject.GetComponentInChildren<Slider>();
//         MuteToggle = UIObject.GetComponentInChildren<Toggle>();

//         VolumeSlider?.onValueChanged.AddListener(SetVolume);
//         MuteToggle?.onValueChanged.AddListener(SetMute);

//         if (PlayerPrefs.HasKey(VolumeKey) && PlayerPrefs.HasKey(MuteKey))
//         {
//             _volume = PlayerPrefs.GetFloat(VolumeKey);
//             _mute = PlayerPrefs.GetInt(MuteKey) > 0;
//         }

//         SetVolumeSlider(_volume);
//         SetMuteToggle(!_mute);
//     }

//     public void SaveTrackSetting()
//     {
//         PlayerPrefs.SetFloat(VolumeKey, _volume);
//         PlayerPrefs.SetInt(MuteKey, _mute ? 1 : 0);
//     }

//     public void ResetTrackSetting()
//     {
//         PlayerPrefs.SetFloat(VolumeKey, DefaultVolume);
//         PlayerPrefs.SetInt(MuteKey, DefaultMute ? 1 : 0);

//         SetVolumeSlider(DefaultVolume);
//         SetMuteToggle(!DefaultMute);
//     }

//     private void SetVolumeSlider(float value)
//     {
//         VolumeSlider.value = value;
//     }

//     private void SetMuteToggle(bool isOn)
//     {
//         MuteToggle.isOn = isOn;
//     }

//     private void SetVolume(float value)
//     {
//         _volume = value;
//         MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.SetVolumeTrack, Track, value);
//     }

//     private void SetMute(bool isOn)
//     {
//         _mute = !isOn;
//         if (_mute)
//         {
//             MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.MuteTrack, Track, 0f);
//             VolumeSlider.interactable = false;
//         }
//         else
//         {
//             MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.UnmuteTrack, Track, _volume);
//             VolumeSlider.interactable = true;
//         }
//     }
// }

[Serializable]
public class PlayerPrefSavableMMSoundManagerTrackSetting
{
    [EnumToggleButtons]
    public MMSoundManager.MMSoundManagerTracks Track;
    public GameObject UIObject;

    public PlayerPrefSavableMMSoundManagerTrackVolume VolumeField = new();
    public PlayerPrefSavableMMSoundManagerTrackMute MuteField = new();

    [HideInInspector] public float _volume;

    [HideInInspector] public Slider slider;
    [HideInInspector] public Toggle toggle;

    public void InitTrackSetting()
    {
        slider = UIObject.GetComponentInChildren<Slider>();
        toggle = UIObject.GetComponentInChildren<Toggle>();

        VolumeField.Track = Track;
        VolumeField.settings = this;
        VolumeField.ConnectEvent(slider);
        VolumeField.Load();

        MuteField.Track = Track;
        MuteField.settings = this;
        MuteField.ConnectEvent(toggle);
        MuteField.Load();
    }

    public void SaveTrackSetting()
    {
        VolumeField.Save();
        MuteField.Save();
    }

    public void LoadTrackSetting()
    {
        VolumeField.Load();
        MuteField.Load();
    }

    public void ResetTrackSetting()
    {
        VolumeField.Reset();
        MuteField.Reset();
    }
}

public class PlayerPrefSavableMMSoundManagerTrackVolume : PlayerPrefSavableSlider
{
    public MMSoundManager.MMSoundManagerTracks Track;
    public override string PrefKey => Track.ToString() + "_volume";

    protected override float DefaultValue => 1f;

    public PlayerPrefSavableMMSoundManagerTrackSetting settings;

    protected override void OnSetUI(float value)
    {
        _currValue = value;
        MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.SetVolumeTrack, Track, value);
        settings._volume = _currValue;
    }
}

public class PlayerPrefSavableMMSoundManagerTrackMute : PlayerPrefSavableToggle
{
    public MMSoundManager.MMSoundManagerTracks Track;
    public override string PrefKey => Track.ToString() + "_mute";

    protected override bool DefaultValue => false;

    public PlayerPrefSavableMMSoundManagerTrackSetting settings;

    protected override void OnSetUI(bool value)
    {
        _currValue = value;
        if (_currValue)
        {
            MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.MuteTrack, Track, 0f);
            settings.slider.interactable = false;
        }
        else
        {
            MMSoundManagerTrackEvent.Trigger(MMSoundManagerTrackEventTypes.UnmuteTrack, Track, settings._volume);
            settings.slider.interactable = true;
        }
    }
}
