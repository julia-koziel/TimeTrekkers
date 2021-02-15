using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StardustTravelDestination : MonoBehaviour
{
    Vector3[] targetDestination = new [] { new Vector3(5,5,0f), new Vector3(5,5,0f), new Vector3(5,5,0f), new Vector3(5,5,0f), new Vector3(5, 5,0f), new Vector3(5, 5,0f), new Vector3(5, 5,0f)};
    public int currentColour;

    public GameObject star;
    private MovingStimulus s;
    public float time=0;
    public int velocity=5;

    private StardustGameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<StardustGameLogic>();
        s = FindObjectOfType<MovingStimulus>();
        transform.position = star.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        currentColour = gameLogic.currentvalue;  

        if(s.clicked)
        {
            moveStardust();
        }

        if (Vector3.Distance(transform.position, targetDestination[currentColour]) < 0.1f)
        {
            reset();
        }

    }

    void moveStardust()
    {
        transform.position = Vector3.MoveTowards(star.transform.position, targetDestination[currentColour], velocity * time);
    }

    public void reset()
    {
        transform.position = star.transform.position;
        time=0;   
        gameObject.SetActive(false);
    }

    
}
