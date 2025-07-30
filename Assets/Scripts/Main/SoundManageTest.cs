using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class SoundManageTest : MonoBehaviour
{
    void Start()
    {
        GetComponent<MMF_Player>().PlayFeedbacks();
    }
}
