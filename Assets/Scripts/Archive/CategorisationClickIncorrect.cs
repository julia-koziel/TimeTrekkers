﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorisationClickIncorrect : MonoBehaviour
{
    public int dinoType=0;
    private CategorisationPretest logic;
    public GameObject[] stimuli;
    public GameObject cross;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        logic = FindObjectOfType<CategorisationPretest>();  
        sound = GetComponent<AudioSource>();
        cross.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log("clicked");

        logic.incorrectclicked();
        cross.SetActive(true);
        sound.Play();

        StartCoroutine(clickdelay());

    }


    private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1.9f); 
        cross.SetActive(false);
    }
}