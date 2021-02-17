using UnityEngine;

public class BOB_PretestStimuliManager : MonoBehaviour 
{
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable bubbleType;
    public IntVariable STILL;
    public IntVariable DRIFTING;
    public Stimulus bubble;
    public IntVariable trial;
    public IntVariable nTrials;
    public GameObject edges;
    Animator bubbleAnimator;
    StimulusDrifter bubbleDrifter;
    Rect innerBounds = new Rect(-8, -5, 16, 5);
    Rect fullyOnScreenBounds;
    Rect onScreenBounds;
    Rect offScreenBounds;
    int[] colours = new int[] { 4, 2 }; // blue, yellow

    void Start() 
    {
        bubbleAnimator = bubble.GetComponent<Animator>();
        bubbleDrifter = bubble.GetComponent<StimulusDrifter>();

        var bubbleExtents = bubble.GetComponent<SpriteRenderer>().bounds.extents;
        fullyOnScreenBounds = new Rect(innerBounds);
        fullyOnScreenBounds.xMin += bubbleExtents.x;
        fullyOnScreenBounds.xMax -= bubbleExtents.x;
        fullyOnScreenBounds.yMin += bubbleExtents.y;
        fullyOnScreenBounds.yMax -= bubbleExtents.y;
        
        onScreenBounds = new Rect(innerBounds);
        onScreenBounds.xMin -= bubbleExtents.x;
        onScreenBounds.xMax += bubbleExtents.x;
        onScreenBounds.yMin -= bubbleExtents.y;

        offScreenBounds = new Rect(onScreenBounds);
        offScreenBounds.xMin -= bubbleExtents.x / 5;
        offScreenBounds.xMax += bubbleExtents.x / 5;
        offScreenBounds.yMin -= bubbleExtents.y / 5;
    }

    void OnEnable() 
    {
        edges.SetActive(true);
    } 
    
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        Vector2 pos;
        if (bubbleType.Value == STILL)
        {
            pos = fullyOnScreenBounds.RandomPointInRect();
            bubbleDrifter.enabled = false;
        }
        else
        {
            if (trial == nTrials - 1) pos = new Vector2(-9.5f, -1);
            else 
            {
                do
                {
                    pos = offScreenBounds.RandomPointInRect();
                } while (onScreenBounds.Contains(pos));
            }
        }


        bubble.transform.position = pos;
        bubble.SetActive(true);

        var colour = colours[trial / 3];
        bubbleAnimator.SetInteger("Colour", colour);
    }

    public void Reset() => inputVariablesManager.Reset();
}