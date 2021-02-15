using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PALS : MonoBehaviour
{

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
            SceneManager.LoadScene("Passive_Avoidance_Learning_Social", LoadSceneMode.Single);
            gameObject.SetActive(false);
        }
    }


}
