using UnityEngine.Events;

public interface IGameEventListener
{
    void RegisterListener();
    void UnregisterListener();
    void OnEventRaised();
}