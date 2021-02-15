using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ID_script : MonoBehaviour
{
   public InputField inputField;

    public string id = "JD";
    public GameObject placeholder;
    private Text placeholderText;

    void Awake()
    {
        id = PlayerPrefs.GetString("ID", "JD");
        PlayerPrefs.SetString("JD", id);
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }

    void OnEnable()
    {
        placeholderText = placeholder.GetComponent<Text>();
    }

    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        id = userInput;
        PlayerPrefs.SetString("ID", id);
        InputComplete();

    }

    void InputComplete()
    {
        Debug.Log(PlayerPrefs.GetString("ID"));
        inputField.ActivateInputField();
    }

}