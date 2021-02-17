// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Ship2Behaviour
//  : MonoBehaviour
// {
//     private float time;
//     private float x;

//     private GameLogicPirateAttack gameLogic;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         time += Time.deltaTime;
//         gameLogic.ship2on();
//         x = -1000 + time * gameLogic.velocity;

//         gameObject.transform.position = new Vector3(x, 0, 0);

//         if (x > 1440)
//         {
//             time = 0;
//             gameObject.transform.position = new Vector3(-1000, 0, 0);
//             gameLogic.ship2off();
//             gameObject.SetActive(false);
//         }  
//     }
// }
