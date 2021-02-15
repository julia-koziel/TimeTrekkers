using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBird2OnClick : MonoBehaviour
{
    private GameLogicAuditoryTrials gameLogic;
    private FrequencyBird1OnClick firstBird;
    private BirdBehaviour BirdBehaviour;
    private AuditoryReadWriteCsv csvReadWrite;
    private Animator animator;

    public float time;
    public int catorder;
    public int correct;

    public int sleepycat;

    public bool clicked;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicAuditoryTrials>();
        BirdBehaviour = FindObjectOfType<BirdBehaviour>();
        firstBird = FindObjectOfType<FrequencyBird1OnClick>();
        csvReadWrite = FindObjectOfType<AuditoryReadWriteCsv>();
        time = gameLogic.SetTime();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 

        if (firstBird.clicked)
         {
            gameObject.SetActive(false);
         }
    
    }

    public void OnMouseDown()
    {   
        time=0;
        animator.SetTrigger("click");
        clicked = true;
        StartCoroutine(clickdelay());
        
        

        if (BirdBehaviour.firstbird == 1)
    {
       if (BirdBehaviour.firstincorrect)
       {
           gameLogic.correct = 0;
       }

       else if (BirdBehaviour.firstcorrect)
       {
           gameLogic.correct=1;
           gameLogic.cummulativecorrect++;
       }
    }

    
       else if (BirdBehaviour.secondbird == 1)
       {
           if(BirdBehaviour.secondincorrect)
           {
               gameLogic.correct =0;
           }

           else if (BirdBehaviour.secondcorrect)
           {
               gameLogic.correct=1;
               gameLogic.cummulativecorrect++;
           }
       }
    }

    private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1.2f);
        gameLogic.newtrial();
        clicked = false;
    }
}
