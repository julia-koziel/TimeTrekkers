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
    public BoolVariable level1;
    public BoolVariable level2;
    [Space(10)]
    public Stimulus[] stimuli;
    public BoolVariable correct;
    public StageList stages;
    public PatternDisplayer template;
    public GameObject pattern;
    public int maxMissedHits;
    int missedHits = 0;

    public void OnStartTrial()
    {
        if (level1.Value==1)
        {
            stageEnd.Raise();
        }
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
        else SceneManager.LoadScene(2);
    }

    public void Reset()
    {
        missedHits = 0;
        inputVariablesManager.Reset();
    }
}