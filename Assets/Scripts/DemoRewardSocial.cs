using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRewardSocial : MonoBehaviour
{
    public GameObject Theo;
    public GameObject[] textboxes;
    public GameObject background;
    private TheoDemoRewardSocialBehaviour TheoBehaviour;
    public GameObject personGood;
    public GameObject personGoodReact;
    public GameObject personBad;
    public GameObject personBadReact;
    public GameObject food;
    public GameObject ITI; 
    public BoolVariable RL;
    
    public GameObject startButton;
    public GameObject repeatButton;
    public GameObject MM;
    public GameObject trials;

    public float time = 0;
    private bool audioOn;
    private bool subtitleOn;
    private bool buttonClicked = false;

    private Vector3[] positions;
    private AudioSource[] aSources;

    public GameEvent stageEnd;
   
    private enum State {
        None,
        Intro,
        Trial1, Trial1Click, Trial1Reaction,
        ITI,
        Trial2, Trial2Click, Trial2Reaction, Trial2Reaction2, End, End2, End3,
        Buttons, StartButtonClicked,
        StartTrials
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        TheoBehaviour = Theo.GetComponent<TheoDemoRewardSocialBehaviour>();
        aSources = GetComponents<AudioSource>();
        RL.Value=0;

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;

        positions = new Vector3[5];
        positions[0] = new Vector3(-4.5f, -0.5f, 0);   
        positions[1] = new Vector3(4.5f, -0.5f, 0);
        positions[2] = new Vector3 (-1, -4, 0);
        positions[3] = new Vector3 (-4.5f, -4, 0);
        positions[4] = new Vector3 (4.5f,-4, 0);
        // can randomize if needed

        personGood.SetActive(false);
        personGoodReact.SetActive(false);
        personBad.SetActive(false);
        personBadReact.SetActive(false);
        food.SetActive(false);
        repeatButton.SetActive(false);
        startButton.SetActive(false);
        Theo.SetActive(false);

        stateToLoad = State.Intro;
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;
        
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {
            case State.Intro:
                Theo.SetActive(true);
                TheoBehaviour.reset();
                personGood.SetActive(true);
                personBad.SetActive(true);
                personBadReact.SetActive(false);
                personGoodReact.SetActive(false);
                food.SetActive(true);
                finishStateLoad();
                StartCoroutine(clickRoutineshort()); // move
                break;

            case State.Trial1:
                Theo.SetActive(true);
                TheoBehaviour.reset();
                personGood.SetActive(true);
                personBad.SetActive(true);
                food.SetActive(true);
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f),
                };
                if (subtitleOn)
                {
                    textboxes[0].SetActive(true);
                }

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
    
                break;

            case State.Trial1Click:
                TheoBehaviour.move(TheoDemoRewardSocialBehaviour.State.ClickBadChest);

                finishStateLoad();
                break;

            case State.Trial1Reaction:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 0f),
                };
            
                food.transform.position = positions[4];
                personBadReact.SetActive(true);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                if (subtitleOn)
                {
                    textboxes[0].SetActive(false);
                    textboxes[1].SetActive(true);
                }
                break;

            case State.ITI:
                personBadReact.SetActive(false);
                personGood.SetActive(false);
                personBad.SetActive(false);
                ITI.SetActive(true);
                background.SetActive(true);
                
                food.transform.position = positions[2];

                if (subtitleOn)
                {
                    textboxes[1].SetActive(false);
                    textboxes[2].SetActive(true);
                }

                finishStateLoad();
                StartCoroutine(clickRoutineshort());

               break;

            case State.Trial2:
                ITI.SetActive(false);
                background.SetActive(false);
                personGood.SetActive(true);
                personBad.SetActive(true);
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 0.5f),
                };
                
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                Debug.Log("Trial2");

                break;

            case State.Trial2Click:
               TheoBehaviour.move(TheoDemoRewardSocialBehaviour.State.ClickGoodChest);
               Debug.Log("Trial2Click");
               Theo.transform.position = personGood.transform.position;
                StartCoroutine(clickRoutine());
               finishStateLoad();
                break;

            
            case State.Trial2Reaction:
                TheoBehaviour.move(TheoDemoRewardSocialBehaviour.State.BackToCentre);
                food.SetActive(true);
                personGoodReact.SetActive(true);
                food.transform.position = positions[3];
                
                finishStateLoad();
                StartCoroutine(clickRoutineshort()); //move?
                break;

            case State.Trial2Reaction2:
                
                if (subtitleOn)
                {
                    textboxes[2].SetActive(false);
                    textboxes[3].SetActive(true);
                }
                finishStateLoad();
                StartCoroutine(clickRoutineshort()); 
              
                break;

            case State.End:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay:0),
                };

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); 
                break;

            case State.End2:

                 audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 4, delay:0f),
                };
                if (subtitleOn)
                {
                    textboxes[3].SetActive(false);
                    textboxes[4].SetActive(true);
                }
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.End3:

            StartCoroutine(clickRoutine());
            finishStateLoad();
            break;


            case State.Buttons:
        
                buttonClicked = false;
                repeatButton.SetActive(true);
                startButton.SetActive(true);

                 if (subtitleOn)
                {
                    textboxes[2].SetActive(false);
                    textboxes[4].SetActive(false);
                }
                // finishStateLoad();
                
                break;

            case State.StartButtonClicked:

                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 5, delay: 0f),
                };
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));

                if (subtitleOn)
                {
                    textboxes[2].SetActive(false);
                    textboxes[3].SetActive(true);
                }

                break;

            case State.StartTrials:
                repeatButton.SetActive(false);
                startButton.SetActive(false);
                trials.SetActive(true);
                stageEnd.Raise();
                gameObject.SetActive(false);
                textboxes[3].SetActive(false);
                finishStateLoad();
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
            currentAudio = aSources[audioInfo[i].index];
            if (audioOn) { currentAudio.Play(); }
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        moveToNextState();
    }

    private IEnumerator clickRoutineshort()
    {
    yield return new WaitForSeconds(1.5f);

        moveToNextState();
    }
    private IEnumerator clickRoutine()
    {
        yield return new WaitForSeconds(13.5f);

        moveToNextState();
    }
    public void restartDemo()
    {
        if (!buttonClicked)
        {
            buttonClicked = true;
            repeatButton.SetActive(false);
            startButton.SetActive(false);
            stateToLoad = State.Intro;
        }
    }

    public void startTrials()
    {
        if (!buttonClicked)
        {
            buttonClicked = true;
            // repeatButton.GetComponent<Button>().interactable = false;
            // startButton.GetComponent<Button>().interactable = false;
            stateToLoad = State.StartButtonClicked;
        }
        
    }

    public void skip()
    {
        if ((int) currentState < (int) State.Buttons)
        {
            foreach (AudioSource audioSource in aSources)
            {
                if (audioSource.isPlaying) { audioSource.Stop(); }
            }


            personGood.SetActive(false);
        
            personBad.SetActive(false);
            // milk.SetActive(false);
            
            StopAllCoroutines();
            stateToLoad = State.Buttons;
        }
    }

    public void onEndBoxReact()
    {
        if (currentState == State.Trial1Click || currentState == State.Trial2Click)
        {
            moveToNextState();
        }
    }

    public void TheoClicked()
    {
       moveToNextState();
       Debug.Log("theoclicked");
    }
}