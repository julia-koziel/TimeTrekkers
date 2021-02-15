using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageRaise : MonoBehaviour
{

    public GameEvent StageEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StageEnd.Raise();
        
    }
}
