using UnityEngine;
using System.Linq;

public class SAC_Level3_Stimuli_Manager : MonoBehaviour
{
    public GameEvent trialEnd;
    public GameEvent stageEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable trialType;
    public CategoricalInputVariable stimulusId;
    public IntVariable TARGET;
    public BoolVariable level1;
    public BoolVariable level2;
    [Space(10)]
    public Stimulus[] stimuli;
    public BoolVariable correct;
    public PatternDisplayer template;
    public GameObject pattern;
    public int maxMissedHits;
    int missedHits = 0;

    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        var stimulus = stimuli.Where(s => s.id == stimulusId.Value).First();
        stimulus.correct = trialType.Value == TARGET;

        Debug.Log(stimulus.correct);

        stimulus.SetActive(true);
        pattern.SetActive(true);
    }

    public void OnResponseWindowEnd()
    {
        if (trialType.Value == TARGET)
        {
            if (!correct) missedHits++;
            else missedHits = 0;
        }

        if (missedHits == maxMissedHits) stageEnd.Raise();
        else trialEnd.Raise();
    }

    public void Reset()
    {
        missedHits = 0;
        inputVariablesManager.Reset();
    }
}