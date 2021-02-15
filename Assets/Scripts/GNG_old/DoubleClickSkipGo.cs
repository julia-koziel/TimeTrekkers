using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClickSkipGo : MonoBehaviour
{
    public GameLogicGoPractice practice;
    public InstructionsClickGo instructions;

    int clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private void Start()
    {
        
    }

    public void OnMouseDown()
    {
        clicked++;
        if (clicked == 1)
        {
            clicktime = Time.time;
        }

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            if (instructions.isActiveAndEnabled)
            {
                instructions.skip();
            }
            else if (practice.isActiveAndEnabled)
            {
                practice.skip();
            }
            clicked = 0;
            clicktime = 0;


        }
        else if (clicked > 2 || Time.time - clicktime > clickdelay) clicked = 0;

    }
}
