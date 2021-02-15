using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStages : MonoBehaviour
{
    public GameObject StageParent;
    public GameObject StageChild;

    public bool parentPractice;
void Awake()
{
    string prefsKey = PrefsKeys.Keys.PracticeType.ToString();
    parentPractice = PlayerPrefs.GetInt(prefsKey, 0) == 0;
}

void Update()
{
    if (parentPractice)
    {
        StageParent.SetActive(true);
        StageChild.SetActive(false);
    }

    else
    {
        StageChild.SetActive(true);
        StageParent.SetActive(false);

    }

}

}
