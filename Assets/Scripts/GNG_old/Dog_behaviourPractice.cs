using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_behaviourPractice : MonoBehaviour
{

    public GameObject GoodDog;
    public GameObject BadDog;
    private GameLogicGoPractice gameLogic;

    private float x;
    private float time;
    private int trial;
    private float velocity;
    private enum dogType
    {
        GOOD = 0,
        BAD = 1
    }
    private int currentDog = -1;
    private int[][] trials = 
    {
        new int[] {0, 0, 0, 0, 0}, // Go-only practice
        new int[] {0, 0, 1, 0, 0, 0, 0, 0, 1, 0} // Mixed practice
    };
    

    void Start()
    {
        Random.InitState(5);
        gameLogic = FindObjectOfType<GameLogicGoPractice>();
        velocity = gameLogic.velocity;
    }

    void Update()
    {
        time = gameLogic.time;
        trial = gameLogic.getTrial();

        if (gameLogic.i && time > gameLogic.interval)
        { // so it runs only once
            int level = gameLogic.getPracticeLevel();
            int trialIndex = trial - 1;
            currentDog = trials[level][trialIndex]; // get dogType from correct trials array
            gameLogic.timetozero();
            time = gameLogic.time;
            gameLogic.pointer();
            Debug.Log("dogactivated");

        }

        switch (currentDog)
        {
            case (int)dogType.GOOD:
                Debug.Log("DogMove");
                GoodDog.SetActive(true);
                x = -13 + time * velocity;
                GoodDog.transform.position = new Vector3(x, -1, 0);
                if (x > 13)
                {
                    gameLogic.reactivate_all();
                    currentDog = -1;
                }
                break;

            case (int)dogType.BAD:
                BadDog.SetActive(true);
                x = -13 + time * velocity;
                BadDog.transform.position = new Vector3(x, -1, 0);
                if (x > 13)
                {
                    gameLogic.reactivate_all();
                    currentDog = -1;
                }
                break;

            default:
                break;
        }
        
    }

    public void reset()
    {
        currentDog = -1;
    }
}
