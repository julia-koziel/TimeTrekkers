// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ID_register : MonoBehaviour
// {
//     public InputField inputField;
//     public string id = "JD";
//     public GameObject placeholder;
//     private Text placeholderText;

//     void Awake()
//     {
//         id = PlayerPrefs.GetString("ID", "JD");
//         PlayerPrefs.SetString("ID", id);
//     }

//     void OnEnable()
//     {
//         placeholderText = placeholder.GetComponent<Text>();
//         placeholderText.text = newText;
//     }

//     void AcceptStringInput(string userInput)
//     {
//         // userInput = userInput.ToLower();
//         id = userInput;
//         PlayerPrefs.SetString("ID", id);
//         InputComplete();

//     }

//     void InputComplete()
//     {
//         Debug.Log(PlayerPrefs.GetString("ID"));
//         //inputField.ActivateInputField();
//     }

// }