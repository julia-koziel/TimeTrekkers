using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsClickGo : MonoBehaviour
{

    public GameObject good_dog_front;
    public GameObject good_dog_move;
    private Instructions_Dog gDogMoveBehaviour;
    public GameObject bad_dog_front;
    public GameObject bad_dog_move;
    private Instructions_Dog bDogMoveBehaviour;
    public GameObject mm;
    public GameObject trials;
    public GameObject practice;
    public GameObject start;
    public GameObject repeat;
    public GameObject goPracticeButton;
    public GameObject mixedPracticeButton;
    private bool buttonPressed = false;

    AudioSource[] aSources;
    private bool spokenAudioEnabled;

    private enum State
    {
        None,
        Intro, IntroDogMove,
        GoPracticeButton, GoPractice, 
        BadDog, BadDogTurning, 
        MixedPracticeButton, MixedPractice,
        Buttons, StartButtonClicked, 
        Trials
    }

    private State stateToLoad = State.None;
    private State currentState = State.None;

    private void Start()
    {
        aSources = GetComponents<AudioSource>();
        stateToLoad = State.Intro;
        gDogMoveBehaviour = good_dog_move.GetComponent<Instructions_Dog>();
        bDogMoveBehaviour = bad_dog_move.GetComponent<Instructions_Dog>();

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        spokenAudioEnabled = PlayerPrefs.GetInt(prefsKey, 0) == 1;
    }
    void Update()
    {
        AudioBundle[] audioInfo;

        switch (stateToLoad)
        {
            case State.Intro:
                
                audioInfo = new AudioBundle[] {
                    new AudioBundle("Ahh", index: 0, delay: 0.2f),
                    new AudioBundle("This is my puppy", index: 1, delay: 0.5f)
                };
                
                good_dog_move.SetActive(true);

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;
            
            
            case State.IntroDogMove:
                
                audioInfo = new AudioBundle[] {
                    new AudioBundle("He loves running around.", index: 2, delay: 0.5f),
                    new AudioBundle("Can you help me catch him?", index: 3, delay: 0.5f),
                    new AudioBundle("You can catch the puppy by touching him!", index: 4, delay: 1),
                };

                gDogMoveBehaviour.move(Instructions_Dog.State.Turning);

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.GoPracticeButton:
                good_dog_move.SetActive(false);
                goPracticeButton.SetActive(true);
                finishStateLoad();
                break;

            case State.GoPractice:
                good_dog_move.SetActive(false);
                bad_dog_front.SetActive(false); // delete
                goPracticeButton.SetActive(false);
                bad_dog_move.SetActive(true);
                bDogMoveBehaviour.move(Instructions_Dog.State.Sitting);

                setPrefsPracticeLvl(1);
                practice.SetActive(true);
                gameObject.SetActive(false);

                finishStateLoad();
                moveToNextState();
                break;

            case State.BadDog: //50

                audioInfo = new AudioBundle[] {
                    new AudioBundle("Now this is my neighbour's grumpy doggy", index: 5, delay: 0.5f),
                    new AudioBundle("Don’t touch the grumpy doggy. He is quite grumpy today.", index: 6, delay: 0.5f),
                    new AudioBundle("When you see the grumpy doggy, don't touch it", index: 7, delay: 0.5f)
                };
                
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.BadDogTurning: //60 
                
                audioInfo = new AudioBundle[] {
                    new AudioBundle("Now let's find the puppy. Remember, touch the friendly puppy, but don't touch the grumpy doggy.",
                                    index: 8,
                                    delay: 0.5f)
                };

                bad_dog_front.SetActive(false);
                bDogMoveBehaviour.move(Instructions_Dog.State.Turning);

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.MixedPracticeButton:
                bad_dog_move.SetActive(false);
                mixedPracticeButton.SetActive(true);
                finishStateLoad();
                break;

            case State.MixedPractice:
                bad_dog_move.SetActive(false);
                mixedPracticeButton.SetActive(false);

                setPrefsPracticeLvl(2);
                practice.SetActive(true);
                
                finishStateLoad();
                moveToNextState();
                gameObject.SetActive(false);
                break;

            case State.Buttons:
                buttonPressed = true;
                start.GetComponent<Button>().interactable = false;
                repeat.GetComponent<Button>().interactable = false;
                start.SetActive(true);
                repeat.SetActive(true);
                
                audioInfo = new AudioBundle[] {
                    new AudioBundle("Ok great. Do you want to practice again?", index: 9, delay: 0.5f)
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); //Don't move to next state
                break;


            case State.StartButtonClicked:
                
                audioInfo = new AudioBundle[] {
                    new AudioBundle("Well done! Let's find the puppy now for real!", index: 10, delay: 0.5f)
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.Trials:
                repeat.SetActive(false);
                start.SetActive(false);
                setPrefsPracticeLvl(1);

                finishStateLoad();
                moveToNextState();

                trials.SetActive(true);
                gameObject.SetActive(false);
                break;

            case State.None:
                break;

            default:
                finishStateLoad();
                break;
        }
    }

    public void moveToNextState()
    {
        stateToLoad = currentState.Next();
    }

    void finishStateLoad()
    {
        currentState = stateToLoad;
        stateToLoad = State.None;
    }

    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            if (spokenAudioEnabled) { currentAudio.Play(); }
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        if (currentState == State.Buttons)
        {
            buttonPressed = false;
            start.GetComponent<Button>().interactable = true;
            repeat.GetComponent<Button>().interactable = true;
        }
        else
        {
            moveToNextState();
        }
    }

    public void startGoPractice()
    {
        stateToLoad = State.GoPractice;
    }

    public void startMixedPractice()
    {    
        stateToLoad = State.MixedPractice;   
    }

    public void Start_trial(){
        if (!buttonPressed)
        {
            buttonPressed = true;
            start.GetComponent<Button>().interactable = false;
            repeat.GetComponent<Button>().interactable = false;
            stateToLoad = State.StartButtonClicked;
        }
    }

    public void Repeat(){
        if (!buttonPressed)
        {
            buttonPressed = true;
            repeat.SetActive(false);
            start.SetActive(false);
            gameObject.SetActive(false);
            practice.SetActive(true);
            stateToLoad = State.Buttons;
        }
        
    }

    public void setPrefsPracticeLvl(int level){
        string practiceLvlKey = PrefsKeys.Keys.GNG_PracticeLevel.ToString();
        PlayerPrefs.SetInt(practiceLvlKey, level);
    }

    public void skip()
    {
        if ((int) currentState < (int) State.GoPracticeButton)
        {
            foreach (AudioSource audioSource in aSources)
            {
                if (audioSource.isPlaying) { audioSource.Stop(); }
            }
            StopAllCoroutines();
            stateToLoad = State.GoPracticeButton;
        }
        else if ((int) currentState < (int) State.BadDog)
        {
            good_dog_move.SetActive(false);
            bad_dog_front.SetActive(false); // delete
            goPracticeButton.SetActive(false);
            bad_dog_move.SetActive(true);
            bDogMoveBehaviour.move(Instructions_Dog.State.Sitting);
            foreach (AudioSource audioSource in aSources)
            {
                if (audioSource.isPlaying) { audioSource.Stop(); }
            }
            StopAllCoroutines();
            stateToLoad = State.BadDog;
        }
        else if ((int) currentState < (int) State.MixedPracticeButton)
        {
            foreach (AudioSource audioSource in aSources)
            {
                if (audioSource.isPlaying) { audioSource.Stop(); }
            }
            StopAllCoroutines();
            stateToLoad = State.MixedPracticeButton;
        }
        else if ((int) currentState < (int) State.Buttons)
        {
            bad_dog_move.SetActive(false);
            mixedPracticeButton.SetActive(false);

            foreach (AudioSource audioSource in aSources)
            {
                if (audioSource.isPlaying) { audioSource.Stop(); }
            }
            StopAllCoroutines();
            stateToLoad = State.Buttons;
        }
    }
}
