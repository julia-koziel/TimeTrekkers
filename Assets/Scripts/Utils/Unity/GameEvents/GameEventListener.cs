using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour, IGameEventListener
{
    public GameEvent Event;
    public UnityEvent Response;
    public bool priority;
    private void OnEnable() => RegisterListener();
    private void OnDisable() => UnregisterListener();
    public void RegisterListener() => Event.RegisterListener(this, priority);
    public void UnregisterListener() => Event.UnregisterListener(this);
    public virtual void OnEventRaised() => Response.Invoke();
}