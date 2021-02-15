using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAnimationTheo : MonoBehaviour
{
    private DemoLogicBird logic;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<DemoLogicBird>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

       if (logic.flyout) 
       {
           animator.SetTrigger("click");
       }


    }
}
