using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRewardSocialProbabilistic : MonoBehaviour
{
      private GameLogicRewardProbabilistic gameLogic;
    public int person;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicRewardProbabilistic>();

    }


    public void OnMouseDown()
    {
        if (gameLogic.j){
            gameLogic.personclicked(person);
        }

    }
    
}

