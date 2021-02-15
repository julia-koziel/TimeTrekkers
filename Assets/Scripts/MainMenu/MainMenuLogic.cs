using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KittensLoad()
    {
        SceneManager.LoadScene(1);
        gameObject.SetActive(false);  
    }

    public void BirdLoad()
    {
       SceneManager.LoadScene(2);
       gameObject.SetActive(false);  
    }
}

