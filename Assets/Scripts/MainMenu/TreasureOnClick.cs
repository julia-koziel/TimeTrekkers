using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureOnClick : MonoBehaviour
{
    public GameObject[] otherbuttons;
    public GameObject zoomed;
    public GameObject oldtext;
    public bool TreasureClick;
    
    
    public void OnMouseDown()
    {
        foreach (GameObject other in otherbuttons)
        {
            other.SetActive(false);
        }
        zoomed.SetActive(true);
        oldtext.SetActive(false);
        TreasureClick = true;
        gameObject.SetActive(false);
    }
}
