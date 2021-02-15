using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject[] chests;
    public GameObject[] opens;

    private GameLogicRewardLearning gameLogic;

    public float interval = 1;

    private int i = 0; // number of chests in total
    private int num = 0; // saves number of chests active in a given trial
    private float time;


    void Start()
    {
        Random.InitState(4);
        gameLogic = FindObjectOfType<GameLogicRewardLearning>();

        foreach (GameObject chest in chests
)
        {
            chest.SetActive(false);
            i += 1;

        }

        foreach (GameObject open in opens)
        {
            open.SetActive(false);
        }


    }

    void Update()
    {
        time = gameLogic.setTime();

        if (gameLogic.i && time > interval)
        { // so it runs only once
            num = Random.Range(0, i);
            chests[num].SetActive(true);
            gameLogic.Good_chest(num);
            gameLogic.logStartTime();
            Debug.Log("time: " + gameLogic.time);
            //opens[num].SetActive(true);
            gameLogic.pointer(); // reverses i until mouseclick
        }

    }

    public void Interval(float value)
    {
        interval = value;
    }
}
