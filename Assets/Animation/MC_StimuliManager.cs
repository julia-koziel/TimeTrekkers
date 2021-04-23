using UnityEngine;

public class MC_StimuliManager : MonoBehaviour
{
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable direction;
    public IntVariable LEFT;
    public IntVariable trial;
    public SnowballParticleSystem snowballs;
    public Stimulus leftBase;
    public Stimulus rightBase;
    public GameEvent trialEnd;
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        var isLeft = direction.Value == LEFT;
        leftBase.SetActive(true);
        rightBase.SetActive(true);
        leftBase.correct = isLeft;
        rightBase.correct = !isLeft;

        snowballs.SetActive(true);
        snowballs.switchOn();
        leftBase.StartResponseWindow();
        rightBase.StartResponseWindow();
        if (trial==1)
        {
            snowballs.nSnowballs.Value=0;
        }
    }

    public void OnResponseWindowEnd()
    {
        trialEnd.Raise();
    }

    public void Reset()
    {
        inputVariablesManager.Reset();
    }
}