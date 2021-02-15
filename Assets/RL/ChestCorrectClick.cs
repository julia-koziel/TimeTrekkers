using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCorrectClick : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject open; // open box
    private GameLogicRewardLearning gameLogic;
    public float time;

    private void Start()
    {
        open.SetActive(false);
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
                Debug.Log("reactivateTrigger");
            }
        }
    }
    public void OnMouseDown()
    {
        if (gameLogic.j){
            Debug.Log("correctclicked");
            gameLogic.logReactionTime();

            gameLogic.resetTime(); 
            open.SetActive(true);
            gameLogic.correcthouse();
            gameLogic.pointer2();
        }

    }
    
}
