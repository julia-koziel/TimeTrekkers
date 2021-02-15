using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLogic : MonoBehaviour

{      
    public GameObject PlayButton;
    public GameObject DemoButton;
    public GameObject ExitButton; 
    public GameObject trials;
    public GameObject instructions;
    public GameObject MM;
    public GameObject Menu;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTrials()
    {
        Menu.SetActive(false);
        trials.SetActive(true);
    }

    public void StartInstructions()
    {
        Menu.SetActive(false);
        instructions.SetActive(true);
    }

    public void BacktoMM()
    {
        Menu.SetActive(false);
        MM.SetActive(true);
    }
}

