using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject[] birds;
   public GameObject [] stimuliset1;
   public GameObject [] stimuliset2;

   public GameObject singing1;
   public GameObject singing2;  

    public GameObject instructions;
    private float time; 
    public int firstbird;
    public int secondbird;
    private Vector3[] positions;
    private GameLogicAuditoryTrials gameLogic;

    public GameObject lowpitch;
    public GameObject highpitch;
    
    public int trialset;
    public int amplitudeset;
    public int presentationset;

    public int stimulus=0;
    public int secondsound =5;

    private bool vibrationHasStarted;
    public int firstsoundlevel; 
    public int secondsoundlevel;
    
    public bool firstcorrect;
    public bool secondcorrect;
    public bool firstincorrect;
    public bool secondincorrect;
    public bool closed= false;


    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicAuditoryTrials>();
        positions = new Vector3[4];
        positions[0] = new Vector3(0,0,0);
        instructions.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
    time = gameLogic.SetTime();

    if (time<gameLogic.lagtime)
    
    { 
        firstcorrect=false;
        secondcorrect=false;
        firstincorrect=false;
        secondincorrect=false;
        instructions.SetActive(false);
    
        
        closed=false;
    }


    if (time < gameLogic.lagtime)
    {
       foreach (GameObject bird in birds)
        {
            bird.SetActive(true);
        }
        presentationset = Random.Range(0,2);
    }

        if (time>2.5f && (time<3.5f))
        {
            vibrationHasStarted = false;            
            sound1();
            singing1.SetActive(true);
            singing2.SetActive(false);
           
        }
        
        if (time > 3.5f && time < 4f)
        {   
            singing1.SetActive(false);
        }

        if (time>4f && time <5f)
        {
            singing2.SetActive(true);
            sound2();
        }

        if (time > 5f)
        {
            singing1.SetActive(false);
            singing2.SetActive(false);
            birdrating();
            vibrationHasStarted = false;
            gameLogic.birdIsClickable = true;

            foreach (GameObject stim in stimuliset1)
            {
                stim.SetActive(false);
            }
        }

        
        switch(trialset)
            {
                case 1:
                firstbird =0;
                secondbird=1;
                break;

                case 2:
                firstbird =1;
                secondbird =0;
                break;

            }
    }

    public void sound1()
    {
        Debug.Log("sound1");

        if ( presentationset <1)
            {
                vibrationHasStarted = true;
                stimuliset1[stimulus].SetActive(true);
                firstcorrect=true;
            }

            if ( presentationset >0.5f)
            {
                vibrationHasStarted = true;
                stimuliset1[5].SetActive(true);
                firstincorrect=true;
            }
    }

    public void sound2()
    {

        if (presentationset <1)

            {
                vibrationHasStarted = true;
                stimuliset1[5].SetActive(true);
                secondincorrect=true;
            } 
            
            
        if (presentationset >0.5f)
        
        {
            vibrationHasStarted = true;
            stimuliset1[stimulus].SetActive(true);
            secondcorrect=true;
        }
    }

    public void birdrating()
    {
        closed = true;
        birds[firstbird].SetActive(true);
        birds[secondbird].SetActive(true);
        instructions.SetActive(true);
        
    }

    // public void ChangeStimuliSet();
    // {
    //     foreach (GameObject stimulus in stimuliset1)
    //     {
    //         stimulus.SetActive(false);
    //     } 
    // }
    
   
}