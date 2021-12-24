using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Word difficulty;
    public bool prompted = false;
    // Start is called before the first frame update
    void Start()
    {
        FocusInputTextBox();
    }

    // Update is called once per frame
    void Update()
    {
        if (!prompted)
        {
            AddOutputText("Haha you in the sewer\n");
            prompted = true;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            if (GetInputText() != "")
            {
                AddOutputText("> " + GetInputText() + "\n");
                SetInputText("");
            }
            FocusInputTextBox();
        }
    }

    #region Utils
    GameObject GetInputTextBox()
    {
        return gameObject.transform.GetChild(1).gameObject;
    }

    string GetInputText()
    {
        return GetInputTextBox().GetComponent<TMP_InputField>().text;
    }

    void SetInputText(string s)
    {
        GetInputTextBox().GetComponent<TMP_InputField>().text = s;
    }

    void FocusInputTextBox()
    {
        GetInputTextBox().GetComponent<TMP_InputField>().Select();
        GetInputTextBox().GetComponent<TMP_InputField>().ActivateInputField();
    }

    GameObject GetOutputTextBox()
    {
        return gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }

    string GetOutputText()
    {
        return GetOutputTextBox().GetComponent<TextMeshProUGUI>().text;
    }

    void SetOutputText(string s)
    {
        GetOutputTextBox().GetComponent<TextMeshProUGUI>().text = s;
    }

    void AddOutputText(string s)
    {
        SetOutputText(GetOutputText() + s);
    }
    #endregion
}
