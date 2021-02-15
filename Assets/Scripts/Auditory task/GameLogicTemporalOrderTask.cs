using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicTemporalOrderTask : MonoBehaviour
{
  
    public GameObject[] birds;
    public GameObject text;

    public int sound;
    public int cummulativecorrect;

    public GameObject end;
    public float time;

    public float lagtime = 0.5f;

    public float catpres = 1;
    public float iti = 2f;

    private bool l = true;

    public int trial =1;

    private bool k = true;

    public int correct;
    public int firstvibration;

    public int secondvibration;
    public bool birdIsClickable = false;

    public GameObject trials;
    private BirdBehaviourTemporalOrder birdBehaviour;
    // private AuditoryReadWriteCsv csvReadWrite;
    


    private Vector3[] positions;
    
    // Start is called before the first frame update
    void Start()
    {
        cummulativecorrect=0;
        birdBehaviour = FindObjectOfType<BirdBehaviourTemporalOrder>();
        positions = new Vector3[3];
        positions[0] = new Vector3(0,1,0);
        positions[1] = new Vector3(-2, 1, 0);
        positions[2] = new Vector3(2, 1, 0);
        // csvReadWrite = FindObjectOfType<AuditoryReadWriteCsv>();
        // csvReadWrite.csvstart();

        
    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime; 
    

        if (trial>15)
        {
            trials.SetActive(false);
            end.SetActive(true);
        }

    }

    public void newtrial()
    {
        // csvReadWrite.csvupdate(trial, cummulativecorrect);
        text.SetActive(false);
        foreach (GameObject bird in birds)
        {
            bird.SetActive(false);
        }

        time =0;
        trial+=1;
        Debug.Log("newtrial");
        birdIsClickable = false;
        firstvibration = birdBehaviour.firstsoundlevel;
        secondvibration = birdBehaviour.secondsoundlevel;
    
    }      



    public float SetTime()
    {
        return time;
    }

    
}
