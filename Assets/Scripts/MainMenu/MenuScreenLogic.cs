using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

public class MenuScreenLogic : MonoBehaviour
{
    public GameObject transitionscreen;
    public GameObject button;
    public GameObject dataUpload;

    public GameObject level1button;
    public GameObject level2button;
    public GameObject level3button;

     public StringVariable participant_id;

    public bool wasClicked;
    public bool TaskSelected;
    public bool sceneLoaded;

    public BoolVariable level1;
    public BoolVariable level2;

    public bool DinoClicked;
    public bool PirateClicked;
    public bool TreasureClicked;
    public bool QueensClick; 
    public bool SHClicked;
    public bool AFClicked;
    public bool MCClicked;
    public bool speedOptimised;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        checkforBAO();
        int level1 = PlayerPrefs.GetInt("level1");
        int level2= PlayerPrefs.GetInt("level2");

      
       if (DinoClicked)
       {
            if (speedOptimised)
            {
                DinoGoLoad();
            }
            else 
            {
                BAOLoad();
            }
       }

       else if(PirateClicked)
    {   
        if (speedOptimised)
    {
        if (level1<1)
        {
            level1button.SetActive(true);
            ButtonClicked();

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

    else
    {
        BAOLoad();
    }
    }   

    else if (QueensClick)
    {
        ButtonClicked();
    }

    else if (TreasureClicked)
    {
        ButtonClicked();
    }

    else if (SHClicked)
    {

        SHLoad();
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

    public void BAOLoad()
        {
            SceneManager.LoadScene(1);
            gameObject.SetActive(false);
        }
    

    public void PirateLoad()
    {       
            gameObject.SetActive(false); 
            SceneManager.LoadScene(3);    
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
        transitionscreen.SetActive(true);
        SceneManager.LoadScene(6);
        gameObject.SetActive(false);
       
    }

    public void DinoGoLoad()
    {   
        transitionscreen.SetActive(true);
        SceneManager.LoadScene(10);
        gameObject.SetActive(false);
        
    }

  

    public void SHLoad()
    {   
        transitionscreen.SetActive(true);
        SceneManager.LoadScene(8);
        gameObject.SetActive(false);
        
    }

    public void AFLoad()
    {
        SceneManager.LoadScene(9);
        gameObject.SetActive(false);
        
    }


    public void checkforBAO()
    {
        var id = participant_id.Value;
        var files = new Queue<string>();
        speedOptimised = false;
        
        if (Directory.Exists(getFolderPath(id)))
        {
            var tsvs = Directory.GetFiles(getFolderPath(id), "*.tsv", SearchOption.AllDirectories);
            tsvs.Where(s => Regex.IsMatch(s, @".+task-BAO-Main.+\.tsv$")).ForEach(files.Enqueue);
            speedOptimised = files.Count > 0;
        }
    }

    string getFolderPath(string id)
    {
        string dataPath;
        
#if   UNITY_EDITOR
        dataPath = Application.dataPath + "/CSV";
#elif UNITY_ANDROID
        dataPath = Application.persistentDataPath;
#elif UNITY_IPHONE
        dataPath = Application.persistentDataPath;
#else
        dataPath = Application.dataPath;
#endif
        return $"{dataPath}/{id}/";
    }

}
