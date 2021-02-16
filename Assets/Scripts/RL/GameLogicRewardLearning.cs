using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicRewardLearning : MonoBehaviour
{
    public GameObject chestgeneral_good;
    public GameObject chestgeneral_bad;
    public GameObject[] chestsCorrect;
    public GameObject[] chestsIncorrect;

    public GameObject[] opens;
    public GameObject[] scores;
    public GameObject instructions;
    public GameObject canvas;
    public GameObject settings;
    // public GameObject MM;
    public GameObject end;

    // private CsvReadWriteTreasure csvReadWrite;

    public int trialNumber;
    public float trialTime;
    public bool restart = false;
    public float time = 0;
    public bool i = true;
    public bool j = true; // so that only the first click counts
    public bool k = true;
    public bool m = true;
    public bool n = true;
    private int trial = 1;
    private int omissionerror = 0;
    private int comissionerror = 0;
    private int omissiontrial = 0;
    private int bad = 0;
    private int good = 0;
    private float startTime = 0;
    private float reactionTime = 0;
    
    private int hitrate = 0;
    private float dprime = 0;
    private int prev_correct_bar = 0;
    private int coins = 0;
    private int pos = -1;
    private int comissiontrial;
    private Vector3[] positions;

    private void Start()
    {
        Random.InitState(3);
        positions = new Vector3[2];
        positions[0] = new Vector3(-2.75f, 0.75f, 0);
        positions[1] = new Vector3(2.75f, 0.75f, 0);
        pos = Random.Range(0, 2);

        foreach (GameObject chest in chestsCorrect)
        {
            chest.transform.position = positions[pos];
            Debug.Log(chest.transform.position);
        }

        foreach (GameObject chest in chestsIncorrect
)
        {
            chest.transform.position = positions[1-pos];
            Debug.Log(chest.transform.position);
        }

        // csvReadWrite = FindObjectOfType<CsvReadWriteTreasure>();
        // csvReadWrite.csvstart();

        foreach (GameObject score in scores) 
        {                                    
            score.SetActive(false);
        }

    }


    private void Update()
    {
        if (k) { time += Time.deltaTime; }

        if (restart)
        {
            comissionerror = 0; // wrongly clicked chest
            omissionerror = 0; // correct chest not clicked
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
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            // csvReadWrite.csvupdate(trial, pos, correct_flag, good, bad, dprime, hitrate, comissionerror, reactionTime);
            
            pos = Random.Range(0, 2);
            foreach (GameObject chest in chestsCorrect)
            {
                chest.transform.position = positions[pos];
                chest.SetActive(false);
            }
            foreach (GameObject chest in chestsIncorrect
    )
            {
                chest.transform.position = positions[1-pos];
                chest.SetActive(false);
            }

            foreach (GameObject open in opens)
            {
                open.SetActive(false);
            }

            foreach (GameObject score in scores) // presenting the score on the right
            {                                    // through chestes
                score.SetActive(false);
            }

           
            restart = false;

            time = 0;
            j = true;
            pointer(); // good chestes (i)
            pointer3(); // bad chestes (n)

            int correct_bar = hitrate - comissionerror;
            coins += correct_bar - prev_correct_bar;
            print(correct_bar);
            print(hitrate);
            if (coins < 0)
            {
                coins = 0;
            }

            if (coins > 0 && m) // activates coins according to score
            {
                for (int l = 0; l < coins; l++)
                {
                    scores[l].SetActive(true);
                }
            }

            prev_correct_bar = hitrate - comissionerror;
            
            

            //Debug.Log("---------------");
            //Debug.Log("---------------");
            //Debug.Log("Trial: " + trial);
            //Debug.Log("Omission Error: " + omissiontrial);
            //Debug.Log("Comission Error: " + comissiontrial);
            //Debug.Log("D-Prime: " + dprime);
            Debug.Log("Reaction time: " + reactionTime);
            trial += 1;
            comissiontrial = 0;
            omissiontrial = 0;

        }

        else
        { // After number of trials run.
            foreach (GameObject score in scores) // presenting the score on the right
            {                                    // through chestes
                score.SetActive(false);
            }
        
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            // csvReadWrite.csvupdate(trial, pos, correct_flag, good, bad, dprime, hitrate, comissionerror, reactionTime);
            // csvReadWrite.output();
            chestgeneral_good.SetActive(false);
            chestgeneral_bad.SetActive(false);
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
            end.SetActive(true);
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

    public void pointer() // for good chest launch
    { 
        i = !i;
        Debug.Log("chestreactivate");
    }

    public void pointer2() // for rendering effect after click
    { 
        j = false;
    }

    public void pointer3() // for good chest launch
    {
        n = !n;
    }

    public void correcthouse()
    {
        hitrate += 1;
    }

    public void startcorrecthouse(){
        omissionerror += 1;
        omissiontrial += 1;
    }

    public void wronghouse()
    {
        comissionerror += 1;  
    }

    // public void Quit()
    // {
    //     // csvReadWrite.output();
    //     MM.SetActive(true);

    // }

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

    public void Bad_chest(int num)
    {
        bad = num; //which bad chest is being displayed
    }
    public void Good_chest(int num)
    {
        good = num; // which good chest is being displayed
    }

    public void logReactionTime()
    {
        reactionTime = time - startTime;
    }

    public void logStartTime()
    {
        startTime = time;
    }
}

