using System;
using Tools.DataPersistence.PlayerPref;
using Tools.GameProgrammingPatterns.Singleton;

public class LevelManager : MonoSingleton<LevelManager>
{
    public SnakeHead CurrSnakeHead;

    public void OnLevelComplete()
    {

    }
}

public class PlayerPrefLevelComplete : PlayerPrefSavableField<bool, LevelManager>
{
    public int WorldIndex;

    public int LevelIndex;

    public override string PrefKey => "LevelComplete:" + WorldIndex + LevelIndex;

    protected override bool DefaultValue => false;

    protected override bool ConvertValue(string value)
    {
        return Convert.ToBoolean(value);
    }

    protected override void SetHandler(bool value)
    {
        
    }

    protected override void OnHandlerInput(bool value)
    {
        
    }
}
