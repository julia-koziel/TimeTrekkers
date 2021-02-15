using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(StimulusMover))]
public class DelayedInputVariableLogger : MonoBehaviour 
{
    StimulusMover stimulusMover;
    public FloatVariable[] variables;
    float[] trackedValues;
    public FloatVariable[] loggedVariables;
    void OnEnable() 
    {
        stimulusMover = GetComponent<StimulusMover>();
        Assert.AreEqual(variables.Length, loggedVariables.Length, "Variables and logged variables need to be the same length");
        trackedValues = new float[variables.Length];
        variables.ForEach((i, v) => trackedValues[i] = v);
    }

    public void LogVariables()
    {
        if (!stimulusMover.IsMoving) trackedValues.ForEach((i, v) => loggedVariables[i].Value = v);
    }
}