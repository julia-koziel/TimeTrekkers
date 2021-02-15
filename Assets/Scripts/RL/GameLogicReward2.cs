using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogicReward2 : MonoBehaviour
{
    public GameObject[] boxes;
    public GameObject ITI;
    public GameObject level1;
    public GameObject level2;
    private AudioSource[] sounds; // 1 correct, 0 incorrect
    public GameObject[] scores;
    public GameObject instructions2;
    public GameObject canvas;
    public GameObject settings;
    public GameObject MM;
    public GameObject end;
    public GameObject oldtext;
    public string hitratetext;

    private CsvReadWriteTreasure csvReadWrite;
    private bool isApplicationQuitting = false;

    public bool restart = false;
    public float time = 0.0f;
    public bool j = true; // so that only the first click counts
    public bool m = true; // coins on settings

    List<int> box_correct = new List<int>();
    List<int> box_wrong = new List<int>();

    public int trial = 1;
    public int n_trials = 40;
    private int comissionerror = 0;

    private float startTime = 0;
    private float reactionTime = 0;
    private int correct_flag = 0;
    public int hitrate = 0;
    private double dprime = 0;
    private int prev_correct_bar = 0;
    private int coins = 0;
    private int pos = -1;
    private int box_shown = -1;
    private int box2_shown = -1;
    private int prev_box_shown = -1;
    private int prev_box2_shown = -1;
    public int consecutivehitrate = 0;
    public int level=1;

    private string id; 

    private Vector3[] positions = {
        new Vector3(-3, 0, 1), // left
        new Vector3(3, 1, 1),// right
    };

    private State stateToLoad = State.None;
    private State currentState = State.None;


    private enum State
    {
        None,
        Trial, TrialClick,
        End
    }

    void Start()
    {
        Random.InitState(3);
        box_shown = 0;
        box2_shown = 1;

        foreach (GameObject score in scores)
        {
            score.SetActive(false);
        }
        foreach (GameObject box in boxes)
        {
            box.SetActive(false);
        }

        csvReadWrite = FindObjectOfType<CsvReadWriteTreasure>();
        csvReadWrite.csvstart();

        sounds = gameObject.GetComponents<AudioSource>();
        stateToLoad = State.Trial;
        returnID();
        oldtext.SetActive(false);
    }


    void Update()
    {
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {

            case State.Trial: // Setting of the 1st pair (correct click)
                logStartTime();
                pos = Random.Range(0, 2); // Position of box_shown (1)
                boxes[box_shown].transform.position = positions[pos];
                boxes[box_shown+4].transform.position = positions[pos]; //open correct 
                boxes[box_shown+8].transform.position = positions[pos]; //open wrong

                boxes[box2_shown+4].transform.position = positions[1 - pos];
                boxes[box2_shown+8].transform.position = positions[1 - pos];
                boxes[box2_shown].transform.position = positions[1 - pos];

                boxes[box_shown].SetActive(true);
                boxes[box2_shown].SetActive(true);

                finishStateLoad();
                break;

            case State.TrialClick: // This is done by the Mouse Click House script.
                goto default;

            case State.End:
                SetLevel();
                break;

            case State.None:
                break;

            default:
                finishStateLoad();
                break;
        }

        time += Time.deltaTime;
    }

    public void moveToNextState()
    {
        if (currentState == State.Trial)
        {
            stateToLoad = State.Trial;
        }
        else if (currentState == State.End)
        {
            stateToLoad = State.End;
        }
        else { stateToLoad = currentState.Next();
            print(currentState);
            print(stateToLoad);
        }
    }

    void finishStateLoad()
    {
        currentState = stateToLoad;
        stateToLoad = State.None;
    }

    public void boxclicked(int box)
    {
        j = false;
        logReactionTime();
        if (trial == 1) // first trial is always correct
        {
            if (box_shown == box)
            {
                box_correct.Add(box_shown);
                box_wrong.Add(box2_shown);
            }
            else
            {
                pos = 1 - pos; // to mark the box clicked
                box_wrong.Add(box_shown);
                box_correct.Add(box2_shown);
            }
            box_shown = 0;
            box2_shown = 1;
            level=1;
            StartCoroutine(correcthouse(box));
        }

         else if (trial == 2) // second trial is always wrong
        {
            if (box_shown == box)
            {
                pos = 1 - pos; // to show box clicked
                box_correct.Add(box2_shown);
                box_wrong.Add(box_shown);
            }
            else
            {
                box_wrong.Add(box2_shown);
                box_correct.Add(box_shown);
            }
            level =1;
            StartCoroutine(wronghouse(box));

        }
        else if (trial<21)
        {
            if (box_correct.Contains(box)) // correct box pressed
            {
                StartCoroutine(correcthouse(box));
            }
            else
            {
                StartCoroutine(wronghouse(box));
            }

            level=1;
        }
        
        else if (trial>20)
           {
               if(box_correct.Contains(box))
               {
                   StartCoroutine(correctboxreversed(box));
               }

                else
            {
                StartCoroutine(wrongboxreversed(box));
            }

            level=2;
        }
    }
    

    public IEnumerator correcthouse(int box)
    {
        hitrate += 1;
        correct_flag = 1;
        sounds[1].Play();
        boxes[box].SetActive(false);
        boxes[box+4].SetActive(true);
        yield return new WaitForSeconds(1);
       
        ITI.SetActive(true);
        

        yield return new WaitForSeconds(2);
        boxes[box + 4].SetActive(false);
        boxes[box_shown].SetActive(false);
        boxes[box2_shown].SetActive(false);

        reactivateAll(pos);
        moveToNextState();

        if (consecutivehitrate<6)
        {
            consecutivehitrate+=1;
        }
        if (consecutivehitrate>6)
        {
            consecutivehitrate=8;
        }

        Debug.Log("Correct");

    }

    public IEnumerator wronghouse(int box)
    {
        comissionerror += 1;
        correct_flag = 0;
        sounds[0].Play();
        boxes[box].SetActive(false);
        boxes[box + 8].SetActive(true);
        

        yield return new WaitForSeconds(1);
        ITI.SetActive(true);
        
        yield return new WaitForSeconds(2);
        boxes[box + 8].SetActive(false);
        boxes[box_shown].SetActive(false);
        boxes[box2_shown].SetActive(false);



        reactivateAll(1-pos);

        moveToNextState();
        
        if (consecutivehitrate<6)
        {
            consecutivehitrate=0;
        }
        if (consecutivehitrate>7)
        {
            consecutivehitrate=8;
        }
        Debug.Log("Incorrect");
    }

    public IEnumerator wrongboxreversed(int box)
    {
        hitrate += 1;
        correct_flag = 1;
        consecutivehitrate =8;
        sounds[1].Play();
        boxes[box].SetActive(false);
        boxes[box+4].SetActive(true);
        ITI.SetActive(true);
        yield return new WaitForSeconds(1);
        ITI.SetActive(true);
        
        yield return new WaitForSeconds(2);
        boxes[box + 4].SetActive(false);
        boxes[box_shown].SetActive(false);
        boxes[box2_shown].SetActive(false);

        reactivateAll(pos);
        moveToNextState();
        j=true;

        Debug.Log("Correctreversed");
    }

    public IEnumerator correctboxreversed(int box)
    {
        comissionerror += 1;
        correct_flag = 0;
        consecutivehitrate=8;
        sounds[0].Play();
        boxes[box].SetActive(false);
        boxes[box + 8].SetActive(true);
        yield return new WaitForSeconds(1);
        ITI.SetActive(true);
        
        yield return new WaitForSeconds(2);
        boxes[box + 8].SetActive(false);
        boxes[box_shown].SetActive(false);
        boxes[box2_shown].SetActive(false);
        j=true;



        reactivateAll(1-pos);

        moveToNextState();
    }

    public void logReactionTime()
    {
        reactionTime = time - startTime;
    }

    public void logStartTime()
    {
        startTime = time;
    }

    public float setTime()
    { // updates time in different scripts.
        return time;
    }

    public void resetTime()
    {
        time = 0;
    }

     public void SetLevel()
    {
        if (hitrate>30)
        {
            csvReadWrite.output();
            level1.SetActive(false);
            instructions2.SetActive(true); // change in the future
        }

        else 
        {
            csvReadWrite.output();
            level1.SetActive(false);
            instructions2.SetActive(true);
        }
    }

    public void reactivateAll(int clicked_pos)
    {

        if (trial < n_trials)
        {
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            csvReadWrite.csvupdate(id, trial, clicked_pos, correct_flag, box_shown, box2_shown,
                                   dprime, hitrate, comissionerror, reactionTime);

            pos = Random.Range(0, 2);

            foreach (GameObject score in scores) // presenting the score on the right
            {                                    // through boxes
                score.SetActive(false);
            }

           
            restart = false;

            time = 0;
            ITI.SetActive(false);

            int correct_bar = hitrate - comissionerror;
            coins += correct_bar - prev_correct_bar;

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

            Debug.Log("Reaction time: " + reactionTime);

            if (trial == 1)
            {
                box_shown = 2;
                box2_shown = 3;
            }
            else
            {
                while (box_shown == prev_box_shown && box2_shown == prev_box2_shown)
                {
                    box_shown = box_correct[Random.Range(0, 2)];
                    box2_shown = box_wrong[Random.Range(0, 2)];
                }
            }
            trial += 1;
            j = true;
            prev_box_shown = box_shown;
            prev_box2_shown = box2_shown;
        }

        else
        { // After number of trials run.
            j = true;
            foreach (GameObject score in scores) // presenting the score on the right
            {                                    // through boxes
                score.SetActive(false);
            }
        
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            csvReadWrite.csvupdate(id, trial, clicked_pos, correct_flag, box_shown, box2_shown, dprime, 
                                   hitrate, comissionerror, reactionTime);
            csvReadWrite.output();

            Debug.Log("Reaction time: " + reactionTime);
            time = 0;
            currentState = State.End;
            hitratetext = hitrate.ToString();
        }
    }

    void OnDisable()
    {
        if (isApplicationQuitting) 
        {
            csvReadWrite.output();
        }

    }
 
    void OnApplicationQuit () {
        isApplicationQuitting = true;
    }

    void returnID()
    {
        id = PlayerPrefs.GetString("ID");
    }
}

