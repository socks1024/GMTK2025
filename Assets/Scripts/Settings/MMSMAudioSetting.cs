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

    protected override void OnHandlerInput(float value)
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

    protected override void OnHandlerInput(bool value)
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
