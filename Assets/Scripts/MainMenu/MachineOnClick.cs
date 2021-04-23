using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineOnClick : MonoBehaviour
{
    private Animator animator;
    public GameObject console;
    public GameObject machine;
    public GameObject introscreen;
    public float time;


    private MainMenuLogicNew gamelogic;

    public bool wasClicked = false;
    // Start is called before the first frame update
    void Start()
    {
    animator = GetComponent<Animator>();
    }
    void Update()
    {
    
    time += Time.deltaTime;
    if (time>1)
        animator.SetTrigger("Click"); 
        Debug.Log("machineclicked"); 
        wasClicked = true;
        LaunchConsole();


   }

   

    public void LaunchConsole()
    {
        if (time>4)
        {
        Debug.Log("console");
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
        }

    


    }

}
