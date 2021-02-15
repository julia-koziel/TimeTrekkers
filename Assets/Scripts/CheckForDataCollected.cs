using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
public class CheckForDataCollected : MonoBehaviour
{
    public string participantId;
    Queue<string> files;
    public GameObject titlescreen;
    public GameObject introscreen;
    public GameObject consent; 

    private MainMenuLogicNew intro;


    // Start is called before the first frame update
    void Start()
    {
        participantId = PlayerPrefs.GetString("ID");
        files = new Queue<string>();
        var tsvs = Directory.GetFiles(getFolderPath(participantId), "*.tsv", SearchOption.AllDirectories);
        tsvs.Where(s => Regex.IsMatch(s, @".+UPLOADED\.tsv$")).ForEach(files.Enqueue);
        var csvs = Directory.GetFiles(getFolderPath(participantId), "*.csv", SearchOption.AllDirectories);
        csvs.Where(s => Regex.IsMatch(s, @".+UPLOADED\.csv$")).ForEach(files.Enqueue);
        intro = FindObjectOfType<MainMenuLogicNew>();

        if (files.Count > 0)
        {
           introscreen.SetActive(true);
           titlescreen.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMM()
    {
        SceneManager.LoadScene(2);
    }

    string getFolderPath(string participantId)
    {
        string dataPath;
        
#if   UNITY_EDITOR
        dataPath = Application.dataPath + "/CSV";
#elif UNITY_ANDROID
        dataPath = Application.persistentDataPath;
#elif UNITY_IPHONE
        dataPath = Application.persistentDataPath;
#else
        dataPath = Application.dataPath;
#endif
        return $"{dataPath}/{participantId}/";
    }
}

