using System;
using System.Collections.Generic;
using UnityEngine;

public class TrialRunner : MonoBehaviour, IGameEventListener
{
    public GameEvent onStartTrial;
    public GameEvent onEndTrial;
    public GameEvent onEndStage;
    public IntVariable nTrials;
    public IntVariable trial;
    float time;
    public float interTrialInterval;
    public bool running {get; set;} // TODO see what default is
    bool betweenTrials;
    public void StartTrials() 
    { 
        time = interTrialInterval; 
        trial.Value = 0; 
        running = true; 
        betweenTrials = true; 
    }
    public void Pause() => running = false;
    public void Play() => running = true;

    private void OnEnable() => RegisterListener();
    private void OnDisable() => UnregisterListener();
    public void RegisterListener() => onEndTrial.RegisterListener(this);
    public void UnregisterListener() => onEndTrial.UnregisterListener(this);
    public void OnEventRaised() => OnEndTrial();

    void Update()
    {
        if (betweenTrials && running) 
        {
            time += Time.deltaTime;
            if (time > interTrialInterval)
            {   
                betweenTrials = false;

                if (trial < nTrials) onStartTrial.Raise(); 
                else OnEndTrials();
            }
        }
    }

    public void OnEndTrial()
    {
        time = 0;
        trial++;
        betweenTrials = true;
    }

    void OnEndTrials()
    {
        running = false;
        onEndStage.Raise();
    }

    public void Reset()
    {
        betweenTrials = false;
        running = false;
        trial.intValue = 0; // TODO am i needed
        time = 0;
    }
}