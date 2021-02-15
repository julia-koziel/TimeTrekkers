using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ToolShed.Android.OS;

public class DemoLogicTactile : MonoBehaviour
{
    
    private AudioSource[] aSources;

    public GameObject trials;
    public GameObject intro;
    public GameObject instructions;
    public GameObject cat1;
    public GameObject cat2;
    public GameObject cat3;
    public GameObject cat4;
    public GameObject scientist;
    public GameObject sleepy;
    public GameObject singlebed;
    public GameObject doublebed;
    public GameObject Theo;
    public GameObject sleepbubble;

    public GameObject question;
    public GameObject WatchAgain;
    public GameObject ParentPractice;
    public GameObject practice;

    private DemoLogic logic;
    private Animator animator;
    public GameObject sound;
    public bool click = false;
    public bool point = false;


    private State stateToLoad = State.None;
    private State currentState = State.None;


    private enum State {
        None,
        scientistinstructions,
        scientistturning,
        kitty1,
        ITI,
        kitty2,
        PreRating,
        Ratingcats,
        Trials,
        Buttons,
        ParentPracticeClicked,
        Practice,
        }

    // Start is called before the first frame updated
    void Start()
    
    { 
        animator = GetComponent<Animator>();
        scientist.SetActive(true);
        stateToLoad = State.scientistinstructions; 
        aSources = GetComponents<AudioSource>();
    }




    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo;

        switch (stateToLoad)
    {
        case State.scientistinstructions:
        audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 1f),};
       ;
                scientist.SetActive(true);
                WatchAgain.SetActive(false);
                ParentPractice.SetActive(false);
        StartCoroutine(playAudio(audioInfo));
        finishStateLoad();

        break;
        

        case State.scientistturning:
         audioInfo = new AudioBundle[] {
                    new AudioBundle( index: 1, delay: 0f),};
                click = true;
                cat1.SetActive(true);
                cat1.transform.position = singlebed.transform.position; 
                singlebed.SetActive(true);
        
        
            StartCoroutine(playAudio(audioInfo));
            finishStateLoad();

        
            break;

        case State.kitty1: 
        
        audioInfo = new AudioBundle[] {
                    new AudioBundle( index: 2, delay: 0.5f),
                    
                    };
                cat1.SetActive(true);
                cat1.transform.position = singlebed.transform.position; 
                singlebed.SetActive(true);
                Vibrator.Vibrate(700, 250);
                sound.SetActive(true);
                Debug.Log("State2");
                StartCoroutine(playAudio(audioInfo));
               
       
            finishStateLoad();
            break;

        case State.ITI:
        
                cat1.SetActive(false);
                finishStateLoad();
                StartCoroutine(clickdelayshort());
                sound.SetActive(false);
                break;

        case State.kitty2:
        audioInfo = new AudioBundle[] {
                    new AudioBundle( index: 3, delay: 0.5f),};
                cat2.SetActive(true);
                cat2.transform.position = singlebed.transform.position;
                Vibrator.Vibrate(700, 100);
                finishStateLoad();
                StartCoroutine(playAudio(audioInfo));
                break;

            case State.PreRating:
                audioInfo = new AudioBundle[] {
                    new AudioBundle( index: 4, delay: -1.5f),};

                StartCoroutine(playAudio(audioInfo));
                finishStateLoad();

                break;
            

            case State.Ratingcats:
            
                cat3.SetActive(true);
                cat4.SetActive(true);
                doublebed.SetActive(true);
                scientist.SetActive(true);
                singlebed.SetActive(false);
                cat2.SetActive(false);
                point = true;
                StartCoroutine(clickdelayshort());
                finishStateLoad();

                
                break;
            

            case State.Trials:
            
                click = false;

                sleepy.SetActive(true);
                sleepbubble.SetActive(true);
                cat3.SetActive(false);
                cat4.SetActive(false);
                singlebed.SetActive(true);
                doublebed.SetActive(false);
                Theo.SetActive(false);
                finishStateLoad();
                StartCoroutine(clickdelayshort());

                break;

            case State.Buttons:

                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 5, delay: 3f),};
                sleepy.SetActive(false);
                singlebed.SetActive(false);
                
                question.SetActive(true);
                WatchAgain.SetActive(true);
                ParentPractice.SetActive(true);
                sleepbubble.SetActive(false);
                StartCoroutine(playAudio(audioInfo));
                Theo.SetActive(false);
                
                click = false;
                point = false;

                break;

            case State.ParentPracticeClicked:

                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 6, delay: 1f),};
                
                StartCoroutine(playAudio(audioInfo));
                
                finishStateLoad();

                break;

            case State.Practice:

                practice.SetActive(true);
                gameObject.SetActive(false);

                break;
    }
    }

    public void moveToNextState()
    {
        // Function may not be needed depending on audio files
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
            currentAudio.Play();
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

        if (currentState != State.Buttons)
        {
        moveToNextState();
        }
    }

    private IEnumerator clickdelayshort()
    {
       yield return new WaitForSeconds(0.7f);
        moveToNextState();
    }

    public void watchAgain()
    {
        stateToLoad = State.scientistinstructions;
    }

    public void parentPractice()
    {
        stateToLoad = State.ParentPracticeClicked;
    }

}
