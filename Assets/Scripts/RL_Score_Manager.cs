using UnityEngine;
using UnityEngine.Assertions;

public class RL_Score_Manager : MonoBehaviour 
{
    public IntVariable score;
    public IntVariable nTrials;
    public GameObject[] coins;
    public TranslatableAudioClip coinsound;
    public TranslatableAudioClip lossSound;
    public int lastActive = -1;

    AudioTranslator audioTranslator;

    void Awake(){
        audioTranslator = GetComponent<AudioTranslator>();
        Assert.IsTrue(coins.Length >= nTrials, "Not enough coins for number of trials");
    }

    public void OnResponseWindowEnd()
    {
        score.Value = Mathf.Clamp(score, 0, coins.Length);
        if (score > lastActive + 1) {
            coins[++lastActive].SetActive(true);
            audioTranslator.Play(coinsound);
        }

        else if (score < lastActive + 1) 
        {coins[lastActive--].SetActive(false);
        audioTranslator.Play(lossSound);
        }
    }

    public void Reset()
    {
        coins.ForEach(c => c.SetActive(false));
        lastActive = -1;
        score.Value = 0;
    }
}