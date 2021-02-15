using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickBadPractice : MonoBehaviour
{

    public bool move = true;
    private Renderer rend;
    private GameLogicGoPractice gameLogic;
    private float time;
    public bool i = false;
    public AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        gameLogic = FindObjectOfType<GameLogicGoPractice>();

    }

    private void Update()
    {
        if (i)
        {
            time += Time.deltaTime;
        
            if (time > 1.5 && !move)
            {
                rend.enabled = true;
                move = true;
            }
        }

    }


    public void OnMouseDown()
    {
        if (gameLogic.j)
        {
            gameLogic.pointer2();
            sound.Play();

            if (i)
            {
                rend.enabled = false;
                move = false;

                time = 0;
            }
            // else { sound.Play(); }
        }

    }


}
