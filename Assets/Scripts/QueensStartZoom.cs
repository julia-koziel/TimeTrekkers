using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueensStartZoom : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject oldtext;
    public GameObject text;
    public MenuScreenLogic gameLogic;
    // Start is called before the first frame update
 
     void Start()
    {
        gameLogic = FindObjectOfType<MenuScreenLogic>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void OnMouseDown()
    {
        
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        text.SetActive(true);
        oldtext.SetActive(false);
        gameLogic.ButtonClicked();
    }
}
