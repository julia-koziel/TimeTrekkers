using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Stimulus))]
public class StimulusConditionalResponseRLS : MonoBehaviour 
{
    public UnityEvent RewardedOne;
    public UnityEvent RewardedTwo;
    public UnityEvent UnrewardedOne;
    public UnityEvent UnrewardedTwo;

    public CategoricalInputVariable audio;
    Stimulus stimulus;
    void Awake()
    {
        stimulus = GetComponent<Stimulus>();
    }

    public void Respond()
    {
        if (stimulus.correct) 
        {
            if (audio==0)
            {
                RewardedOne.Invoke();
            }
            else 
            {
                RewardedTwo.Invoke();
            }
        }

        else
        {
            if (audio==0)
            {
                UnrewardedOne.Invoke();
            }
            else 
            {
                UnrewardedTwo.Invoke();
            }
        }
    }

}