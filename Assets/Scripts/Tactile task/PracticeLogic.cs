using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeLogic : MonoBehaviour
{       
    public GameObject practice;
    public GameObject trials;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ParentPracticeAgain()
    {
        practice.SetActive(true);
        gameObject.SetActive(false);
    }

    public void StartTrials()
    {
        trials.SetActive(true);
        gameObject.SetActive(false);
    }
}
