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
    public bool QueensClick;
    private AudioSource audio;
    // Start is called before the first frame update
 
     void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();

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
        QueensClick = true;
        gameObject.SetActive(false);
    }
}
