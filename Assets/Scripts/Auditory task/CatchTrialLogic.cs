using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTrialLogic : MonoBehaviour
{
    public GameObject[] birds;
    public GameObject squirrel;

   public GameObject singing1;
   public GameObject singing2; 

   
    public GameObject text;
    

    public GameObject instructions;
    public GameObject auditoryTrials;
    private float time; 
    public int firstbird;
    public int secondbird;
    private Vector3[] positions;
    private GameLogicAuditoryTrials gameLogic;

    public int trialset;
    public int amplitudeset;
    public int presentationset;

    public bool closed= false;
    public bool stand = false;


    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicAuditoryTrials>();
        positions = new Vector3[4];
        positions[0] = new Vector3(0,0,0);
        instructions.SetActive(false);
        text.SetActive(false);

     
    }
    // Update is called once per frame
    void Update()
    {
    time = gameLogic.SetTime();

    if (time<gameLogic.lagtime)
    
    { 
    
        instructions.SetActive(false);
        text.SetActive(false); 
        
        closed=false;
    }


    if (time < gameLogic.lagtime)
    {
        squirrel.SetActive(true);
    }

    if (time > 2)
    {
        stand = true;
    }


    if (time > 5f)
    {
        gameObject.SetActive(false);
        gameLogic.auditoryTrial();
    }
    }

}
    
    