using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickBee_practice : MonoBehaviour
{

    Gamelogicbees_practice gameLogic;
    bool correct = false;
    private void Start()
    {
        gameLogic = FindObjectOfType<Gamelogicbees_practice>();

    }

    public void OnMouseDown()
    {
        gameLogic.Trial_n(correct);
    }
}
