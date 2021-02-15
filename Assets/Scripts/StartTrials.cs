using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTrials : MonoBehaviour
{
    public GameObject Intro;
    public GameObject Trials;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        Intro.SetActive(false);
        Trials.SetActive(true);
    }
}
