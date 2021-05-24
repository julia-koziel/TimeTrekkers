using UnityEngine;

public class MinimumCorrectCondition : MonoBehaviour 
{
    public IntVariable trial;
    public IntVariable nTrial;
    public BoolVariable correct;
    public GameEvent stageRepeat;
    public int minimumCorrect;
    int cumulativeCorrect = 0;
    public void OnTrialEnd()
    {
        if (correct) cumulativeCorrect++;
        if (trial == nTrial - 1)
        {
            if (cumulativeCorrect < minimumCorrect) stageRepeat.Raise();
        }
    }

    public void Reset()
    {
        cumulativeCorrect = 0;
    }
}