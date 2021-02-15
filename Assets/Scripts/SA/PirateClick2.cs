using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateClick2 : MonoBehaviour
{
    // Start is called before the first frame update
    private MenuScreenLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
        gameLogic =FindObjectOfType<MenuScreenLogic>();

    }

    // Update is called once per frame
    void Update()
    {
       gameLogic.PirateClicked=true; 
    }
}

