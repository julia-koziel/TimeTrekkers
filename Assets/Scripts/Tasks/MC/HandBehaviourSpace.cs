using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehaviourSpace : MonoBehaviour
{
    private Animator animator;
    private GameLogicSpaceDemo gameLogic;
    public GameObject leftPlanet;
    public GameObject rightPlanet;
    public GameObject alienContainer;
    public GameObject alien;
    private Animator alienAnimator;
    public GameObject SnowballsContainer;
    private SnowballParticleSystem Snowballs;
    private int clickTrigger = Animator.StringToHash("isClicked");
    private IEnumerator clickRoutine;
    public enum State {
        None,
        PointLeftPlanet, ClickLeftPlanet,
        BackToCentre,
        PointRightPlanet, ClickRightPlanet
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;
    private float moveSpeed = 5.0f;
    private Vector3 targetDest;
    private float rotateSpeed = 70.0f;
    private Quaternion targetRotation;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        alienAnimator = alien.GetComponent<Animator>();
        gameLogic = FindObjectOfType<GameLogicSpaceDemo>();
        // Snowballs = SnowballsRect.GetComponent<SnowballsBehaviour>();
        Snowballs = SnowballsContainer.GetComponent<SnowballParticleSystem>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateToLoad)
        {
            case State.PointLeftPlanet:
                targetDest = startPosition;
                targetRotation = Quaternion.Euler(0, 0, 50);
                goto default;

            case State.ClickLeftPlanet:
                targetDest = new Vector3(leftPlanet.transform.position.x,
                                        leftPlanet.transform.position.y - 1,
                                        leftPlanet.transform.position.z);

                targetRotation = Quaternion.Euler(0, 0, 0);
                Debug.Log(targetDest);
                goto default;

            case State.BackToCentre:
                targetDest = startPosition;
                targetRotation = Quaternion.Euler(0, 0, 0);
                goto default;

            case State.PointRightPlanet:
                targetDest = startPosition;
                targetRotation = Quaternion.Euler(0, 0, -50);
                goto default;

            case State.ClickRightPlanet:
                targetDest = new Vector3(rightPlanet.transform.position.x,
                                         rightPlanet.transform.position.y - 1,
                                         rightPlanet.transform.position.z);

                targetRotation = Quaternion.Euler(0, 0, 0);
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
            // Move hand
            float moveStep =  moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetDest, moveStep);

            // Rotate hand if needed
            float rotateStep = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateStep);
            

            // Check if approximately at destination and at final rotation
            if (Vector3.Distance(transform.position, targetDest) < 0.001f &&
                Mathf.Abs(Quaternion.Angle(transform.rotation, targetRotation)) < 1f)
            {
                switch (currentState)
                {
                    case State.PointLeftPlanet:
                        goto default;

                    case State.ClickLeftPlanet:
                        clickRoutine = click();
                        StartCoroutine(clickRoutine);
                        goto default;

                    case State.BackToCentre:
                        StartCoroutine(delayedMoveToNextState());
                        goto default;

                    case State.PointRightPlanet:
                        goto default;

                    case State.ClickRightPlanet:
                        clickRoutine = click();
                        StartCoroutine(clickRoutine);
                        goto default;

                    default:
                        currentState = State.None;
                        break;
                }
            }
        }
    }

    private IEnumerator click()
    {
        animator.SetTrigger(clickTrigger);
        yield return new WaitForSeconds(0.1f);
        animator.SetTrigger(clickTrigger);
        // Snowballs.switchOn(false);
        Vector3 pos = alienContainer.transform.position;
        pos.x = transform.position.x;
        alienContainer.transform.position = pos;
        alien.SetActive(true);
        alienAnimator.SetTrigger("MoveFront");
        move(State.BackToCentre);
    }

    private IEnumerator delayedMoveToNextState()
    {
        yield return new WaitForSeconds(2);
        gameLogic.moveToNextState();
    }

    public void move(State direction) {
        stateToLoad = direction;
    }

    public void reset()
    {
        stateToLoad = State.None;
        currentState = State.None;
        StopAllCoroutines();
        transform.position = startPosition;
        transform.rotation = Quaternion.identity;
    }
}
