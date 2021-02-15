using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introscript : MonoBehaviour
{
    public GameObject intro;
    public GameObject trials;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Click()
    {
    intro.SetActive(false);
    trials.SetActive(true);
    }
}
