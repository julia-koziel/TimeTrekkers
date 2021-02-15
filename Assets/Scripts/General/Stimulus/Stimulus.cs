using UnityEngine;
using UnityEngine.Events;

public class Stimulus : MonoBehaviour
{
    public StimulusGameEvent responseWindowStart;
    public GameEvent responseWindowEnd;
    public NullableFloatVariable loggedResponseTime;
    public BoolVariable loggedCorrect;
    public IntVariable loggedId; // Can be null
    [Space(10)]
    public IntVariable id; // Can be null
    public bool correct;
    [Space(10)]
    public UnityEvent onClick;
    // Stimulus is only clickable during response window, if not already clicked
    // Can be switched off during demos and stimulus presentation through ClickBlocker component
    bool clickable = true;
    bool wasClicked = false;
    float startTime;
    float? responseTime;
    void OnEnable() => clickable = false; 
    public void StartResponseWindow()
    {
        startTime = Time.time;
        clickable = true;
        wasClicked = false;
        responseTime = null;
        responseWindowStart.Raise(this);
    }
    void OnMouseDown()
    {
        if (clickable)
        {
            responseTime = Time.time - startTime;
            clickable = false;
            wasClicked = true;
            onClick.Invoke();
        }
    }
    public void LogData()
    {
        loggedResponseTime.Value = responseTime;
        loggedCorrect.boolValue = wasClicked == correct;
        if (id != null) loggedId.Value = id.Value;
        responseWindowEnd.Raise();
    }

    // Used by scientist
    public void Click() => onClick.Invoke();
    public void SetClickable(bool clickable) => this.clickable = clickable;
}