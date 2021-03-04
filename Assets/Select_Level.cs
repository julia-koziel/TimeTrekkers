﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select_Level : MonoBehaviour
{
    public BoolVariable level1;
    public BoolVariable level2;

    public GameObject stage1;
    public GameObject stage2;
    public GameObject stage3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (level1.Value==1)
    {
        stage1.SetActive(false);
        stage2.SetActive(true);
    }

    else if (level2.Value==1)
    {
        stage1.SetActive(false);
        stage2.SetActive(false);
        stage3.SetActive(true);

    }

    else
    {
        stage1.SetActive(true);
    }

}
}
