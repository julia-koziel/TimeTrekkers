using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestClick : MonoBehaviour
{
    private GameLogicReward2 gameLogic;
    public int box;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicReward2>();

    }


    public void OnMouseDown()
    {
        if (gameLogic.j)
        {
            gameLogic.boxclicked(box);
        }

    }
}
    