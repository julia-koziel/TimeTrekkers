using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class SH_StimuliManager : MonoBehaviour
{
    public GameEvent trialEnd;
    public InputVariablesManager inputVariablesManager;
    public CategoricalInputVariable trialType;
    public IntVariable CurrentChild;
    public IntVariable Score;
    public IntVariable ChosenChild;
    public IntVariable block;
    public IntVariable trial;
    public IntVariable Rock;
    public IntVariable Paper;
    public IntVariable Scissors;
    [Space(10)]
    
    public Stimulus[] children;
    public Stimulus[] cards;
    public Stimulus Theo;
    public Stimulus[] New;
    public GameObject[] outcomes;
    public GameObject machine;      

    [Space(10)]
    public BoolVariable correct;
    public BoolVariable participantsGo;
    public int game;
    public int outcome;
    public int blockTrial;
    public float clicked;
    public int score;
    public int oppscore;

    public Transform leftPos;
    public Transform centrePos;
    public Transform rightPos;
    public Transform topleft;
    public Transform topmid;
    public Transform topright;
    public Transform bottomleft;
    public Transform bottommid;
    public Transform bottomright;

    Vector3 right = new Vector3(-4, 3, 0);
    Vector3 centre = new Vector3(0, 0, 0);

    // Start is called before the first frame update

    public void OnEnable()
    {
        block.Value=0;
    }

    public void OnStartTrial()
    {
        inputVariablesManager.updateInputVariables();
        Score.Value = score;
        
        if(trial>5 && blockTrial>4)
        {
            ChooseOpponent();
        }

        else
        {
        children[game].SetActive(true);
        
        foreach (Stimulus cards in cards)
        {
            cards.SetActive(true); 
        }

        foreach (GameObject outcome in outcomes)
        {
            outcome.SetActive(false);
        }

        }

        cards[0].transform.position = topleft.transform.position;
        cards[1].transform.position = topmid.transform.position;
        cards[2].transform.position = topright.transform.position;
        cards[3].transform.position = bottomleft.transform.position;
        cards[4].transform.position = bottommid.transform.position;
        cards[5].transform.position = bottomright.transform.position;
        
    }


    public void OnResponseWindowEnd()
    {
        
        this.BuildMovement()
                .First(() => Move(cards[trialType]))
                .Then(1, false, false, Move(cards[trialType].transform, from: right, to: centre, duration: 1.5f))
                .Start();     

        this.In(1).Call(() => 
            {
                Move(cards[trialType].transform, from: centre, to: right, duration: 0.5f);
                this.In(2f).Call(() => 
            {
                trialEnd.Raise();
                blockTrial++;
            });
        }); 
    
    }
    

    public void Reset()
    {
        inputVariablesManager.Reset();
    }

    public void SChoice()
    {
        if (trialType==0)
        {
            outcomes[1].SetActive(true);
            Win(children[game]);
            oppscore++;

        }

        else if (trialType==1)
        {
            outcomes[2].SetActive(true);
            Lose(children[game]);
            score++;
        }

        else if (trialType==2)
        {
            outcomes[0].SetActive(true);
        }
    }

    public void RChoice()
    {
        if (trialType==0)
        {
            outcomes[0].SetActive(true);
        }

        else if (trialType==1)
        {
            outcomes[1].SetActive(true);
            Win(children[game]);
            oppscore++;
        }

        else 
        {
            outcomes[2].SetActive(true);
            Lose(children[game]);
            score++;
        }

    }

     public void PChoice()
    {
        if (trialType==0)
        {
            outcomes[2].SetActive(true);
            Lose(children[game]);
            score++;
        }

        else if (trialType==1)
        {
            outcomes[0].SetActive(true);
        }

        else
        {
            outcomes[1].SetActive(true);
            Win(children[game]);
            oppscore++;
        }

    }
    
    public void ChooseOpponent()
    {
         this.In(1).Call(() => 
            {
                CurrentChild++;
                Debug.Log(CurrentChild);
                block++;
                machine.SetActive(true);
                
                this.In(0.5f).Call(() => 
            { 
                Theo.SetActive(true);
                New[block].SetActive(true);
                
                score=0;
                oppscore=0;
            });
        }); 
    
    }

    public void NewGame()
    {
        game++;
        blockTrial=0;
        Theo.SetActive(false);
        New[CurrentChild].SetActive(false);

        foreach (Stimulus cards in cards)
        {
            cards.SetActive(false); 
        }
        ChosenChild.Value=1;
    }

    public void OldGame()
    {
        blockTrial=0;
        ChosenChild.Value=0;
    }


    public void Win(Stimulus stimulus) => stimulus.GetComponent<Animator>().SetTrigger("win");
    public void Lose(Stimulus stimulus) => stimulus.GetComponent<Animator>().SetTrigger("lose");

    public void GetId(Stimulus stimulus)
    {
       var stimulusclicked = stimulus.loggedId.Value;
       stimulusclicked = clicked;
       Debug.Log(clicked);
       
    }

    public void Move(Stimulus stimulus)
    {
        stimulus.transform.position = centrePos.position;
    }


    Action<float> Move(Transform transform, Vector3 from, Vector3 to, float duration)
    {
        var dist = to - from;
        var step = dist / duration;
        
        Action<float> movement = deltaTime =>
        {
            transform.position = from + step * deltaTime;
        };

        return movement;
    }
}

