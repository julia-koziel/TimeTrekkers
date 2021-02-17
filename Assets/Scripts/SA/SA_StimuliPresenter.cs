using UnityEngine;

public class SA_StimuliPresenter : MonoBehaviour 
{
    public GameEvent stageEnd;
    public Stimulus PipsCar;
    public Stimulus OtherCar1;
    public Transform leftPos;
    public Transform rightPos;
    [Space(10)]
    public AudioTranslator audioTranslator;
    public TranslatableAudioClip pipOnATripThisIsHisCar;
    public TranslatableAudioClip butTheseAreNotHisCars;

    void OnEnable()
    {
        PipsCar.transform.position = leftPos.position;
        OtherCar1.transform.position = rightPos.position;

        var audioLength1 = audioTranslator.getLength(pipOnATripThisIsHisCar);
        var audioLength2 = audioTranslator.getLength(butTheseAreNotHisCars);

        PipsCar.SetActive(true);
        audioTranslator.Play(pipOnATripThisIsHisCar);

        this.In(Mathf.Max(2, audioLength1)).Call(() => 
        {
            PipsCar.GetComponent<AudioSource>().Play();
            ShowSymbol(PipsCar);

            this.In(2).Call(() => 
            {
                OtherCar1.SetActive(true);
                audioTranslator.Play(butTheseAreNotHisCars);

                this.In(Mathf.Max(2, audioLength2)).Call(() => 
                {
                    ShowSymbol(OtherCar1);

                    this.In(2).Call(stageEnd.Raise);
                });
            });
        });
    }

    void ShowSymbol(Stimulus stimulus) => stimulus.transform.GetChild(0).gameObject.SetActive(true);
    public void Reset()
    {
        audioTranslator.audioSource.Stop();
        StopAllCoroutines();
    } 
}