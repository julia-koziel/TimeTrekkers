using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicProbabilisticNonSocial : MonoBehaviour
{
    public GameObject[] chests;
    private AudioSource[] sounds;
    public GameObject[] scores;
    public GameObject ITI;
    public GameObject instructions;
    public GameObject canvas;
    public GameObject settings;
    public GameObject MM;
    public GameObject trials;
    public GameObject end;
    public GameObject endscreen;
    public GameObject textboxes;
    public int random;
    private CSVReadWriteRLP csvReadWrite;

    public bool restart = false;
    public float time = 0.0f;
    public bool j = true; // so that only the first click counts
    public bool m = true; // coins on settings

    List<int> chest_correct = new List<int>();
    List<int> chest_wrong = new List<int>();
    List <int> probabilityschedule = new List<int>(10){0,0,1,1,1,1,1,1,1,1};

    public int trial = 1;
    public int n_trials = 40;
    private int comissionerror = 0;

    public int consecutivehitrate = 0;
    private float startTime = 0;
    private float reactionTime = 0;
    private int correct_flag = 0;
    private int hitrate = 0;
    private double dprime = 0;
    private int prev_correct_bar = 0;
    private int coins = 0;
    private int pos = -1;
    public int chest_shown = -1;
    public int chest2_shown = -1;
    public int prev_chest_shown = -1;
    public int prev_chest2_shown = -1;


    private Vector3[] positions = {
        new Vector3(-3, 0, 1), // left
        new Vector3(3f, 1, 0),
        new Vector3(2,-3,0),// right
        new Vector3(-3,0,1),
        new Vector3(3f,1,0)
    };

    private State stateToLoad = State.None;
    private State currentState = State.None;
    public int index=5;
    public int rewarded;
    private enum State
    {
        None,
        Trial, TrialClick,
        End
    }

    void Start()
    {
        
        Random.InitState(3);
       
        chest_shown = 0;
        chest2_shown = 1;


        foreach (GameObject chest in chests)
        {
            chest.SetActive(false);
        }

        csvReadWrite = FindObjectOfType<CSVReadWriteRLP>();
        csvReadWrite.csvstart();

        stateToLoad = State.Trial;

        sounds = GetComponents<AudioSource>();
        textboxes.SetActive(false);

    }


    void Update()
    {
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {

            case State.Trial: // Setting of the 1st pair (correct click)
                logStartTime();
                pos = Random.Range(0, 2); // Position of chest_shown (1)

                chests[chest_shown].transform.position = positions[pos];
                chests[chest_shown+4].transform.position = positions[pos]; //open correct 
                chests[chest_shown+8].transform.position = positions[pos]; //open wrong

                chests[chest2_shown+4].transform.position = positions[1 - pos];
                chests[chest2_shown+8].transform.position = positions[1 - pos];
                chests[chest2_shown].transform.position = positions[1 - pos];

                chests[chest_shown].SetActive(true);
                chests[chest2_shown].SetActive(true);

                finishStateLoad();
                break;

            case State.TrialClick: // This is done by the Mouse Click House script.
                goto default;

            case State.End:
                end.SetActive(true);
                endscreen.SetActive(true);
                trials.SetActive(false);
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

    public void chestclicked(int chest)
    {
        if (trial>2)
        {
            index = Random.Range(1,11);
            rewarded = probabilityschedule[index];
        }
        
        Debug.Log(rewarded);

        j = false;
        logReactionTime();
        if (trial == 1) // first trial is always correct
        {
            if (chest_shown == chest)
            {
                chest_correct.Add(chest_shown);
                chest_wrong.Add(chest2_shown);
            }
            else
            {
                pos = 1 - pos; // to mark the chest clicked
                chest_wrong.Add(chest_shown);
                chest_correct.Add(chest2_shown);
            }
            chest_shown = 0;
            chest2_shown = 1;
            StartCoroutine(correcthouse(chest));
        }

        else if (trial == 2) // second trial is always wrong
        {
            if (chest_shown == chest)
            {
                pos = 1 - pos; // to show chest clicked
                chest_correct.Add(chest2_shown);
                chest_wrong.Add(chest_shown);
            }
            else
            {
                chest_wrong.Add(chest2_shown);
                chest_correct.Add(chest_shown);
            }

            StartCoroutine(wronghouse(chest));
            }
        
        else
        {
            if (trial <40)
            {
            if (chest_correct.Contains(chest) && rewarded > 0) 
            
            // correct chest pressed
            {
                StartCoroutine(correcthouse(chest));
            }
            else if (chest_wrong.Contains(chest) && rewarded>0)
            {
                StartCoroutine(wronghouse(chest));
            }

            else if (chest_correct.Contains(chest) && rewarded <1)
            {
                StartCoroutine(wronghouse(chest));
            }

            else 
            {
                StartCoroutine(correcthouse(chest));
            }
            }

            else 
            {
                if (chest_correct.Contains(chest) && rewarded<1) 
            
            // correct chest pressed
            {
                StartCoroutine(correcthouse(chest));
            }
            else if (chest_wrong.Contains(chest) && rewarded<1)
            {
                StartCoroutine(wronghouse(chest));
            }

            else if (chest_correct.Contains(chest) && rewarded>0)
            {
                StartCoroutine(wronghouse(chest));
            }

            else 
            {
                StartCoroutine(correcthouse(chest));
            }



            }
        }
        

    }

    public IEnumerator correcthouse(int chest)
    {
        hitrate += 1;
        correct_flag = 1;
        chests[chest].SetActive(false);
        chests[chest+4].SetActive(true);
        sounds[1].Play();
        
        yield return new WaitForSeconds(1.5f);

        chests[chest + 4].SetActive(false);
        chests[chest_shown].SetActive(false);
        chests[chest2_shown].SetActive(false);

        ITI.SetActive(true);

        yield return new WaitForSeconds(1);

        reactivateAll(pos);
        moveToNextState();
    }

    public IEnumerator wronghouse(int chest)
    {
        comissionerror += 1;
        correct_flag = 0;
        chests[chest].SetActive(false);
        chests[chest + 8].SetActive(true);
        sounds[0].Play();
        
        yield return new WaitForSeconds(1.5f);

        chests[chest + 8].SetActive(false);
        chests[chest_shown].SetActive(false);
        chests[chest2_shown].SetActive(false);
        ITI.SetActive(true);

        yield return new WaitForSeconds(1);

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

    public void Quit()
    {
        // csvReadWrite.output();
        MM.SetActive(true);
    }

    public void reactivateAll(int clicked_pos)
    {

        if (trial < n_trials)
        {
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            csvReadWrite.csvupdate(trial, clicked_pos, correct_flag,
                                hitrate, reactionTime);

            pos = Random.Range(0, 2);

            restart = false;

            time = 0;
            ITI.SetActive(false);

            foreach (GameObject score in scores) // presenting the score on the right
            {                                    // through boxes
                score.SetActive(false);
            }

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
                chest_shown = 2;
                chest2_shown= 3;
            }
            else
            {
                while (chest_shown == prev_chest_shown && chest2_shown == prev_chest2_shown)
                {
                    chest_shown = chest_correct[Random.Range(0, 2)];
                    chest2_shown = chest_wrong[Random.Range(0, 2)];
                }
            }
            trial += 1;
            j = true;
            prev_chest_shown = chest_shown;
            prev_chest2_shown = chest2_shown;
        }

        else
        { // After rewarded of trials run.
            j = true;
        
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            csvReadWrite.csvupdate(trial, pos, correct_flag, 
                                   hitrate, reactionTime);
            csvReadWrite.output();

            Debug.Log("Reaction time: " + reactionTime);
            time = 0;
            currentState = State.End;
        }
    }
}

