using UnityEngine;
using UnityEngine.Assertions;

public class RL_Score_Manager : MonoBehaviour 
{
    public IntVariable score;
    public IntVariable nTrials;
    public GameObject[] coins;
    int lastActive = -1;

    void Awake() => Assert.IsTrue(coins.Length >= nTrials, "Not enough coins for number of trials");
    public void OnResponseWindowEnd()
    {
        score.Value = Mathf.Clamp(score, 0, coins.Length);
        if (score > lastActive + 1) coins[++lastActive].SetActive(true);
        else if (score < lastActive + 1) coins[lastActive--].SetActive(false);
    }

    public void Reset()
    {
        coins.ForEach(c => c.SetActive(false));
        lastActive = -1;
        score.Value = 0;
    }
}