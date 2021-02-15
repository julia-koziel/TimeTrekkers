using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellClick : MonoBehaviour
{
    private Animator animator;
    private SoundPlaying sound;
    private GameLogicSustainedAttentionLevel2 gameLogic;

    private float time;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = FindObjectOfType<SoundPlaying>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time<1)
        {
            animator.SetTrigger("Default");
        }
    }

    public void OnMouseDown()
    {
        animator.SetTrigger("Click");  
        Debug.Log("bellclicked");   
        time =0;
        if (sound.correct)
        {
            gameLogic.correctAuditory =1;
        }

        else
        {
            gameLogic.correctAuditory = 0;
        }

    }
}
