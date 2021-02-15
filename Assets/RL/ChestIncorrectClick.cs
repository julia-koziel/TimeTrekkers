using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestIncorrectClick : MonoBehaviour
{
    public GameObject chestOpen; // open box
    private GameLogicRewardLearning gameLogic;
    private ChestCorrectClick mouseClick;

    public float time;

    private void Start()
    {
        
        chestOpen.SetActive(false);
        gameLogic = FindObjectOfType<GameLogicRewardLearning>();

    }

    private void Update()
    {
        if (!gameLogic.j)
        {
            time = gameLogic.setTime();
            if (time > gameLogic.trialTime - 0.05)
            {
                print("Right renderer");
                // rend.enabled = true;
            }
            if (time > gameLogic.trialTime) // end of trial
            {
                gameLogic.reactivateAll();
            }
        }
    }
    public void OnMouseDown()
    {
        if (gameLogic.j){
            gameLogic.logReactionTime();

            gameLogic.resetTime(); 
            chestOpen.SetActive(true);
            gameLogic.pointer2();
            gameLogic.wronghouse();
        }

    }
}
