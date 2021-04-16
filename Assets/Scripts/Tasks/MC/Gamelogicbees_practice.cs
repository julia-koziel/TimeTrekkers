using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamelogicbees_practice : MonoBehaviour
{
    // clicks
    // ps rotation
    // trials
    // Connected components
    public GameObject partsys; // Particle System aka bees
    public GameObject ps_coh; // Coherent Signal. Block 2, 3
    public GameObject ps_inco; // Incoherent signal
    public GameObject practice; // button to practice again
    public GameObject practice_task; // the wholve canvas of the practice trials
    public GameObject start;
    public GameObject gamelogic_main;
    public GameObject planet1;
    public GameObject planet2;

    // Task Parameters
    public int n_trials = 5;
    public int blocks = 3;
    public float delayPeriod = 1;
    public float time_before_click = 1;
    bool gate = true;

    // Task flags
    int trial = 0;
    public int dir = -1;
    int prev_dir = 1;
    float time = 0;
    bool flag = true;

    void Start()
    {
        Random.InitState(42);
        partsys.SetActive(false);
        ps_inco.SetActive(false);
        dir = Random.Range(0, 2);
    }

    void Update()
    {
        if (gate)
        {
            time += Time.deltaTime;

            // Only show bees after delay period
            if (time > delayPeriod && flag)
            {

                time = 0;
                partsys.SetActive(true);
                if (dir == 0) // right
                { partsys.transform.position = new Vector3(5f, 0, -9); }

                if (dir == 1) // left
                { partsys.transform.position = new Vector3(-5f, 0, -9); }

                //change rotation if new direction
                if (prev_dir != dir)
                {
                    partsys.transform.Rotate(0, 0, 180, Space.World);
                    ps_coh.transform.Rotate(0, 0, 180, Space.World);
                    ps_inco.transform.Rotate(0, 0, 180, Space.World);
                }

                flag = false;

            }
        }


    }
    public void Trial_n(bool correct)
    {

        if (!flag && time > time_before_click) // click only works after delay period
        {
             // Reinitialization
            trial += 1;
            time = 0;
            prev_dir = dir;
            dir = Random.Range(0, 2);
            flag = true;
            // change blocks

            partsys.SetActive(false);
            ps_inco.SetActive(false);
            ps_coh.SetActive(false);

            if (trial == 5)
            {
                trial = 0;
                practice.SetActive(true);
                start.SetActive(true);
                gate = false;
                partsys.transform.Rotate(0, 0, 180, Space.World);
                ps_coh.transform.Rotate(0, 0, 180, Space.World);
                ps_inco.transform.Rotate(0, 0, 180, Space.World);
            }
        }
    }

    public void Main_task()
    {
        planet1.SetActive(false);
        planet2.SetActive(false);
        gamelogic_main.SetActive(true);
        start.SetActive(false);
        practice.SetActive(false);
    }

    public void Practice()
    {
        gate = true;
        start.SetActive(false);
        practice.SetActive(false);
    }

}
