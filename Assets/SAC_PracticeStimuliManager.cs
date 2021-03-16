using UnityEngine;
using System.Linq;
using System;

public class SAC_PracticeStimuliManager : MonoBehaviour
{
    public GameEvent trialEnd;
    public GameEvent stageEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable trialType;
    public CategoricalInputVariable stimulusId;
    public IntVariable TARGET;
    public IntVariable trial;

    [Space(10)]
    public Stimulus[] stimuli;
    public BoolVariable correct;
    public PatternDisplayer template;
    public GameObject pattern;
    public GameObject background;
    public GameObject redline;
    public int maxMissedHits;
    int missedHits = 0;

    public TranslatableAudioClip PressOnTheLastShip;
    public AudioTranslator audioTranslator;


    public void OnAwake()
    {
        background.SetActive(true);
        audioTranslator = GetComponent<AudioTranslator>();

    }
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        var stimulus = stimuli.Where(s => s.id == stimulusId.Value).First();
        stimulus.correct = trialType.Value == TARGET;
        Debug.Log(stimulus.correct);

        stimulus.SetActive(true);
        pattern.SetActive(true);

        if(trial==3)
        {
            audioTranslator.Play(PressOnTheLastShip);
        }

        else if(trial==4 | trial==5 | trial==18 | trial==22 |trial==23)
        {
            redline.SetActive(true);

        }
        else
        {
            redline.SetActive(false);
        }
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