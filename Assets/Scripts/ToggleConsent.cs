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
    private Text placeholderText;
    public GameEvent StageEnd;
    public DataGameEvent dataSubmission;


    void Awake()
    {
        inputField.onEndEdit.AddListener(AcceptStringInput);
    }
    
    void OnEnable()
    {
        placeholderText = placeholder.GetComponent<Text>();
        string newText = signature;
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
                            } 

            if(string.IsNullOrEmpty(inputField.text))
            {
            error.SetActive(true);
            start.SetActive(false);
            }
        }
    }

    public void ConsentUpload()
    {
                var data = new List<string[]>();
                data.Add(new string[] {"name", 
                "I confirm that I have read and understand the information sheet. Access the information sheet", 
                "I understand what participation in this study involves", 
                "I have had the opportunity to consider the information and to ask questions. A researcher answered my questions satisfactorily.", 
                "I understand that my child will be assigned a 6-digit study code upon enrolment in the study and that his/her responses  will be only recorded under this study code.  Personal information will be stored securely and confidential  and separate from responses on any of the study measures.", 
                "I understand that my son/daughter’s participation is voluntary and that we are free to withdraw at any time  without having to give any reason.", 
                "I agree for my son/daughter to play cognitive games on a tablet/smartphone", 
                "I agree to fill in some questionnaires about my child’s behavior."});
                
                var name = signature;
                var statement1 = consentBoxes[0].isOn ? 1:0;
                var statement2 = consentBoxes[1].isOn ? 1:0;
                var statement3 = consentBoxes[2].isOn ? 1:0;
                var statement4 = consentBoxes[3].isOn ? 1:0;
                var statement5 = consentBoxes[4].isOn ? 1:0;
                var statement6 = consentBoxes[5].isOn ? 1:0;
                var statement7 = consentBoxes[6].isOn ? 1:0;
                
                data.Add(new string[] {signature, $"{statement1}", $"{statement2}",$"{statement3}", $"{statement4}", $"{statement5}", $"{statement6}", $"{statement7}" });
                Debug.Log(signature);
                dataSubmission.Raise(data, "consent form");
                StageEnd.Raise();
    }

    

    public void TickAll()
    {
        foreach(Toggle toggle in consentBoxes) 
        {
            toggle.isOn=true;
        }            
    }





}