using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// TODO implement tighter stopping system

public class Movement
{
    MonoBehaviour mb;
    List<MovementStep> steps;
    public Movement(MonoBehaviour mb, float duration = 1, bool linear = false, bool reverse = false, params Action<float>[] run) : this(mb)
    {
        steps.Add(new MovementStep(duration, linear, reverse, run));
    }
    public Movement(MonoBehaviour mb, params Action[] run) : this(mb)
    {
        steps.Add(new MovementStep(run));
    }
    public Movement(MonoBehaviour mb)
    {
        this.mb = mb;
        steps = new List<MovementStep>();
    }
    public Movement First(float duration = 1, bool linear = false, bool reverse = false, params Action<float>[] run)
    {
        steps.Add(new MovementStep(duration, linear, reverse, run));
        return this;
    }
    public Movement First(params Action[] run)
    {
        steps.Add(new MovementStep(run));
        return this;
    }
    public Movement Then(float duration = 1, bool linear = false, bool reverse = false, params Action<float>[] run)
    {
        var step = new MovementStep(duration, linear, reverse, run);
        steps[steps.Count - 1].onEnd = delegate { step.runStep(mb); };
        steps.Add(step);
        return this;
    }
    public Movement Then(params Action[] run)
    {
        var step = new MovementStep(run);
        steps[steps.Count - 1].onEnd = delegate { step.runStep(mb); };
        steps.Add(step);
        return this;
    }
    public void Start()
    {
        if (steps.Count > 0) steps[0].runStep(mb);
        // TODO add "no movements added"
    }
    public void Stop() 
    {
        mb.StopAllCoroutines();
        steps.ForEach(s => s.isStopped = true);
    }

    public class MovementStep
    {
        Action<float>[] animFuncs;
        Action[] nonAnimFuncs;
        AnimationCurve curve;
        internal Action onEnd;
        float duration;
        bool linear;
        bool reverse;
        bool isCoroutine;
        public bool isStopped = false;
        public MovementStep(float duration = 1, bool linear = false, bool reverse = false, params Action<float>[] animFunc)
        {
            this.animFuncs = animFunc;
            this.duration = duration;
            this.linear = linear;
            this.reverse = reverse;
            this.curve = linear ? AnimationCurve.Linear(0, 0, duration, 1) : AnimationCurve.EaseInOut(0, 0, duration, duration);
            this.isCoroutine = true;
        }
        public MovementStep(params Action[] nonAnimFuncs)
        {
            this.nonAnimFuncs = nonAnimFuncs;
            this.isCoroutine = false;
        }
        // TODO allow external references
        internal IEnumerator move()
        {
            float time = 0;
            while (time < duration)
            {
                time += Time.deltaTime;
                float d = reverse ? curve.Evaluate(duration-time) : curve.Evaluate(time);
                animFuncs.ForEach(f => f(d));
                yield return 0;
            }
            if (onEnd != null) onEnd();
        }

        public void runStep(MonoBehaviour mb)
        {
            if (!isStopped)
            {
                if (isCoroutine) mb.StartCoroutine(move());
                else
                {
                    nonAnimFuncs.ForEach(f => f());
                    if (onEnd != null) onEnd();
                }
            }
        }
    }
}

public static class MovementExtension
{
    public static Movement BuildMovement(this MonoBehaviour mb) => new Movement(mb);
}
