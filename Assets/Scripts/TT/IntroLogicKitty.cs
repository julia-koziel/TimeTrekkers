using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolShed.Android.OS;

public class IntroLogicKitty : MonoBehaviour
{
    public GameObject trials;
    public GameObject intro;
    public GameObject instructions;

    void Update()
    {

    }

    public void startinstructions()
    {
        instructions.SetActive(true);
        gameObject.SetActive(false);
    }

}

    
