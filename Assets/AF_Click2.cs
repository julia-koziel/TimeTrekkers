using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AF_Click2 : MonoBehaviour
{
    public bool AFClicked;
    private MenuScreenLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
        gameLogic = FindObjectOfType<MenuScreenLogic>();

    }

    // Update is called once per frame
    void Update()
    {
       gameLogic.AFClicked=true; 
    }
}