using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QueensClick2 : MonoBehaviour
{
    public float time;
    // Start is called before the first frame update
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
       gameLogic.DinoClicked=true; 
       if (time>2)
       {
           QueensLoad();
       }
       gameLogic.TreasureClicked=true; 
       gameLogic.TaskSelected=true;
    }

    public void QueensLoad()
    {
        SceneManager.LoadScene(5);
        gameObject.SetActive(false);
        
    }
}
