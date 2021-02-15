using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoRewardNonSocial : MonoBehaviour
{
    private AudioSource[] aSources;
    public GameObject Theo;
    private TheoDemoRewardNonSocialBehaviour TheoBehaviour;
    public GameObject chestGood;
    public GameObject chestGoodOpen;
    public GameObject chestBad;
    public GameObject chestBad2;
    public GameObject chestBadOpen;
    public GameObject coin;
    public GameObject crab;
    public GameObject ITI;
    public GameObject oldtext;
    public GameObject[] textboxes;
    public GameObject MM;
    public GameObject trials;
    
    public GameObject startButton;
    public GameObject repeatButton;
    private bool buttonClicked = false;
    
    public float time = 0;
    private float delay = 0.5f;
    private int magicPos = -1;
    private Vector3[] positions;
    
    private bool audioOn;
    private bool subtitleOn;

    private enum State {
        None,
        Trial1, Trial1Click, Trial1Reaction,
        ITI,
        Trial2, Trial2Click, Trial2Reaction, Trial2Reaction2,
        Buttons, StartButtonClicked,
        StartTrials
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        TheoBehaviour = Theo.GetComponent<TheoDemoRewardNonSocialBehaviour>();
        aSources = GetComponents<AudioSource>();

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        string prefsKey2 = PrefsKeys.Keys.Subtitle.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey2, 0) == 1;
        
        positions = new Vector3[2];
        positions[0] = new Vector3(-4, 0, 1);   // Left box
        positions[1] = new Vector3(1, 0.5f, 1); // Right box
        magicPos = 0;                           // can randomize if needed

        chestGood.transform.position = positions[magicPos];
        chestGoodOpen.transform.position = positions[magicPos];
       
        
        chestBad.transform.position = positions[1-magicPos];
        chestBadOpen.transform.position = positions[1-magicPos];

        chestGood.SetActive(false);
        chestGoodOpen.SetActive(false);
        chestBad.SetActive(false);
        chestBadOpen.SetActive(false);
        coin.SetActive(false);
        repeatButton.SetActive(false);
        startButton.SetActive(false);
        Theo.SetActive(false);
        oldtext.SetActive(false);
        
        stateToLoad = State.Trial1;
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;
        
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {

            case State.Trial1:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f),
                };
                Theo.SetActive(true);
                TheoBehaviour.reset();
                chestGood.SetActive(true);
                chestBad.SetActive(true);
                 if (subtitleOn)
                {
                    textboxes[0].SetActive(true);
                }
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); // move
                break;

            case State.Trial1Click:
                TheoBehaviour.move(TheoDemoRewardNonSocialBehaviour.State.ClickBadChest);
                
                finishStateLoad();
                break;

            case State.Trial1Reaction:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 0.5f),
                };
            
                coin.SetActive(false);
                chestBad.SetActive(false);
                chestBadOpen.SetActive(true);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); // move?
                
                if (subtitleOn)
                {
                    textboxes[0].SetActive(false);
                    textboxes[1].SetActive(true);
                }
                break;

            case State.ITI:
                Theo.SetActive(false);
                chestBadOpen.SetActive(false);
                chestBad.SetActive(false);
                chestGood.SetActive(false);
                ITI.SetActive(true);
                TheoBehaviour.move(TheoDemoRewardNonSocialBehaviour.State.BackToCentre);
                StartCoroutine(clickRoutine());
                Debug.Log("ITI");

                if (subtitleOn)
                {
                    textboxes[1].SetActive(false);
                }

                finishStateLoad();

               break;

            case State.Trial2:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 0.5f),
                };
                ITI.SetActive(false);
                chestGood.SetActive(true);
                chestGood.transform.position = positions[1-magicPos];
                chestGoodOpen.transform.position = positions[1-magicPos];
                chestBad2.transform.position = positions[magicPos];
                chestBad2.SetActive(true);
                Theo.SetActive(true);
                
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); //move?

                Debug.Log("Trial2");

                if (subtitleOn)
                {
                    textboxes[1].SetActive(false);
                    textboxes[2].SetActive(true);
                }

                break;

            case State.Trial2Click:
                

               TheoBehaviour.move(TheoDemoRewardNonSocialBehaviour.State.ClickGoodChest);
               Debug.Log("Trial2Click");
            //    Theo.transform.position = chestGood.transform.position;
               finishStateLoad();
                break;

            case State.Trial2Reaction:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay: 0f),
                };
                coin.SetActive(true);
                chestGoodOpen.SetActive(true);

    
                if (subtitleOn)
                {
                    textboxes[2].SetActive(false);
                    textboxes[3].SetActive(true);
                }
                
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); //move?
                break;

            case State.Trial2Reaction2:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 4, delay: 0f),
                };

                coin.SetActive(true);
                chestGoodOpen.SetActive(true);
                if (subtitleOn)
                {   
                    textboxes[3].SetActive(false);
                    textboxes[4].SetActive(true);
                }

                finishStateLoad();
                StartCoroutine(playAudio(audioInfo)); //move?
                break;



            case State.Buttons:

                chestGood.SetActive(false);
                chestGoodOpen.SetActive(false);
                chestBad2.SetActive(false);
                chestBadOpen.SetActive(false);
                coin.SetActive(false);
                Theo.SetActive(false);
                textboxes[4].SetActive(false);
                buttonClicked = false;
                repeatButton.SetActive(true);
                startButton.SetActive(true);
                
                break;

            case State.StartButtonClicked:
                 if (subtitleOn)
                {   
                    textboxes[4].SetActive(false);
                    textboxes[5].SetActive(true);
                }
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 5, delay: 0f),
                };
                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
                break;

            case State.StartTrials:

                Debug.Log("reached starttrials");
                finishStateLoad();
                repeatButton.SetActive(false);
                startButton.SetActive(false);
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
            currentAudio = aSources[audioInfo[i].index];
            if (audioOn) { currentAudio.Play(); }
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        if (currentState == State.StartButtonClicked)
        {
            Debug.Log("Finished startbutton audio");
        }

        moveToNextState();
    }

    private IEnumerator clickRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        moveToNextState();
    }
    public void restartDemo()
    {
        if (!buttonClicked)
        {
            buttonClicked = true;
            repeatButton.SetActive(false);
            startButton.SetActive(false);
            stateToLoad = State.Trial1;
        }
    }

    public void startTrials()
    {
        if (!buttonClicked)
        {
            buttonClicked = true;
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


            chestGood.SetActive(false);
        
            chestBad.SetActive(false);
            
            StopAllCoroutines();
            stateToLoad = State.Buttons;
        }
    }

    public void onEndBoxOpen()
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
