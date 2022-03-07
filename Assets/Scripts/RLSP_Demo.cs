using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RLSP_Demo : MonoBehaviour
{   
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable side;
    public CategoricalInputVariable probindex;
    public IntVariable LEFT;
    public IntVariable RIGHT;
    public IntVariable trial;
    public IntVariable clickedId;
    public IntVariable rewardedId;
    public IntVariable unrewardedId;
    public GameObject food;
    
    public BoolVariable correct;
    public BoolVariable participantsGo;
    public BoolVariable criterionReached;
    public BoolVariable RL;
    public IntVariable criterion;
    public IntVariable criterionTracker;
    public IntVariable score;
    public GameEvent trialEnd;
    public GameObject ITI;
   
    [Space(10)]
    public List<Stimulus> rewardedStimuli;
    public List<Stimulus> unrewardedStimuli;
    Stimulus[] _rs;
    Stimulus[] _urs;
    Stimulus swapStimulus;
    public Stimulus[] all;
    public Transform leftPos;
    public Transform centrePos;
    public Transform rightPos;

    public Vector3 position;
    public int CrValue;
    public float rewardDuration;

    List<int>Cr = new List<int>();
    

    void OnEnable()
    {
        _rs = rewardedStimuli.ToArray();
        _urs = unrewardedStimuli.ToArray();
    }
    public void OnStartTrial()
    {   
         inputVariablesManager.updateInputVariables();

        if (participantsGo && trial == 0)
        {
            rewardedStimuli[0].transform.position = leftPos.position;
            rewardedStimuli[1].transform.position = rightPos.position;
            rewardedStimuli.ForEach(s => s.SetActive(true));
        }
        else if (participantsGo && trial == 1)
        {
            unrewardedStimuli[0].transform.position = leftPos.position;
            rewardedStimuli[1].transform.position = rightPos.position;
            unrewardedStimuli.ForEach(s => s.SetActive(true));
        }
        else
        {
            var rewarded = rewardedStimuli[Random.Range(0, 2)];
            var unrewarded = unrewardedStimuli[Random.Range(0, 2)];
            
            rewardedId.Value = rewarded.id;
            unrewardedId.Value = unrewarded.id;

            rewarded.transform.position = side.Value == LEFT ? leftPos.position : rightPos.position;
            unrewarded.transform.position = side.Value == RIGHT ? leftPos.position : rightPos.position;
            rewarded.SetActive(true);
            unrewarded.SetActive(true);
        }
    }

    // Priority listener
    public void OnEndResponseWindow()
    {
        if (participantsGo)
        {
            if (trial == 0)
            {
                rewardedId.Value = clickedId;
                swapStimulus = rewardedStimuli.First(s => s.id != clickedId.Value);
                side.Value = rewardedStimuli.IndexOf(swapStimulus) == 0 ? RIGHT : LEFT;

                rewardedStimuli.Remove(swapStimulus);
                unrewardedId.Value = swapStimulus.id;
                swapStimulus.correct = false;
            }
            else if (trial == 1)
            {
                unrewardedId.Value = clickedId;
                unrewardedStimuli.Add(swapStimulus);

                swapStimulus = unrewardedStimuli.First(s => s.id != clickedId.Value);
                side.Value = unrewardedStimuli.IndexOf(swapStimulus) == 0 ? LEFT : RIGHT;

                unrewardedStimuli.Remove(swapStimulus);
                rewardedId.Value = swapStimulus.id;
                swapStimulus.correct = true;
                rewardedStimuli.Add(swapStimulus);
            }
        }
        
        score.Value = correct ? score + 1 : score - 1;

        this.In(rewardDuration).Call(trialEnd.Raise);
    }

    public void Reset()
    {
        StopAllCoroutines();
        inputVariablesManager.Reset();
        rewardedStimuli = _rs.ToList();
        unrewardedStimuli = _urs.ToList();
        rewardedStimuli.ForEach(s => s.correct = true);
        unrewardedStimuli.ForEach(s => s.correct = false);
    }
}