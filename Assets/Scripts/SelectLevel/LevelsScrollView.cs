using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelsScrollView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ScrollRect>().horizontalNormalizedPosition = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.Instance.MMLoadMainScene();
        }
    }
}
