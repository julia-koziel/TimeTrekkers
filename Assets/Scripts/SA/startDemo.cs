using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startDemo : MonoBehaviour
{
    public GameObject introScreen;
    public GameObject instructions;

    public bool wasClicked = false;
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
    wasClicked = true;
    introScreen.SetActive(false);
    instructions.SetActive(true);
    }
}
