using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicLevel2 : MonoBehaviour
{
    private float time;
    public GameObject trialsLevel2;

    public GameObject bell;
     
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime;

       if (time>5)
       {
           trialsLevel2.SetActive(true);
           gameObject.SetActive(false);
           
       } 
    }
}
