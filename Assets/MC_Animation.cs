using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioTranslator))]

public class MC_Animation : MonoBehaviour
{
    public GameObject Theo;
    public GameObject sabertooth;
    public GameObject mammoth;
    public GameObject snow;
    public GameObject shelter;
    public GameObject left1;
    public GameObject left;
    public GameObject right;
    Vector3 camStart = new Vector3(0, 0, -10);
    Vector3 camEnd = new Vector3(0, 0, -10);
    float camStartSize = 5;
    float camEndSize = 4;
    float camZoomSize = 0.6f;

    public GameEvent stageEnd;

    Camera cam;
    
    AudioSource audioSource;
    AudioTranslator audioTranslator;

    void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        audioTranslator = GetComponent<AudioTranslator>();
        cam = Camera.main;
    }

    void OnEnable()
    {
        this.BuildMovement()
            
            .First(() => {SetActive(sabertooth, Theo, shelter, mammoth);})
            .Then(duration:5, run: dt => {})
            
            .Then(() => {
                            cam.orthographicSize = camStartSize;
                            cam.transform.position = camStart;

                        })
            .Then(run: time => {
                            cam.orthographicSize = camStartSize + (camEndSize - camStartSize) * Mathf.Pow(time, 0.3f);
                            cam.transform.position = camStart + (camEnd - camStart) * Mathf.Pow(time, 0.3f);
                            SetActive(snow, right, left); 
                            SetNonactive(Theo, sabertooth, shelter, left1, mammoth);
                        
                        }, duration: 2)           
            .Start();
    }

    void SetActive(params GameObject[] stimuli) => stimuli.ForEach(s => s.SetActive(true));

    void SetNonactive(params GameObject[] stimuli) => stimuli.ForEach(s => s.SetActive(false));

    void Stabilise(params GameObject[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("sit"));
    }

    void AnimateMovement(params GameObject[] stimuli)
    {
        stimuli.ForEach(s => s.GetComponent<Animator>().SetTrigger("move"));
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