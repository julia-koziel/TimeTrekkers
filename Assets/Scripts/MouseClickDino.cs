using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickDino : MonoBehaviour
{

    public GameObject react;
    public bool move = true;
    private Renderer rend;
    private GameLogicGo gameLogic;
    private AudioSource sound;
    private float time = 0;
    public bool i = false;
    private Animator animator;
    int isStoppedHash = Animator.StringToHash("isStopped"); // Quick access to animator transition bool
    private bool stoppingDog = false;
    private bool startingDog = false;
    private bool isStopped = false;
    private bool hasBeenStopped = false;
    private float barkDelay = 3;
    // NB checks if dog has been stopped during trial, not if dog is currently stopped
    private IEnumerator barkForAttention;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
        rend.enabled = true;
        // react.SetActive(false);
        gameLogic = FindObjectOfType<GameLogicGo>();

    }

    private void Update()
    {
        if (i)
        {
            time += Time.deltaTime;

            if (time > 1.5 && !move)
            {
                rend.enabled = true;
                // react.SetActive(false);
                move = true;
            }
        }

        if (stoppingDog)
        {
            // Transitions dog animation to stopped dog
            animator.SetBool(isStoppedHash, true);
            isStopped = true;
            hasBeenStopped = true;
            stoppingDog = false;
            sound.Play();
            barkForAttention = soundCoroutine();
            StartCoroutine(barkForAttention);
            // start barking routine
        }

        if (startingDog)
        {
            // Transitions dog animation back to moving dog
            animator.SetBool(isStoppedHash, false);
            isStopped = false;
            startingDog = false;
            StopCoroutine(barkForAttention);
            gameLogic.timetozero();
        }

    }

    private IEnumerator soundCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(barkDelay);
            sound.Play();
            Debug.Log("Bark!");
        }
    }

    public void OnMouseDown()
    {

        if (gameLogic.j)
        {
            gameLogic.logReactionTime();
            gameLogic.countplus();
            sound.Play();
            gameLogic.pointer2();

            if (i) {
                // react.SetActive(true);
                time = 0;
                rend.enabled = false;
                move = false;
            }
        }

        if (isStopped)
        {
            startDog(); // Restarts dog when clicked
        }
    }

    public void i_change()
    {
        i = !i;
    }

    public void stopDog()
    {
        stoppingDog = true;
    }

    public void startDog()
    {
        startingDog = true;
    }

    public bool dogHasBeenStopped()
    {
        return hasBeenStopped;
    }

    public void setDogHasBeenStopped(bool dogHasBeenStopped) // TODO rename
    {
        hasBeenStopped = dogHasBeenStopped;
    }

    public bool dogIsStopped()
    {
        return isStopped;
    }

    
}
