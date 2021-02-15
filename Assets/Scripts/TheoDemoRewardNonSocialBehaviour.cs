using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoDemoRewardNonSocialBehaviour : MonoBehaviour
{
    private Animator animator;
    private DemoRewardNonSocial gameLogic;
    public GameObject goodChest;
    public GameObject badChest;

    private int clickTrigger = Animator.StringToHash("isClicked");
    public enum State {
        None = 0,
        ClickBadChest = 1,
        BackToCentre = 2,
        ClickGoodChest = 4
    }
    private State stateToLoad = State.None;
    private State currentState = State.None;
    private Vector3 targetDest;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameLogic = FindObjectOfType<DemoRewardNonSocial>();
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
    
        switch (stateToLoad)
        {
            case State.ClickBadChest:
                targetDest = new Vector3(goodChest.transform.position.x,
                                         goodChest.transform.position.y - 1, // Box not centered
                                         goodChest.transform.position.z);
                
                animator.SetTrigger("point");
                StartCoroutine(clickRoutine());

                goto default;

            case State.BackToCentre:
                targetDest = startPosition;
                goto default;

            case State.ClickGoodChest:
                targetDest = new Vector3(badChest.transform.position.x,
                                         badChest.transform.position.y - 1, // Box not centered
                                         badChest.transform.position.z);
                animator.SetTrigger("pointgood");
                StartCoroutine(clickRoutine());
                goto default;

            case State.None:
                break;
                
            default:
                // set time to 0
                currentState = stateToLoad;
                stateToLoad = State.None;
                break;
        }
    }

    private IEnumerator clickRoutine()
    {
        yield return new WaitForSeconds(2f);
        
        animator.SetTrigger("click");
    }

    public void TheoClicked()
    {
        gameLogic.TheoClicked();
    }

    public void move(State direction) {
        stateToLoad = direction;
    }

    public Vector3 getStartPosition() {
        return startPosition;
    }

    public void reset()
    {
        stateToLoad = State.None;
        currentState = State.None;
        transform.position = startPosition;
    }
}

