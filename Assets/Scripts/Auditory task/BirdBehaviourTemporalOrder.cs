using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviourTemporalOrder : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject[] birds;

    public GameObject sound;
    public GameObject position1;
    public GameObject position2;
    public GameObject text;
    

    public GameObject instructions;
    private float time; 
    public int firstbird;
    public int secondbird;
    private Vector3[] positions;
    private GameLogicTemporalOrderTask gameLogic;

    public GameObject lowpitch;
    public GameObject highpitch;
    
    public int trialset;
    public int amplitudeset;
    public int presentationset;

    public int[] firstsound =  {0,1,2,3,4,5,6,7,8,9};
    public int[] secondsound
     = {0,1,2,3,4,5,6,7,8,9};

    private bool vibrationHasStarted;
    public bool sleepyactive;
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
        firstsoundlevel =0; 
        secondsoundlevel =0;
        gameLogic = FindObjectOfType<GameLogicTemporalOrderTask>();
        positions = new Vector3[4];
        positions[0] = new Vector3(0,0,0);
        positions[1] = new Vector3(-2.5f, 0, 0);
        positions[2] = new Vector3(2.5f, 0, 0);
        instructions.SetActive(false);
        firstsound[0] = 100;
        firstsound[1] = 105;
        firstsound[2] = 110;
        firstsound[3] = 115;
        firstsound[4] = 120;
        firstsound[5] = 125;
        firstsound[6] = 130;
        firstsound[7] = 135;
        firstsound[8] = 140;
        firstsound[9] = 145;
        text.SetActive(false);
    

        secondsound
        [0] = 200;
        secondsound
        [1] = 200;
        secondsound
        [2] = 200;
        secondsound
        [3] = 200;
        secondsound
        [4] = 200;
        secondsound
        [5] = 200;
        secondsound
        [6] = 200;
        secondsound
        [7] = 200;
        secondsound
        [8] = 200;
        secondsound
        [9] = 200;
        
        
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
        text.SetActive(false); 
        trialset = Random.Range(1,21);
        presentationset = Random.Range(0,2);
        closed=false;
    }


    if (time < gameLogic.lagtime)
    {

    foreach (GameObject bird in birds)
            {
                bird.SetActive(false);
               
    }
       
        if (time>1.5f && (time<2.5f))
        {
            birds[firstbird].SetActive(true);
            birds[secondbird].SetActive(true); 
            birds[firstbird].transform.position = positions[1];
            birds[secondbird].transform.position = positions[2];
         }   
            Debug.Log("firstbird");
            
        }

        if (time>2.5f && (time<3))
        {
            vibrationHasStarted = false;            
            sound1();
           
            // sounds[gameLogic.sound].SetActive(false);            
        }
        
        if (time > 3 && time < 3.75f)
        {   
            // gameLogic.sound=+1;
            sound2();
            // sounds[gameLogic.sound].SetActive(true);
        }

        if (time > 3.75f)
        {
            birds[secondbird].SetActive(false);
            catrating();
            gameLogic.sound =+1;
            vibrationHasStarted = false;
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

                case 3:
                firstbird=2;
                secondbird=1;
                break;

                case 4:
                firstbird=3;
                secondbird=4;
                break;

                case 5:
                firstbird=4;
                secondbird=2;
                break;

                case 6:
                firstbird =5;
                secondbird =3;
                break;

                case 7:
                firstbird = 3;
                secondbird=0;
                break;

                case 8:
                firstbird=4;
                secondbird=0;
                break;

                case 9:
                firstbird =5;
                secondbird =0;
                break;

                case 10:
                firstbird = 6;
                secondbird = 0;
                break;

                case 11:
                firstbird = 1;
                secondbird = 5;
                break;

                case 12:
                firstbird = 2;
                secondbird = 5;
                break;

                case 13:
                firstbird = 1;
                secondbird = 6;
                break;

                case 14:
                firstbird = 2;
                secondbird = 6;
                break;

                case 15:
                firstbird = 3;
                secondbird = 6;
                break;

                case 16:
                firstbird = 4;
                secondbird = 6;
                break;

                case 17:
                firstbird = 6;
                secondbird = 2;
                break;

                case 18:
                firstbird = 6;
                secondbird = 3;
                break;

                case 19:
                firstbird = 6;
                secondbird =4;
                break;

                case 20:
                firstbird = 6;
                secondbird = 5;
                break;

                case 21:
                firstbird = 5;
                secondbird = 6;
                break;
            }
    }

    public void sound1()
    {
        if (!vibrationHasStarted && presentationset <1)
            {
                vibrationHasStarted = true;
                highpitch.SetActive(true);
                firstincorrect=true;
            }

            if (!vibrationHasStarted && presentationset >0.9f)
            {
                vibrationHasStarted = true;
                lowpitch.SetActive(true);
                
               firstcorrect=true;
            }
    }

    public void sound2()
    {

        if (!vibrationHasStarted && presentationset <1)
        {
            vibrationHasStarted = true;
            highpitch.SetActive(true);
            secondcorrect=true;
        }

        if (!vibrationHasStarted && presentationset >0.9f)
            {
                vibrationHasStarted = true;
                lowpitch.SetActive(true);
                secondincorrect=true;
            }
    }



    public void catrating()
    {
        
        text.SetActive(true);
        closed = true;
       
        closed=true;
        birds[firstbird].SetActive(true);
        birds[secondbird].SetActive(true);
        instructions.SetActive(true);
        sound.SetActive(false);

        if (presentationset<1)
        {
            birds[firstbird].transform.position = positions[1];
            birds[secondbird].transform.position = positions[2];
        }
        
        if (presentationset>0)
        {
            birds[firstbird].transform.position = positions[2];
            birds[secondbird].transform.position = positions[1];
        }
        

    }
    
    
}