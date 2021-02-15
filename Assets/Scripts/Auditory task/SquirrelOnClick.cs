using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquirrelOnClick : MonoBehaviour
{   
    private GameLogicAuditoryTrials gameLogic;
    private DemoLogicBird demoLogic;
    private Animator animator;

    public float time;

    public bool clicked; 
       
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogicAuditoryTrials>();
        demoLogic = FindObjectOfType<DemoLogicBird>();
        time = gameLogic.SetTime();
        animator = GetComponent<Animator>(); 
    
    }

    // Update is called once per frame
    void Update()
    {
        if (demoLogic.run)
        {
            animator.SetTrigger("click");
        }
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("click");
        clicked = true;
        Debug.Log("Click");
        StartCoroutine(clickdelay());
    }

     private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1f);
        gameLogic.auditoryTrial();
        gameObject.SetActive(false);
        
    }
}
