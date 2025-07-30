using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class ReturnToMain : MonoBehaviour
{
    public void Return()
    {
        GetComponent<MMF_Player>().PlayFeedbacks();
    }
}
