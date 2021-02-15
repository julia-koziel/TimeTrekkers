using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    public GameObject Ins1;
    public GameObject Ins2;
    public GameObject Ins3;
    public GameObject intro;
    public GameObject trials;
    // Start is called before the first frame update
    void Start()
    {
    Ins1.SetActive(true); 
    }

    // Update is called once per frame
    void Update()
    {
    
    }


    public void inst2()
    {
    Ins1.SetActive(false);
    Ins2.SetActive(true);
    }

    public void inst3()
    {
    Ins2.SetActive(false);
    Ins3.SetActive(true);
    }
    
    public void starttrials()
    {
    Ins3.SetActive(false);
    trials.SetActive(true);
    }
}
