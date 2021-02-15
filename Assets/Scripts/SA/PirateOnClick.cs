using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateOnClick : MonoBehaviour
{
    
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject oldtext;
    public GameObject text;
    public bool PirateClick;

    private new AudioSource chime;
    // Start is called before the first frame update
 
     void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void OnMouseDown()
    {
        chime.Play(); 
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        text.SetActive(true);
        oldtext.SetActive(false);
        PirateClick = true;
        gameObject.SetActive(false);
    }
}