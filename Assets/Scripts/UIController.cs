using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    Resolution res;
    int margin = 10;

    // Start is called before the first frame update
    void Start()
    {
        res = Screen.currentResolution;
        SetUILayout();
    }

    // Update is called once per frame
    void Update()
    {
        if (res.height != Screen.currentResolution.height || res.width != Screen.currentResolution.width) {
         
             
 
             res = Screen.currentResolution;
 
         }
    }

    void SetUILayout() 
    {
        GameObject output = GameObject.Find("Text (TMP)");
        GameObject input = GameObject.Find("InputField (TMP)");

        int height = Screen.currentResolution.height;
        int width = Screen.currentResolution.width;

        output.GetComponent<RectTransform>().position = new Vector3(margin, margin, 1);
        output.GetComponent<RectTransform>().sizeDelta = new Vector2(width - margin, (height - margin) / 2);
    }
}
