using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end_level2 : MonoBehaviour
{
   public BoolVariable level2;
   public BoolVariable level1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetLevel1()
    {
        level1.Value=0;
        level2.Value=1;
    }
}