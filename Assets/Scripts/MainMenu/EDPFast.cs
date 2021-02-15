﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EDPFast : MonoBehaviour {

    public AudioSource click;
    float time = 0;

    private void Start()
    {
        click.Play();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            SceneManager.LoadScene("Emotional_Dot_Probe_Fast", LoadSceneMode.Single);
            gameObject.SetActive(false);
        }
    }


}
