using UnityEngine;

[RequireComponent(typeof(Stimulus))]
public class StimulusTimeout : MonoBehaviour, IGameEventListener<Stimulus>, IGameEventListener
{
    public StimulusGameEvent responseWindowStart;
    public GameEvent responseWindowEnd;
    public BoolVariable correct;
    public NullableFloatVariable responseTime;
    public BoolVariable participantsGo;
    public bool participantOnly = true;
    public float timeOut;
    Stimulus stimulus;
    void Awake() => stimulus = GetComponent<Stimulus>();
    private void OnEnable() => RegisterListener();
    private void OnDisable() => UnregisterListener();
    public void RegisterListener() 
    {
        responseWindowStart.RegisterListener(this, true);
        responseWindowEnd.RegisterListener(this, true);
    } 
    public void UnregisterListener()
    {
        responseWindowStart.UnregisterListener(this);
        responseWindowEnd.UnregisterListener(this);
    } 
    public void OnEventRaised(Stimulus parameter) 
    {
        if (participantsGo && participantOnly)
        {
            this.In(timeOut).Call(() => { 
                correct.boolValue = false; 
                responseTime.Value = null;
                responseWindowEnd.Raise(); 
            });
        }
    }
    public void OnEventRaised()
    {
        StopAllCoroutines();
    }
}