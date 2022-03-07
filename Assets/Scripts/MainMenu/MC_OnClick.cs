using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_OnClick : MonoBehaviour
{
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject MCIcon;
    public bool MCClick;


    private MenuScreenLogic gameLogic;
    // Start is called before the first frame update
 
     void Start()
    {
        gameLogic = GetComponent<MenuScreenLogic>();
        // chime = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void OnMouseDown()
    
    {  
        MCClick = true;
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        MCIcon.SetActive(false);
        gameLogic.MCClicked=true;
       
    }
}