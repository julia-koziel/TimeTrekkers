using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreenLogic : MonoBehaviour
{
    public GameEvent StageEnd;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTrials()
    {
        StageEnd.Raise();
        Debug.Log("clicked");
    }

    public void StartDemo()
    {
        StageEnd.Raise();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(2);
    }
    
    }
