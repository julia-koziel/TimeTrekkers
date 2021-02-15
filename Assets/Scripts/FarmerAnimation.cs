using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAnimation : MonoBehaviour
{
    private Animator animator;
    private SRLAnimationStages logic;
    private TheoAnimation theo;
    public Transform target;
    public float time;
    public float velocity = 0.1f; 

    // Start is called before the first frame update
     void Start()
    {
        animator = GetComponent<Animator>();
        logic = FindObjectOfType<SRLAnimationStages>();
        theo = FindObjectOfType<TheoAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, velocity * time);
        if(Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            animator.SetTrigger("stable");
        }
    }
}