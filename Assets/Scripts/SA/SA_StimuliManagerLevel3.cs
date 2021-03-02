using UnityEngine;

public class SA_StimuliManagerLevel3 : MonoBehaviour 
{
    public GameEvent trialEnd;
    public GameEvent stageEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable trialType;
    public CategoricalInputVariable shipId;
    public IntVariable target1;
    public IntVariable target2;
    public IntVariable finaltarget;
    public IntVariable OTHER;
    [Space(10)]

    public GameObject[] shipPrefabs;
    public BoolVariable correct;
    public IntVariable trial;
    public IntVariable loggedTrial;
    public IntVariable loggedshipType;
    public IntVariable nTrials;
    public int maxMissedHits;
    public float separationDistance;
    public int x;
    public int y;
    public int target2trial;
    public int finaltrial;
    
    int missedHits = 0;
    float speed;


    void Awake() 
    {
        speed = shipPrefabs[0].GetComponent<StimulusMover>().speed;
    }

    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        if (shipId.Value == target1) 
        {
            trial.Value = x;
            target2trial =x+1;
        }

        else if (trial.Value == target2trial)
        {   
            shipId.Value = target2;
            finaltrial = target2trial+1;
        }

        else if (trial.Value == finaltrial)
        {
            shipId.Value = finaltarget;
        }


        Instantiate(shipPrefabs[shipId]);
        Debug.Log(shipPrefabs[shipId]);

        if (trial < nTrials - 1) this.In(separationDistance / speed).Call(trialEnd.Raise);

    }

    public void OnResponseWindowEnd()
    {
        // if (loggedshipType.intValue == PIP)
        // {
        //     if (!correct) missedHits++;
        //     else missedHits = 0;
        // }

        // if (missedHits == maxMissedHits) stageEnd.Raise();

        // Wait until all ships offscreen before ending trialRunner
        if (loggedTrial == nTrials - 1) trialEnd.Raise();
    }

    public void Reset()
    {
        missedHits = 0;
        inputVariablesManager.Reset();
    }
}