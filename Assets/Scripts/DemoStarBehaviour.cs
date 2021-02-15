using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoStarBehaviour : MonoBehaviour
{
    public GameObject GoodDino;
    private DemoLogicStardust gameLogic;

    private Animator animator;
    public GameObject[] stardust;

    public float x;
    private float time;
    private int trial;
    private float velocity;
    private enum dogType
    {
        GOOD = 0,
    }
    private int currentDog = 0;
    private int[][] trials = 
    {
        new int[] {0, 0, 0, 0, 0}, // Go-only practice
        new int[] {1, 0, 0, 0, 0, 0, 0} // Mixed practice
    };
    

    void Start()
    {
        Random.InitState(5);
        gameLogic = FindObjectOfType<DemoLogicStardust>();
        velocity = gameLogic.velocity;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        time = gameLogic.time;
        trial = gameLogic.getTrial();

        if (gameLogic.i && time > 3)
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
                GoodDino.SetActive(true);
                x = -15 + time * velocity;
                GoodDino.transform.position = new Vector3(x, 0, 0);

                if (x >0)
                {
                    animator.SetTrigger("pop");
                } 
                if (x > 13f)
                {
                    gameLogic.reactivate_all();
                    stardust[currentDog].SetActive(false);
                    animator.SetTrigger("stable");
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

