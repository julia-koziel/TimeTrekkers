using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameLogicQueens : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject instructions;
    public GameObject instructions1;
    public GameObject instructions2;
    public GameObject instructions3;
    public GameObject trials;
    public GameEvent StageEnd; 

    public GameObject MM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


public void StartInstructions()
{
    StageEnd.Raise();
    gameObject.SetActive(false);  
    introScreen.SetActive(false);
}

public void BacktoMM()
{
    MM.SetActive(true);
}


}
