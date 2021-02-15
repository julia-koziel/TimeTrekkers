using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorisationPretestSA : MonoBehaviour
{
    public GameObject correct;
    public GameObject[] incorrect;
    public GameObject tick;
    public GameObject cross;
    public GameObject intro;
    public GameObject practiceButton;
    private InstructionsStateLogic introLogic;
    
    public int ship;
    public int score;
    public float time;
    public int practicelvl;
    public int trial;
    public int trialNumber;
    private int p;
    private Vector3[] positions;
        
    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector3[2];
        positions[0] = new Vector3(-3, -1, 0);
        positions[1] = new Vector3(3, -1, 0);
        p = Random.Range(0, 2);

        correct.SetActive(true);
        incorrect[ship].SetActive(true);
        correct.transform.position = positions[p];
        incorrect[ship].transform.position = positions[1-p];
        introLogic = FindObjectOfType<InstructionsStateLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (trial>trialNumber)
        {
            correct.SetActive(false);
            incorrect[ship].SetActive(false);
            // in intro logic set playerprefs;
            SetPractice();
            reset();
        }

    }

        public void correctclicked()
    {
        score += 1;
        StartCoroutine(clickdelay());
    }

        public void incorrectclicked()
    {
        Debug.Log("newtrial");
        StartCoroutine(clickdelay());
        
    }

    public void Trial()

    {   
        trial+=1;
        p = Random.Range(0,2);
        ship = Random.Range(0,5);
        correct.SetActive(true);
        incorrect[ship].SetActive(true);

        correct.transform.position = positions[p];
        incorrect[ship].transform.position = positions[1-p];
    }
    
     private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(1.5f); 

        correct.SetActive(false);
        incorrect[ship].SetActive(false);

        yield return new WaitForSeconds(0.5f);
        Trial();
    }

    public void SetPractice()
    {
        if (score>4)
        {
            practicelvl =0;
            introLogic.PretestCorrect();
        
        }

        else
        {
            practicelvl =1;
            introLogic.PretestIncorrect();
        }
    }

    public int getPracticeTp()
    {
        return practicelvl;
    }


    public void reset()
    {
        trial=0;
        score=0;
        correct.SetActive(true);
        incorrect[ship].SetActive(true);
        gameObject.SetActive(false);
    }

}