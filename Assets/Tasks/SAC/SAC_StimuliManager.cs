using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class SAC_StimuliManager : MonoBehaviour 
{
    public GameEvent trialEnd;
    public GameEvent stageEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable trialType;
    public CategoricalInputVariable stimulusId;
    public IntVariable TARGET;
    public IntVariable trial;
    public BoolVariable level1;
    public BoolVariable level2;
    [Space(10)]
    public Stimulus[] stimuli;
    public BoolVariable correct;
    // public StageList stages;
    public GameObject pattern;
    public int maxMissedHits;
    int missedHits = 0;

    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        var stimulus = stimuli.Where(s => s.id == stimulusId.Value).First();
        stimulus.correct = trialType.Value == TARGET;

        Debug.Log(stimulus.correct);
        level1.Value=1;
        PlayerPrefs.SetInt("level1", 1);

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

        if (missedHits == maxMissedHits) 
        {stageEnd.Raise();
        }
        else trialEnd.Raise();
    }

    public void Reset()
    {
        missedHits = 0;
        inputVariablesManager.Reset();
    }
}