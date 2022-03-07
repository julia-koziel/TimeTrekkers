using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioTranslator))]

public class RLSP_Animation : MonoBehaviour
{
    public GameEvent stageEnd;
    public GameObject Theo;
    public GameObject background;
    public GameObject canvas;
    [Space(10)]

    public TranslatableAudioClip HiThere;
    public TranslatableAudioClip Play;
   
    AudioSource audioSource;
    AudioTranslator audioTranslator;

    void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        audioTranslator = GetComponent<AudioTranslator>();
    }

    void OnEnable()
    {

        this.BuildMovement()
            
            .First(() => {audioTranslator.Play(HiThere); background.SetActive(true);})
            .Then(duration: (audioTranslator.getLength(HiThere)), run: dt => {})
            .Then(() => {canvas.SetActive(false);})
            .Then(stageEnd.Raise)
            .Start();
    }

    void SetActive(params GameObject[] stimuli) => stimuli.ForEach(s => s.SetActive(true));

    void Stabilise(params GameObject[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("stable"));
    }

    void AnimateMovement(params GameObject[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("speak"));
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

    public void Reset()
    {
        audioSource.Stop();
        StopAllCoroutines();
    }
}
