using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AF_Click2 : MonoBehaviour
{
    public bool AFClicked;
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
       gameLogic.AFClicked=true; 
       if (time>2)
       {
           AFLoad();
       }
       gameLogic.AFClicked=true; 
       gameLogic.TaskSelected=true;
    }

    public void AFLoad()
    {
        SceneManager.LoadScene(9);
        gameObject.SetActive(false);
        
    }
}