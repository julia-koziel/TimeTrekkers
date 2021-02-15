using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestClickProbabilistic : MonoBehaviour
{
    // Start is called before the first frame update
    private GameLogicProbabilisticNonSocial gameLogic;
    public int chest;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicProbabilisticNonSocial>();

    }


    public void OnMouseDown()
    {
        if (gameLogic.j)
        {
            gameLogic.chestclicked(chest);
        }

    }
}
    