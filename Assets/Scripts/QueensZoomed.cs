using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QueensZoomed : MonoBehaviour
{
    public float time;
    public bool QueenClick;
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

       gameLogic.QueensClick=true; 
       if (time>2)
       {
           QueensLoad();
       }
    }
    public void QueensLoad()
    {
        SceneManager.LoadScene(4);
        gameObject.SetActive(false);
        
    }
}
