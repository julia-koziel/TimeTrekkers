using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SH_Click2 : MonoBehaviour
{
    public bool SHClicked;
    private MenuScreenLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
        gameLogic = FindObjectOfType<MenuScreenLogic>();

    }

    // Update is called once per frame
    void Update()
    {
       gameLogic.SHClicked=true; 
    }
}