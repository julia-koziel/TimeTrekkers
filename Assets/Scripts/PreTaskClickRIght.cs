using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreTaskClickRIght : MonoBehaviour
{
    // Start is called before the first frame updatepublic GameObject instructions;

    PreTestLogic PretaskScript;

    private void Start()
    {
        PretaskScript = FindObjectOfType<PreTestLogic>();
    }

    private void OnMouseDown()
    {
        PretaskScript.Correct();
    }
}
