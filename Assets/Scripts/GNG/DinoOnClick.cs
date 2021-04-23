using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoOnClick : MonoBehaviour
{
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject DinoIcon;
    public bool DinoClick;
    public float time;

    public MenuScreenLogic gameLogic;
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
        
        DinoClick = true;
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        DinoIcon.SetActive(false);
       
    }
}