using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolShed.Android.OS;

public class GameLogicDemo : MonoBehaviour
{

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


    public GameObject WatchAgain;
    public GameObject ParentPractice;
    public GameObject practice;

    private DemoLogic logic;
    private Animator animator;
    public GameObject sound;
    public bool click = false;
    public bool point = false;

    private IEnumerator delay;

    private State stateToLoad = State.None;
    private State currentState = State.None;


    private enum State {
        None,
        scientistinstructions,
        scientistturning,
        kitty1,
        ITI,
        kitty2,
        Ratingcats,
        Trials,
        Buttons,}

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        logic = FindObjectOfType<DemoLogic>();
        scientist.SetActive(true);
        stateToLoad = State.scientistinstructions;
        
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (stateToLoad)
        {
            case State.scientistinstructions:

                
                Debug.Log("stateloaded");
                scientist.SetActive(true);
                WatchAgain.SetActive(false);
                ParentPractice.SetActive(false);
                StartCoroutine(clickdelay());
                finishStateLoad();
                break;

            case State.scientistturning:
                click = true;
                StartCoroutine(clickdelay());
                finishStateLoad();

                break;

            case State.kitty1:
                cat1.SetActive(true);
                cat1.transform.position = singlebed.transform.position; 
                singlebed.SetActive(true);
                Vibrator.Vibrate(700, 250);
                sound.SetActive(true);
                Debug.Log("State2");
                StartCoroutine(clickdelay());
                finishStateLoad();
                
                break;

            case State.ITI:
                cat1.SetActive(false);
                finishStateLoad();
                StartCoroutine(clickdelay());
                sound.SetActive(false);
                break;

            case State.kitty2:
                cat2.SetActive(true);
                cat2.transform.position = singlebed.transform.position;
                Vibrator.Vibrate(700, 100);
                finishStateLoad();
                StartCoroutine(clickdelay());
                break;

            case State.Ratingcats:
                cat3.SetActive(true);
                cat4.SetActive(true);
                doublebed.SetActive(true);
                StartCoroutine(clickdelay());
                scientist.SetActive(true);
                singlebed.SetActive(false);
                cat2.SetActive(false);
                point = true;
                finishStateLoad();

                StartCoroutine(clickdelayshort());
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
                StartCoroutine(clickdelay());

                break;

            case State.Buttons:
                // CameraTransition.Instance.transitionOut();
                sleepy.SetActive(false);
                singlebed.SetActive(false);
                WatchAgain.SetActive(true);
                ParentPractice.SetActive(true);
                sleepbubble.SetActive(false);
                Theo.SetActive(false);
                finishStateLoad();
                click = false;
                point = false;

                break;

            case State.None:
                break;

            default:
                finishStateLoad();
                break;


    }
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
        yield return new WaitForSeconds(2f);
        moveToNextState();
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
        practice.SetActive(true);
        gameObject.SetActive(false);
    }

        
    }


