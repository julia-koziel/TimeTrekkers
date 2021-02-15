using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class On_click : MonoBehaviour
{
    public GameObject[] birds;
    public int bird;
  
    public int response;
    private GameLogicBird Gamelogic;
    private float time;
    private Vector3[] positions;
    private int pos;


    // Start is called before the first frame update
    void Start()
    {
        Gamelogic = FindObjectOfType<GameLogicBird>();   
        positions = new Vector3[3];
        positions[0] = new Vector3(-4, 0, 0);
        positions[1] = new Vector3(1, 0, 0);
        positions[2] = new Vector3(1, 0, 0);
        pos = Random.Range(0, 2);
    }

    void Update()
    {
       time += Time.deltaTime;

       if (time > 1.5f) 
       pos = Random.Range(0, 3);
    }
    // Update is called once per frame
    void OnMouseDown()
    {
    foreach (GameObject bird in birds)
    {
        bird.transform.position = positions[pos];
    }
    // bird.transform.position = positions[pos];
    bird = Random.Range(0,2);
    birds[bird].SetActive(true);
    Debug.Log("clicked"); 
    // Gamelogic.trial_n(correct);
    time=0;
    }
}


//     private List<int> Shuffle_list(List<int> x)
//     {
//         for (int i = 0; i < x.Count; i++)
//         {
//             int temp_a = x[i];
//             int randomIndex = Random.Range(i, x.Count);
//             x[i] = x[randomIndex];
//            x[randomIndex] = temp_a;
//         }
//         return x;
//     }
// }
