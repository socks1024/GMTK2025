using System.Collections.Generic;
using Tools.DataPersistence.PlayerPref;
using UnityEngine;
using UnityEngine.UI;

public class VideoSetting : PlayerPrefSavableFieldContainer<PlayerPrefFullScreen, bool, Toggle>
{
    private PlayerPrefFullScreen playerPrefFullScreen = new();

    public Toggle FullScreenToggle;

    protected override List<PlayerPrefFullScreen> SetFields()
    {
        List<PlayerPrefFullScreen> l = new()
        {
            playerPrefFullScreen
        };
        return l;
    }

    protected override void ConnectEvent()
    {
        playerPrefFullScreen.ConnectEvent(FullScreenToggle);
        playerPrefFullScreen.HandlerWriteValue += OnFullScreenInput;
    }

    protected void OnFullScreenInput(bool value)
    {
        if (value)
        {
            Resolution[] resolutions = Screen.resolutions;
            Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, true);
            Screen.fullScreen = true;
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
        }
    }
}

public class PlayerPrefFullScreen : PlayerPrefSavableToggle
{
    public override string PrefKey => "FullScreen";

    protected override bool DefaultValue => true;
}




