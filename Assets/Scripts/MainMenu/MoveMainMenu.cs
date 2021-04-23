using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMainMenu : MonoBehaviour
{
    public GameObject Canvas1;
    public GameObject Canvas2;

    private void OnMouseDown()
    {
        Canvas2.SetActive(true);
        Canvas1.SetActive(false);
    }
}
