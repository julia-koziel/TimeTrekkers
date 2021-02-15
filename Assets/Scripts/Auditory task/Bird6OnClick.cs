﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird6OnClick : MonoBehaviour
{
     private GameLogicTemporalOrderTask gameLogic;
    
    private BirdBehaviourTemporalOrder birdBehaviour;

    public float time;
    public int birdorder;
    public int correct;
    public GameObject dancing; 
    public GameObject closed;
    private Animator animator;

    public int sleepybird;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicTemporalOrderTask>();
        birdBehaviour = FindObjectOfType<BirdBehaviourTemporalOrder>();
        time = gameLogic.SetTime();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (birdBehaviour.closed)
        {
          closed.SetActive(true);
         }
    }

    public void OnMouseDown()
    {
        if (birdBehaviour.firstbird == 5)
        {
           correct = 1;
           gameLogic.cummulativecorrect++;
        //    animator.SetTrigger("Click");
        dancing.SetActive(true);
           StartCoroutine(delay());
       }
        

    
       else if (birdBehaviour.secondbird == 5)
       {
           correct=0;
           gameLogic.newtrial();
       }
       
       
       
       if (gameLogic.birdIsClickable)
        {
            gameLogic.newtrial();
           
        }
    }

     public IEnumerator delay()
    {
       yield return new WaitForSeconds(1.5f);

        dancing.SetActive(false);
        gameLogic.newtrial();
        
    }
}

