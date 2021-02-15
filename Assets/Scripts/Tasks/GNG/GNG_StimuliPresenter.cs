using UnityEngine;

public class GNG_StimuliPresenter : MonoBehaviour 
{
    public GameEvent stageEnd;
    public Stimulus Go;
    public Stimulus NoGo;
    public Transform leftPos;
    public Transform rightPos;
    [Space(10)]
    public TranslatableAudioClip thisIsEmmasPuppy;
    public TranslatableAudioClip dontTouchTheKitty;
    public AudioTranslator audioTranslator;
    void OnEnable()
    {
        Go.transform.position = leftPos.position;
        NoGo.transform.position = rightPos.position;

        var audioLength1 = audioTranslator.getLength(thisIsEmmasPuppy);
        var audioLength2 = audioTranslator.getLength(dontTouchTheKitty);

        Go.SetActive(true);
        audioTranslator.Play(thisIsEmmasPuppy);
        
        this.In(Mathf.Max(2, audioLength1)).Call(() => {
            MakeReact(Go);

            this.In(2).Call(() => {
                NoGo.SetActive(true);
                audioTranslator.Play(dontTouchTheKitty);

                this.In(Mathf.Max(2, audioLength2)).Call(() => {
                    MakeReact(NoGo);

                    this.In(2).Call(stageEnd.Raise);
                });
            });
        });
        
        
    }

    void MakeReact(Stimulus stimulus)
    {
        stimulus.GetComponent<AudioSource>().Play();
        stimulus.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Reset()
    {
        StopAllCoroutines();
    }
}