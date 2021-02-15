using UnityEngine;

public class GNG_StimuliManager : MonoBehaviour
{
    public GameEvent trialEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable trialType;
    public IntVariable GO;
    public IntVariable NOGO;
    [Space(10)]
    public GameObject Go;
    public GameObject NoGo;
    public GameObject Break;
    [Space(10)]
    public BoolVariable correct;
    public int maxMissedHits;
    bool isBreakTrial = false;
    int missedHits = 0;
    
    public void OnStartTrial()
    {
        if (isBreakTrial)
        {
            Break.SetActive(true);
            isBreakTrial = false;
            return;
        }
        
        inputVariablesManager.updateInputVariables();
        if (trialType.Value == GO) Go.SetActive(true);
        else NoGo.SetActive(true);
    }

    public void OnResponseWindowEnd()
    {
        if (trialType.Value == GO)
        {
            if (!correct) missedHits++;
            else missedHits = 0;
        } 
        if (missedHits == maxMissedHits)
        {
            isBreakTrial = true;
            missedHits = 0;
        }
        trialEnd.Raise();
    }

    public void Reset()
    {
        missedHits = 0;
        inputVariablesManager.Reset();
    }
}