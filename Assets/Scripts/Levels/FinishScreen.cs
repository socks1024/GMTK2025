using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    private MMF_Player _MMF_Player;
    private int _bodyLength;

    void Start()
    {
        _MMF_Player = GetComponent<MMF_Player>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            LevelManager.Instance.MMLoadSelectLevelScene();
        }
    }

    public void PopUp(int bodyLength)
    {
        LevelManager.Instance.CurrSnakeHead.GetComponent<SnakeInput>().IsActive = false;

        _bodyLength = bodyLength;
        _MMF_Player.PlayFeedbacks();
    }

    public void CompleteLevel()
    {
        LevelManager.Instance.CompleteCurrLevel();
    }
}
