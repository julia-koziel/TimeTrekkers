using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicSpatial : MonoBehaviour
{
    public GameObject[]  stimuli;
    public GameObject seaweed1;
    public GameObject seaweed2;
    public GameObject seaweed3;
    public GameObject seaweed4;
    public GameObject seaweed5;
    public GameObject seaweed6;
    public GameObject seaweed7;
    public GameObject seaweed8;
    public GameObject seaweed9;

    public Vector3[] positions;
    public int pos;
    public int trial;
    public int stimulus;
    public int index;
    public float time;
    private float InterStimulus=1.5f;
    public int TrialNumber=30;
    public int currentstimulus;
    private bool k =true;
    // int[] PositionSequence = new int[];

    int[] StimuliSequence = new int [] {6, 2, 3, 4, 1};

    // Start is called before the first frame update
    void Start()
   
    {

    positions[0] = seaweed1.transform.position;
    positions[1] = seaweed2.transform.position;
    positions[2] = seaweed3.transform.position;
    positions[3] = seaweed4.transform.position;
    positions[4] = seaweed5.transform.position;
    positions[5] = seaweed6.transform.position;
    positions[6] = seaweed7.transform.position;
    positions[7] = seaweed8.transform.position;
    positions[8] = seaweed9.transform.position;

    trial=0;
    time=0;
    index=StimuliSequence[currentstimulus];
    currentstimulus=0;

    foreach (GameObject stimulus in stimuli)
    {
        stimulus.SetActive(false);

    }

    }
    // Update is called once per frame
    void Update()
    {
        
    time += Time.deltaTime;

    if (k)
    {
        Trials();
    }   
    else if (time>InterStimulus)
        {
            time = 0;
            pos = Random.Range(0, 8);
            
            stimulus++;
            stimuli[stimulus].SetActive(true);
            stimuli[stimulus].transform.position = positions[pos];
    }

    }

    public void Trials()
{
    k=false;
    stimulus =0;
    pos = Random.Range(0,8);
    if (trial < TrialNumber)

    {   stimulus = Random.Range(0,8);
        stimuli[stimulus].SetActive(true);
        stimuli[stimulus].transform.position = seaweed1.transform.position;
    
     if(stimulus>4)
    {
        Trials();
    }   

}
}
public void CurrentSequence()
{
// Change sequence every trial 

}

void sequenceLegth()
{
 //check if sequence should be changed based on correct/incorrect answers
}

}
