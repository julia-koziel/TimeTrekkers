using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationTheoBird : MonoBehaviour
{
    private DemoLogicBird logic;
    public GameObject bird2;
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

       if (logic.click) 
       {
           animator.SetTrigger("click");
       }

       if (logic.point)
       {
           animator.SetTrigger("point");
       }

       if (logic.point2)
       {
           animator.SetTrigger("point2");
       }
    }

    public void BirdFly()
    {
        logic.flyout = true;
        bird2.SetActive(false);
        Debug.Log("flyout");

    }

    public void SquirrelRun()
    {
        logic.run = true;
    }

}
