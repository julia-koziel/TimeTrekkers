using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShipPractice : MonoBehaviour
{
    // Start is called before the first frame update
    private float time;
    private float x;
    private BoxCollider2D boxcoll;
    PiratePracticelogic gameLogic;
    private float clickTime;
    public int trial;
    private float shipX, shipY, mouseX, mouseY;
    private int shipType = 1;
    private string shipName;
    private float startTime;
    private float reactionTime;
    public bool wasclicked = false;
    private bool logged = false;
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
        gameLogic = FindObjectOfType<PiratePracticelogic>();
        boxcoll = GetComponent<BoxCollider2D>();
        shipName = gameObject.ToString();
    }

    void Update()
    {
        time += Time.deltaTime;
        gameLogic.ship1on();
        x = -12 + time * gameLogic.velocity;

        gameObject.transform.position = new Vector3(x, -1, 0);

        if (x >= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x - 0.5f && !logged)
        {
            startTime = gameLogic.time;
            logged = true;
        }

        if (x > 13) // out of screen. restart
        {
            boxcoll.enabled = true;
            gameLogic.correctship(); // a correct ship passed the screen
            if (wasclicked) //if the ship was clicked, mark as clicked
            {
                gameLogic.countplus();
                Debug.Log(clickTime - startTime);
                animator.SetTrigger("DefaultState");
                Debug.Log("default");
            }
            else
            {
                clickTime = 0;
                shipX = shipY = mouseX = mouseY = reactionTime = -1;
            }
            
            reset_wasclicked();

            time = 0;
            gameObject.transform.position = new Vector3(-12, -1, 0);
            logged = false;
            gameLogic.ship1off();
           

            gameObject.SetActive(false);
        }
    }

    public void OnMouseDown()
    {
        // so that only the first click is considered
        boxcoll.enabled = false;
        wasclicked = true;
        animator.SetTrigger("Click");

        clickTime = gameLogic.time;
        Vector3 shipPos = Camera.main.WorldToViewportPoint(transform.position);
        shipX = shipPos.x;
        shipY = shipPos.y;
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseX = mousePos.x;
        mouseY = mousePos.y;
        reactionTime = clickTime - startTime;
        Debug.Log("targetclicked");
        
    }

    public void reset_wasclicked()
    {
        wasclicked = false;
    }

}

