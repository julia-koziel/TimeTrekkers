using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveId : MonoBehaviour
{

    ID_register id;
    // Use this for initialization
    void Start()
    {
        id = FindObjectOfType<ID_register>();
        DontDestroyOnLoad(id);

    }
}
	

