using UnityEngine;

public class GNG_PretestStimuliManager : MonoBehaviour
{
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable side;
    public IntVariable LEFT;
    public IntVariable RIGHT;
    public IntVariable trial;
    public BoolVariable correct;
    public GameEvent trialEnd;
    public GameEvent stageRepeat;
    public BoolVariable participantsGo;
    [Space(10)]
    public Stimulus Go;
    public Stimulus NoGo;
    public Transform leftPos;
    public Transform rightPos;
    
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        if (side.Value == LEFT)
        {
            Go.transform.position = leftPos.position;
            NoGo.transform.position = rightPos.position;
        }
        else
        {
            Go.transform.position = rightPos.position;
            NoGo.transform.position = leftPos.position;
        }

        Go.SetActive(true);
        NoGo.SetActive(true);
    }

    public void OnEndResponseWindow()
    {
        if (participantsGo && !correct)
        {
            this.In(1).Call(stageRepeat.Raise);
        }
        else this.In(1).Call(trialEnd.Raise);
    }

    public void Reset()
    {
        StopAllCoroutines();
        inputVariablesManager.Reset();
    }
}