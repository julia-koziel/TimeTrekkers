using UnityEngine;

public class MC_StimuliManager : MonoBehaviour 
{
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable direction;
    public IntVariable LEFT;
    public SpaceshipParticleSystem spaceships;
    public Stimulus leftPlanet;
    public Stimulus rightPlanet;
    public GameEvent trialEnd;
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        var isLeft = direction.Value == LEFT;
        leftPlanet.correct = isLeft;
        rightPlanet.correct = !isLeft;

        spaceships.switchOn();
        leftPlanet.StartResponseWindow();
        rightPlanet.StartResponseWindow();
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