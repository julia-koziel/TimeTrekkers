using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class TD_Scientist : MonoBehaviour
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
    public float clickDuration = 0.5f;
    public GameEvent pause;
    public GameEvent play;
    public StimulusGameEvent responseWindowStart;
    public Stimulus Now;
    public Stimulus Later;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        
    }

    public void clickTD()
        {
            StartCoroutine(Click(Later.transform));
        }

    public void clickTD2()
    {
        StartCoroutine(Click(Now.transform));
    }

    public IEnumerator Click(Transform target)
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
            transform.position = startPos + (xTarget-startPos.x)*delta * Vector3.right;
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
}
