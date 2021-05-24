using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioTranslator))]

public class MC_Animation : MonoBehaviour
{
    Movement sequence;
    public GameObject background;
    public GameObject Theo;
    public GameObject sabertooth;
    public GameObject mammoth;
    public GameObject snow;
    public GameObject shelter;
    public GameObject left;
    public GameObject right;
    public GameObject animation;
    float defaultSpeed;
    float slowerSpeed = 0.7f;
    float defaultLifetime;

    Vector3 camStart = new Vector3(75, -45, -10);
    Vector3 camEnd = new Vector3(0, 0, -10);
    float camStartSize = 100;
    float camEndSize = 5f;
    float camZoomSize = 0.3f;
    public SpaceshipParticleSystem snowballs;

    public TranslatableAudioClip sabrina1;
    public TranslatableAudioClip sabrina2;
    public TranslatableAudioClip sabrina3;
    public TranslatableAudioClip theo1;
    public TranslatableAudioClip theo2;
    public TranslatableAudioClip theo3;

    public GameEvent stageEnd;

    Camera cam;
    
    AudioSource audioSource;
    AudioTranslator audioTranslator;

    void Awake() 
    {
        audioTranslator = GetComponent<AudioTranslator>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        this.BuildMovement()
            
            .First(run: time => {})
                        .Then(() => {
                            cam.orthographicSize = camStartSize;
                            cam.transform.position = camStart;
                        })
            .Then(() => {audioTranslator.Play(theo1);AnimateMovement(Theo);})
            .Then(duration: (audioTranslator.getLength(theo1)), run: dt => {})
            .Then(() => {audioTranslator.Play(sabrina1);AnimateMovement(sabertooth);Stabilise(Theo);})
            .Then(duration: (audioTranslator.getLength(sabrina1)), run: dt => {})
            .Then(() => {audioTranslator.Play(theo2); AnimateMovement(Theo); Stabilise(sabertooth);})
            .Then(duration: (audioTranslator.getLength(theo2)), run:dt => {})
            .Then(() => {audioTranslator.Play(sabrina2); AnimateMovement(sabertooth); Stabilise(Theo);})
            .Then(duration: 3, run: dt => {})
            .Then(run:time=> {
                            cam.orthographicSize = camStartSize + (camEndSize - camStartSize) * Mathf.Pow(time, 3f);
                            cam.transform.position = camStart + (camEnd - camStart) * Mathf.Pow(time, 3f);
                            SetActive(snow);
            }, duration:2)
            .Then(run: time => {
                            SetNonactive(background, sabertooth, mammoth, Theo, shelter);
                            SetActive(left, right); 
                            snowballs.switchOn();
                        }, duration: 8)
            .Then(() => {audioTranslator.Play(theo3); AnimateMovement(Theo); Stabilise(sabertooth);})
            .Then(duration: (audioTranslator.getLength(theo3)), run:dt => {})            
            .Then(run: time => {
                            animation.SetActive(false);
                            stageEnd.Raise(); 
                        }, duration: 0.1f)
            .Start();          

    }

    void SetActive(params GameObject[] stimuli) => stimuli.ForEach(s => s.SetActive(true));

    void SetNonactive(params GameObject[] stimuli) => stimuli.ForEach(s => s.SetActive(false));

    void Stabilise(params GameObject[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("stable"));
    }

    void AnimateMovement(params GameObject[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("speak"));
    }


    public void Reset()
    {
        audioSource.Stop();
        StopAllCoroutines();
    }
}