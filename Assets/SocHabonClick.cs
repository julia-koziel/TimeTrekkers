using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocHabonClick : MonoBehaviour
{
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject SHIcon;
    public bool SHClick;


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
        SHClick = true;
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        SHIcon.SetActive(false);
       
    }
}