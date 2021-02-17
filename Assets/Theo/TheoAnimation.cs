using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheoAnimation : MonoBehaviour
{
    public float time;
    public float velocity;
    public Transform target;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, 0.1f * time);
        if(Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            anim.SetTrigger("speak");
        }
    }

    public void react()
    {
        anim.SetTrigger("scared");
    }
}
