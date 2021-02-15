using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicRewardProbabilistic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] people;
    private AudioSource[] sounds; // 1 correct, 0 incorrect
    public GameObject chicken;
    public GameObject ITI;
    public GameObject instructions;
    public GameObject canvas;
    public GameObject MM;
    public GameObject end;
    public int random;
    private CsvReadWriteRLSP csvReadWrite;

    public bool restart = false;
    public float time = 0.0f;
    public bool j = true; // so that only the first click counts
    public bool m = true; 

    List<int> person_correct = new List<int>();
    List<int> person_wrong = new List<int>();
    List <int> probabilityschedule = new List<int>(10){0,0,1,1,1,1,1,1,1,1};

    public int trial = 1;
    public int n_trials = 80;
    private int comissionerror = 0;
    private int bad = 0;
    private int good = 0;

    public int consecutivehitrate = 0;
    private float startTime = 0;
    private float reactionTime = 0;
    private int correct_flag = 0;
    private int hitrate = 0;
    private double dprime = 0;
    private int prev_correct_bar = 0;
    private int coins = 0;
    private int pos = -1;
    public int person_shown = -1;
    public int person2_shown = -1;
    public int prev_person_shown = -1;
    public int prev_person2_shown = -1;


    private Vector3[] positions = {
        new Vector3(-4.5f, -0.5f, 0), // left
        new Vector3(4.5f, -0.5f, 0),
        new Vector3(0,-3,0),// right
        new Vector3(-4.5f,-3,0),
        new Vector3(4.5f,-3,0)
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
        sounds = GetComponents<AudioSource>();
        person_shown = 0;
        person2_shown = 1;


        foreach (GameObject person in people)
        {
            person.SetActive(false);
        }

        csvReadWrite = FindObjectOfType<CsvReadWriteRLSP>();
        csvReadWrite.csvstart();

        stateToLoad = State.Trial;
        chicken.transform.position = positions[2];

    }


    void Update()
    {
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {

            case State.Trial: // Setting of the 1st pair (correct click)
                logStartTime();
                pos = Random.Range(0, 2); // Position of person_shown (1)
                chicken.SetActive(true);
                people[person_shown].transform.position = positions[pos];
                people[person_shown+4].transform.position = positions[pos]; //open correct 
                people[person_shown+8].transform.position = positions[pos]; //open wrong

                people[person2_shown+4].transform.position = positions[1 - pos];
                people[person2_shown+8].transform.position = positions[1 - pos];
                people[person2_shown].transform.position = positions[1 - pos];

                people[person_shown].SetActive(true);
                people[person2_shown].SetActive(true);

                finishStateLoad();
                break;

            case State.TrialClick: // This is done by the Mouse Click House script.
                goto default;

            case State.End:
                end.SetActive(true);
                csvReadWrite.output();
                gameObject.SetActive(false);

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

    public void personclicked(int person)
    {
        if (trial>2)
        {
            index = Random.Range(1,10);
            rewarded = probabilityschedule[index];
        }
        
        Debug.Log(rewarded);

        j = false;
        logReactionTime();
        if (trial == 1) // first trial is always correct
        {
            if (person_shown == person)
            {
                person_correct.Add(person_shown);
                person_wrong.Add(person2_shown);
            }
            else
            {
                pos = 1 - pos; // to mark the person clicked
                person_wrong.Add(person_shown);
                person_correct.Add(person2_shown);
            }
            person_shown = 0;
            person2_shown = 1;
            StartCoroutine(correcthouse(person));
        }

        else if (trial == 2) // second trial is always wrong
        {
            if (person_shown == person)
            {
                pos = 1 - pos; // to show person clicked
                person_correct.Add(person2_shown);
                person_wrong.Add(person_shown);
            }
            else
            {
                person_wrong.Add(person2_shown);
                person_correct.Add(person_shown);
            }

            StartCoroutine(wronghouse(person));
            }
        
        else
        {
            if (trial <40)
            {
            if (person_correct.Contains(person) && rewarded > 0) 
            
            // correct person pressed
            {
                StartCoroutine(correcthouse(person));
            }
            else if (person_wrong.Contains(person) && rewarded>0)
            {
                StartCoroutine(wronghouse(person));
            }

            else if (person_correct.Contains(person) && rewarded <1)
            {
                StartCoroutine(wronghouse(person));
            }

            else 
            {
                StartCoroutine(correcthouse(person));
            }
            }

            else 
            {
                if (person_correct.Contains(person) && rewarded<1) 
            
            // correct person pressed
            {
                StartCoroutine(correcthouse(person));
            }
            else if (person_wrong.Contains(person) && rewarded<1)
            {
                StartCoroutine(wronghouse(person));
            }

            else if (person_correct.Contains(person) && rewarded>0)
            {
                StartCoroutine(wronghouse(person));
            }

            else 
            {
                StartCoroutine(correcthouse(person));
            }



            }
        
        }
        

    }

    public IEnumerator correcthouse(int person)
    {
        hitrate += 1;
        correct_flag = 1;
        people[person].SetActive(false);
        people[person+4].SetActive(true);
        
        if (trial ==1)
        {
             if (pos<1)
        {
            chicken.transform.position = positions[3];
        }

        else
        {
            chicken.transform.position = positions[4];
        }
        }

        else if (trial <40)
        {
        if (rewarded>0)
        {
        if (pos<1)
        {
            chicken.transform.position = positions[3];
        }

        else
        {
            chicken.transform.position = positions[4];
        }
        }

        if (rewarded<1)
        {
           if (pos<1)
        {
            chicken.transform.position = positions[4];
        }

         else
        {
            chicken.transform.position = positions[3];
        }
        
        }
        }

        else
        {
            
          if (rewarded>0)
        {
        if (pos<1)
        {
            chicken.transform.position = positions[4];
        }

        else
        {
            chicken.transform.position = positions[3];
        }
        }

        if (rewarded<1)
        {
           if (pos<1)
        {
            chicken.transform.position = positions[3];
        }

        else
        {
            chicken.transform.position = positions[4];
        }  

        }
        
        }

        yield return new WaitForSeconds(3f);

        people[person + 4].SetActive(false);
        people[person_shown].SetActive(false);
        people[person2_shown].SetActive(false);
        chicken.transform.position = positions[2];

        ITI.SetActive(true);

        yield return new WaitForSeconds(1);

        reactivateAll(pos);
        moveToNextState();
    }

    public IEnumerator wronghouse(int person)
    {
        comissionerror += 1;
        correct_flag = 0;
        // sounds[0].Play();
        people[person].SetActive(false);
        people[person + 8].SetActive(true);
        
        if (trial ==2)
        {
             if (pos<1)
        {
            chicken.transform.position = positions[4];
        }

        else
        {
            chicken.transform.position = positions[3];
        }
        }
        
        else if(trial<40)
        {
        if (rewarded>0)
        {
            if (pos<1)
        {
            chicken.transform.position = positions[4];
        }

        else
        {
            chicken.transform.position = positions[3];
        }
        }

        if (rewarded<1)
        {
            if (pos<1)
        {
            chicken.transform.position = positions[3];
        }
            else if(pos>0)
            {
                chicken.transform.position = positions[4];
            }
        }
        
        }

        else
        {
            
          if (rewarded>0)
        {
        if (pos<1)
        {
            chicken.transform.position = positions[3];
        }

        else
        {
            chicken.transform.position = positions[4];
        }
        }

        if (rewarded<1)
        {
           if (pos<1)
        {
            chicken.transform.position = positions[4];
        }

        else
        {
            chicken.transform.position = positions[3];
        }  

        }
        
        }

        

        yield return new WaitForSeconds(3);

        people[person + 8].SetActive(false);
        people[person_shown].SetActive(false);
        people[person2_shown].SetActive(false);
        chicken.transform.position = positions[2];

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
        csvReadWrite.output();
        MM.SetActive(true);
    }

    public void reactivateAll(int clicked_pos)
    {

        if (trial < n_trials)
        {
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            csvReadWrite.csvupdate(trial, clicked_pos, correct_flag, good, bad,
                                   dprime, hitrate, comissionerror, reactionTime);

            pos = Random.Range(0, 2);

            restart = false;


            time = 0;
            chicken.SetActive(true);
            ITI.SetActive(false);

            int previous = hitrate;

            int correct_bar = hitrate - comissionerror;
            coins += correct_bar - prev_correct_bar;

            prev_correct_bar = hitrate - comissionerror;

            Debug.Log("Reaction time: " + reactionTime);

            if (trial == 1)
            {
                person_shown = 2;
                person2_shown= 3;
            }
            else
            {
                while (person_shown == prev_person_shown && person2_shown == prev_person2_shown)
                {
                    person_shown = person_correct[Random.Range(0, 2)];
                    person2_shown = person_wrong[Random.Range(0, 2)];
                }
            }
            trial += 1;
            j = true;
            prev_person_shown = person_shown;
            prev_person2_shown = person2_shown;
        }

        else
        { // After rewarded of trials run.
            j = true;
        
            dprime = ((hitrate - comissionerror) / 1.0f) / trial;
            csvReadWrite.csvupdate(trial, pos, correct_flag, good, bad, dprime, 
                                   hitrate, comissionerror, reactionTime);
            csvReadWrite.output();

            Debug.Log("Reaction time: " + reactionTime);
            time = 0;
            currentState = State.End;
        }
    }
}

