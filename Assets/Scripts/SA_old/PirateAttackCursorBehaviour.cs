using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateAttackCursorBehaviour : MonoBehaviour
{
    private AudioSource[] aSources;
    public GameObject[] textboxes;
    private Animator animator;
    private PirateAttackDemoLogic gameLogic;
    public AudioSource carHorn;
    private int clickTrigger = Animator.StringToHash("isClicked");
    private IEnumerator clickRoutine;
    public enum State {
        None,
        TargetShip,
        GoodShip,
        TargetShip2,
        TargetShip3,
        ClickPipLate,
        BackToCentre
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;
    private bool audioOn;
    private bool subtitleOn; 
    public bool clicked;
    private Vector3 targetDest;
    private Vector3 startPosition;
    
    
    private bool spokenAudioEnabled;

    void Start()
    {

        animator = GetComponent<Animator>();
        aSources = GetComponents<AudioSource>();
        gameLogic = FindObjectOfType<PirateAttackDemoLogic>();
        startPosition = transform.position;

        string prefsKey = PrefsKeys.Keys.SpokenAudio.ToString();
        subtitleOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;
        audioOn = PlayerPrefs.GetInt(prefsKey, 0) == 1;
        

    }

    void OnEnable()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        AudioBundle[] audioInfo; 
        switch (stateToLoad)
        {
            case State.TargetShip:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 0, delay: 0),
                };
                targetDest = new Vector3(2.5f,-1.5f,0);
                animator.SetTrigger("point");
                clickRoutine = click(currentState);
                StartCoroutine(clickRoutine);
                StartCoroutine(playAudio(audioInfo));

                // goto default;
                break;

             case State.GoodShip:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 1, delay: 3),
                };
                targetDest = new Vector3(2.5f,-1.5f,0);
                StartCoroutine(playAudio(audioInfo));
                // goto default;
                break;

            case State.TargetShip2:
                animator.SetTrigger("point");
                clickRoutine = click(currentState);
                StartCoroutine(clickRoutine);
                break;

             case State.TargetShip3:
                audioInfo = new AudioBundle[] {
                    new AudioBundle(index: 2, delay: 0),
                };
                targetDest = new Vector3(2.5f,-1.5f,0);
                animator.SetTrigger("point");
                clickRoutine = click(currentState);
                StartCoroutine(clickRoutine);
                StartCoroutine(playAudio(audioInfo));
                // goto default;
                break;

            case State.BackToCentre:
                targetDest = startPosition;
                goto default;

            case State.None:
                break;
                
            default:
                // set time to 0
                currentState = stateToLoad;
                stateToLoad = State.None;
                break;
        }

        if (currentState != State.None) {
            
            // speed = currentState == State.ClickPipLate ? fastSpeed : normalSpeed;
            // speed = currentState == State.BackToCentre ? slowSpeed : normalSpeed;

            // float step =  speed * Time.deltaTime; // calculate distance to move
            // transform.position = Vector3.MoveTowards(transform.position, targetDest, step);

            // // Check if approximately at destination
            // if (Vector3.Distance(transform.position, targetDest) < 0.1f)
            // {
                if (currentState != State.BackToCentre)
                {
                    clickRoutine = click(currentState);
                    StartCoroutine(clickRoutine); // Cursor clicks
                }
                
                currentState = State.None;
        }
    }

    private IEnumerator click(State clickState)
    {
        animator.SetTrigger("point");
        yield return new WaitForSeconds(2f);

        // if (clickState != State.ClickWrong) { carHorn.Play(); }
        // animator.SetTrigger(clickTrigger);

        yield return new WaitForSeconds(0.05f);
        animator.SetTrigger("click");
        stateToLoad = State.BackToCentre;
        clicked=false;
    }

    private IEnumerator playAudio(AudioBundle[] audioInfo)
    {
        AudioSource currentAudio;

        for (int i = 0; i < audioInfo.Length; i++)
        {
            yield return new WaitForSeconds(audioInfo[i].delay);
            currentAudio = aSources[audioInfo[i].index];
            if (audioOn) {currentAudio.Play();}
            yield return new WaitForSeconds(currentAudio.clip.length + audioInfo[i].delay);
        }

    }

    public void move(State direction) {
        stateToLoad = direction;
    }

    public Vector3 getStartPosition() {
        return startPosition;
    }

    public void TheoClicked()
    {
        clicked = true;
    }


    public void reset()
    {
        stateToLoad = State.None;
        currentState = State.None;
        StopAllCoroutines();
        // foreach (AudioSource audioSource in aSources)
        // {
        //     if (audioSource.isPlaying) { audioSource.Stop(); }
        // }
        transform.position = startPosition;
    }
}