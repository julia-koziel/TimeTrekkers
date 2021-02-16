using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiratePracticelogic : MonoBehaviour
{
    public float distance = 6f;
    public float velocity = 5;
    public float interval = 5f;
    public bool quit = false;
    public float deltatime = 0;
    public float time;
    public bool printout = false;
    public bool ship1 = false;
    public bool ship2 = false;
    public bool ship3 = false;
    public bool ship4 = false;

    public bool ship5 = false;
    public bool ship6 = false;
    public bool ship7 = false;
    public bool ship8 = false;
    public bool ship9 = false;

    public bool k = true;
    public bool lvl1 = true;
    public float duration = 300;
    public GameObject Instructions2;
    public InstructionsStateLogic logic;

    public GameObject end;
    public GameObject canvas;
    public GameObject ships;
    public GameObject MM;

    private int count = 0;
    private int wrong = 0;
    private int correct = 0;
    private int missedInARow = 0;
   
    public GameObject TrialsButton;
    public GameObject description;
    public GameObject PracticeButton;
    public GameObject pretest;
    

    // Start is called before the first frame update
    void Start()
    {
   
    time =+ Time.deltaTime;
    pretest.SetActive(false);
    logic = FindObjectOfType<InstructionsStateLogic>();
    }


    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        deltatime += Time.deltaTime;
        

        if(time > 30){    // 30 seconds of practice;
           
    
        gameObject.SetActive(false);
        logic.PostPractice();
        time = 0;
        }
    


    }
     public void PracticeAgain()
    {
        time=0;
        ships.SetActive(true);
        TrialsButton.SetActive(false);
        PracticeButton.SetActive(false);
    }

    public void countplus(){
        count += 1;
    }

    public void wrongclick(){
        wrong += 1;
    }

    public void correctship(){
        correct += 1;
    }

    public void ship1on()
    {
        ship1 = true;
    }

    public void ship2on()
    {
        ship2 = true;
    }

    public void ship3on()
    {
        ship3 = true;
    }

    public void ship4on()
    {
        ship4 = true;
    }

    public void ship5on()
    {
        ship5 = true;
    }
    public void ship6on()
    {
        ship6 = true;
    }
    public void ship7on()
    {
        ship7 = true;
    }
    public void ship8on()
    {
        ship8 = true;
    }
    public void ship9on()
    {
        ship9 = true;
    }
    public void ship1off()
    {
        ship1 = false;
    }

    public void ship2off()
    {
        ship2 = false;
    }

    public void ship3off()
    {
        ship3 = false;
    }

    public void ship4off()
    {
        ship4 = false;
    }
    public void ship5off()
    {
        ship5 = false;
    }
    public void ship6off()
    {
        ship6 = false;
    }
    public void ship7off()
    {
        ship7 = false;
    }
    public void ship8off()
    {
        ship8 = false;
    }
    public void ship9off()
    {
        ship9 = false;
    }
    public void Quit()
    {
        // csv.csvupdate(time, count, misses, wrong);
        // csv.output();
        MM.SetActive(true);
    }

    // public void Open_settings()
    // {
    //     settings.SetActive(true);
    //     canvas.SetActive(false);
    //     ships.SetActive(false);
    //     ships_lvl2.SetActive(false);
    //     k = false;
    // }

    // public void Close_settings()
    // {
    //     settings.SetActive(false);
    //     canvas.SetActive(true);

    //     if (lvl1) { 
    //         ships.SetActive(true);
    //         ships_lvl2.SetActive(false);
    //     }
    //     else { 
    //         ships_lvl2.SetActive(true);
    //         ships.SetActive(false);
    //          }


    //     k = true;
    // }

    public void Velocity(float value)
    {
        velocity = value;
    }

    public void Duration(float value)
    {
        duration = value;
    }


        


}

  