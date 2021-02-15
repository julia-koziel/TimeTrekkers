using UnityEngine;
using System.Collections.Generic;

public class GameEvent<T> : ScriptableObject
{
    List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();
    List<IGameEventListener<T>> priorityListeners = new List<IGameEventListener<T>>();
    public void Raise(T param)
    {
        for (int i = priorityListeners.Count - 1; i >= 0 ; i--)
        {
            priorityListeners[i].OnEventRaised(param);
        }

        for (int i = listeners.Count - 1; i >= 0 ; i--)
        {
            listeners[i].OnEventRaised(param);
        }
    }
    public void RegisterListener(IGameEventListener<T> listener, bool priority = false)
    {
        if (priority && !priorityListeners.Contains(listener)) priorityListeners.Add(listener);
        else if (!listeners.Contains(listener)) listeners.Add(listener);
    }
    public void UnregisterListener(IGameEventListener<T> listener)
    {
        if (priorityListeners.Contains(listener)) priorityListeners.Remove(listener);
        if (listeners.Contains(listener)) listeners.Remove(listener);
    }
}