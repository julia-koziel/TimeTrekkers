using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTargetBehaviour : MonoBehaviour
{
    private float time;
    private float x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        

        gameObject.transform.position = new Vector3(x, 0, 0);

        if (x > 13)
        {
            time = 0;
            gameObject.transform.position = new Vector3(-13, 0, 0);
            gameObject.SetActive(false);
        }  
    }
}
