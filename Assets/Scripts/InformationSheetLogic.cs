using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationSheetLogic : MonoBehaviour
{
    public GameObject consentform;
    // Start is called before the first frame update
    void Start()
    {
        consentform.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TurnOnConsent()
    {
        consentform.SetActive(true);
        gameObject.SetActive(false);
    }
}
