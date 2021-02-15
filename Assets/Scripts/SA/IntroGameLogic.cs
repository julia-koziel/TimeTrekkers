using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameLogic : MonoBehaviour
{
    public GameObject StartButton;
    public GameObject DemoButton;
    public GameObject ExitButton;
    public GameObject instructions;
    public GameObject instructions2;
    public GameObject trials;
    public GameObject introscreen;
    public GameObject pretest;
    private startDemo demoClick;
    private GameLogicPirateAttack localtime;
    private GameObject text;
    private float time; 

   
    // Start is called before the first frame update
    void Start()
    {
      StartButton.SetActive(true);
      DemoButton.SetActive(true);
      ExitButton.SetActive(true);  
      text.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
    }

    public void Pretest()
    {
        pretest.SetActive(true);
        introscreen.SetActive(false);
    }

    public void intro2()
        {
            Debug.Log("intronext");
            instructions.SetActive(false);
            instructions2.SetActive(true);
            introscreen.SetActive(false);
            text.SetActive(false);

        }

    public void backtomenu()
        {
            instructions2.SetActive(false);
            introscreen.SetActive(true);
        }

    public void StartTrials()
    {
        trials.SetActive(true);
        instructions2.SetActive(false);
        gameObject.SetActive(false);
    }
}
