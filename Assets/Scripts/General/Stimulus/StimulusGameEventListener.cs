using UnityEngine;
using UnityEngine.Events;

public class StimulusGameEventListener : MonoBehaviour, IGameEventListener<Stimulus>
{
    public StimulusGameEvent Event;
    public UnityEvent Response;
    public bool priority;
    private void OnEnable() => RegisterListener();
    private void OnDisable() => UnregisterListener();
    public void RegisterListener() => Event.RegisterListener(this, priority);
    public void UnregisterListener() => Event.UnregisterListener(this);
    public virtual void OnEventRaised(Stimulus stimulus) => Response.Invoke();
}