using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickBee_l : MonoBehaviour
{

    Gamelogicbees gameLogic;
    bool correct = false;
    private void Start()
    {
        gameLogic = FindObjectOfType<Gamelogicbees>();
    }

    public void OnMouseDown()
    {
        if(gameLogic.dir == 0)
        {
            correct = true;
        }
        else { correct = false; }
        gameLogic.Trial_n(correct);
    }
}
