using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM : MonoBehaviour
{
    public string MM_name = "MainMenu_w_versions";
    float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if(time > 0.5){
            SceneManager.LoadScene(MM_name, LoadSceneMode.Single);
            gameObject.SetActive(false);
        }
    }


}

