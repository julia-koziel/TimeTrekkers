using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenLogic : MonoBehaviour
{
    public GameObject transitionscreen;
    public GameObject button;
    public GameObject dataUpload;

    public GameObject level1button;
    public GameObject level2button;
    public GameObject level3button;

    public bool wasClicked;
    public bool TaskSelected;
    public bool sceneLoaded;

    public BoolVariable level1;
    public BoolVariable level2;

    public bool DinoClicked;
    public bool PirateClicked;
    public bool TreasureClicked;
    public bool QueensClicked; 
    public bool SHClicked;
    public bool AFClicked;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        int level1 = PlayerPrefs.GetInt("level1");
        int level2= PlayerPrefs.GetInt("level2");
        Debug.Log(level1);

      
       if (DinoClicked)
       {
           if (time>3)
           {
           DinoGoLoad();
           }
       }

       else if(PirateClicked)
    {    
        if (level1<1)
        {
            level1button.SetActive(true);
        }

        else if(level1>0)
        {
            level1button.SetActive(true);
            level2button.SetActive(true);
        }

        else if (level2>0)
        {
            level1button.SetActive(true);
            level2button.SetActive(true);
            level3button.SetActive(true);
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

    else if (SHClicked)
    {

        if (time>3)
        {
        SHLoad();
        }

    }

    else if (AFClicked)
    {

        if (time>3)
        {
        AFLoad();
        }
    }  

    }

    public void ButtonClicked()
    {
        if (TaskSelected)
        {
        wasClicked=true;
        transitionscreen.SetActive(true);
        sceneLoaded = true;
        time =0;
        }

    }
    public void PirateLoad()
    {  
            SceneManager.LoadScene(3);
            gameObject.SetActive(false);    
    }
    public void PirateLevel2()
    {  
            SceneManager.LoadScene(12);
            gameObject.SetActive(false);    
    }
    public void PirateLevel3()
    {  
            SceneManager.LoadScene(13);
            gameObject.SetActive(false);    
    }

    public void TreasureLoad()
    {
        SceneManager.LoadScene(4);
        gameObject.SetActive(false);
       
    }

    public void DinoGoLoad()
    {
        SceneManager.LoadScene(10);
        gameObject.SetActive(false);
        
    }

    public void QueensLoad()
    {
        SceneManager.LoadScene(5);
        gameObject.SetActive(false);
        
    }

    public void SHLoad()
    {
        SceneManager.LoadScene(8);
        gameObject.SetActive(false);
        
    }

    public void AFLoad()
    {
        SceneManager.LoadScene(9);
        gameObject.SetActive(false);
        
    }
}
