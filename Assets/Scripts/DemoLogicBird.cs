using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLogicBird : MonoBehaviour
{
    private AudioSource[] aSources;
    public GameObject trials;
    public GameObject instructions;
    public GameObject bird1;
    public GameObject bird2;
    public GameObject bird3;
    public GameObject bird4;

    public GameObject squirrel;

    public GameObject Theo;
    public GameObject highpitch;
    public GameObject lowpitch;


    public GameObject WatchAgain;
    public GameObject ParentPractice;
    public GameObject practice;

    private Animator animator;
    public bool click = false;
    public bool flyout = false;
    public bool point = false;
    public bool point2 = false;
    public bool run = false;
    public bool practiceClicked = false;

    private IEnumerator delay;

    private State stateToLoad = State.None;
    private State currentState = State.None;

    private enum State {
        None,
        Theoinstructions,
        Theoturning,
        bird1,
        ITI,
        bird2,
        Ratingbirds,
        Click,
        BirdsFlyingOut,
        Squirrel,
        ClickingSquirrel,
        SquirrelOff,
        Buttons,
        PracticeButtonClicked,
        Practice}

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Theo.SetActive(true);
        stateToLoad = State.Theoinstructions;
        aSources = GetComponents<AudioSource>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;

        switch (stateToLoad)
        {
            case State.Theoinstructions:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0f),};
                Debug.Log("stateloaded");
                Theo.SetActive(true);
                WatchAgain.SetActive(false);
                ParentPractice.SetActive(false);
                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
                break;

            case State.Theoturning:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 1f),};
                bird1.SetActive(true);
                bird2.SetActive(true);
                click = true;
                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();

                break;

            case State.bird1:
                bird3.SetActive(true);
                Debug.Log("State2");
                highpitch.SetActive(true);
                StartCoroutine(clickdelay());
                finishStateLoad();
                
                break;

            case State.ITI:
                bird3.SetActive(false);
                finishStateLoad();
                StartCoroutine(clickdelay());
                break;

            case State.bird2:
                bird4.SetActive(true);
                lowpitch.SetActive(true);

                finishStateLoad();
                StartCoroutine(clickdelay());
                break;

            case State.Ratingbirds:
                    bird4.SetActive(false);
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 1f),};
                
                StartCoroutine(playAudio(audioInfo));
                

                finishStateLoad();

                break;

            case State.Click:
                bird3.SetActive(false);
                bird4.SetActive(false);
                Theo.SetActive(true);
    
                point = true;
                StartCoroutine(clickdelay());

                finishStateLoad();
                
                break;
            
            case State.BirdsFlyingOut:
                // point = false;
                StartCoroutine(clickdelay());

                finishStateLoad();

                break;


            case State.Squirrel:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 3, delay: 0.5f),};
                bird1.SetActive(false);
                bird2.SetActive(false);
                bird3.SetActive(false);
                bird4.SetActive(false);
                squirrel.SetActive(true);
                StartCoroutine(playAudio(audioInfo));
                flyout=false;

                StartCoroutine(clickdelay());

                finishStateLoad();

                break;

            case State.SquirrelOff:
                point2 = true;
                StartCoroutine(clickdelay());
                finishStateLoad();
                break;

    

            case State.Buttons:

                WatchAgain.SetActive(true);
                ParentPractice.SetActive(true);
                Theo.SetActive(false);
                squirrel.SetActive(false);
                finishStateLoad();
                click = false;
                point = false;
                

                break;

            case State.PracticeButtonClicked:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 4, delay: 0.5f),};

                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
            
                break;
                
            case State.Practice:
                practice.SetActive(true);
                gameObject.SetActive(false);
                click = false;
                bird3.SetActive(false);
                bird4.SetActive(false);
            
                Theo.SetActive(false);
                finishStateLoad();
                StartCoroutine(clickdelay());

                break; 


            case State.None:
                break;

            default:
                finishStateLoad();
                break;


    }
    }

    
    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            currentAudio.Play();
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        if (practiceClicked)
        {
            stateToLoad = State.Practice;
        }

        moveToNextState();
    }


    void moveToNextState()
    {
        // Function may not be needed depending on audio files
        stateToLoad = currentState.Next();

    }

    void finishStateLoad()
    {
        currentState = stateToLoad;
        stateToLoad = State.None;
    }
    // Start is called before the first frame update

    private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1.0f);
        moveToNextState();
    }

    public void watchAgain()
    {
        stateToLoad = State.Theoinstructions;
    }

    public void parentPractice()
    {
        practiceClicked=true;
        stateToLoad = State.PracticeButtonClicked;
    }

}
