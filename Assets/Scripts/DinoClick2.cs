using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DinoClick2 : MonoBehaviour
{
    public float time;
    public bool DinoClick;
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
           DinoGoLoad();
       }
    }
    public void DinoGoLoad()
    {
        SceneManager.LoadScene(10);
        gameObject.SetActive(false);
        
    }
}
