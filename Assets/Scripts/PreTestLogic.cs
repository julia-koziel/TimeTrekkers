using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ToolShed.Android.OS;

public class PreTestLogic : MonoBehaviour
{   
    
    private AudioSource[] aSources;
    public GameObject practice;
    public GameObject demo;
    public GameObject trials;
    public GameObject mm;
    public GameObject startpractice;
    public GameObject pretestrepeat;
    private bool buttons1Clicked = false;
    private bool buttons2Clicked = false;
    public int replayed =0;

   
    private bool spokenAudioEnabled;
    public GameObject start;
    public GameObject PreTest;
    public GameObject end;

    private Vector3[] positions;

    public GameObject button1;
    public GameObject button2;

    public GameObject cat1;
    public GameObject cat2;
    public GameObject cat3;
    public GameObject cat4;
    public GameObject sleepy;
   
    public GameObject singlebed;
    public GameObject doublebed;

    public bool rerun = true; // rerun pre-task
    public bool click = false;

    private enum State
    {
        None,
        Intro,
        TargetKitty, ITI, OtherKitty, 
        Pretest, PretestIncorrect, PretestCorrect, 
        ButtonsDemoPractice, PracticeButtonClicked, 
        Practice,
    }

    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        stateToLoad = State.TargetKitty;

        positions = new Vector3[4];
        positions[0] = singlebed.transform.position;
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

            case State.Intro:
            audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0.5f),};
                singlebed.SetActive(true);
                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();

                break;

            case State.TargetKitty:
                if (replayed==0)
                {  audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 0.5f),};
                    StartCoroutine(playAudio(audioInfo));
                }
                else
                {StartCoroutine(waitforareaction());
                }
                cat1.SetActive(true);
                singlebed.SetActive(true);
                Vibrator.Vibrate(500, 200);
                
                finishStateLoad();
            
                break;

            case State.ITI:

                cat1.SetActive(false);
            
                StartCoroutine(waitforareaction());

                finishStateLoad();
                
                break;


             case State.OtherKitty:
                

                cat2.SetActive(true);
                singlebed.SetActive(true);
                StartCoroutine(waitforareaction());
                finishStateLoad();
            
                break;

            case State.Pretest:
               audioInfo = new AudioBundle[] {
                    new AudioBundle(index:2 , delay: 1f),};
                cat2.SetActive(false);
                singlebed.SetActive(false);
                doublebed.SetActive(true);
                button1.SetActive(true);
                button2.SetActive(true);  
                StartCoroutine(playAudio(audioInfo));          

                finishStateLoad();
                
                break;

            case State.PretestIncorrect:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index:4 , delay: 1f),};

                button1.SetActive(false);
                button2.SetActive(false);


                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
                
                break;

            case State.PretestCorrect:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index:3 , delay: 0.5f),};

                doublebed.SetActive(false);
                sleepy.SetActive(true);
                singlebed.SetActive(true);
                // yes, that's target ship
                button1.SetActive(false);
                button2.SetActive(false);
                startpractice.SetActive(true);
                
                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
               
                break;


            case State.ButtonsDemoPractice:

                buttons1Clicked = false;
                startpractice.SetActive(true);
                break;

            case State.PracticeButtonClicked:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index:4 , delay: 0.5f),};

                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();
            
                break;

            case State.Practice:

                gameObject.SetActive(false);
                startpractice.SetActive(false);
                practice.SetActive(true);
                finishStateLoad();
                PreTest.SetActive(false);
                moveToNextState();
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
    
    yield return new WaitForSeconds(3);
        
       
     if (currentState == State.PretestIncorrect)
        {
           stateToLoad = State.TargetKitty;
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
           Restartclick();
        }

        else if (currentState== State.Pretest)
        {
            Debug.Log("wait");
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
        stateToLoad = State.TargetKitty;
        startpractice.SetActive(false);
        pretestrepeat.SetActive(false);
        doublebed.SetActive(false);
        sleepy.SetActive(false);
        

        if (replayed==2)
        {
            EndGame();
        }

    }

    // public void restartDemo()
    // {
    //     repeat.SetActive(false);
    //     start.SetActive(false);
    //     stateToLoad = State.IntroTargetShip;
    // }

    public void startTrials()
    {
        start.SetActive(false);
        trials.SetActive(true);
        gameObject.SetActive(false);
    }
        

    public void Correct() // 
    {
        stateToLoad = State.PretestCorrect;
    }

    public void Incorrect()
    {
        stateToLoad = State.PretestIncorrect;
    }

    public void RepeatDemo(){

        if (!buttons1Clicked)
        {   
            buttons1Clicked = true;
            pretestrepeat.SetActive(false);
            startpractice.SetActive(false);
            stateToLoad = State.TargetKitty;
        }
        
    }

    public void StartPractice()
    {
        if (!buttons1Clicked)
        {
            buttons1Clicked = true;
            // pretestrepeat.GetComponent<Button>().interactable = false;
            // startpractice.GetComponent<Button>().interactable = false;
            stateToLoad = State.PracticeButtonClicked;
        }
        
    }

    public void EndGame()
    {
        end.SetActive(true);
    }

}
