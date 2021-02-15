using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ToggleConsent : MonoBehaviour
{
    public Toggle[] consentBoxes;
    public GameObject start;
    public GameObject error;
    public InputField inputField;

    public GameObject placeholder;
    private string signature;
    public bool input=false;
    private Text placeholderText;


    void Awake()
    {
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    
    void OnEnable()
    {
        placeholderText = placeholder.GetComponent<Text>();
        // string translatedPrompt = Languages.translate("Enter Subject ID...");
        string newText = signature;
        // placeholderText.text = newText;
    }

    void AcceptStringInput(string userInput)
    {
        userInput = userInput.ToLower();
        signature = userInput;
    }


    // Start is called before the first frame update
    void Start()
    {
        consentBoxes = GetComponentsInChildren<Toggle>();
        start.SetActive(false);
        error.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        

         foreach(Toggle toggle in consentBoxes) 
         {
            
            if(toggle.isOn) {
                start.SetActive(true);
                error.SetActive(false);
                inputField.interactable = true;
            } else {
                error.SetActive(true);
                start.SetActive(false);
                inputField.interactable = false;
            }

             if(string.IsNullOrEmpty(inputField.text))
         {
            error.SetActive(true);
            start.SetActive(false);
         }
        }


    }





}