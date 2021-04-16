using UnityEngine;

public class MC_StimuliManager : MonoBehaviour
{
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable direction;
    public IntVariable LEFT;
    public SnowballParticleSystem snowballs;
    public Stimulus leftBase;
    public Stimulus rightBase;
    public GameEvent trialEnd;
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        var isLeft = direction.Value == LEFT;
        leftBase.correct = isLeft;
        rightBase.correct = !isLeft;

        snowballs.switchOn();
        leftBase.StartResponseWindow();
        rightBase.StartResponseWindow();
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