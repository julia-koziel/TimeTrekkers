using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// todo: ensure that there is a correct number of stimuli in each new trial
public class SWM_StimuliManager : MonoBehaviour
{
    public InputVariablesManager inputVariablesManager;
    public Stimulus seaweed; //the prefab of the stimulus is constantly instantiated throughout the task 
    public int nSeaweed; 
    public int targetloc=0;
    public Stimulus[] target;
    public IntVariable Length;
    public BoolVariable correct;
    public BoolVariable Sequence; //after a correct answer with a target being found, a new sequence of stimuli is presented. 
    public Transform[] locations;
    List<List<int>> myList = new List<List<int>>();
    List<int> loc = new List<int>() {2,3,6,7};

    public int listindex;
    public int blockReset;
    public GameEvent trialEnd;

    public List<int> usedLocations;

    
    public void OnEnable()
    {
        myList.Add(new List<int> {2,3,6,7});
        myList.Add(new List<int> {1,4,6,8});
        myList.Add(new List<int> {5,4,3,1});
        myList.Add(new List<int> {8,2,4,5});
        myList.Add(new List<int> {2,7,6,1,0,4});
        myList.Add(new List<int> {8,7,2,3,1,6});
        myList.Add(new List<int> {3,5,7,8,1,0});
        myList.Add(new List<int> {1,2,3,5,6,8});
        myList.Add(new List<int> {0,1,2,3,4,5,7,6});
        myList.Add(new List<int> {0,2,3,4,5,7,8,1});
        myList.Add(new List<int> {0,1,2,4,5,6,8,3});

    }

    public void OnStartTrial()
    {   
        inputVariablesManager.updateInputVariables();
        seaweed.SetActive(true);
        PlaceStimuli();
    }

    public void OnResponseWindowEnd()
    {
        if(correct)
        {
            target[listindex].SetActive(true);
            nSeaweed=0;

            this.In(0.5f).Call(() => {
                trialEnd.Raise();
                Sequence.Value=1;
                blockReset++;
                listindex++;
                
                usedLocations.Clear();
                });
        }

        else
        {
           this.In(0.5f).Call(() => {
               trialEnd.Raise();
                });
            Sequence.Value=0;
        }

    }

   
    public void Update()
    {
        while (nSeaweed<Length-1)
        {
            PlaceNew();
        }

        loc=myList[listindex];
        Debug.Log(nSeaweed);
    }

    public void PlaceNew()
    {
        // var stimuli = loc.Select(location => Instantiate(seaweed, locations[location].position, Quaternion.identity))
        //                        .ToArray();
         for( int i = 0; i < myList[listindex].Count-1; i++)
        {
            Instantiate(seaweed, locations[loc[i]].position, Quaternion.identity);
            usedLocations.Add(loc[i]);
            nSeaweed++;  
        }
        var targetLocation = myList[listindex].Last();
        targetloc = targetLocation;
        target[listindex].transform.position= locations[targetloc].transform.position;
        var targetseaweed = Instantiate(seaweed, locations[targetloc].position, Quaternion.identity);
        targetseaweed.correct=true;
        usedLocations.Add(targetloc);

    }

    public void PlaceOld()
    {
        var stimuli = usedLocations.Select(location => Instantiate(seaweed, locations[location].position, Quaternion.identity))
                               .ToArray();

        stimuli.ForEach((i, stimulus) => {
            IntVariable id = (IntVariable)ScriptableObject.CreateInstance(typeof(IntVariable));
            id.Value = usedLocations[i];
            stimulus.id = id;
        });

        var targetLocation = usedLocations.Last();
            targetloc = targetLocation;
            target[listindex].transform.position= locations[targetloc].transform.position;
            var targetseaweed = Instantiate(seaweed, locations[targetloc].position, Quaternion.identity);
            targetseaweed.correct=true;
    }


    public void PlaceStimuli()
    {   
        seaweed.SetActive(true);

        if (!Sequence)
        {
            PlaceOld();
        }
        else
        {
            nSeaweed=0;
        }
    }

     public void Reset()
    {
        StopAllCoroutines();
    }


}
