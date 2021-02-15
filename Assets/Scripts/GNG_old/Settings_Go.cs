
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings_Go : MonoBehaviour
{
    public InputField input1;
    public InputField input2;
    public InputField input3;

    private GameLogicGo gameLogic;

    void Awake()
    {
        input1.onEndEdit.AddListener(AcceptStringInput1);
        input2.onEndEdit.AddListener(AcceptStringInput2);
        input3.onEndEdit.AddListener(AcceptStringInput3);
    }

    private void Start()
    {
        gameLogic = FindObjectOfType<GameLogicGo>();
    }



    public void AcceptStringInput1(string userInput) // should look for exceptions
    {
        int trialnumber = int.Parse(userInput);
        gameLogic.Trialnum(trialnumber);
        //gamelogic function to update()
    }

    public void AcceptStringInput2(string userInput)
    {
        float velocity = float.Parse(userInput);
        //gamelogic function to update()
    }

    public void AcceptStringInput3(string userInput)
    {
        float interval = float.Parse(userInput);
        gameLogic.Interval(interval);
        //gamelogic function to update()
    }




}