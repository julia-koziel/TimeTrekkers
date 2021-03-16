using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGameObject : MonoBehaviour
{

    public GameObject demo;
    public GameObject map;
    public GameEvent stageEnd;

    public float time;
    // Start is called before the first frame update
    void Start()
    {   
        time=0;
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time>2.75f)
        {
           map.SetActive(false);
           demo.SetActive(true);
        }
        
        if (time>28)
        {
            stageEnd.Raise();
        }
    }
}
