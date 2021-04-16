using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamelogicbees : MonoBehaviour
{
    public GameObject blockScreen;
    public GameObject leftPlanet;
    public GameObject rightPlanet;
    public GameObject alien;
    public GameObject alienContainer;
    private Animator alienAnimator;
    public GameObject continueButton;
    public GameObject trialsContainer;
    public GameObject SnowballsContainer;
    private SnowballParticleSystem Snowballs;
    public GameObject instructions;
    public GameObject MM;
    private CsvReadWriteBees csv;

    // Task Parameters
    public int n_trials = 12;
    public float delayPeriod = 2;
    public float time_before_click = 0.25f;

    // Task flags
    int trial = 0;
    int block = 1;
    private float[] trialCoherences = new float[] {
        0.7f, 0.7f, 0.7f, 0.7f,
        0.8f, 0.8f, 0.8f, 0.8f,
        0.9f, 0.9f, 0.9f, 0.9f
    };
    public int dir = -1;
    int prev_dir = 1;
    float time = 0;
    bool flag = true;
    bool trialNeedsStarting = true;
    float coherence = 1;
    int block1trialsCorrect = 0;

    void Start()
    {
        csv = FindObjectOfType<CsvReadWriteBees>();
        alienAnimator = alien.GetComponent<Animator>();
        Snowballs = SnowballsContainer.GetComponent<SnowballParticleSystem>();
        // Snowballs.switchOn(false);
        csv.csvstart();
        Random.InitState(42);
        dir = Random.Range(0, 2);

    }

    void Update()
    {
        time += Time.deltaTime;

        // Only show bees after delay period
        if (flag)
        {
            switch (block)
            {
                case 1:
                    coherence = 1;
                    Snowballs.setParams(dir, coherence);

                    flag = false;
                    break;

                case 2:
                    goto default;

                case 3:
                    goto default;

                case 4:
                    goto default;

                case 5:
                    End_game();
                    break;
                
                default:
                    if (trial == 0)
                    {
                        System.Random rng = new System.Random();
                        rng.Shuffle(trialCoherences);
                    }
                    coherence = trialCoherences[trial];
                    Snowballs.setParams(dir, coherence);
                    flag = false;
                    break;
            }
        }

        if (time > delayPeriod && trialNeedsStarting)
        {
            time = 0;
            alien.SetActive(false);
            // Snowballs.switchOn(true);
            trialNeedsStarting = false;
        }
    }

    public void Trial_n(bool correct)
    {
        Debug.Log(correct);
        if (!trialNeedsStarting && time > time_before_click) // click only works after delay period
        {
            Vector3 pos = alienContainer.transform.position;
            pos.x = dir == 0 ? -6 : 6.2f;
            alienContainer.transform.position = pos;
            
            alien.SetActive(correct);
            alienAnimator.SetTrigger("MoveFront");
            csv.csvupdate(trial, block, dir, correct, time, coherence); // check if not including demo
            // Reinitialization
            trial += 1;
            time = 0;
            prev_dir = dir;
            dir = Random.Range(0, 2);
            Snowballs.setDirection(dir);
            // change blocks
            if (block == 1)
            {
                block1trialsCorrect = correct ? block1trialsCorrect+1 : 0;
            }

            // Snowballs.switchOn(false);

            if (trial == n_trials || block1trialsCorrect == 5)
            {
                StartCoroutine(blockBreak());
            }
            else
            {
                flag = true;
                trialNeedsStarting = true;
            }
        }
        else if (time < delayPeriod && trialNeedsStarting)
        {
            Debug.Log("click mid ITI");
            time = 0;
        }
    }

    private IEnumerator blockBreak()
    {
        yield return new WaitForSeconds(delayPeriod);
        print("ended block");
        alien.SetActive(false);
        trial = 0;
        block += 1;
        block1trialsCorrect = 0;
        // End_game(); // valid only if we are considering just 1 block
        if (block == 5)
        {
            End_game();
        }
        else
        {
            flag = false;
            continueButton.SetActive(true);
            blockScreen.SetActive(true);
            leftPlanet.SetActive(false);
            rightPlanet.SetActive(false);
        }
    }

    public void continueToNextBlock()
    {
        flag = true;
        trialNeedsStarting = true;
        continueButton.SetActive(false);
        blockScreen.SetActive(false);
        leftPlanet.SetActive(true);
        rightPlanet.SetActive(true);
        Debug.Log(dir);
    }

    public void End_game()
    {
        print("Called end function");
        csv.output();
        
        instructions.SetActive(true); // add end screen
        trialsContainer.SetActive(false);
        gameObject.SetActive(false);
    }
}
