using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickBad : MonoBehaviour
{


    // public GameObject react;
    private AudioSource sound;
    public bool move = true;
    private Renderer rend;
    private GameLogicGo gameLogic;
    private float time = 0;
    public bool i = false;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        // react.SetActive(false);
        gameLogic = FindObjectOfType<GameLogicGo>();

    }

    private void Update()
    {
        if (i)
        {
            time += Time.deltaTime;

            if (time > 1.5 && !move)
            {
                rend.enabled = true;
                // react.SetActive(false);
                move = true;
            }
        }


    }

    public void OnMouseDown()
    {
        if (gameLogic.j)
        {
            gameLogic.logReactionTime();
            gameLogic.pointer2();
            gameLogic.wrongclick();
            sound.Play();

            if (i)
            {

                // react.SetActive(true);
                time = 0;
                rend.enabled = false;
                move = false;
            }

        }

    }

    public void i_change()
    {
        i = !i;
    }


}
