using MoreMountains.Tools;
using Tools.GameProgrammingPatterns.Singleton;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [HideInInspector]
    public SnakeHead CurrSnakeHead;

    [HideInInspector]
    public LevelInfo CurrLevel;

    public void CompleteCurrLevel()
    {
        CurrSnakeHead.GetComponent<SnakeInput>().IsActive = false;

        CurrLevel.UnlockNextLevel();

        MMLoadSelectLevelScene();
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
