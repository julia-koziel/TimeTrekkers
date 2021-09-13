using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStart : MonoBehaviour
{

    private Animator animator;
    private DinoOnClick Dino;
    private TreasureOnClick Treasure;
    private PirateOnClick Pirate; 
    private QueensStartZoom Queens;
    // private pirateButton pirate;
    // private treasureButton treasure;

    // Start is called before the first frame update
    void Start()
    {
        Dino  = FindObjectOfType<DinoOnClick>();
        Treasure = FindObjectOfType<TreasureOnClick>();
        Pirate = FindObjectOfType<PirateOnClick>();
        Queens = FindObjectOfType<QueensStartZoom>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Dino.DinoClick)
        {
            animator.SetTrigger("Click");  
        }

        if(Treasure.TreasureClick)
        {
            animator.SetTrigger("Click");
        }

        if (Pirate.PirateClick)
        {
            animator.SetTrigger("Click");
        }

        // if(Queens.QueensClick)
        // {
        //     animator.SetTrigger("Click");
        // }
        
    }
}
