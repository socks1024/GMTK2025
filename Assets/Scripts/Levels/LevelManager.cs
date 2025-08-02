using MoreMountains.Tools;
using Timers;
using Tools.GameProgrammingPatterns.Singleton;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [HideInInspector]
    public SnakeHead CurrSnakeHead;

    [HideInInspector]
    public LevelInfo CurrLevel;

    public float WaitInterval = 1f;

    public void CompleteCurrLevel()
    {
        CurrLevel.UnlockNextLevel();

        TimersManager.SetTimer(this, WaitInterval, () => MMLoadSelectLevelScene());
    }

    public void RestartCurrLevel()
    {
        MMLoadLevelScene(CurrLevel);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrLevel();
        }
    }

    #region LoadScene

    private void MMLoadScene(string sceneName)
    {
        MMAdditiveSceneLoadingManagerSettings mySettings = new MMAdditiveSceneLoadingManagerSettings();
        mySettings.LoadingSceneName = LoadingSceneName;
        MMAdditiveSceneLoadingManager.LoadScene(sceneName, mySettings);
    }

    public void MMLoadLevelScene(LevelInfo levelInfo)
    {
        CurrLevel = levelInfo;

        MMLoadScene(levelInfo.LevelSceneName);
    }

    public void MMLoadMainScene()
    {
        MMLoadScene(MainSceneName);
    }

    public void MMLoadSelectLevelScene()
    {
        MMLoadScene(SelectLevelSceneName);
    }

    #endregion

    #region Inspector Field

    public string MainSceneName;

    public string SelectLevelSceneName;

    public string LoadingSceneName;

    #endregion
}
