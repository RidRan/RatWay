using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Word difficulty;
    // Start is called before the first frame update
    void Start()
    {
        GetInputTextBox().GetComponent<TMP_InputField>().Select();
        GetInputTextBox().GetComponent<TMP_InputField>().ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            GameObject outputTextBox = GetOutputTextBox();
            GameObject inputTextBox = GetInputTextBox();
            string input = inputTextBox.GetComponent<TMP_InputField>().text;
            if (input != "")
            {
                outputTextBox.GetComponent<TextMeshProUGUI>().text = outputTextBox.GetComponent<TextMeshProUGUI>().text + "> " + input + "\n";
                inputTextBox.GetComponent<TMP_InputField>().text = "";
            }
            inputTextBox.GetComponent<TMP_InputField>().Select();
            inputTextBox.GetComponent<TMP_InputField>().ActivateInputField();
        }
    }

    GameObject GetInputTextBox()
    {
        return gameObject.transform.GetChild(1).gameObject;
    }

    GameObject GetOutputTextBox()
    {
        return gameObject.transform.GetChild(5).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
    }


}
