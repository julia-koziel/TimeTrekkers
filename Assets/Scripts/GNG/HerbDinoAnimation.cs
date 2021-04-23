using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbDinoAnimation : MonoBehaviour
{
    public DinoAnimation logic; 
    public TRexAnimation tRex;
    private Animator animator;
    public float time;
    public float x;
    public int velocity = 10;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tRex = FindObjectOfType<TRexAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

       if (logic.Herbrun)
       {
           animator.SetTrigger("run");

       }
    }

    void setNonActive()
    {
        gameObject.SetActive(false);
    }
}


