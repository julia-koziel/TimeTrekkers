using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SH_Click2 : MonoBehaviour
{
    public bool SHClicked;
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
       gameLogic.SHClicked=true; 
       if (time>2)
       {
           SHLoad();
       }
       gameLogic.SHClicked=true; 
       gameLogic.TaskSelected=true;
    }

    public void SHLoad()
    {
        SceneManager.LoadScene(8);
        gameObject.SetActive(false);
        
    }
}