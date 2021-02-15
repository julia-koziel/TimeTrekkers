﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GNFast : MonoBehaviour
{

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
            SceneManager.LoadScene("Go_nogo_Fast", LoadSceneMode.Single);
            gameObject.SetActive(false);
        }
    }


}
