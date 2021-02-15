using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyBird1OnClick : MonoBehaviour
{
    private GameLogicAuditoryTrials gameLogic;
    private FrequencyBird2OnClick secondBird;
    
    private BirdBehaviour BirdBehaviour;
    private AuditoryReadWriteCsv csvReadWrite;
    private Animator animator;

    public float time;
    public int catorder;

    public int sleepycat;

    public bool clicked; 
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicAuditoryTrials>();
        secondBird = FindObjectOfType<FrequencyBird2OnClick>();
        BirdBehaviour = FindObjectOfType<BirdBehaviour>();
        animator = GetComponent<Animator>();
        csvReadWrite = FindObjectOfType<AuditoryReadWriteCsv>();
        
    }

    // Update is called once per frame
    void Update()
    {
         time += Time.deltaTime; 


         if (secondBird.clicked)
         {
             gameObject.SetActive(false);
         }
 
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("click");
        clicked = true;
        Debug.Log("click");
        time=0;
        StartCoroutine(clickdelay());

        if (BirdBehaviour.firstbird == 0)
    {
       
       if (BirdBehaviour.firstincorrect)
       {
           gameLogic.correct = 0;
          
       }

       else
       {
           gameLogic.correct=1;
           gameLogic.cummulativecorrect++;
       }
    }
    
       else if (BirdBehaviour.secondbird == 0)
       {
           if(BirdBehaviour.secondincorrect)
           {
               gameLogic.correct =0;
           }

           else
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
