﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongAudioBehaviour : MonoBehaviour {

    float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if (time > 1)
        {
            gameObject.SetActive(false);
            time = 0;
        }
		
	}
}
