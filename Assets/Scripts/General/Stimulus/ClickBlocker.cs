using UnityEngine;
public class ClickBlocker : MonoBehaviour, IGameEventListener<Stimulus>
{
    public StimulusGameEvent responseWindowStart;
    private void OnEnable() => RegisterListener();
    private void OnDisable() => UnregisterListener();
    public void RegisterListener() => responseWindowStart.RegisterListener(this);
    public void UnregisterListener() => responseWindowStart.UnregisterListener(this);
    public void OnEventRaised(Stimulus stimulus) => stimulus.SetClickable(false);
}