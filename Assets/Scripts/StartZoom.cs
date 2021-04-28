using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZoom : MonoBehaviour
{
    private Animator animator;
    private MainMenuLogicNew gameLogic;
   
    public GameObject text;
    public GameObject DinoButton;
    public GameObject PirateButton;
    public GameObject QueensButton;
    public GameObject TreasureButton;
    private State stateToLoad = State.None;
    private State currentState = State.None;
    public GameObject Instructions;
    private float time;
    
    private enum State
    {
        None,
        DinoScreen, PirateScreen, TreasureScreen, QueensScreen,
        End
    }

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(true);
        animator = GetComponent<Animator>();
        gameLogic = FindObjectOfType<MainMenuLogicNew>();
        DinoButton.SetActive(false);
        PirateButton.SetActive(false);
        TreasureButton.SetActive(false);
        QueensButton.SetActive(false);
        stateToLoad = State.DinoScreen;
        currentState = State.DinoScreen;
        Instructions.SetActive(true);
    }

        // Update is called once per frame
    void Update()
        {
            
        switch (stateToLoad)
        {

            case State.DinoScreen: // Setting of the 1st pair (correct click)
            currentState = State.DinoScreen;
            DinoButton.SetActive(true);
            break;

            case State.PirateScreen: 
            currentState = State.PirateScreen;// This is done by the Mouse Click House script.
            DinoButton.SetActive(false);
            PirateButton.SetActive(true);
            break;
            
            case State.TreasureScreen:
            currentState = State.TreasureScreen;
            PirateButton.SetActive(false);
            TreasureButton.SetActive(true);
            break;

            case State.QueensScreen:
            currentState = State.QueensScreen;
            PirateButton.SetActive(false);
            TreasureButton.SetActive(false);
            QueensButton.SetActive(true);
            break;

            case State.End: 
            currentState = State.End;
            QueensButton.SetActive(false);
            finishStateLoad();
                break;

            case State.None:
                break;

        }
        time += Time.deltaTime;


    }

      public void moveToNextState()
    {
        stateToLoad = currentState.Next();
        Debug.Log("Next");

    }

    public void finishStateLoad()
    {
        currentState = stateToLoad;
        stateToLoad = State.DinoScreen;
    }
}
