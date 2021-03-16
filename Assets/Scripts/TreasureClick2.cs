using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TreasureClick2 : MonoBehaviour
{   
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
       gameLogic.DinoClicked=true; 
       if (time>2)
       {
           TreasureLoad();
       }
       gameLogic.TreasureClicked=true; 
       gameLogic.TaskSelected=true;
    }

     public void TreasureLoad()
    {
        SceneManager.LoadScene(4);
        gameObject.SetActive(false);
       
    }
}
