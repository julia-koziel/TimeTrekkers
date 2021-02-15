using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureOnClick : MonoBehaviour
{
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject oldtext;
    public GameObject text;
    public bool TreasureClick;
    private AudioSource audio;
    // Start is called before the first frame update
 
     void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void OnMouseDown()
    {
        audio.Play();
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        text.SetActive(true);
        oldtext.SetActive(false);
        TreasureClick = true;
        gameObject.SetActive(false);
    }
}
