using UnityEngine;
using UnityEngine.Events;

public class OnMouseDownEvent : MonoBehaviour 
{
    public UnityEvent Event;
    void OnMouseDown() 
    {
        Event.Invoke();
    }
}