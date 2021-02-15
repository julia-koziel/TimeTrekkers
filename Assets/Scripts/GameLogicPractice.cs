using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicPractice : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject singlebed;
    public GameObject singlebed2;
    public GameObject[] cats;

    public GameObject[] sleepy;

    public int sound;
    public int cummulativecorrect;

    public GameObject end;
    public float time;

    public float lagtime = 9f;

    public float catpres = 2f;
    public float iti = 2f;

    private bool l = true;

    public int trial =1;

    private bool k = true;

    public int correct;
    public int firstvibration;

    public int secondvibration;
    public bool catIsClickable = false;

    public GameObject trials;
    private KittyBehaviourPractice kittyBehaviour;
    
    public int sleepycat;

    private Vector3[] positions;
    
    // Start is called before the first frame update
    void Start()
    {
        cummulativecorrect=0;
        kittyBehaviour = FindObjectOfType<KittyBehaviourPractice>();
        positions = new Vector3[3];
        positions[0] = new Vector3(0,1,0);
        positions[1] = new Vector3(-2, 1, 0);
        positions[2] = new Vector3(2, 1, 0);

    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime; 
    

        if (trial>3)
        {
            trials.SetActive(false);
            end.SetActive(true);
        }

        if (time > 13)
        {
            catIsClickable = true;
        }
    }

    public void newtrial()
    {   
        foreach (GameObject cat in cats)
        {
            cat.SetActive(false);
        }
        singlebed2.SetActive(false);
        singlebed.transform.position = positions[0];
        time =0;
        trial+=1;
        Debug.Log("newtrial");
        catIsClickable = false;
        firstvibration = kittyBehaviour.firstamplevel;
        secondvibration = kittyBehaviour.secondamplevel;
        
        if (k)
        {
            correct = 1;
        }

        if (!k)
        {
            correct = 0;
        }
    }      



    public float SetTime()
    {
        return time;
    }

    


}
