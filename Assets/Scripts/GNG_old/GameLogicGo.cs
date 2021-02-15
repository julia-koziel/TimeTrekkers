using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicGo : MonoBehaviour {

    public GameObject BadDog;
    public GameObject GoodDog;
    public GameObject Dogs;
    public GameObject end;
    public GameObject trials;
    public GameObject texts;
    public GameObject settings;
    public GameObject MM;

    private Dog_behaviour dog_Behaviour;
    private MouseClickDino mouseClick;
    private MouseClickBad mouseClickBad;
    private CsvReadGo csv;
    private bool isApplicationQuitting = false;
    public int trialNumber;
    public float velocity;
    public FloatVariable speed;
    public float interval = 1.5f;
    public float time;
    public int level;
    public bool restart = false;
    public bool quit = false;
    public bool i = true;
    public bool j = true;
    public bool k = true;
    private int trial = 1;
    public int count = 0;
    public int wrong = 0;
    public double correctprop;
    public double wrongprop;
    private double inindex;
    private int clicked = 0;
    private int missedHits = 0;
    private float startTime = 0;
    private float reactionTime = 0;
    private string id;

    void Awake()
    {
        string velocitylevel = PrefsKeys.Keys.GNG_Level.ToString();
        level = PlayerPrefs.GetInt(velocitylevel, level);
        velocity = speed;

    }
    
    void Start () {

        mouseClick = FindObjectOfType<MouseClickDino>();
        mouseClickBad = FindObjectOfType<MouseClickBad>();
        dog_Behaviour = FindObjectOfType<Dog_behaviour>();
        csv = FindObjectOfType<CsvReadGo>();
        csv.csvstart();
        returnID();
    }
	
	// Update is called once per frame
	void Update () {

        if (k)
        {
            time += Time.deltaTime;
        }
        if (restart)
        {
            trial = 0;
            reactivate_all();
            i = true;


        }
    }

    public void reactivate_all(){

        if (dog_Behaviour.isBreakTrial())
        {
            j = true;
            csv.csvupdate(id, -1, -1, -1, -1, -1, -1, -1); // signals BREAK trial, needs writing into SOP
            // MouseClick1.SetActive(false);
            // MouseClick2.SetActive(false);
            BadDog.SetActive(false);
            GoodDog.SetActive(false);
            BadDog.transform.position = new Vector3(-13, 0, 0);
            GoodDog.transform.position = new Vector3(-13, 0, 0);
            time = 0;
            // NB trial not increased, breakTrial not counted in trialCount
            pointer();
            missedHits = 0; // resets counter
            dog_Behaviour.setBreakTrial(false);
        }
        else if (trial < trialNumber)
        {
            j = true;
            correctprop = (double)count / dog_Behaviour.ngood;
            wrongprop = (double)wrong / dog_Behaviour.nbad;
            inindex = correctprop / wrongprop;
            csv.csvupdate(id, trial, velocity, dog_Behaviour.gdog, dog_Behaviour.bdog,
                          clicked, inindex, reactionTime);

            if (clicked == 0 && dog_Behaviour.gdog == 1)
            {
                missedHits++;
                // If child misses 5 Go trials, next (uncounted) trial is break trial
                // Break trial = dog stops in middle and barks
                if (missedHits >= 5) { 
                    dog_Behaviour.setBreakTrial(true); 
                }
            }

            Debug.Log("---------------");
            Debug.Log("---------------");
            Debug.Log("Trial: " + trial);
            Debug.Log("corrects: " + count + "/" + dog_Behaviour.ngood);
            Debug.Log("wrongs: " + wrong + "/" + dog_Behaviour.nbad);
            Debug.Log("Inhibition Index: " + inindex);
            Debug.Log("RT: " + reactionTime);


            BadDog.SetActive(false);
            GoodDog.SetActive(false);
            BadDog.transform.position = new Vector3(-13, 0, 0);
            GoodDog.transform.position = new Vector3(-13, 0, 0);
            time = 0;
            trial += 1;
            pointer();

        }
        else{
            j = true;
            correctprop = (double)count / dog_Behaviour.ngood;
            wrongprop = (double)wrong / dog_Behaviour.nbad;
            inindex = correctprop / wrongprop;
            csv.csvupdate(id, trial, velocity, dog_Behaviour.gdog, dog_Behaviour.bdog,
                          clicked, inindex, reactionTime);
            csv.output();
            Debug.Log("Game is over");
            Dogs.SetActive(false);
            time = 0;
            end.SetActive(true);
           trials.SetActive(false);
        }

        if(quit){
            csv.output();
            Debug.Log("Game is over");
            end.SetActive(true);
            Dogs.SetActive(false);
            trials.SetActive(false);
        }

        clicked = 0;

    }
    public int getTrial()
    {
        return trial;
    }
    public void pointer()
    { //on/off system so that it is ran once.
        i = !i;
    }

    public void pointer2()
    { //on/off system so that it is ran once.
        j = !j;
    }

    public void countplus(){
        count += 1;
        clicked = 1;
        missedHits = 0; // resets when dog clicked
    }

    public void wrongclick(){
        wrong += 1;
        clicked = 1;
    }

    public void timetozero(){
        time = 0;
    }

    public void Quit()
    {
        csv.output();
        MM.SetActive(true);
    }

    public void Open_settings()
    {
        settings.SetActive(true);
        texts.SetActive(false);
        k = false;
    }

    public void Close_settings()
    {
        settings.SetActive(false);
        texts.SetActive(true);

        k = true;
    }

    public void Trialnum(int trials)
    {
        trialNumber = trials;
    }

    // public void Velocity(float velocity_set)
    // {
    //     velocity = velocity_set;
    // }

    public void Interval(float wait_time)
    {
        interval = wait_time;
    }

    public void logReactionTime()
    {
        Debug.Log(time);
        reactionTime = time - startTime;
    }

    public void logStartTime()
    {
        Debug.Log(time);
        startTime = time;
    }

    void OnDisable()
    {
        if (isApplicationQuitting) 
        {
            csv.output();
        }

    }

    void returnID()
    {
        id = PlayerPrefs.GetString("ID");
    }
 

    void OnApplicationQuit () {
        isApplicationQuitting = true;
    }
}
