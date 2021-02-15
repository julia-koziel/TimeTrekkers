using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu(menuName="Custom/VariableManager/InputVariablesManager")]
public class InputVariablesManager : ScriptableObject
{
    public InputVariable[] inputVariables;
    public IntVariable trial;
    public IntVariable nTrials;
    // TODO allow block length shuffling
    // TODO offer dict-like interface?
    float[,] _trialMatrix;
    public float[,] TrialMatrix
    {
        private get
        {
            if (_trialMatrix == null) _trialMatrix = Randomiser.Randomise(inputVariables, nTrials);
            return _trialMatrix;
        }
        set
        {
            Assert.AreEqual(value.GetLength(1), inputVariables.Length, $"Error: matrix does not have values for all input variables, given: {value.GetLength(1)} IV(s), required: {inputVariables.Length} IV(s)");
            nTrials.Value = value.GetLength(0);
            _trialMatrix = value;
        }
    }
    public float[] getInputVariableVals()
    {
        return TrialMatrix.Row(trial);
    }
    public void updateInputVariables()
    {
        var ivs = getInputVariableVals();
        for (int i = 0; i < ivs.Length; i++)
        {
            inputVariables[i].Value = ivs[i];
        }
    }

    // TODO reshuffle from current trial

    public void Reset()
    {
        _trialMatrix = null;
    }
}