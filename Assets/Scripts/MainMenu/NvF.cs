using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NvF : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        SceneManager.LoadScene("NoveltyFamiliarity", LoadSceneMode.Single);
        gameObject.SetActive(false);
    }


}
