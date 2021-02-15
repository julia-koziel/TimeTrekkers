using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPersonCorrect : MonoBehaviour
{
    
    private GameLogicRewardSocial gameLogic;
    public int box_number;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicRewardSocial>();

    }


    // public void OnMouseDown()
    // {
    //     if (gameLogic.j){
    //         gameLogic.boxclicked(box_number);
    //     }

    // }
}
    
