using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_behaviour : MonoBehaviour
{

    public GameObject GoodDog;
    public GameObject BadDog;
    public GameObject Meteor;
    private GameLogicGo gameLogic;

    private int randomprob = 7;
    private float x;
    private float time;
    private float stopTime = 0;
    private float startTime = 0;
    private float velocity;

    public int ngood = 0;
    public int nbad = 0;
    public int gdog = -1;
    public int bdog = -1;
    private bool logged = false;
    private bool breakTrial = false; // Toggles break trial to draw back attention of child
    private MouseClickDino mouseClick; // Used to stop and start dog

    //---------------------------------------------------//
    // Added by Amschel, delete as needed. If integrating badDogsSeparatedUpdate() into Update(), you can remove 'public bool addBadDogSeparator;'
    public bool addBadDogSeparator = true;              // Switches to new code
    public int minBadDogSeparation = 1;          // minBadDogSeparation = X -> any two bad dog trials are always at least X apart
    public int badDogEveryXdogs = 6;             // badDogEveryXDogs = 6 --> 1 bad dog every 6
    private int badDogBuffer = 1;                // Used to track buffer needed between blocks to keep NoGos separated, set at 1 to avoid badDog on first trial
    private int[] currentBlock;                  // For randomisation per block (block based on badDogEveryXdogs)
    private int blockTrialTracker = 0;           // For iterating through blocks
    private int currentDog = -1;
    private enum dogType
    {
        GOOD = 0,
        BAD = 1,
        BREAK = 2
    }
    // End added
    //---------------------------------------------------//

    void Start()
    {
        Random.InitState(2);
        gameLogic = FindObjectOfType<GameLogicGo>();
        mouseClick = FindObjectOfType<MouseClickDino>();
        velocity = gameLogic.velocity;
    }

    void Update()
    {
        if (gameLogic.k)
        {
            time = gameLogic.time;
        }

        //---------------------------------------------------//
        // Added by Amschel, skips rest of function for easy deletion
        // (Can copy-paste 'badDogsSeparatedUpdate' function into here to integrate fully when approved)
        if (addBadDogSeparator)
        {
            badDogsSeparatedUpdate();
        }
        else
        {
            // Original code in here //
            //---------------------------------------------------//

            if (gameLogic.i && time > gameLogic.interval)
            { // so it runs only once
                randomprob = Random.Range(0, 6);
                print("number: " + randomprob);
                gameLogic.timetozero();
                time = gameLogic.time;
                gameLogic.pointer();

                if (randomprob < 5) { 
                    ngood += 1; // cumulative info
                    gdog = 1; // per trial info
                    bdog = 0; // per trial info
                }
                else if (randomprob < 6) {
                    nbad += 1;
                    gdog = 0;
                    bdog = 1;
                }
            }

            if (randomprob < 5)
            {

                GoodDog.SetActive(true);
                x = -13 + time * gameLogic.velocity;
                GoodDog.transform.position = new Vector3(x, -1.6f, 0);
                if (x > 13)
                {
                    gameLogic.reactivate_all();
                    randomprob = 7;
                }
            }
            else if(randomprob < 6)
            {

                BadDog.SetActive(true);
                x = -13 + time * gameLogic.velocity;
                BadDog.transform.position = new Vector3(x, -1.8f, 0);
                if (x > 13)
                {
                    gameLogic.reactivate_all();
                    randomprob = 7;
                }
            }
        }
    }

    //---------------------------------------------------//
    // Added by amschel, delete as appropriate, or copy into main Update() function
    void badDogsSeparatedUpdate() {
        
        if (gameLogic.i && time > gameLogic.interval)
        { // so it runs only once
            if (breakTrial)
            {
                currentDog = (int)dogType.BREAK;
                gameLogic.timetozero();
                time = gameLogic.time;
                gameLogic.pointer();
            }
            else
            {
                int trial = gameLogic.getTrial() - 1;                         // Trial indexing
                int nTrials = gameLogic.trialNumber;                          // Renamed for ease of reading

                blockTrialTracker = trial == 0 ? 0 : blockTrialTracker;       // Handles start and restarts
                if (trial == 0 || blockTrialTracker == currentBlock.Length)   // Create new shuffled block at end of old block, or at start of trials
                {
                    createNextBlock();
                    blockTrialTracker = 0;
                }
                currentDog = trial < nTrials ? currentBlock[blockTrialTracker]: -1;    // Reads trial type from currentBlock, and handles end of trial
                blockTrialTracker++;

                gameLogic.timetozero();
                time = gameLogic.time;
                gameLogic.pointer();

                switch (currentDog)
                {
                    case (int)dogType.GOOD:
                        ngood += 1; // cumulative info
                        gdog = 1; // per trial info
                        bdog = 0; // per trial info
                        break;

                    case (int)dogType.BAD:
                        nbad += 1;
                        gdog = 0;
                        bdog = 1;
                        break;
                    default:
                        break;
                }
            }
            
        }

        switch (currentDog)
        {
            case (int)dogType.GOOD:
                GoodDog.SetActive(true);

                x = -13 + time * gameLogic.velocity;
                GoodDog.transform.position = new Vector3(x, -1.6f, 0);

                if (x >= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x - 3 && !logged)
                {
                    gameLogic.logStartTime();
                    logged = true;
                }

                if (x > 13) 
                {
                    currentDog = -1; 
                    gameLogic.reactivate_all(); 
                    logged = false;
                }
                break;

            case (int)dogType.BAD:
                BadDog.SetActive(true);

                x = -13 + time * gameLogic.velocity;
                BadDog.transform.position = new Vector3(x, -1.8f, 0);

                if (x >= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x - 3 && !logged)
                {
                    gameLogic.logStartTime();
                    logged = true;
                }

                if (x > 13) 
                {
                    currentDog = -1; 
                    gameLogic.reactivate_all(); 
                    logged = false;
                }
                break;

            case (int)dogType.BREAK:
                GoodDog.SetActive(true);

                int startPoint = mouseClick.dogHasBeenStopped() ? 0 : -13;
                x = mouseClick.dogIsStopped() ? 0 : startPoint + time * gameLogic.velocity;
                
                if (x >= 0 && !mouseClick.dogHasBeenStopped())
                {
                    mouseClick.stopDog();
                    Meteor.SetActive(true);
                }

                if (!mouseClick.dogIsStopped())
                {
                    GoodDog.transform.position = new Vector3(x, -1.6f, 0);
                    Meteor.SetActive(false);
                }
                else
                {
                    GoodDog.transform.position = new Vector3(0, -1.6f, 0);
                }
                
                if (x > 13) 
                {
                    currentDog = -1; 
                    mouseClick.setDogHasBeenStopped(false);
                    gameLogic.reactivate_all(); 
                }
                break;

            default:
                break;
        }
    
    }
    // End of added update function //

    // Creates block by block array of good & bad dog trials (0s and 1s as per dogType enum)
    void createNextBlock() {
        
        int nTrials = gameLogic.trialNumber;                               // Renamed for ease of reading
        int blockLength = badDogEveryXdogs;                                // Renamed for ease of reading
        int badDogSep = minBadDogSeparation;                               // Renamed for ease of reading

        currentBlock = new int[blockLength];                               // Allows for possible online adjustment of blockLength

        System.Random rnd = new System.Random();                           // I ended up using System.Random rather than Unity.Random, hope this is ok          

        for (int j = 0; j < blockLength; j++)
        {
            currentBlock[j] = (int) dogType.GOOD;                          // Set default as goodDog (enum value 0)
        }
        int badDogIndex = rnd.Next(badDogBuffer, blockLength);             // Random index within block, kept greater than buffer from most recent bad dog
        currentBlock[badDogIndex] = (int) dogType.BAD;                     // Set bad dog value within block (enum value 1)
        
        // Buffer to track space needed from badDog trial in current block to badDog trial in next block
        int buffer = badDogIndex - (blockLength - 1 - badDogSep);      

        // So if current block [0,0,0,0,1,0], separation = 2, then buffer = 1 so that next block can't be [1,0,0,0,0,0]
        // As this would give [0,0,0,0,1,0][1,0,0,0,0,0], separation of only 1
        // In this example, badDogIndex = 4, blockLength = 6, badDogSep = 2 
        // -> buffer = 4 - (5 - 2) = 1                     [line 204]
        // -> in next block badDogIndex = rnd.Next(1,6)    [line 200]
        badDogBuffer = buffer > 0 ? buffer : 0;                            // Ignore buffer when negative
    }
    // End added by Amschel

    public bool isBreakTrial()
    {
        return breakTrial;
    }

    public void setBreakTrial(bool isBreakTrial)
    {
        breakTrial = isBreakTrial;
    }
}