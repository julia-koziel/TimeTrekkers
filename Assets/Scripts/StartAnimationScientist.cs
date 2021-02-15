using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationScientist : MonoBehaviour
{
    private DemoLogicTactile logic;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<DemoLogicTactile>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

       if (logic.click) 
       {
           animator.SetTrigger("click");
       }

       if (logic.point)
       {
           animator.SetTrigger("point");
       }
    }

    public void pressed()
    {
        logic.moveToNextState();
    }
}
