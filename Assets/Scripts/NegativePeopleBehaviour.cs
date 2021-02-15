using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativePeopleBehaviour : MonoBehaviour
{
     public GameObject[] people;
    public GameObject[] expressions;

    private GameLogicRewardSocial gameLogic;

    public float interval = 1;

    private int i = 0; // number of people in total
    private int num = 0; // saves number of people active in a given trial
    private float time;


    void Start()
    {
        Random.InitState(2);
        gameLogic = FindObjectOfType<GameLogicRewardSocial>();

        foreach (GameObject person in people)
        {
            person.SetActive(false);
            i += 1;

        }

        foreach (GameObject expression in expressions)
        {
            expression.SetActive(false);
        }


    }



    void Update()
    {
        time = gameLogic.setTime();

        if (gameLogic.n && time > interval)
        { // so it runs only once

            num = Random.Range(0, i);
            gameLogic.Bad_Person(num);
            people[num].SetActive(true);
            //expressions[num].SetActive(true);

            gameLogic.pointer3(); // reverses i until mouseclick
        }

    }

    public void Interval(float value)
    {
        interval = value;
    }
}
