using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SWM_StimuliManager : MonoBehaviour
{
    public InputVariablesManager inputVariablesManager;
    public Stimulus seaweed; //the prefab of the stimulus is constantly instantiated throughout the task 
    public int nSeaweed; 
    public int targetloc=0;
    public Stimulus target;
    public IntVariable Length;
    public BoolVariable correct;
    public BoolVariable Sequence; //after a correct answer with a target being found, a new sequence of stimuli is presented. 
    public Transform[] locations;
    
    public int blockReset;
    public GameEvent trialEnd;

    public List<int> usedLocations;

    // Start is called before the first frame update
    public void OnStartTrial()
    {   
        inputVariablesManager.updateInputVariables();
        PlaceStimuli();
    }

    // Update is called once per frame
    public void OnResponseWindowEnd()
    {
        if(correct)
        {
            target.SetActive(true);

            this.In(0.5f).Call(() => {
                trialEnd.Raise();
                Sequence.Value=1;
                blockReset++;
                nSeaweed=0;
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

        if(blockReset==4)
        {
            Length.Value=Length.Value+2;
        }
    }

   
    public void Update()
    {
        while (nSeaweed<Length)
        {
            PlaceNew();
            seaweed.SetActive(true);
        }
    }

    public void PlaceNew()
    {
        var location = Random.Range(0,9);
       
        while(usedLocations.Contains(location))
        {
            location = Random.Range(0,9);
        }

         while(!usedLocations.Contains(location))
        {
            Instantiate(seaweed, locations[location].position, Quaternion.identity);
            usedLocations.Add(location);
            nSeaweed++;   
        }
        if ((nSeaweed==Length))
        {
            var targetLocation = usedLocations.Last();
            targetloc = targetLocation;
            target.transform.position= locations[targetloc].transform.position;
            var targetseaweed = Instantiate(seaweed, locations[targetloc].position, Quaternion.identity);
            targetseaweed.correct=true;
        }
    }

    public void PlaceOld()
    {
        var stimuli = usedLocations.Select(location => Instantiate(seaweed, locations[location].position, Quaternion.identity))
                               .ToArray();
        var targetLocation = usedLocations.Last();
            targetloc = targetLocation;
            target.transform.position= locations[targetloc].transform.position;
            var targetseaweed = Instantiate(seaweed, locations[targetloc].position, Quaternion.identity);
            targetseaweed.correct=true;
    }


    public void SetTarget()
    {
        var targetLocation = usedLocations.Last();
            targetloc = targetLocation;
            target.transform.position= locations[targetloc].transform.position;
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
    }

     public void Reset()
    {
        StopAllCoroutines();
    }

}
