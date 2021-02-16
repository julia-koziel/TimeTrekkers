using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsBehaviourAnimation : MonoBehaviour
{
    private Animator animator;
    private TheoAnimation theo;
    public float time=0;
    public float velocity; 

    SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        theo = FindObjectOfType<TheoAnimation>();
        rend = GetComponent<SpriteRenderer>();
        Color c = rend.material.color;
        c.a = 0f;
        rend.material.color = c;
    }   

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
    }

    public void triggerShips()
    {
        animator.SetTrigger("sink");
        // StartCoroutine ("FadeIn");
    }

    // public void SetNonActive()
    // {
    //     gameObject.SetActive(false);
    // }

    IEnumerator FadeIn()
    {
        for(float f = 0.05f; f <= 1; f+= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds (0.05f);
        }
    }
}
