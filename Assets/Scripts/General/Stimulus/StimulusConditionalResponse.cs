using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Stimulus))]
public class StimulusConditionalResponse : MonoBehaviour 
{
    public UnityEvent Rewarded;
    public UnityEvent Unrewarded;
    Stimulus stimulus;
    void Awake()
    {
        stimulus = GetComponent<Stimulus>();
    }

    public void Respond()
    {
        if (stimulus.correct) Rewarded.Invoke();
        else Unrewarded.Invoke();
    }

}