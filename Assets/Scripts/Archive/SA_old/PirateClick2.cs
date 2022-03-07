using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PirateClick2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    private MenuScreenLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        
        gameLogic =FindObjectOfType<MenuScreenLogic>();

    }

    // Update is called once per frame
    void Update()
    {
       time += Time.deltaTime;

       gameLogic.ButtonClicked();
       if (time>2)
       {
           PirateLoad();
       }
    }
    public void PirateLoad()
    {
        SceneManager.LoadScene(3);
        gameObject.SetActive(false);
        
    }
}
