using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_Manager : MonoBehaviour
{
    public IntVariable block;
    public GameObject machine;
    public Vector2 dest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
    }

    public void MoveShip()
    {
        float speed = 6;
        float frameRate = 0.02f;
        float baseStep = speed * frameRate;
        float step;
        
        for (int i = 0; i < block; i++)
            {
                step = baseStep * Mathf.Abs(block-i-1);
                Vector3 currentPos = machine.transform.position;
                machine.transform.position = Vector3.MoveTowards(currentPos, dest, step);
            }
    }
}   
