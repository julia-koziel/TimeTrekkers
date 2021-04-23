using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

[RequireComponent(typeof(AudioSource))]
public class AP_StimuliManager : MonoBehaviour
{
    public GameEvent trialEnd;

    public GameEvent StageEnd;
    public InputVariablesManager inputVariablesManager;
    public BoolVariable participantsGo;
    public CategoricalInputVariable trialType;
    public IntVariable AUDITORY;
    public IntVariable CATCH;
    public CategoricalInputVariable side;
    public IntVariable LEFT;
    public IntVariable RIGHT;
    public CategoricalInputVariable order;
    public IntVariable FIRST;
    public IntVariable SECOND;
    public BoolVariable correct;
    public IntVariable trial;
    public Stimulus leftSpaceship;
    public Stimulus rightSpaceship;
    public NullableFloatVariable comparisonFrequency;
    int[] comparisonFrequencies = new int[] {500, 525, 550, 575, 600, 625, 650, 675, 700, 725, 750, 775, 800, 825, 850, 875, 900, 925, 950, 975, 1000};
    public AudioClip highestTone;
    public AudioClip[] tones;
    public GameObject signallerCorrect;
    public GameObject signallerIncorrect;
    public GameObject asteroids;
    public int comparisonIndex = 0;

    public float previous;
    public int reversal;
    public float stimulusDuration;
    public float ITI;
    Vector3 shipStartLeft = new Vector3(-2, 0, 0);
    Vector3 shipStartRight = new Vector3(2, 0,0);
    Vector3 shipSizeStart = new Vector3(0.01f, 0.01f, 0);
    Vector3 shipSize = new Vector3(1, 1, 0);
    Vector3 shipSizeEnd = new Vector3(3, 3, 0);
    Vector3 shipEndLeft = new Vector3(-15, 0, 0);
    Vector3 shipEndRight = new Vector3(15, 0, 0);
    Vector3 left = new Vector3(-3.5f, -2, 0);
    Vector3 right = new Vector3(3.5f, -2, 0);
    Vector3 centre = new Vector3(0, -4, 0);
    Vector3 squirrelStart = new Vector3(-12, -4, 0);
    Vector3 squirrelEnd = new Vector3(12, -4, 0);
    AudioSource audioSource;

    void Awake() => audioSource = GetComponent<AudioSource>();

    public void OnStartTrial()
    {
        asteroids.SetActive(false);
        signallerIncorrect.SetActive(false);
        signallerCorrect.SetActive(false);
        inputVariablesManager.updateInputVariables();
        previous=correct.Value;
        if (participantsGo)
        {
           trialType.Value = AUDITORY;
        }

        // Check trial type
        if (trialType.Value == AUDITORY)
        {
            // Correct side
            var correctSpaceship = side.Value == LEFT ? leftSpaceship : rightSpaceship;
            var wrongSpaceship = side.Value == LEFT ? rightSpaceship : leftSpaceship;

            // Set stimulus in/correct
            correctSpaceship.correct = true;
            wrongSpaceship.correct = false;

            // Correct order
            var firstSpaceship = order.Value == FIRST ? correctSpaceship : wrongSpaceship;
            var firstTone = order.Value == FIRST ? highestTone : tones[comparisonIndex];

            var secondSpaceship = order.Value == SECOND ? correctSpaceship : wrongSpaceship;
            var secondTone = order.Value == SECOND ? highestTone : tones[comparisonIndex];

            comparisonFrequency.Value = comparisonFrequencies[comparisonIndex];
            Debug.Log(comparisonFrequency.Value);
            this.BuildMovement()
                .First(() => SetActive(leftSpaceship, rightSpaceship))
                .Then(1, false, false, Move(leftSpaceship.transform, from: shipStartLeft, to: left, duration: 1),
                                        Increase(leftSpaceship.transform, from: shipSizeStart, to: shipSize, duration: 1),
                                        Move(rightSpaceship.transform, from: shipStartRight, to: right, duration: 1),
                                        Increase(rightSpaceship.transform, from: shipSizeStart, to: shipSize, duration: 1))
                                        
                .Then(() => Stabilise(leftSpaceship, rightSpaceship))
                .Then(run: dt => {})
                .Then(() => { MakeSing(firstSpaceship); audioSource.PlayOneShot(firstTone); })
                .Then(run: dt => {})
                .Then(() => Stabilise(firstSpaceship))
                .Then(run: dt => {})
                .Then(() => { MakeSing(secondSpaceship); audioSource.PlayOneShot(secondTone); })
                .Then(run: dt => {})
                .Then(() => Stabilise(secondSpaceship))
                .Then(run: dt => {})
                .Then(() => { StartResponseWindow(leftSpaceship, rightSpaceship); })
                .Start();
        
    }
    }

    void SetActive(params Stimulus[] stimuli) => stimuli.ForEach(s => s.SetActive(true));

    void Stabilise(params Stimulus[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("sit"));
    }

    void AnimateMovement(params Stimulus[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("move"));
    }

    void MakeSing(Stimulus stimulus) => stimulus.GetComponent<Animator>().SetTrigger("sing");

    void Reward()
    {
        if (correct.Value==1)
        {
           signallerCorrect.SetActive(true);
        }
        else
        {
            signallerIncorrect.SetActive(true);
        }

    }
    void StartResponseWindow(params Stimulus[] stimuli) => stimuli.ForEach(s => s.StartResponseWindow());

    public void OnEndResponseWindow()
    {
            var correctSpaceship = side.Value == LEFT ? leftSpaceship : rightSpaceship;
            var wrongSpaceship = side.Value == LEFT ? rightSpaceship : leftSpaceship;


        if (trialType.Value == AUDITORY)
        {
            // Reward();
            
            this.BuildMovement()
                
                .First(() => AnimateMovement(leftSpaceship, rightSpaceship))
                .Then(1, false, false, Move(leftSpaceship.transform, from: left, to: shipEndLeft, duration: 2),
                                        Move(rightSpaceship.transform, from: right, to: shipEndRight, duration: 2),
                                        Increase(leftSpaceship.transform, from: shipSize, to: shipSizeEnd, duration: 2),
                                        Increase(rightSpaceship.transform, from: shipSize, to: shipSizeEnd, duration: 2))
                .Then(Reward)
                .Then(trialEnd.Raise)
                .Start();

            var newIdx = correct ? comparisonIndex + 2 : comparisonIndex - 1;
            comparisonIndex = Mathf.Clamp(newIdx, 0, tones.Length - 1);
            
            Debug.Log(comparisonIndex);

            if (previous!=correct.Value)
               {
                   reversal = reversal+1;
                   Debug.Log(reversal);
               }
               else
               {
                   reversal=0;
               }

             if (trial.Value >30 && reversal >4)
            {
                StageEnd.Raise();
            }
        asteroids.SetActive(true);
        }

    }

    Action<float> Move(Transform transform, Vector3 from, Vector3 to, float duration)
    {
        var dist = to - from;
        var step = dist / duration;
        
        Action<float> movement = deltaTime =>
        {
            transform.position = from + step * deltaTime;
        };

        return movement;
    }

    Action<float> Increase(Transform transform, Vector3 from, Vector3 to, float duration)
    {
        var dist = to - from;
        var step = dist / duration;
        
        Action<float> movement = deltaTime =>
        {
            transform.localScale = from + step * deltaTime;
        };

        return movement;
    }

    public void Reset()
    {
        comparisonIndex = 0;
        inputVariablesManager.Reset();
    }

}

