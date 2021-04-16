using UnityEngine;

public class MC_End : MonoBehaviour 
{
    void OnEnable() 
    {
        var cam = Camera.main;
        cam.transform.position = new Vector3(-160, -230, -10);
        cam.orthographicSize = 300;
    }
}