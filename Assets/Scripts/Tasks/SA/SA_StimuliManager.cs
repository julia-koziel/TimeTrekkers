using UnityEngine;

public class SA_StimuliManager : MonoBehaviour 
{
    public GameEvent trialEnd;
    public GameEvent stageEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable carType;
    public CategoricalInputVariable carId;
    public IntVariable PIP;
    public IntVariable OTHER;
    [Space(10)]
    public GameObject[] carPrefabs;
    public BoolVariable correct;
    public IntVariable trial;
    public IntVariable loggedTrial;
    public IntVariable loggedCarType;
    public IntVariable nTrials;
    public int maxMissedHits;
    public float separationDistance;
    int missedHits = 0;
    float speed;
    void Awake() 
    {
        speed = carPrefabs[0].GetComponent<StimulusMover>().speed;
    }

    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        if (carType.Value == PIP) carId.Value = PIP;

        Instantiate(carPrefabs[carId]);

        if (trial < nTrials - 1) this.In(separationDistance / speed).Call(trialEnd.Raise);
    }

    public void OnResponseWindowEnd()
    {
        if (loggedCarType.intValue == PIP)
        {
            if (!correct) missedHits++;
            else missedHits = 0;
        }

        if (missedHits == maxMissedHits) stageEnd.Raise();

        // Wait until all cars offscreen before ending trialRunner
        if (loggedTrial == nTrials - 1) trialEnd.Raise();
    }

    public void Reset()
    {
        missedHits = 0;
        inputVariablesManager.Reset();
    }
}