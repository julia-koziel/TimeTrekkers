using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureIntroLogic : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject instructions;
    public GameObject instructions1;
    public GameObject instructions2;
    public GameObject instructions3;
    public GameObject trials;
    public GameEvent stageEnd;

    public BoolVariable RL;

    public GameObject MM;
    // Start is called before the first frame update
    void Start()
    {
        RL.Value=1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


public void StartInstructions()
{
    introScreen.SetActive(false);
    instructions.SetActive(true);
}

public void startTrials()
{
    introScreen.SetActive(false);
    stageEnd.Raise();
    
}

public void BacktoMM()
{
    gameObject.SetActive(false);
    MM.SetActive(true);
}

public void intro2()
{
    instructions1.SetActive(false);
    instructions2.SetActive(true);

}

public void intro3()
{
    instructions2.SetActive(false);
    instructions3.SetActive(true);
}


}

