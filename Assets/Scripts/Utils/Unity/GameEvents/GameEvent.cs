using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName="Custom/GameEvent/GameEvent")]
public class GameEvent : ScriptableObject
{
    List<IGameEventListener> listeners = new List<IGameEventListener>();
    List<IGameEventListener> priorityListeners = new List<IGameEventListener>();
    public void Raise()
    {
        for (int i = priorityListeners.Count - 1; i >= 0 ; i--)
        {
            priorityListeners[i].OnEventRaised();
        }

        for (int i = listeners.Count - 1; i >= 0 ; i--)
        {
            listeners[i].OnEventRaised();
        }
    }
    public void RegisterListener(IGameEventListener listener, bool priority = false)
    {
        if (priority && !priorityListeners.Contains(listener)) priorityListeners.Add(listener);
        else if (!listeners.Contains(listener)) listeners.Add(listener);
    }
    public void UnregisterListener(IGameEventListener listener)
    {
        if (priorityListeners.Contains(listener)) priorityListeners.Remove(listener);
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }
}