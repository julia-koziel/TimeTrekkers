using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EDP_Slow_click : MonoBehaviour
{
    public GameObject real;
    public GameObject fake;
    public AudioSource click;

    float time = 0;

    private void Start()
    {
        click.Play();
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            real.SetActive(true);
            fake.SetActive(true);
        }
    }
}
