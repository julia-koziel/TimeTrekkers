using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTargetBehaviour : MonoBehaviour
{
    private float time;
    private float x;

    private GameLogicPirateAttack gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        gameLogic.ship1on();
        x = -1 + time * gameLogic.velocity;

        gameObject.transform.position = new Vector3(x, 0, 0);

        if (x > 13)
        {
            time = 0;
            gameObject.transform.position = new Vector3(-13, 0, 0);
            gameLogic.ship1off();
            gameObject.SetActive(false);
        }  
    }
}
