using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCatPractice : MonoBehaviour
{
    private GameLogicPractice gameLogic;
    
    private KittyBehaviourPractice catBehaviour;

    public float time;
    public int catorder;
    public int correct;
    

    public int sleepycat;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicPractice>();
        catBehaviour = FindObjectOfType<KittyBehaviourPractice>();
        time = gameLogic.SetTime();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void OnMouseDown()
    {
        if (gameLogic.catIsClickable)
        {
            gameLogic.newtrial();
            gameLogic.sleepycat = 0;
           
        }

    }
}
