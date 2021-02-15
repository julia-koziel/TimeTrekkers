using UnityEngine;

[RequireComponent(typeof(AudioTranslator))]
public class SA_PretestStimuliManager : MonoBehaviour 
{
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable position;
    public IntVariable otherCar1Id;
    public IntVariable otherCar2Id;
    public IntVariable LEFT;
    public IntVariable RIGHT;
    public BoolVariable correct;
    public GameEvent trialEnd;
    public GameEvent stageRepeat;
    public BoolVariable participantsGo;
    [Space(10)]
    public Stimulus PipsCar;
    public Stimulus[] OtherCars;
    public Transform leftPos;
    public Transform rightPos;
    [Space(10)]
    public TranslatableAudioClip yesThatsPipsCar;
    public TranslatableAudioClip noThatsADifferentCar;
    AudioTranslator audioTranslator;

    void Awake() => audioTranslator = GetComponent<AudioTranslator>();
    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();

        int other1 = Random.Range(0, OtherCars.Length);

        var otherCar1 = OtherCars[other1];
        
        otherCar1Id.Value = otherCar1.id;

        if (position.Value == LEFT) 
        {
            PipsCar.transform.position = leftPos.position;
            otherCar1.transform.position = rightPos.position;
        }
    
        else 
        {
            otherCar1.transform.position = leftPos.position;
            PipsCar.transform.position = rightPos.position;
        }

        PipsCar.SetActive(true);
        otherCar1.SetActive(true);
    }

    public void OnEndResponseWindow()
    {
        if (participantsGo)
        {
            if (correct)
            {
                var audioLength = audioTranslator.getLength(yesThatsPipsCar);
                this.In(0.5f).Call(() => audioTranslator.Play(yesThatsPipsCar));
                this.In(audioLength + 0.5f).Call(trialEnd.Raise);
            }
            else
            {
                var audioLength = audioTranslator.getLength(noThatsADifferentCar);
                this.In(0.5f).Call(() => audioTranslator.Play(noThatsADifferentCar));
                this.In(audioLength + 1).Call(stageRepeat.Raise);
            }
            
        }
        else this.In(1).Call(trialEnd.Raise);
    }

    public void Reset()
    {
        audioTranslator.audioSource.Stop();
        StopAllCoroutines();
        inputVariablesManager.Reset();
    }
}