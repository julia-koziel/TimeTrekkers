using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetShipAnimation : MonoBehaviour
{
    private AudioSource audio;
    private Animator animator;
    private PirateAttackAnimation logic;
    private TheoAnimation theo;
    public Transform target;
    public float time;
    public float velocity = 40; 

    // Start is called before the first frame update
     void Start()
    {
        animator = GetComponent<Animator>();
        logic = FindObjectOfType<PirateAttackAnimation>();
        audio = GetComponent<AudioSource>();
        theo = FindObjectOfType<TheoAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, velocity * time);
        if(Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            triggerShips();
            theo.react();
        }
    }

    void setNonActive()
    {
        gameObject.SetActive(false);
    }

    void sound()
    {
        audio.Play();
    }

    void triggerShips()
    {
        logic.move = true;
        
    }
}
