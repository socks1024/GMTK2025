using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetAll : MonoBehaviour
{
    public UnityEvent OnReset;

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();

        OnReset?.Invoke();
    }
}
