using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickGoPractice : MonoBehaviour
{


    public GameObject react;
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
        react.SetActive(false);
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
                react.SetActive(false);
                move = true;
            }
        }
    }


    public void OnMouseDown()
    {
            
        if (gameLogic.j)
        {
            
            gameLogic.pointer2();
            if (i)
            {
                react.SetActive(true);
                time = 0;
                rend.enabled = false;
                move = false;
            }
    
        }

        sound.Play();
    }


}