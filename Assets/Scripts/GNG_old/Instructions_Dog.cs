using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions_Dog : MonoBehaviour
{

    private Animator animator;
    private int turnTrigger = Animator.StringToHash("turnToWalk");
    public float velocity = 11.25f;
    public bool isBadDog;
    public GameObject mixedPracticeButton;
    private float x;
    private float time = 0;
    private float walkingStartPos;
    private float walkingY;
    private Vector3 startPosition;
    public enum State {
        None,
        Sitting,
        Turning,
        Running
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        stateToLoad = State.Sitting;
        if (isBadDog) { velocity = 7f; }
    }

    // Update is called once per frame
    void Update()
    {
        switch (stateToLoad)
        {
            case State.Sitting:
                transform.position = startPosition;
                walkingStartPos = startPosition.x;
                walkingY = startPosition.y;
                goto default;

            case State.Turning:
                animator.SetTrigger(turnTrigger);
                goto default;

            case State.Running:
                goto default;

            case State.None:
                break;

            default:
                currentState = stateToLoad;
                stateToLoad = State.None;
                break;
        }

        switch (currentState)
        {
            case State.Sitting:
                break;

            case State.Turning:
                
                if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Walking")
                {
                    stateToLoad = State.Running;
                }
                break;

            case State.Running:
                time += Time.deltaTime;
                x = walkingStartPos + time * velocity;
                gameObject.transform.position = new Vector3(x, walkingY, 0);

                if (x > 13)
                {
                    if (isBadDog)
                    {
                        gameObject.SetActive(false);
                    }
                    else
                    {
                        walkingStartPos = -13;
                        time = 0;
                    }
                }
                break;

            case State.None:
                break;

            default:
                break;
        }
    }

    public void move(State direction) {
        stateToLoad = direction;
    }

    public Vector3 getStartPosition() {
        return startPosition;
    }
}
