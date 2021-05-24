using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationOpen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OpenLink()
    {
        Application.OpenURL("mailto:timetrekkers-study@kcl.ac.uk");
    }
    
}
