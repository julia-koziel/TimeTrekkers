using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicBird : MonoBehaviour
{
    public GameObject[] birds;
    public float time;
    
    public int trial_n;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time < 0.5f)
        {
        foreach (GameObject bird in birds)
        {
            bird.SetActive(false);
        }
        }
    }

    public float setcurrentTime()
    {
        return time;
    }
}

