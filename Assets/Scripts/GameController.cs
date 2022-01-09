using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public Word difficulty;
    public bool prompted = false;
    public Vector2 playerPosition = Vector2.zero;
    Sewer sewer;

    private string[] directions =
    {
        "North",
        "Northeast",
        "East",
        "Southeast",
        "South",
        "Southwest",
        "West",
        "Northwest"
    };

    Parser p;
    // Start is called before the first frame update
    void Start()
    {
        FocusInputTextBox();

        sewer = new Sewer(199, 199);
        sewer.Generate(sewer.At(Vector2.zero), -1, 10, 2);
        p = new Parser();
    }

    // Update is called once per frame
    void Update()
    {
        if (!prompted)
        {
            string output = "You can go";
            SewerRoom current = sewer.At(playerPosition);
            for (int i = 0; i < 8; i++)
            {
                if (current[i] != null)
                {
                    output += " " + directions[i];
                }
            }
            output += ".";
            AddOutputText(output + "\n");
            prompted = true;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            string input = GetInputText().Trim();
            if (input != "")
            {
                string[] response = p.Parse(input).Split(' ');
                string output = "";

                switch(response[0])
                {
                    case "move":
                        if (response[1] == "not")
                        {
                            output += "You would walk out the room, but you are not sure which way to go.";
                        } 
                        else if (response[1] == "none")
                        {
                            output += "You would walk out the room, but you forget which way to go.";
                        } 
                        else
                        {
                            Vector2 direction = new Vector2(int.Parse(response[1]), int.Parse(response[2]));

                            playerPosition += direction;
                            output += "You walk " + directions[SewerRoom.VectorToIndex(direction)] + "out the room.";
                        }
                        break;
                    case "not":
                        output += "You are not sure what to do.";
                        break;
                }

                AddOutputText("> " + output + "\n");
                SetInputText("");
                prompted = false;
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
