using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using AudioTiming = DemoProtocol.AudioTiming;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioTranslator))]
public class Scientist : MonoBehaviour, IGameEventListener<Stimulus>
{
    Animator animator { get => GetComponent<Animator>(); }
    AudioTranslator audioTranslator { get => GetComponent<AudioTranslator>(); }
    Queue<Stimulus> stimuli = new Queue<Stimulus>();
    Stimulus stimulus;
    Vector3 startPos;
    public DemoProtocol demoProtocol;
    public IntVariable trial;
    public List<int> trialsSeen;
    public float reactionTime;
    float preClickDelay = 0;
    public float clickDuration = 0.5f;
    public GameEvent pause;
    public GameEvent play;
    public StimulusGameEvent responseWindowStart;
    private void OnEnable() 
    {
        RegisterListener();
        reactionTime = demoProtocol?.reactionTime ?? reactionTime;
        trialsSeen = new List<int>();
    } 
    private void OnDisable() => UnregisterListener();
    public void RegisterListener() => responseWindowStart.RegisterListener(this);
    public void UnregisterListener() => responseWindowStart.UnregisterListener(this);
    public void OnEventRaised(Stimulus stimulus)
    {
        // Do I have a protocol and does it cover this trial?
        if (demoProtocol != null && trial.intValue.In(demoProtocol.trialNumbers))
        {
            var i = demoProtocol.trialNumbers.IndexOf(trial);
            var preClickClip = demoProtocol.preClickClips[i];
            var onClickClip = demoProtocol.onClickClips[i];
            var postClickClip = demoProtocol.postClickClips[i];

            if (!trial.intValue.In(trialsSeen.ToArray()))
            {
                preClickDelay = audioTranslator.getLength(preClickClip);
                if (preClickClip != null) this.In(reactionTime).Call(() => audioTranslator.Play(preClickClip));
                trialsSeen.Add(trial);
            }

            // If stimulus is correct and I am not meant to click wrong
            // Or if stimulus is incorrect and I am meant to click wrong
            if (stimulus.correct == !demoProtocol.wrongClicks[i])
            {
                float onClickDelay = preClickDelay + reactionTime;
                float postClickDelay = onClickDelay + clickDuration + reactionTime;

                float audioDelay = reactionTime;
                stimuli.Enqueue(stimulus);
                this.In(preClickDelay).Call(() => StartCoroutine(Click(stimulus.transform)));
                if (onClickClip != null) this.In(onClickDelay).Call(() => audioTranslator.Play(onClickClip));
                if (postClickClip != null) this.In(postClickDelay).Call(() => {
                    pause.Raise();
                    audioTranslator.Play(postClickClip);
                    this.In(audioTranslator.getLength(postClickClip)).Call(play.Raise);
                });
            }
        }
        // No protocol, click if correct
        else if (stimulus.correct) 
        {
            stimuli.Enqueue(stimulus);
            StartCoroutine(Click(stimulus.transform));
        }
    }

    void OnClick()
    {
        var stimulus = stimuli.Dequeue();
        stimulus.Click();
    }

    IEnumerator Click(Transform target)
    {
        yield return new WaitForEndOfFrame();
        var x = target.position.x;
        var time = Time.time;
        yield return 0;
        yield return new WaitForEndOfFrame();
        var dTime = Time.time - time;
        var newX = target.position.x;
        var dist = newX - x;
        var speed = dist / dTime;
        var targetX = newX + speed * (reactionTime + clickDuration);
        
        startPos = transform.position;

        Action<float> wait = delta => {};

        Action<float> moveToTarget = delta => {
            var xTarget = CameraTransition.convertMainToScientist(targetX * Vector3.right).x;
            transform.position = startPos + (xTarget-startPos.x)*delta * Vector3.right / clickDuration;
            // Change this to allow for continual following?
            // If yes, then 
        };
        var moveWithClick = this.BuildMovement()
                        .First(run: wait, duration: reactionTime)
                        .Then(run: () => animator.SetTrigger("click"))
                        .Then(run: moveToTarget, duration: clickDuration)
                        .Then(run: moveToTarget, duration: clickDuration, reverse: true);
        
        moveWithClick.Start();
    }

    public void Reset()
    {
        animator.ResetTrigger("click");
        animator.Play("Sprite Layer.Resting");
        // Start position handled by camera transition
        audioTranslator.audioSource.Stop();
        StopAllCoroutines();
        demoProtocol = null;
    }
}