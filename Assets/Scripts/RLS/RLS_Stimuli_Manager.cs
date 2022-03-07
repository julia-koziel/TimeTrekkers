using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RLS_Stimuli_Manager : MonoBehaviour
{   
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable side;
    public CategoricalInputVariable audio;
    public IntVariable LEFT;
    public IntVariable RIGHT;
    public IntVariable trial;
    public IntVariable clickedId;
    public IntVariable rewardedId;
    public IntVariable unrewardedId;
    public BoolVariable correct;
    public BoolVariable participantsGo;
    public IntVariable score;
    public GameEvent trialEnd;
    public GameObject ITI;
    public GameEvent StageEnd;
    public GameObject food;
    [Space(10)]
    public List<Stimulus> rewardedStimuli;
    public List<Stimulus> unrewardedStimuli;
    Stimulus[] _rs;
    Stimulus[] _urs;
    Stimulus swapStimulus;
    public Stimulus[] all;
    public Transform leftPos;
    public Transform rightPos;
    public Transform centre;

    public Vector3 position;
    public float rewardDuration;

    void OnEnable()
    {
        _rs = rewardedStimuli.ToArray();
        _urs = unrewardedStimuli.ToArray();
    }
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();
        audio.Value = Random.Range(0,1);

        if (participantsGo && trial == 0)
        {
            rewardedStimuli[0].transform.position = leftPos.position;
            rewardedStimuli[1].transform.position = rightPos.position;
            rewardedStimuli.ForEach(s => s.SetActive(true));
        }
        else if (participantsGo && trial == 1)
        {
            unrewardedStimuli[0].transform.position = leftPos.position;
            unrewardedStimuli[1].transform.position = rightPos.position;
            unrewardedStimuli.ForEach(s => s.SetActive(true));
        }
        else if (participantsGo && trial<19)
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

        else 
        {
            rewardedStimuli.ForEach(s => s.correct = false);
            unrewardedStimuli.ForEach(s => s.correct = true);
           
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

            else if (trial>39)
            {
                StageEnd.Raise();
                SceneManager.LoadScene(7);
            }

        }
        
        // TODO replace with 'rewarded'

        score.Value = correct ? score + 1 : score - 1;
        position = all[clickedId].transform.position;
        position.y = position.y-2;
        food.transform.position = position;

         this.In(rewardDuration).Call(() => 
        {
            ITI.SetActive(true);

            this.In(2).Call(() => 
            {
                ITI.SetActive(false);
                trialEnd.Raise();

        });
     });
    }
    

    public void Reset()
    {
        StopAllCoroutines();
        inputVariablesManager.Reset();
        rewardedStimuli = _rs.ToList();
        unrewardedStimuli = _urs.ToList();
        rewardedStimuli.ForEach(s => s.correct = true);
        rewardedStimuli.ForEach(s => s.correct = true);
    }
}