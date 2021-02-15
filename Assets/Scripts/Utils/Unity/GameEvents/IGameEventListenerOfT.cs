public interface IGameEventListener<T>
{
    void RegisterListener();
    void UnregisterListener();
    void OnEventRaised(T parameter);
}