using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AF_On_Click : MonoBehaviour
{
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject AFIcon;
    public bool AFClicked;


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
        
        AFClicked = true;
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        AFIcon.SetActive(false);
       
    }
}