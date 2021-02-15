using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTestLogicAuditory : MonoBehaviour
{
    public GameObject practice;
    public GameObject parentpractice;
    public GameObject demo;
    public GameObject trials;
    public GameObject mm;
    public GameObject repeatpractice;
    public GameObject startpretest;
    public GameObject startpractice;
    public GameObject pretestrepeat;
    private bool buttons1Clicked = false;
    private bool buttons2Clicked = false;
    public int replayed =0;

    AudioSource[] aSources;
    private bool spokenAudioEnabled;
    public GameObject start;
    public GameObject repeat;
    public GameObject PreTest;
    public GameObject end;
    public GameObject highpitch;
    public GameObject lowpitch;

    private Vector3[] positions; 

    public GameObject bird1;
    public GameObject bird2;
    public GameObject singing1;
    public GameObject singing2;
    public GameObject button1;
    public GameObject button2;

    public bool rerun = true; // rerun pre-task
    public bool click = false;

    float delay = 2.0f;

    private enum State
    {
        None,
        StartButtons, BirdsFlying, Bird1, ITI, Bird2,
        Pretest, PretestIncorrect, PretestCorrect, 
        ButtonsDemoPractice, PracticeButtonClicked, 
        Practice,
    }

    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        stateToLoad = State.StartButtons;

        positions = new Vector3[4];
        positions[1] = new Vector3(-4, 1, 0);
        positions[2] = new Vector3(4, 1, 0);
        aSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    { 
        AudioBundle[] audioInfo;
        // Determines which state to load, only runs loading once, rest of time set to None
        switch (stateToLoad)
        {
    
            case State.StartButtons:
                
                startpretest.SetActive(true);
                repeatpractice.SetActive(true);
                
                finishStateLoad();

                break;


            case State.BirdsFlying:
            audioInfo = new AudioBundle[] {
                new AudioBundle(index: 0, delay: 0f),};
                startpretest.SetActive(false);
                repeatpractice.SetActive(false);
                bird1.SetActive(true);
                bird2.SetActive(true);
                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
            
                break;

            case State.Bird1:
                singing1.SetActive(true);
                lowpitch.SetActive(true);

                StartCoroutine(waitforareaction());
                finishStateLoad();

                break;

            case State.ITI:

                singing1.SetActive(false);
                StartCoroutine(waitforareaction());

                finishStateLoad();
                
                break;


             case State.Bird2:
                singing2.SetActive(true);
                highpitch.SetActive(true);

                StartCoroutine(waitforareaction());
                finishStateLoad();
            
                break;

            case State.Pretest:
                audioInfo = new AudioBundle[] {
                new AudioBundle(index: 1, delay: 3f),};

                bird1.SetActive(false);
                bird2.SetActive(false);
                button1.SetActive(true);
                button2.SetActive(true);   
                StartCoroutine(playAudio(audioInfo));

                finishStateLoad();
    
                break;

            case State.PretestIncorrect:
                button1.SetActive(false);
                button2.SetActive(false);

                StartCoroutine(waitforareaction());

                finishStateLoad();
                
                break;

            case State.PretestCorrect:
                audioInfo = new AudioBundle[] {
                new AudioBundle(index: 2, delay: 0f),};
            
                bird1.SetActive(false);
                bird2.SetActive(false);
                pretestrepeat.SetActive(true);
                startpractice.SetActive(true);
                StartCoroutine(playAudio(audioInfo));
                // yes, that's target ship
    
                finishStateLoad();
               
                break;


            case State.ButtonsDemoPractice:
                buttons1Clicked = false;
                pretestrepeat.SetActive(true);
                startpractice.SetActive(true);
                
                finishStateLoad();

                break;

            case State.PracticeButtonClicked:
                gameObject.SetActive(false);
                practice.SetActive(true);
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



    private IEnumerator waitforareaction()
    {
    
    yield return new WaitForSeconds(1.5f);
        
       
     if (currentState == State.PretestIncorrect)
        {
            Restartclick();
        }
        else
        {
            moveToNextState();
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
        
    
        if (currentState == State.PretestIncorrect)
        {
            StartPretest();
        }
        else
        {
            moveToNextState();
        }
        
    }

    public void Restartclick()
    {
        replayed++;
        currentState = State.None;
        stateToLoad = State.BirdsFlying;
        startpractice.SetActive(false);
        pretestrepeat.SetActive(false);

        if (replayed==2)
        {
            EndGame();
        }

    }

    public void restartDemo()
    {
        parentpractice.SetActive(true);
        gameObject.SetActive(false);
    }

    public void startPractice()
    {
        gameObject.SetActive(false);
        practice.SetActive(true);
    }
        

    public void Correct() // right ship on pre-task
    {
        stateToLoad = State.PretestCorrect;
    }

    public void Incorrect()
    {
        stateToLoad = State.PretestIncorrect;
    }

    public void RepeatDemo()
    {

        if (!buttons1Clicked)
        {   
            buttons1Clicked = true;
            pretestrepeat.SetActive(false);
            startpractice.SetActive(false);
            stateToLoad = State.BirdsFlying;
        }
        
    }

    public void StartPretest()
    {
        if (!buttons1Clicked)
        {
            buttons1Clicked = true;
            // pretestrepeat.GetComponent<Button>().interactable = false;
            // startpractice.GetComponent<Button>().interactable = false;
            stateToLoad = State.BirdsFlying;
        }
        
    }

    public void EndGame()
    {
        end.SetActive(true);
    }

}
