using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship2MouseClick : MonoBehaviour
{
    // Start is called before the first frame update
    private float time;
    private float x;
    private GameLogicPirateAttack gameLogic;
    private BoxCollider2D boxcoll;
    private float clickTime;
    public int trial;
    private float shipX, shipY, mouseX, mouseY;
    private int shipType = 0;
    private string shipName;
    private float startTime;
    public bool wasclicked = false;
    private bool logged = false;
    private string id;
    private float reactionTime;

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicPirateAttack>();
        boxcoll = GetComponent<BoxCollider2D>();
        shipName = gameObject.ToString();
        returnID();
    }

    void Update()
    {
        time += Time.deltaTime;
        gameLogic.ship2on();
        x = -12 + time * gameLogic.velocity;

        gameObject.transform.position = new Vector3(x, -1, 0);

        if (x >= Camera.main.ScreenToWorldPoint(new Vector3(0,0,0)).x - 0.5f && !logged)
        {
            startTime = gameLogic.time;
            logged = true;
        }

        if (x > 13)
        {
            if (!wasclicked) //if the car was clicked, mark as clicked
            {
                clickTime = 0;
                reactionTime = -1;
            }
            
            gameLogic.updateCSV(id, clickTime, trial, shipType, wasclicked, reactionTime);
            wasclicked = false;

            boxcoll.enabled = true;
            time = 0;
            gameObject.transform.position = new Vector3(-12, -1, 0);
            logged = false;
            gameLogic.ship2off();
            gameObject.SetActive(false);
        }

    }
    public void OnMouseDown()
    {
        // so that only the first click is considered
        boxcoll.enabled = false;
        gameLogic.wrongclick();
        wasclicked = true;
        
        clickTime = gameLogic.time;
        Vector3 carPos = Camera.main.WorldToViewportPoint(transform.position);
        shipX = carPos.x;
        shipY
 = carPos.y;
        Vector3 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        mouseX = mousePos.x;
        mouseY = mousePos.y;
        reactionTime = clickTime - startTime;
    }
    void returnID()
    {
        id = PlayerPrefs.GetString("ID");
    }
}

