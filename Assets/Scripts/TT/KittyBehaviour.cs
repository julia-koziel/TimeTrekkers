using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolShed.Android.OS;

public class KittyBehaviour : MonoBehaviour

{    public GameObject[] cats;
    public GameObject[] sleepycats;
    private AudioSource purring;
    public GameObject singlebed2;
    public GameObject singlebed;
    public GameObject text;
    public GameObject placeholder;
    public GameObject placeholder2;

    public GameObject instructions;
    private float time; 
    public int firstcat;
    public int secondcat;
    private Vector3[] positions;
    private GamelogictrialsKitty gameLogic;
    public int trialset;
    public int amplitudeset;
    
    public int vibrationpresentation;

    public int[] loweramplitude =  {0,1,2,3,4,5,6,7,8,9};
    public int[] higheramplitude
     = {0,1,2,3,4,5,6,7,8,9};

    private bool vibrationHasStarted;
    public bool sleepyactive;
    public int firstamplevel; 
    public int secondamplevel;
    public GameObject sleepbubble;
    
    public bool firstcorrect;
    public bool secondcorrect;
    private bool g = true;
    public bool firstincorrect;
    public bool secondincorrect;

    // Start is called before the first frame update
    void Start()
    {
        firstamplevel =0; 
        secondamplevel =0;
        gameLogic = FindObjectOfType<GamelogictrialsKitty>();
        purring = GetComponent<AudioSource>();
        positions = new Vector3[4];
        positions[0] = singlebed.transform.position;
        positions[1] = placeholder.transform.position;
        positions[2] = placeholder2.transform.position;
        instructions.SetActive(false);
        loweramplitude[0] = 100;
        loweramplitude[1] = 105;
        loweramplitude[2] = 110;
        loweramplitude[3] = 115;
        loweramplitude[4] = 120;
        loweramplitude[5] = 125;
        loweramplitude[6] = 130;
        loweramplitude[7] = 135;
        loweramplitude[8] = 140;
        loweramplitude[9] = 145;
        text.SetActive(false);
    

        higheramplitude
        [0] = 200;
        higheramplitude
        [1] = 200;
        higheramplitude
        [2] = 200;
        higheramplitude
        [3] = 200;
        higheramplitude
        [4] = 200;
        higheramplitude
        [5] = 200;
        higheramplitude
        [6] = 200;
        higheramplitude
        [7] = 200;
        higheramplitude
        [8] = 200;
        higheramplitude
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
        vibrationpresentation = Random.Range(0,2);
        sleepycats[gameLogic.sleepycat].SetActive(true);
        singlebed.transform.position = positions[0];
        singlebed2.SetActive(false);
        sleepbubble.SetActive(true);
        text.SetActive(false);
    }

    if (gameLogic.trial<2)
    {
        firstcat=0;
        secondcat=2;
    }

    if ((time < gameLogic.lagtime) && (gameLogic.trial > 1.5f))
    {

    foreach (GameObject cat in cats)
            {
                cat.SetActive(false);
                cat.transform.position = positions[0];
            } 
        trialset = Random.Range(1,11);
    }

        if ((time > gameLogic.lagtime) && (time <gameLogic.lagtime + gameLogic.iti))
    //switchcase to randomise presentation
        {   
            foreach (GameObject sleepycat in sleepycats)
            {
                sleepycat.SetActive(false);
            } 
           sleepbubble.SetActive(false);
        }

        if ((time>gameLogic.lagtime + gameLogic.iti) && time <8)
        {
            cats[firstcat].SetActive(true);
            Vibration1();
        }

        if ((time > 9) && time < 10)
        {
            vibrationHasStarted = false;
            cats[firstcat].SetActive(false);            
        }
        
        if (time > 10 && time < 12)
        {   
            gameLogic.sound=+1;
            cats[secondcat].SetActive(true); 
            Vibration2();
        }

        if (time > 13)
        {
            cats[secondcat].SetActive(false);
            catrating();
            gameLogic.sound =+1;
            vibrationHasStarted = false;
        }

        switch(trialset)
            {
                case 1:
                firstcat =0;
                secondcat=1;
                break;

                case 2:
                firstcat =1;
                secondcat =0;
                break;

                case 3:
                firstcat=2;
                secondcat=1;
                break;

                case 4:
                firstcat=3;
                secondcat=4;
                break;

                case 5:
                firstcat=4;
                secondcat=2;
                break;

                case 6:
                firstcat =2;
                secondcat =3;
                break;

                case 7:
                firstcat = 3;
                secondcat=0;
                break;

                case 8:
                firstcat=4;
                secondcat=0;
                break;

                case 9:
                firstcat =4;
                secondcat =3;
                break;

                case 10:
                firstcat = 2;
                secondcat = 4;
                break;

            }

    }

    public void catrating()
    {
        singlebed.SetActive(true);
        singlebed2.SetActive(true);
        text.SetActive(true);
        singlebed2.SetActive(true);
        singlebed.transform.position = positions[1];
        singlebed2.transform.position = positions[2];
        cats[firstcat].transform.position = positions[1];
        cats[secondcat].transform.position = positions[2];
        cats[firstcat].SetActive(true);
        cats[secondcat].SetActive(true);
        instructions.SetActive(true);
        

    }
    

    public void Vibration1()
        {
            if (!vibrationHasStarted && vibrationpresentation <1)
            {
                vibrationHasStarted = true;
                Vibrator.Vibrate(500,loweramplitude[firstamplevel]);
                Debug.Log("vibrationlower");
                firstincorrect=true;
            }

            if (!vibrationHasStarted && vibrationpresentation >0.9f)
            {
                vibrationHasStarted = true;
                Vibrator.Vibrate(500, higheramplitude[secondamplevel]);
                Debug.Log("vibrationhigher");
               firstcorrect=true;
            }
        
        }

    public void Vibration2()
    {

        if (!vibrationHasStarted && vibrationpresentation <1)
        {
            vibrationHasStarted = true;
            Vibrator.Vibrate(500, higheramplitude[secondamplevel]);
            Debug.Log("vibrationhigher");
            secondcorrect=true;
        }

        if (!vibrationHasStarted && vibrationpresentation >0.9f)
            {
                vibrationHasStarted = true;
                Vibrator.Vibrate(500, loweramplitude[secondamplevel]);
                Debug.Log("vibrationlower");
                secondincorrect=true;
            }
    }
    
    
}

