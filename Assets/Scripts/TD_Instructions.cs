using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class TD_Instructions : MonoBehaviour
{
    public Stimulus Now;
    public Stimulus Later;
    public GameObject smallhourglass;
    public GameObject[] stickers;
    public GameObject stickers1;
    public GameObject stickers2;
    public GameObject sparkle1;
    public GameObject sparkle2;
    public GameObject hourglass;
    public GameEvent stageEnd;

    public TD_Scientist scientist;

    public TranslatableAudioClip Intro;
    public TranslatableAudioClip MoreStickers;
    public TranslatableAudioClip LessStickers;
    public TranslatableAudioClip Ready;
    
    public Transform position; 
    AudioTranslator audioTranslator;

   void Awake() 
    {
        audioTranslator = GetComponent<AudioTranslator>();
    }

    void OnEnable()
    {
            
        this.BuildMovement()
            .First(() => {audioTranslator.Play(Intro); SetActive(Now, Later);})
            .Then(duration: audioTranslator.getLength(Intro), run: dt => {})
            .Then(() => {sparkle2.SetActive(true); scientist.SetActive(true);})
            .Then(duration:1, run: dt => {})
            .Then(() => {scientist.clickTD();})
            .Then(duration:1, run: dt => {})
            .Then(() => {audioTranslator.Play(MoreStickers); scientist.Click(Later.transform); hourglass.SetActive(true); smallhourglass.SetActive(false); stickers1.SetActive(false); stickers2.SetActive(false);SetNonActive(Now, Later); })
            .Then(duration:5, run: dt => {})
            .Then(() => {stickers2.SetActive(true); stickers2.transform.position= position.transform.position; hourglass.SetActive(false);})
            .Then(duration:2, run: dt => {})
            .Then(() => {audioTranslator.Play(LessStickers); SetActive(Now, Later); smallhourglass.SetActive(true); sparkle1.SetActive(true); stickers1.SetActive(true); sparkle2.SetActive(false);})
            .Then(duration:3, run: dt => {})
            .Then(() => {scientist.clickTD2();})
            .Then(duration:1, run: dt => {})
            .Then(() => {stickers1.SetActive(true); SetNonActive(Now, Later); smallhourglass.SetActive(false); stickers2.SetActive(false);})
            .Then(duration:3, run: dt => {})
            .Then(() => {audioTranslator.Play(Ready); stickers1.SetActive(false);})
            .Then(duration: audioTranslator.getLength(Ready), run: dt => {})
            .Then(() => {stageEnd.Raise(); gameObject.SetActive(false);})
            .Start();
            
    }

     public void Reset()
    {
        audioTranslator.audioSource.Stop();
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    void SetActive(params Stimulus[] stimuli) => stimuli.ForEach(s => s.SetActive(true));
    void SetNonActive(params Stimulus[] stimuli) => stimuli.ForEach(s => s.SetActive(false));
}