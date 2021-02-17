using UnityEngine;

public class BOB_StimuliManager : MonoBehaviour
{
    public GameEvent trialEnd;
    [Space(10)]
    public FloatVariable speed;
    public IntVariable trial;
    public IntVariable nTrials;
    public BoolVariable correct;
    public GameObject button;
    public IntVariable score;
    public int blockLength;
    public float maxJitter;
    [Space(10)]
    public GameObject Bubble;
    public Optimiser optimiser;
    Animator bubbleAnimator;
    float scoreSum;
    
    void Start()
    {
        bubbleAnimator = Bubble.GetComponent<Animator>();
        button.SetActive(false);
    }
    void OnEnable() => optimiser.SetActive(true);
    public void OnStartTrial()
    {
        // When score is lagging by a block, skip orange (1) indigo (5) magenta (7) or silver(8)
        if ((score.Value / blockLength).In(1, 5, 7, 8) && trial - score >= blockLength) score.Value += blockLength;
        // Get colour index
        // var colour = score / blockLength;

        this.In(Random.Range(0, maxJitter)).Call(() => {
            Bubble.SetActive(true);
            // bubbleAnimator.SetInteger("Colour", colour);
        });
    }

    public void OnResponseWindowEnd()
    {
        // if trial == nTrials - 1 make final bubble
        // (max score = nTrials, but we only really need to know rest of block)
        
        scoreSum += correct * speed;

        if (!correct) trialEnd.Raise();
    }

    public void OnTrialEnd()
    {
        if ((trial + 1) % blockLength == 0)
        {
            optimiser.RegisterPoint(scoreSum / blockLength);
            scoreSum = 0;

            // optimiser.DrawGraph();
            if (trial == nTrials - 1) optimiser.SaveMax();
            
        }
    }

    public void Reset()
    {
        scoreSum = 0;
    }
}