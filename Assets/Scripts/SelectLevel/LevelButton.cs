using System;
using Tools.DataPersistence.PlayerPref;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public LevelInfo Level;

    [HideInInspector]
    public Button button;

    public LevelButton NextLevelButton;

    public bool AlwaysInteractable;

    public void Start()
    {
        Debug.Log(Level.LevelSceneName + " " + Level.UnlockInfo.CurrValue);

        button = GetComponent<Button>();

        Level.InitLevelInfo();

        if (NextLevelButton != null) Level.NextLevel = NextLevelButton.Level;

        button.onClick.AddListener(() => LevelManager.Instance.MMLoadLevelScene(Level));
        button.interactable = Level.UnlockInfo.CurrValue || AlwaysInteractable;
    }
}

[Serializable]
public class LevelInfo
{
    public string LevelSceneName;

    [HideInInspector]
    public LevelInfo NextLevel;

    public PlayerPrefLevelUnlocked UnlockInfo = new();

    public void InitLevelInfo()
    {
        UnlockInfo.LevelName = LevelSceneName;
        UnlockInfo.ConnectEvent(this);
        UnlockInfo.Load();
    }

    public void UnlockNextLevel()
    {
        Debug.Log(LevelSceneName + " Trying Unlock Next Level");

        if (NextLevel is null) return;

        Debug.Log("Next Level : " + NextLevel.LevelSceneName + ", Current Unlock Status:" + NextLevel.UnlockInfo.CurrValue);

        var u = NextLevel.UnlockInfo;

        u.HandlerWriteValue?.Invoke(true);
        u.Save();
        u.Load();

        Debug.Log("Current Unlock Status:" + NextLevel.UnlockInfo.CurrValue);
    }
}

public class PlayerPrefLevelUnlocked : PlayerPrefSavableField<bool, LevelInfo>
{
    public string LevelName;

    public override string PrefKey => "LevelComplete_" + LevelName;

    protected override bool DefaultValue => false;

    protected override bool ConvertValue(string value)
    {
        return Convert.ToBoolean(value);
    }

    protected override void SetHandler(bool value)
    {
        HandlerWriteValue?.Invoke(value);
    }
}
