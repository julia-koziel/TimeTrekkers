using UnityEngine;
using System.Collections;

public class MC_BlockBreak : MonoBehaviour 
{
    public UILogic ui;
    public TranslatableString text;
    public GameObject ss1;
    public GameObject ss2;
    public GameObject ss3;
    GameObject container;
    public int numSpaceships;
    public float delay;
    void OnEnable() 
    {
        ui.continueText = text.TranslatedString;
        ui.waitForParent = true;
        ui.showUI();

        StartCoroutine(makeSpaceships());
    }

    IEnumerator makeSpaceships()
    {
        container = new GameObject("container");
        container.transform.parent = transform;
        var nCreated = 0;
        while (nCreated < numSpaceships)
        {
            Instantiate(ss1, Vector3.down * -6, Quaternion.identity, container.transform);
            Instantiate(ss2, Vector3.down * -6, Quaternion.identity, container.transform);
            Instantiate(ss3, Vector3.down * -6, Quaternion.identity, container.transform);
            nCreated += 1;
            yield return new WaitForSeconds(delay);
        }
    }

    public void Reset()
    {
        Destroy(container);
        StopAllCoroutines();
    }
}