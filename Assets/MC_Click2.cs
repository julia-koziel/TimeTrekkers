using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MC_Click2 : MonoBehaviour
{
    public bool MCClicked;
    public float time;
    private MenuScreenLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
        gameLogic = FindObjectOfType<MenuScreenLogic>();

    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime; 
       gameLogic.MCClicked=true; 
       if (time>2)
       {
           MCLoad();
       }
       gameLogic.AFClicked=true; 
       gameLogic.TaskSelected=true;
    }

    public void MCLoad()
    {
        SceneManager.LoadScene(13);
        gameObject.SetActive(false);
        
    }
}