using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexAnimation : MonoBehaviour
{
    private AudioSource audio;
    private Animator animator;
    private DinoAnimation logic;
    private HerbDinoAnimation herb;
    public float time;
    public int velocity = 10; 
    public float x;
    public bool Herbrun = false;

    // Start is called before the first frame update
     void Start()
    {
        animator = GetComponent<Animator>();
        logic = FindObjectOfType<DinoAnimation>();
        herb = FindObjectOfType<HerbDinoAnimation>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        x = 1 * time * velocity;
       if (logic.Trun)
       {
           animator.SetTrigger("run");
           gameObject.transform.position = new Vector3(x,0,0);
       }
    }

    void setNonActive()
    {
        gameObject.SetActive(false);
    }

    void roar()
    {
        audio.Play();
    }

    void triggerHerb()
    {
        logic.Herbrun = true;
        
    }
}
