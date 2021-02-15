using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenLogic : MonoBehaviour
{
    public GameObject transitionscreen;
    public GameObject button;
    public GameObject dataUpload;

    public bool wasClicked;
    public bool sceneLoaded;

    public bool DinoClicked;
    public bool PirateClicked;
    public bool TreasureClicked;
    public bool QueensClicked; 

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime; 

    if (wasClicked)
    {
        
       if (DinoClicked)
       {
        
           if (time>3)
           {
           DinoGoLoad();
           }
       }

       else if(PirateClicked)
    { 

        if (time>3)
        {
        PirateLoad();
        }
    }   

    else if (QueensClicked)
    {

        if (time>3)
        {
        QueensLoad();
        }
    }

    else if (TreasureClicked)
    {

        if (time>3)
        {
        TreasureLoad();
        }

    }
    }  
    }

    public void ButtonClicked()
    {
        wasClicked = true;
        transitionscreen.SetActive(true);
        sceneLoaded = true;
        time =0;

    }
     void PirateLoad()
    {  
            SceneManager.LoadScene(3);
            gameObject.SetActive(false);    
    }

    public void TreasureLoad()
    {
        SceneManager.LoadScene(5);
        gameObject.SetActive(false);
       
    }

    public void DinoGoLoad()
    {
        SceneManager.LoadScene(4);
        gameObject.SetActive(false);
        
    }

    public void QueensLoad()
    {
        SceneManager.LoadScene(6);
        gameObject.SetActive(false);
        
    }
}
