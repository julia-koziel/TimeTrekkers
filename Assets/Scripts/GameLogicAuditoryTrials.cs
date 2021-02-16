using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicAuditoryTrials : MonoBehaviour
{
  
    public GameObject AuditoryTrials;
    public GameObject CatchTrials;
    public GameObject[] birds;
    public GameObject text;

    public GameObject squirrel;
    public GameObject end;
    
    public float time;
    public float lagtime = 0.5f;
    public float catpres = 1;
    public float iti = 2f;

    public int trial =1;
    public int trialNumber;
    public int cummulativecorrect;
    public int correct;
    public int previoustrial;
    public int firstvibration;
    public int secondvibration;
    public bool birdIsClickable = false;
    public bool reversal = false;

    public GameObject trials;
    private BirdBehaviour birdBehaviour;
    private AuditoryReadWriteCsv csvReadWrite;
    


    private Vector3[] positions;
    
    // Start is called before the first frame update
    void Start()
    {
        cummulativecorrect=0;
        birdBehaviour = FindObjectOfType<BirdBehaviour>();
        positions = new Vector3[3];
        positions[0] = new Vector3(0,1,0);
        positions[1] = new Vector3(-2, 1, 0);
        positions[2] = new Vector3(2, 1, 0);
        csvReadWrite = FindObjectOfType<AuditoryReadWriteCsv>();
        csvReadWrite.csvstart();

        
    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime; 
    

        if (trial>trialNumber)
        {
            trials.SetActive(false);
            end.SetActive(true);
        }

    }



    public void newtrial()
    {
        if (trial ==3 || trial==7 || trial==12)
        {
            CatchTrials.SetActive(true);
            AuditoryTrials.SetActive(false);
            time=0;
        }

        else
        {
            auditoryTrial();
            Stimuli();
            // CalculateReversal();
        }
        
    }      

    public void auditoryTrial()

    {   
        CatchTrials.SetActive(false);
        AuditoryTrials.SetActive(true);
        // csvReadWrite.csvupdate(trial, cummulativecorrect);
        text.SetActive(false);
        foreach (GameObject bird in birds)
        {
            bird.SetActive(false);
        }
        time=0;
        trial+=1;
        Debug.Log("newtrial");
        birdIsClickable = false;
        previoustrial = correct;
        
        
    }

    public void Stimuli()
    {
        
        if (correct==1)
        {
            birdBehaviour.stimulus++;
        }

       else if (correct==0)
       {
           if (birdBehaviour.stimulus>0)
           {birdBehaviour.stimulus--;}
       }

    }


    // public void calculateReversal();
    // {
    //     if (previoustrial==1 & correct==0)
    //     {
    //         reversal = true;
    //     }
    // }

    public float SetTime()
    {
        return time;
    }

    
}
