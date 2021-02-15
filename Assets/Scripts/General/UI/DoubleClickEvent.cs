using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoubleClickEvent : MonoBehaviour
{
    public UnityEvent Event;
    int clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;

    private void Start()
    {
        
    }

    public void OnMouseDown()
    {
        clicked++;
        
        if (clicked == 1) clicktime = Time.time;

        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            Event.Invoke();
            clicked = 0;
            clicktime = 0;
        }

        else if (clicked > 2 || Time.time - clicktime > clickdelay) clicked = 0;
    }
}
