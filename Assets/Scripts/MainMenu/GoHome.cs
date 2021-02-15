using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoHome : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SceneManager.LoadScene("MainMenu" +
                               "" +
                               "" +
                               "" +
                               "" +
                               "" +
                               "" +
                               "" +
                               "", LoadSceneMode.Single);
        gameObject.SetActive(false);
    }


}

