using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BOB_ScoreManager : MonoBehaviour, IGameEventListener<Stimulus>
{
    public BoolVariable isParticipantsGo;
    public StarBehaviour starPrefab;
    public GameEvent trialEnd;
    public DataGameEvent dataSubmit;
    public Optimiser optimiser;

    public GameEvent StageEnd;
    public IntVariable trial;
    public IntVariable nTrials;

    public FloatVariable speed;
    public StimulusGameEvent responseWindowStart;
    public IntVariable score;
    public BoolVariable correct;

    public GameObject[] barfill;
    Stimulus bubble;
    List<GameObject> stars = new List<GameObject>();
    List<GameObject> bigStars = new List<GameObject>();
    public Color[] rainbowColours;
    int block = 3;
    void OnEnable() => RegisterListener();
    void OnDisable() => UnregisterListener();
    public void RegisterListener() => responseWindowStart.RegisterListener(this);
    public void UnregisterListener() => responseWindowStart.UnregisterListener(this);
    public void OnEventRaised(Stimulus stimulus) => bubble = stimulus;

    public void OnResponseWindowEnd()
    {
        if (isParticipantsGo && correct)
        {
            if  (bigStars.Count < 6)
        {
            starPrefab.destination = getStarPosition(block);
            starPrefab.colour = rainbowColours[bigStars.Count];
            
            var star = Instantiate(starPrefab.gameObject, bubble.transform.position, Quaternion.identity, this.transform);
            stars.Add(star);
        }
            else
            {
                StageEnd.Raise();
            }
        }

        else if (!isParticipantsGo) this.In(1).Call(trialEnd.Raise);
    }

    public void OnStarArrived()
    {
        score++;
        
        if (score % block == 0) StartCoroutine(CombineStars());

        else if ((trial % 3) - score >= 0) StartCoroutine(CombineStars());

        else EndTrial();
    }

    // TODO hard-code to block
    Vector2 getStarPosition(int bar) => new Vector2((bar)+3, 3);

    IEnumerator CombineStars()
    {
        barfill[bigStars.Count].SetActive(true);

        if (trial==nTrials)
        {
            foreach (GameObject bar in barfill)
            {
                bar.SetActive(true);
            }
        }

        float speed = 6;
        float frameRate = 0.02f;
        float baseStep = speed * frameRate;
        float step;

        Vector3 dest = getStarPosition(2);

        var bigStar = stars.Pop(0);
        stars.ForEach(star => Destroy(star));
        stars.Clear();

        bigStars.Add(bigStar);
        var bigStarPositions = new Vector3[bigStars.Count];
        bigStarPositions.ForEach((i, _) => new Vector3((0.75f-bigStars.Count*0.75f + i*1.5f), 4, 0));

        var scaleChange = new Vector3(0.02f, 0.02f, 0);
        var bigStarDest = bigStarPositions[bigStars.Count - 1];
        step = Vector3.Distance(bigStarDest, bigStar.transform.position) / 20f;

        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < bigStars.Count - 1; j++)
            {
                bigStars[j].transform.position += (Vector3.left * 0.75f) / 20f;
            }
            bigStar.transform.position = Vector3.MoveTowards(bigStar.transform.position, bigStarDest, step);
            bigStar.transform.localScale += scaleChange;
            yield return 0;
        }
        
        EndTrial();
    }

    void EndTrial()
    {
          if (trial == nTrials - 1)
            {
                var data = new List<string[]>();
                data.Add(new string[] {"participant_id", "optimised_speed", "adjusted_speed"});
                
                var id = dataSubmit.participantId.Value;
                var raw = optimiser.Max;
                var adjusted = raw * 0.8f;
                speed.SaveValue(adjusted);
                
                data.Add(new string[] {id, $"{raw}", $"{adjusted}"});
                dataSubmit.Raise(data, "Optimisation");
                this.In(1).Call(trialEnd.Raise);
            }
            else trialEnd.Raise();
    }

    public void Reset() 
    {
        for (int i = transform.childCount-1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        stars.Clear();
        bigStars.Clear();
        
        score.Value = 0;
    }

}