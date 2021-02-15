using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PAL : MonoBehaviour
{

    // Use this for initialization
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
            SceneManager.LoadScene("Passive_Avoidance_Learning", LoadSceneMode.Single);
            gameObject.SetActive(false);
        }
    }


}
