using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP : MonoBehaviour
{

    float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if(time > 0.5){
            SceneManager.LoadScene("Homepage", LoadSceneMode.Single);
            gameObject.SetActive(false);
        }
    }


}

