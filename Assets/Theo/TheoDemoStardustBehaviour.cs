﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoDemoStardustBehaviour : MonoBehaviour
{
   private AudioSource[] aSources;
    private Animator animator;
    private DemoLogicStardust logic;

    public GameObject stardust;
    private int clickTrigger = Animator.StringToHash("isClicked");
    
    private IEnumerator clickRoutine;
    public enum State {
        None,
        ClickPip1,
        ClickPip2,
        ClickWrong,
        ClickPipLate,
        BackToCentre
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;
    public bool clicked;
    private Vector3 targetDest;
    private Vector3 startPosition;
    
    private bool spokenAudioEnabled;

    void Start()
    {

        animator = GetComponent<Animator>();
        logic = FindObjectOfType<DemoLogicStardust>();
        startPosition = transform.position;
        aSources = GetComponents<AudioSource>();

    }

    void OnEnable()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateToLoad)
        {
            case State.ClickPip1:

                targetDest = new Vector3(0,-800,0);
                animator.SetTrigger("point");
                clickRoutine = click(currentState);
                StartCoroutine(clickRoutine);
                break;

            case State.ClickPip2:
                targetDest = new Vector3(2.1f,-1.5f,0);
                animator.SetTrigger("point");
                clickRoutine = click(currentState);
                StartCoroutine(clickRoutine);
                break;

            case State.ClickWrong:
                animator.SetTrigger("click");
                break;

            case State.ClickPipLate:
                targetDest = new Vector3(3,-1.5f,0);
                goto default;

            case State.BackToCentre:
                targetDest = new Vector3(0,-800,0);
                break;

            case State.None:
                break;
                
            default:
                // set time to 0
                currentState = stateToLoad;
                stateToLoad = State.None;
                break;
        }

        // if (currentState != State.None) {
            
        // //     speed = currentState == State.ClickPipLate ? fastSpeed : normalSpeed;
        // //     speed = currentState == State.BackToCentre ? slowSpeed : normalSpeed;

        // //     float step =  speed * Time.deltaTime; // calculate distance to move
        // //     transform.position = Vector3.MoveTowards(transform.position, targetDest, step);

        // //     // Check if approximately at destination
        // //     if (Vector3.Distance(transform.position, targetDest) < 0.1f)
        // //     {
        //         if (currentState != State.BackToCentre)
        //         {
        //             clickRoutine = click(currentState);
        //             StartCoroutine(clickRoutine); // Cursor clicks
        //         }
                
        //         currentState = State.None;
        //     }
        }

    private IEnumerator click(State clickState)
    {
        yield return new WaitForSeconds(0.5f);

        // animator.SetTrigger("click");
        stateToLoad= State.None;
    }





     private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            Debug.Log(audioInfo[i].text);
            currentAudio = aSources[audioInfo[i].index];
            currentAudio.Play();
            yield return new WaitForSeconds(1);
        }
    }

    public void move(State direction) {
        stateToLoad = direction;
    }

    public Vector3 getStartPosition() {
        return startPosition;
    }

    public void reset()
    {
        animator.SetTrigger("click");
    }

    public void TheoClicked()
    {
        logic.playSound();
    }
}