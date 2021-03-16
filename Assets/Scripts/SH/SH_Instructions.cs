using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioTranslator))]

public class SH_Instructions : MonoBehaviour
{
    public GameEvent stageEnd;
    public GameObject scissors;
    public GameObject rock;
    public GameObject paper;
    public GameObject arrows;
    public GameObject win;
    [Space(10)]

    public TranslatableAudioClip HiThere;
    public TranslatableAudioClip RockCard;
    public TranslatableAudioClip ScissorsCard;
    public TranslatableAudioClip PaperCard;
    public TranslatableAudioClip LetMeShowYou;
    public TranslatableAudioClip RockWins;
    public TranslatableAudioClip ScissorsWins;
    public TranslatableAudioClip PaperWins;
    Vector3 scissorsv = new Vector3(0, 2, 0);
    Vector3 rockv = new Vector3(3, -2, 0);
    Vector3 paperv = new Vector3(-3, -2, 0);

    AudioSource audioSource;
    AudioTranslator audioTranslator;

    void Awake() 
    {
        audioSource = GetComponent<AudioSource>();
        audioTranslator = GetComponent<AudioTranslator>();
    }

    void OnEnable()
    {
        scissors.SetActive(false);

        this.BuildMovement()
            
            .First(() => {audioTranslator.Play(HiThere);})
            .Then(duration: (audioTranslator.getLength(HiThere)), run: dt => {})
            .Then(() => {audioTranslator.Play(ScissorsCard); scissors.SetActive(true);})
            .Then(duration: (audioTranslator.getLength(ScissorsCard)), run: dt => {})
            .Then(() => {audioTranslator.Play(RockCard); SetActive(rock);})
            .Then(duration: (audioTranslator.getLength(RockCard)), run: dt => {})
            .Then(() => { audioTranslator.Play(PaperCard); SetActive(paper);})
            .Then(duration: (audioTranslator.getLength(PaperCard)), run: dt => {})
            .Then(() => {arrows.SetActive(true);})
            .Then(1, false, false, Move(scissors.transform, from: scissorsv, to: paperv, duration: 1),
                                   Move(scissors.transform, from: paperv, to: scissorsv, duration: 1))
            .Then(() => { audioTranslator.Play(ScissorsWins); win.SetActive(true); })
            .Then(duration: audioTranslator.getLength(ScissorsWins), run: dt => {})
            .Then(1, false, false, Move(paper.transform, from: paperv, to: rockv, duration: 1),
                                   Move(paper.transform, from: rockv, to: paperv, duration: 1))
            .Then(() => { audioTranslator.Play(PaperWins); win.SetActive(true);})
            .Then(() => {win.SetActive(false);})
            .Then(duration: audioTranslator.getLength(PaperWins), run: dt => {})
            .Then(1, false, false, Move(rock.transform, from: rockv, to: scissorsv, duration: 1),
                                   Move(rock.transform, from: scissorsv, to: rockv, duration: 1))
            .Then(() => { audioTranslator.Play(RockWins); win.SetActive(true); })
            .Then(() => {win.SetActive(false);})
            .Then(duration: audioTranslator.getLength(RockWins), run: dt => {})
            .Then(() => {audioTranslator.Play(LetMeShowYou); })
            .Then(duration: audioTranslator.getLength(LetMeShowYou), run: dt => {})
            .Then(stageEnd.Raise)
            .Start();
    }

    void SetActive(params GameObject[] stimuli) => stimuli.ForEach(s => s.SetActive(true));

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
