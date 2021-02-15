using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionRegister : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string version = GetComponent<Text>().text;
        string key = PrefsKeys.Keys.VersionNumber.ToString();
        PlayerPrefs.SetString(key, version);
    }
}
