﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_click_home_bee : MonoBehaviour
{
    private Gamelogicbees gameLogic;
    public GameObject MM;
    int clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private void Start()
    {
        gameLogic = FindObjectOfType<Gamelogicbees>();
    }

    public void OnMouseDown()
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
            print(clicktime);
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            if (gameLogic != null)
            {
                gameLogic.End_game();
            } 
            else
            {
                MM.SetActive(true);
            }
            clicked = 0;
            clicktime = 0;

        }
        else if (clicked > 2 || Time.time - clicktime > clickdelay) clicked = 0;

    }
}
