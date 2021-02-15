using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject howToPlay;
    public GameObject startGame;
    public GameObject triangle;
    public GameObject instruction1;
    public GameObject instruction2;
    public GameObject rocklabel;
    public GameObject paperlabel;
    public GameObject scissorslabel;
    
    void Start()
    {
        instruction1.GetComponent<Renderer>().enabled = false;
        instruction2.GetComponent<Renderer>().enabled = false;
        rocklabel.GetComponent<Renderer>().enabled = false;
        paperlabel.GetComponent<Renderer>().enabled = false;
        scissorslabel.GetComponent<Renderer>().enabled = false;
    }

    
    void Update()
    {
        
    }

    public void HowToPlay()
    {
        instruction1.GetComponent<Renderer>().enabled = true;
        instruction2.GetComponent<Renderer>().enabled = true;
        rocklabel.GetComponent<Renderer>().enabled = true;
        paperlabel.GetComponent<Renderer>().enabled = true;
        scissorslabel.GetComponent<Renderer>().enabled = true;
        triangle.transform.position = new Vector2(2, -1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
