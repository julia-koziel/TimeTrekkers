using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicRewardSocial : MonoBehaviour
{
    public GameObject StimuliPositive;
    public GameObject StimuliNegative;
    public GameObject[] StimuliPos;
    public GameObject[] StimuliNeg;
    public GameObject chicken;
    public GameObject crate;
    public GameObject instructions;
    public GameObject end;
    public GameObject canvas;
    public GameObject settings;
    public GameObject trials;

    public GameObject rateButton;
    public GameObject MM;

    // private CsvReadWritePassiveSocial csvReadWrite;

    public int trialNumber;
    public float trialTime;
    public bool restart = false;
    public float time = 0;
    public bool i = true;
    public bool j = true;
    public bool k = true;
    public bool m = true;
    public bool n = true;
    private int trial = 1;
    private int omissionerror = 0;
    private int comissionerror = 0;
    private int omissiontrial = 0;
    private int good = -1;
    private int bad = -1;
    private float startTime = 0;
    private float reactionTime = 0;
    private int correct_flag = 0;
    private int incorrect_flag = 0;
    private int hitrate = 0;
    private double dprime = 0;
    private int pos = -1;
    private Vector3[] positions;

    private string id;

    //  void Awake()
    // {
    //     string key = PrefsKeys.Keys.RLS_RatingType.ToString();
    //     int ratingType = PlayerPrefs.GetInt(key, 0);
    //     rating = ratingType == 0 ? rating1 : rating2;
    // }

    private void Start()
    {
        Random.InitState(3);
        positions = new Vector3[2];
        positions[0] = new Vector3(-1, 0, 0);
        positions[1] = new Vector3(1, 0, 0);
        pos = Random.Range(0, 2);

        foreach (GameObject Person in StimuliPos)
        {
            Person.transform.position = positions[pos];
        }

        foreach (GameObject Person in StimuliNeg)
        {
            Person.transform.position = positions[1 - pos];
        }
        
        crate.SetActive(true);

        // csvReadWrite = FindObjectOfType<CsvReadWritePassiveSocial>();
        // csvReadWrite.csvstart();

    }


    private void Update()
    {
        if (k) { time += Time.deltaTime; }

        if (restart)
        {
            comissionerror = 0; // wrongly clicked Person
            omissionerror = 0; // correct Person not clicked
            hitrate = 0; // number of correct clicks
            reactivateAll(); 
            trial = 1; // goes back to first trial
            j = true;
        }
    }
    public void reactivateAll()
    {
        

        if (trial < trialNumber)
        {
            print(trial);
            dprime = ((hitrate - comissionerror) / 1.0) / trial;

            // csvReadWrite.csvupdate(trial, pos, correct_flag, good, bad, dprime, 
            //  hitrate, comissionerror, reactionTime);
            
            pos = Random.Range(0, 2);
            foreach (GameObject Person in StimuliPos)
            {
                Person.transform.position = positions[pos];
                Person.SetActive(false);
            }

            foreach (GameObject Person in StimuliNeg)
            {
                Person.transform.position = positions[1 - pos];
                Person.SetActive(false);
            }

            chicken.SetActive(false);

            restart = false;

            time = 0;
            j = true;
            pointer();
            pointer3();
            int correct_bar = hitrate - comissionerror;
            

            //Debug.Log("---------------");
            //Debug.Log("---------------");
            //Debug.Log("Trial: " + trial);
            //Debug.Log("Omission Error: " + omissiontrial);
            //Debug.Log("Comission Error: " + comissiontrial);
            //Debug.Log("D-Prime: " + dprime);
            Debug.Log("Reaction time: " + reactionTime);
            trial += 1;
            omissiontrial = 0;
            print(trial);

        }

        else
        { // After number of trials run.

            //ps.SetActive(false);
            dprime = ((hitrate - comissionerror) / 1.0) / trial;
            // csvReadWrite.csvupdate(trial, pos, correct_flag, good, bad, dprime,
            //  hitrate, comissionerror, reactionTime);
            // csvReadWrite.output();
            StimuliPositive.SetActive(false);
            StimuliNegative.SetActive(false);
            restart = false;

            //Debug.Log("---------------");
            //Debug.Log("---------------");
            //Debug.Log("Trial: " + trial);
            //Debug.Log("Omission Error: " + omissiontrial);
            //Debug.Log("Comission Error: " + comissiontrial);
            //Debug.Log("D-Prime: " + dprime);
            //Debug.Log("Trial: " + trial);
            //Debug.Log("Game is over.");
            Debug.Log("Reaction time: " + reactionTime);
            time = 0;
            crate.SetActive(false);
            chicken.SetActive(false);
            rateButton.SetActive(true);
        }
    }

    public float setTime()
    { // updates time in different scripts.
        return time;
    }

    public void resetTime()
    {
        time = 0;
    }

    public void pointer()
    { //on/off system so that housesbehaviour is ran once.
        i = !i;
    }

    public void pointer2()
    { //on/off system so that housesbehaviour is ran once.
        j = false;
    }

    public void pointer3()
    { //on/off system so that housesbehaviour is ran once.
        n = !n;
    }


    public void correcthouse()
    {
        omissionerror -= 1;
        omissiontrial -= 1;
        hitrate += 1;
        correct_flag = 1;
        incorrect_flag = 0;
    }

    public void startcorrecthouse(){
        omissionerror += 1;
        omissiontrial += 1;
    }

    public void wronghouse()
    {
        comissionerror += 1;
        correct_flag = 0;
        incorrect_flag = 1;

    }

    public void Quit()
    {
        // csvReadWrite.output();
        MM.SetActive(true);
    }

    public void Open_settings()
    {
        settings.SetActive(true);
        canvas.SetActive(false);
        k = false;
    }

    public void Close_settings()
    {
        settings.SetActive(false);
        canvas.SetActive(true);
        k = true;
    }

    public void Trialnum(int trials)
    {
        trialNumber = trials;
    }

    public void Duration(float duration)
    {
        trialTime = duration;
    }

    public void score_toggle()
    {
        m = !m;
    }

    public void Bad_Person(int num)
    {
        bad = num; //which bad Person is being displayed
    }
    public void Good_Person(int num)
    {
        good = num; // which good Person is being displayed
    }

    public void logReactionTime()
    {
        reactionTime = time - startTime;
    }

    public void logStartTime()
    {
        startTime = time;
    }

    void returnID()
    {
        id = PlayerPrefs.GetString("ID");
    }

}

