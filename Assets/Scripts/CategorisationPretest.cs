using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategorisationPretest : MonoBehaviour
{       
    
    public GameObject correct;
    public GameObject incorrect;
    public GameObject tick;
    public GameObject cross;
    public GameObject instructions;
    private InstructionsStateLogic introLogic;
    
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

        Random.InitState(3);
        positions = new Vector3[2];
        positions[0] = new Vector3(-3.5f, -1, 0);
        positions[1] = new Vector3(3.5f, -1, 0);
        p = Random.Range(0, 2);
        introLogic = FindObjectOfType<InstructionsStateLogic>();
        correct.SetActive(true);
        incorrect.SetActive(true);
        correct.transform.position = positions[p];

        incorrect.transform.position = positions[1-p];
    }

    // Update is called once per frame
    void Update()
    {
        if (trial>trialNumber)
        {
            correct.SetActive(false);
            incorrect.SetActive(false);
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
        StartCoroutine(clickdelay());   
    }

    public void Trial()

    {   
        p = Random.Range(0,2);
        correct.SetActive(true);
        incorrect.SetActive(true);

        correct.transform.position = positions[p];
        incorrect.transform.position = positions[1-p];
    }
    
     private IEnumerator clickdelay()
    {
        yield return new WaitForSeconds(2f); 

        correct.SetActive(false);
        incorrect.SetActive(false);
        trial+=1;

        yield return new WaitForSeconds(1f);
        Trial();
    }

    public void SetPractice()
    {
        if (score>4)
        {
            practicelvl =0;
            introLogic.PretestCorrect();
            gameObject.SetActive(false);
        }

        else
        {
            practicelvl =1;
            introLogic.PretestIncorrect();
            gameObject.SetActive(false);

        }
    }

    public void reset()
    {
        trial=0;
        correct.SetActive(true);
        incorrect.SetActive(true);
        score=0;
    }

    public int getPracticeTp()
    {
        return practicelvl;
    }

}

