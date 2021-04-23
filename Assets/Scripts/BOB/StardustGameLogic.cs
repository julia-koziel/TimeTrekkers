using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StardustGameLogic : MonoBehaviour
{
    public GameObject[] stardust;
    public GameObject[] barfill;

    private MovingStimulus star;
    private StardustTravelDestination reward;

    private int trialNumber = 30;
    public int currentvalue=0;
    public int rewardvalue=5;
    public int rewardclick=0;

    // Start is called before the first frame update
    void Start()
    {
        star = FindObjectOfType<MovingStimulus>();
        reward = FindObjectOfType<StardustTravelDestination>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void click()
    {
        rewardclick++;
        trialNumber++;
        stardust[currentvalue].SetActive(true);
        determinereward();
    }

    public void determinereward()
    {
        if (rewardclick==rewardvalue)
        {
            barfill[currentvalue].SetActive(true);
            rewardclick =0;
            currentvalue++;
        }
    }

    public void reset()
    {
        foreach (GameObject stard in stardust)
        {
            stard.SetActive(false);
        }
    }

}
