using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAttackDemoLogic : MonoBehaviour
{
    public GameObject instructions;
    public GameObject[] pipsCars;
    public GameObject[] otherCars;
    public GameObject[] subtitles;

    public GameEvent StageEnd;
    private PirateAttackCursorBehaviour handCursor;
    private bool subtitleOn;
    private IEnumerator handMovement;
    public float distBetweenCars = 6f;
    public float velocity = 6;
    private float position = 0;
    private float time = 0;
    private int trial = 0;
    private int[] trials = {
        0, 0, 0, 0, 1,   0, 0, 0, 0, 0,
        0, 0, 0, 1, 0,   0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 
    };
    private int[] cursorTrials = {
        4, 5, 13, 20,
    };

    // private int[] soundTrials = {

    // }

    private int nTrials=25;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(1);

        foreach (GameObject car in pipsCars)
        {
            car.SetActive(false);
        }
        
        foreach (GameObject car in otherCars)
        {
            car.SetActive(false);
        }

        nTrials = trials.Length;
        handCursor = FindObjectOfType<PirateAttackCursorBehaviour>();
         string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;

        if (subtitleOn)
        {
            subtitles[0].SetActive(true);
            subtitles[2].SetActive(false);
        }
    }

    void OnEnable()
    {
        time = 0;
        foreach (GameObject car in pipsCars)
        {
            car.SetActive(false);
            car.GetComponent<DemoShipBehaviour>().Restart();
        }
        
        foreach (GameObject car in otherCars)
        {
            car.SetActive(false);
            car.GetComponent<DemoShipBehaviour>().Restart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        position = time * velocity;
        
        if (position > distBetweenCars)
        {
            // TODO tidy with function
            if (trials[trial] == 1) // Pip's Car
            {
                int index = Random.Range(0, pipsCars.Length);
                while(pipsCars[index].activeSelf) { index = Random.Range(0, pipsCars.Length); }
                pipsCars[index].SetActive(true);
            } 
            else
            {
                int index = Random.Range(0, otherCars.Length);
                while(otherCars[index].activeSelf) { index = Random.Range(0, otherCars.Length); }
                otherCars[index].SetActive(true);
            }

            for (int i = 0; i < cursorTrials.Length; i++)
            {
                if (cursorTrials[i] == trial) {
                    switch (i)
                    {
                        case 0:
                            handMovement = moveHand(PirateAttackCursorBehaviour.State.TargetShip);
                            break;

                        case 1:
                            handMovement = moveHand(PirateAttackCursorBehaviour.State.GoodShip);
                            if (subtitleOn){
                                subtitles[0].SetActive(false);
                                subtitles[1].SetActive(true);
                            }
                            break;

                        case 2:
                            handMovement = moveHand(PirateAttackCursorBehaviour.State.TargetShip2);
                        
                            break;

                        case 3:
                            handMovement = moveHand(PirateAttackCursorBehaviour.State.TargetShip3);
                            if (subtitleOn){
                                subtitles[1].SetActive(false);
                                subtitles[2].SetActive(true);
                            }
                            break;

                    }

                    StartCoroutine(handMovement);
                }
            }

            trial++;
            time = 0;
        }
        
        if(trial >= nTrials){
            handCursor.reset();
            StageEnd.Raise();
            subtitles[2].SetActive(false);
            gameObject.SetActive(false);
            trial = 0;
            time = 0;
        }

    }

    private IEnumerator moveHand(PirateAttackCursorBehaviour.State direction)
    {
        float delay = direction == PirateAttackCursorBehaviour.State.ClickPipLate ? 0.8f : 0.6f;
        yield return new WaitForSeconds(delay);
        handCursor.move(direction);
    }

    public void skip()
    {
        StopAllCoroutines();
        handCursor.reset();
        instructions.SetActive(true);
        trial = 0;
        time = 0;
        gameObject.SetActive(false);
        subtitles[2].SetActive(true);
    
    }
}