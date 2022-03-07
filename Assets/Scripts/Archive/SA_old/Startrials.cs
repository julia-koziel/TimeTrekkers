using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startrials : MonoBehaviour
{
    public GameObject intro;
    public GameObject trials;

    // Start is called before the first frame update
    void Start()
    {
    trials.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
    intro.SetActive(false);
    trials.SetActive(true);
    }
}
