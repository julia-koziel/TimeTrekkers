using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardBehaviour : MonoBehaviour
{
    public GameObject playerRock;
    public GameObject playerPaper;
    public GameObject playerScissors;
    public GameObject cpuRock;
    public GameObject cpuPaper;
    public GameObject cpuScissors;
    public GameObject playerWin;
    public GameObject playerDraw;
    public GameObject playerLose;
    public GameObject matchEnd;
    public GameObject sameOpponent;
    public GameObject newOpponent;

    private int playerCard;
    private int cpuCard;
    private int trialCounter;
    private float step;


    private Vector2 playerRockStart;
    private Vector2 playerPaperStart;
    private Vector2 playerScissorsStart;
    private Vector2 cpuRockStart;
    private Vector2 cpuPaperStart;
    private Vector2 cpuScissorsStart;
    private Vector2 playerCardSet;
    private Vector2 cpuCardSet;

    private int playerScoreTotal;
    public Text playerScore;
    private int cpuScoreTotal;
    public Text cpuScore;
    public int matchCounter;
    public int nextGame;


    void Start()
    {
        playerWin.GetComponent<Renderer>().enabled = false;
        playerDraw.GetComponent<Renderer>().enabled = false;
        playerLose.GetComponent<Renderer>().enabled = false;
        matchEnd.SetActive(false);
        playerScore.GetComponent<Text>();
        cpuScore.GetComponent<Text>();
        sameOpponent.SetActive(false);
        newOpponent.SetActive(false);

        moveZtoZero(playerRock);
        moveZtoZero(playerPaper);
        moveZtoZero(playerScissors);
        moveZtoZero(cpuRock);
        moveZtoZero(cpuPaper);
        moveZtoZero(cpuScissors);

        playerRockStart = playerRock.transform.position;
        playerPaperStart = playerPaper.transform.position;
        playerScissorsStart = playerScissors.transform.position;
        cpuRockStart = cpuRock.transform.position;
        cpuPaperStart = cpuPaper.transform.position;
        cpuScissorsStart = cpuScissors.transform.position;

        trialCounter = 0;
        playerScoreTotal = 0;
        cpuScoreTotal = 0;

        playerCardSet = new Vector2(-1, 0);
        cpuCardSet = new Vector2(1, 0);
        
        
    }

    private void moveZtoZero(GameObject gameObject)
    {
        Vector3 pos = gameObject.transform.position;
        pos.z = 0;
        gameObject.transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        playerScore.GetComponent<Text>().text = playerScoreTotal.ToString();
        cpuScore.GetComponent<Text>().text = cpuScoreTotal.ToString();
    }

    public void RockCardSelected()
    {
        StartCoroutine(MoveTo(playerCardSet, playerRock));
        playerCard = 1;
        playerPaper.GetComponent<Button>().interactable = false;
        playerScissors.GetComponent<Button>().interactable = false;
        playerRock.GetComponent<Button>().interactable = false;
        CPUChoice();
    }

    private IEnumerator MoveTo(Vector3 destination, GameObject gameObject)
    {
        float totalDistance = Vector3.Distance(gameObject.transform.position, destination);
        float step = totalDistance / 8;
        while (Vector3.Distance(gameObject.transform.position, destination) > 0.01f)
        {
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destination, step);
            yield return new WaitForSeconds(0.01f);
        }
    }


    public void PaperCardSelected()
    {
        StartCoroutine(MoveTo(playerCardSet, playerPaper));
        playerCard = 2;
        playerRock.GetComponent<Button>().interactable = false;
        playerScissors.GetComponent<Button>().interactable = false;
        playerPaper.GetComponent<Button>().interactable = false;
        CPUChoice();
    }

    public void ScissorsCardSelected()
    {
        StartCoroutine(MoveTo(playerCardSet, playerScissors));
        playerCard = 3;
        playerRock.GetComponent<Button>().interactable = false;
        playerPaper.GetComponent<Button>().interactable = false;
        playerScissors.GetComponent<Button>().interactable = false;
        CPUChoice();
    }

    void CPUChoice()
    {
        cpuCard = Random.Range(1, 4);

        if (cpuCard == 1)
        {
            StartCoroutine(MoveTo(cpuCardSet, cpuRock));
        }

        if (cpuCard == 2)
        {
            StartCoroutine(MoveTo(cpuCardSet, cpuPaper));
        }

        if (cpuCard == 3)
        {
            StartCoroutine(MoveTo(cpuCardSet, cpuScissors));
        }

        OutcomeCalculation();
    }

    void OutcomeCalculation()
    {
        if (playerCard == 1 && cpuCard == 1) //both play rock
        {
            playerDraw.GetComponent<Renderer>().enabled = true;
            StartCoroutine(Wait());
        }

        if (playerCard == 2 && cpuCard == 2) //both play paper
        {
            playerDraw.GetComponent<Renderer>().enabled = true;
            StartCoroutine(Wait());
        }

        if (playerCard == 3 && cpuCard == 3) //both play scissors
        {
            playerDraw.GetComponent<Renderer>().enabled = true;
            StartCoroutine(Wait());
        }

        if (playerCard == 1 && cpuCard == 2) //player rock and cpu paper
        {
            playerLose.GetComponent<Renderer>().enabled = true;
            cpuScoreTotal++;
            StartCoroutine(Wait());
        }

        if (playerCard == 1 && cpuCard == 3) //player rock and cpu scissors
        {
            playerWin.GetComponent<Renderer>().enabled = true;
            playerScoreTotal++;
            StartCoroutine(Wait());
        }

        if (playerCard == 2 && cpuCard == 1) //player paper and cpu rock
        {
            playerWin.GetComponent<Renderer>().enabled = true;
            playerScoreTotal++;
            StartCoroutine(Wait());
        }

        if (playerCard == 2 && cpuCard == 3) //player paper and cpu scissors
        {
            playerLose.GetComponent<Renderer>().enabled = true;
            cpuScoreTotal++;
            StartCoroutine(Wait());
        }

        if (playerCard == 3 && cpuCard == 1) //player scissors and cpu rock
        {
            playerLose.GetComponent<Renderer>().enabled = true;
            cpuScoreTotal++;
            StartCoroutine(Wait());
        }

        if (playerCard == 3 && cpuCard == 2) //player scissors and cpu paper
        {
            playerWin.GetComponent<Renderer>().enabled = true;
            playerScoreTotal++;
            StartCoroutine(Wait());
        }
    }

    void NewTrial()
    {
        
        playerRock.transform.position = playerRockStart;
        playerPaper.transform.position = playerPaperStart;
        playerScissors.transform.position = playerScissorsStart;
        cpuRock.transform.position = cpuRockStart;
        cpuPaper.transform.position = cpuPaperStart;
        cpuScissors.transform.position = cpuScissorsStart;

        if (trialCounter < 10)
        {
            playerRock.GetComponent<Button>().interactable = true;
            playerPaper.GetComponent<Button>().interactable = true;
            playerScissors.GetComponent<Button>().interactable = true;
            playerWin.GetComponent<Renderer>().enabled = false;
            playerLose.GetComponent<Renderer>().enabled = false;
            playerDraw.GetComponent<Renderer>().enabled = false;
        }

        if (trialCounter == 10)
        {
            matchEnd.SetActive(true);
            playerRock.GetComponent<Button>().interactable = false;
            playerPaper.GetComponent<Button>().interactable = false;
            playerScissors.GetComponent<Button>().interactable = false;
            playerWin.GetComponent<Renderer>().enabled = false;
            playerLose.GetComponent<Renderer>().enabled = false;
            playerDraw.GetComponent<Renderer>().enabled = false;
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        trialCounter++;
        Debug.Log(trialCounter);


        if (trialCounter == 10)
        {
            matchEnd.SetActive(true);
        }

        else
        {
            NewTrial();
        }
        
    }

    public void MatchEnd()
    {
        trialCounter = 0;
        playerScoreTotal = 0;
        cpuScoreTotal = 0;
        matchCounter++;
        NewTrial();

        matchEnd.SetActive(false);
        playerRock.transform.position = playerRockStart;
        playerPaper.transform.position = playerPaperStart;
        playerScissors.transform.position = playerScissorsStart;
        cpuRock.transform.position = cpuRockStart;
        cpuPaper.transform.position = cpuPaperStart;
        cpuScissors.transform.position = cpuScissorsStart;



        playerWin.GetComponent<Renderer>().enabled = false;
        playerDraw.GetComponent<Renderer>().enabled = false;
        playerLose.GetComponent<Renderer>().enabled = false;

        if (matchCounter >= 5)
        {
            sameOpponent.SetActive(true);
            newOpponent.SetActive(true);
            playerRock.GetComponent<Button>().interactable = false;
            playerPaper.GetComponent<Button>().interactable = false;
            playerScissors.GetComponent<Button>().interactable = false;
        }
    }

    public void SameOpponent()
    {
        trialCounter = 0;
        playerScoreTotal = 0;
        cpuScoreTotal = 0;
        matchCounter++;
        NewTrial();
        sameOpponent.SetActive(false);
        newOpponent.SetActive(false);
    }

    public void NewOpponent()
    {
        nextGame = Random.Range(1, 4);

        if (nextGame == 1)
        {
            SceneManager.LoadScene(2);
        }

        if (nextGame == 2)
        {
            SceneManager.LoadScene(3);
        }

        if (nextGame == 3)
        {
            SceneManager.LoadScene(4);
        }
    }

}
